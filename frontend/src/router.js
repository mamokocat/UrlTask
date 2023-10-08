import * as VueRouter from 'vue-router'

import UrlList from './components/UrlList'
import RedirectPage from './components/RedirectPage'
import AddEditPage from './components/AddEditPage'
import NotFound from './views/NotFound'

var routes = [
    { path: "/:path", component: RedirectPage, props: true },
    { path: "/", component: UrlList },
    { path: "/urls/:id(\\d+|add)", component: AddEditPage, props: true },
    { path: '/:pathMatch(.*)*', name: 'NotFound', component: NotFound }
]

export const router = VueRouter.createRouter({
    history: VueRouter.createWebHistory(),
    routes
})