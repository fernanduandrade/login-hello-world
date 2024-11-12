import { createApp } from 'vue'
import './style.css'
import App from './App.vue'
import { createMemoryHistory, createRouter, RouteLocationNormalizedLoadedGeneric } from 'vue-router'
import Vue3Toastify, { type ToastContainerOptions } from 'vue3-toastify';
import 'vue3-toastify/dist/index.css';


const routes = [
    { path: '/', name: 'Home', component: () => import('./views/Index.vue') },
    { path: '/login', name: 'Login', component: () => import('./views/Login.vue') },
    { 
        path: '/auth/callback/', name: 'AuthCallback',
        component: () => import('./views/AuthCallback.vue'),    
        props: (route: RouteLocationNormalizedLoadedGeneric) => ({ provider: route.query.provider }) },
]

const router = createRouter({
    history: createMemoryHistory(),
    routes,
})

createApp(App).use(Vue3Toastify, {
    autoClose: 3000,
  } as ToastContainerOptions)
  .use(router).mount('#app')
