import Vue from 'vue'
import VueRouter, { RouteConfig } from 'vue-router'
import AccountLoginOrRegister from '../views/Account/LoginOrRegister.vue'
import Entries from '../views/Entries/Entries.vue'

Vue.use(VueRouter)

const routes: Array<RouteConfig> = [
    { path: '/', name: 'Home', component: AccountLoginOrRegister },
    { path: '/entries', name: 'Entries', component: Entries }
]

const router = new VueRouter({
    routes
})

export default router
