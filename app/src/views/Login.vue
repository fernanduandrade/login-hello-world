
<script lang="ts" setup>
import {  reactive } from 'vue'
import VInput from '../common/components/VInput.vue'
import VButton from '../common/components/VButton.vue'
import { generateCSRF } from '../common/logic'

const form = reactive({
    email: '',
    password: ''
})

function loginDiscord() {
    window.location.assign(import.meta.env.VITE_GOOGLE_DISCORD)
}

function loginGithub() {
    window.location.assign(import.meta.env.VITE_GITHUB_REDIRECT)
}

function loginGoogle() {
    const clientId = import.meta.env.VITE_GOOGLE_CLIENT_ID
    const state = generateCSRF()
    localStorage.setItem("latestCSRFToken", state)
    const redirectDns = import.meta.env.VITE_GOOGLE_DNS_REDIRECT
    let url = import.meta.env.VITE_GOOGLE_REDIRECT
        .replace("@state", state)
        .replace("@clientId", clientId)
        .replace("@redirectDns", redirectDns)
    window.location.assign(url)
}

</script>

<template>
    <main class="auth__container">
        <div class="wrapper-form">
            <h1 class="uppercase text-4xl font-bold">Login Hellow</h1>
            <div class="mt-4 flex flex-col gap-4">
                <div>
                    <label class="font-bold">Email</label>
                    <VInput type="text" v-model="form.email" placeholder="Email" label="Email" />
                </div>
                
                <div>
                    <label class="font-bold">Senha</label>
                    <VInput type="password" v-model="form.password" placeholder="Senha" label="Senha" />
                </div>

                <div class="w-full">
                    <VButton>Login</VButton>
                </div>

                <div class="flex items-center content-evenly gap-3 w-full">
                    <div class="w-full h-[1px] bg-black"></div>
                    <div>
                        <div>
                            <span>OU</span>
                        </div>
                    </div>
                    <div class="w-full h-[1px] bg-black"></div>
                </div>

                <div class="providers">
                    <div class="flex justify-center w-full">
                        <button @click.prevent="loginDiscord"
                            class="flex cursor-pointer items-center rounded-lg shadow-md px-6 py-2 text-sm font-medium text-gray-800 hover:bg-gray-200 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-gray-500">
                        
                            <svg class="h-6 w-6 mr-2" xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink"
                                width="800px" height="800px" viewBox="0 -28.5 256 256" version="1.1" preserveAspectRatio="xMidYMid">
                                <g>
                                    <path
                                        d="M216.856339,16.5966031 C200.285002,8.84328665 182.566144,3.2084988 164.041564,0 C161.766523,4.11318106 159.108624,9.64549908 157.276099,14.0464379 C137.583995,11.0849896 118.072967,11.0849896 98.7430163,14.0464379 C96.9108417,9.64549908 94.1925838,4.11318106 91.8971895,0 C73.3526068,3.2084988 55.6133949,8.86399117 39.0420583,16.6376612 C5.61752293,67.146514 -3.4433191,116.400813 1.08711069,164.955721 C23.2560196,181.510915 44.7403634,191.567697 65.8621325,198.148576 C71.0772151,190.971126 75.7283628,183.341335 79.7352139,175.300261 C72.104019,172.400575 64.7949724,168.822202 57.8887866,164.667963 C59.7209612,163.310589 61.5131304,161.891452 63.2445898,160.431257 C105.36741,180.133187 151.134928,180.133187 192.754523,160.431257 C194.506336,161.891452 196.298154,163.310589 198.110326,164.667963 C191.183787,168.842556 183.854737,172.420929 176.223542,175.320965 C180.230393,183.341335 184.861538,190.991831 190.096624,198.16893 C211.238746,191.588051 232.743023,181.531619 254.911949,164.955721 C260.227747,108.668201 245.831087,59.8662432 216.856339,16.5966031 Z M85.4738752,135.09489 C72.8290281,135.09489 62.4592217,123.290155 62.4592217,108.914901 C62.4592217,94.5396472 72.607595,82.7145587 85.4738752,82.7145587 C98.3405064,82.7145587 108.709962,94.5189427 108.488529,108.914901 C108.508531,123.290155 98.3405064,135.09489 85.4738752,135.09489 Z M170.525237,135.09489 C157.88039,135.09489 147.510584,123.290155 147.510584,108.914901 C147.510584,94.5396472 157.658606,82.7145587 170.525237,82.7145587 C183.391518,82.7145587 193.761324,94.5189427 193.539891,108.914901 C193.539891,123.290155 183.391518,135.09489 170.525237,135.09489 Z"
                                        fill="#5865F2" fill-rule="nonzero">

                                    </path>
                                </g>
                            </svg>
                            <span>Login com Discord</span>
                        </button>
                    </div>
                    <div class="flex justify-center">
                        <button @click.prevent="loginGithub" class="py-2 px-4 flex justify-center items-center bg-gray-600 hover:bg-gray-700 focus:ring-gray-500 focus:ring-offset-gray-200 text-white transition ease-in duration-200 text-center text-base font-semibold shadow-md focus:outline-none focus:ring-2 focus:ring-offset-2 rounded-lg">
                        <svg xmlns="http://www.w3.org/2000/svg" height="20" fill="currentColor" class="mr-2" viewBox="0 0 1792 1792">
                            <path d="M896 128q209 0 385.5 103t279.5 279.5 103 385.5q0 251-146.5 451.5t-378.5 277.5q-27 5-40-7t-13-30q0-3 .5-76.5t.5-134.5q0-97-52-142 57-6 102.5-18t94-39 81-66.5 53-105 20.5-150.5q0-119-79-206 37-91-8-204-28-9-81 11t-92 44l-38 24q-93-26-192-26t-192 26q-16-11-42.5-27t-83.5-38.5-85-13.5q-45 113-8 204-79 87-79 206 0 85 20.5 150t52.5 105 80.5 67 94 39 102.5 18q-39 36-49 103-21 10-45 15t-57 5-65.5-21.5-55.5-62.5q-19-32-48.5-52t-49.5-24l-20-3q-21 0-29 4.5t-5 11.5 9 14 13 12l7 5q22 10 43.5 38t31.5 51l10 23q13 38 44 61.5t67 30 69.5 7 55.5-3.5l23-4q0 38 .5 88.5t.5 54.5q0 18-13 30t-40 7q-232-77-378.5-277.5t-146.5-451.5q0-209 103-385.5t279.5-279.5 385.5-103zm-477 1103q3-7-7-12-10-3-13 2-3 7 7 12 9 6 13-2zm31 34q7-5-2-16-10-9-16-3-7 5 2 16 10 10 16 3zm30 45q9-7 0-19-8-13-17-6-9 5 0 18t17 7zm42 42q8-8-4-19-12-12-20-3-9 8 4 19 12 12 20 3zm57 25q3-11-13-16-15-4-19 7t13 15q15 6 19-6zm63 5q0-13-17-11-16 0-16 11 0 13 17 11 16 0 16-11zm58-10q-2-11-18-9-16 3-14 15t18 8 14-14z"></path>
                        </svg>
                        Login com GitHub
                        </button>
                    </div>
                    <div class="flex items-center justify-center dark:bg-gray-800">
                        <button @click.prevent="loginGoogle" class="px-4 py-2 border flex gap-2 border-slate-200 dark:border-slate-700 rounded-lg text-slate-700 dark:text-slate-200 hover:border-slate-400 dark:hover:border-slate-500 hover:text-slate-900 dark:hover:text-slate-300 hover:shadow transition duration-150">
                            <img class="w-6 h-6" src="https://www.svgrepo.com/show/475656/google-color.svg" loading="lazy" alt="google logo">
                            <span>Login com Google</span>
                        </button>
                    </div>
                </div>
                
            </div>
            
        
        </div>
    </main>
</template>

<style scoped>

.wrapper-form {
    min-width: 520px;
    min-height: 400px;
    @apply flex flex-col shadow-lg bg-white p-4 rounded-lg
}

.providers {
    display: flex;
    flex-direction: column;
    gap: 8px;
    justify-content: center
}

</style>