<template>
    <h3>{{ isEdit ? $t('editUrl') : $t('addUrl') }}</h3>
    <div class="field">
        <div>{{ $t('relativePath') }}</div>
        <input type="text" name="redirectUrl" v-model="redirectUrl">
        <div class="validation-error" v-if="!isRedirectUrlValid">
            {{ $t('notValidUrl') }}
        </div>
    </div>
    
    <div class="field">
        <div>{{ $t('relativePath') }}</div>
        <div>
            <input type="text" name="redirectUrl" v-model="relativePath">
            <button v-on:click="generateRandomPath">{{ $t('randomGeneration') }}</button>
        </div>
        <div class="validation-error" v-for="(error, key, index) in relativePathValidationErrors" :key="index">{{ error }}</div>
    </div>
    <div class="final-url"> {{ $t('urlExample') }} <a :href="redirectUrl">{{ getTinyUrl() }}</a></div>

    <button v-on:click="isEdit ? editUrl() : addUrl()" v-bind:disabled="!isRelativePathValid || !isRedirectUrlValid">
        {{ isEdit ? $t('saveChanges') : $t('addUrl') }}
    </button>
  </template>
  
  <script setup>
    import { onMounted, defineProps, ref, computed, watch } from 'vue'
    import _ from 'lodash'
    import { useNotification } from "@kyvg/vue3-notification";
    import urlApiService from '../apiServices/urlApiService'
    

    const { notify }  = useNotification()

    const redirectUrl = ref("")
    const relativePath = ref("")
    const props = defineProps({
     id: String
    });
    const isEdit = computed(() => {
        return props.id != "add"
    })
    const isRelativePathValid = ref(false)
    const isRedirectUrlValid = ref(false)
    const relativePathValidationErrors = ref([])
  
    onMounted( async () => {
      if (isEdit.value) {
        await urlApiService.getUrl(props.id).then(data => {
        redirectUrl.value = data.redirectUrl
        relativePath.value = data.relativePath
        }).catch(
            err => {
            relativePathValidationErrors.value = err.data
            }
        )
      }
    })

    watch(redirectUrl, _.debounce(() => {
        const validUrlRegexp = /^(http(s):\/\/.)[-a-zA-Z0-9@:%._+~#=]{2,256}\.[a-z]{2,6}\b([-a-zA-Z0-9@:%_+.~#?&//=]*)/g
        isRedirectUrlValid.value = validUrlRegexp.test(redirectUrl.value)
    }, 300)) 

    watch(relativePath, _.debounce(async () => {
        relativePathValidationErrors.value = []
        if(relativePath.value) {
            await urlApiService.validatePath(relativePath.value).then((response) => {
                isRelativePathValid.value = response.data.isValid;

                relativePathValidationErrors.value = response.data.validationErrors
            }).catch(err => 
            {
                notify({type: "error"}, err.response.data)
            })
        } else {
            isRelativePathValid.value = false
        }
    }), 500)

    function getTinyUrl() {
        return process.env.VUE_APP_API_URL + '/' + relativePath.value
    }

    function generateRandomPath() {
        let result = '';
        const characters =
            'abcdefghijklmnopqrstuvwxyz0123456789';
        const pathLength = 16;
        
        for (let i = 0; i < pathLength; i++) {
            result += characters.charAt(Math.floor(Math.random() * characters.length));
        }

        relativePath.value = result
    }

    async function addUrl() {
        await urlApiService.addUrl({relativePath: relativePath.value, redirectUrl: redirectUrl.value}).then(() => 
            {
                notify({type: "success", text: "Url is successfully added"})
            }).catch(err => 
                {
                err.response.data.forEach(error => {
                    notify({type: "error", text: error})
                })
            })
    }

    async function editUrl() {
        await urlApiService.editUrl({relativePath: relativePath.value, redirectUrl: redirectUrl.value}, props.id).then(() => {
                notify({type: "success", text: "Url is successfully edited"})
            }).catch(err => 
                {
                    err.response.data.forEach(error => {
                    notify({type: "error", text: error})
                })
            })
    }
  </script>
  
  <style scoped>

    input {
        width: 400px;
    }

    .field{
        margin-bottom: 15px;
    }

    .final-url{
        margin-bottom: 10px;
    }

    .validation-error {
        color: red;
        font-size: 14px;
    }
  </style>
  