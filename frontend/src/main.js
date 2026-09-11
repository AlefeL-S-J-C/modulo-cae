import { createApp } from 'vue'
import PrimeVue from 'primevue/config'
import Aura from '@primevue/themes/aura'
import ToastService from 'primevue/toastservice'
import ConfirmationService from 'primevue/confirmationservice'
import 'primeicons/primeicons.css'
import './style.css'
import App from './App.vue'
import router from './router'

createApp(App)
  .use(router)
  .use(PrimeVue, { theme: { preset: Aura } })
  .use(ToastService)
  .use(ConfirmationService)
  .mount('#app')
