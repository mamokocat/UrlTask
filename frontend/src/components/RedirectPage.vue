<template>
  <div class="redirect">
      <div v-if="isRequestRunning">
        {{ $t('redirecting') }}
      </div>
      <div v-if="!isRedirectSuccess" class="redirect-error">
        {{ errorMessage }}
      </div>
  </div>
</template>

<script setup>
  import { onMounted, toRefs, defineProps, ref } from 'vue'
  import urlApiService from '../apiServices/urlApiService'

  const props = defineProps({
   path: String
  });

  const {path} = toRefs(props)

  const isRequestRunning = ref(true)
  const isRedirectSuccess = ref(true)
  const errorMessage = ref("")


  onMounted( async () => {
    await urlApiService.tryRedirect(path.value).then(res => {
      window.location.href = res.data
      isRequestRunning.value = false
    }).catch(
      err => {
        isRequestRunning.value = false
        isRedirectSuccess.value = false
        errorMessage.value = err.response.data
      }
    )
  })
</script>

<style scoped>
  .redirect {
    padding-top: 50px;
    font-size: 50px;
  }

  .redirect-error {
    color: red;
    font-size: 40px;
  }
</style>
