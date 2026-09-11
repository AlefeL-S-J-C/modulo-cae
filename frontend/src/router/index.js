import { createRouter, createWebHistory } from 'vue-router'
import DashboardView from '../views/DashboardView.vue'
import ContatosView from '../views/ContatosView.vue'
import HorariosView from '../views/HorariosView.vue'
import FaqsView from '../views/FaqsView.vue'
import ServicosView from '../views/ServicosView.vue'

export default createRouter({
  history: createWebHistory(),
  routes: [
    { path: '/', redirect: '/dashboard' },
    { path: '/dashboard', component: DashboardView },
    { path: '/contatos', component: ContatosView },
    { path: '/horarios', component: HorariosView },
    { path: '/faqs', component: FaqsView },
    { path: '/servicos', component: ServicosView }
  ]
})
