<template>
  <div>
    <button v-on:click="() => this.$router.push(`/urls/add`)">{{ $t('createNew') }}</button>
    <table class="url-table">
      <tr>
        <th>{{ $t('shortUrl') }}</th>
        <th>{{ $t('fullUrl') }}</th>
        <th>{{ $t('visitCount') }}</th>
        <th>{{ $t('createdDate') }}</th>
        <th>{{ $t('edit') }}</th>
        <th>{{ $t('delete') }}</th>
      </tr>
      <tr v-for="url in urlList" :key="url.id">
        <th><a :href="getTinyUrl(url.relativePath)">{{ getTinyUrl(url.relativePath) }}</a></th>
        <th><a :href="url.redirectUrl">{{ url.redirectUrl }}</a></th>
        <th>{{ url.visitCount }}</th>
        <th>{{ url.createdDate }}</th>
        <th><button v-on:click="() => this.$router.push(`/urls/${url.id}`)">{{ $t('edit') }}</button></th>
        <th><button v-on:click="deleteUrl(url)">{{ $t('delete') }}</button></th>
      </tr>
    </table>
    <button v-if="!allUrlsLoaded" v-on:click="loadUrls">{{ $t('loadMore') }}</button>
  </div>
</template>

<script setup>
  import { onMounted, ref } from 'vue'
  import urlApiService from '../apiServices/urlApiService'
  import { useNotification } from "@kyvg/vue3-notification";

  const { notify }  = useNotification()
  const urlList = ref([])
  const skip = ref(0)
  const take = 10
  const allUrlsLoaded = ref(false)

  onMounted(async function() {
    loadUrls()
  })

  async function loadUrls() {
    await urlApiService.getUrlList(skip.value, take).then(function(data) {
      urlList.value = urlList.value.concat(data.records)
      skip.value = skip.value + data.records.length
      allUrlsLoaded.value = skip.value == data.totalRecords
    })
  }

  function getTinyUrl(urlPath) {
    return window.location.origin + '/' + urlPath
  }

  async function deleteUrl(url) {
    await urlApiService.deleteUrl(url.id).then(() => {
      urlList.value = urlList.value.filter((el) => el !== url)
      notify({
        type: "success",
        text: `Url with id ${url.id} successfully deleted`
      });
      
    }).catch(err => {
      notify({
        text: err.response.data,
        type: "error"
      });
    })
  }
</script>

<style scoped>
  tr {
    text-align: left;
    font-size: 14px;
  }

  th {
    padding-right: 20px;
  }

  table *{
    border: 0;
  }

  .url-table {
    margin: 15px 0;
  }
</style>
