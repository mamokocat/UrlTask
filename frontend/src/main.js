import { createApp } from 'vue'
import { router } from './router'
import Notifications from '@kyvg/vue3-notification'
import App from './App.vue'
import i18n from './i18n'

const app = createApp(App)

app.use(router)
app.use(Notifications)
app.use(i18n)

app.mount('#app')
