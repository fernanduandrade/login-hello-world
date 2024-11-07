<script lang="ts" setup>
import { onMounted, ref } from 'vue';
import { useRouter } from 'vue-router'
import { toast } from 'vue3-toastify';

const router = useRouter()
const username = ref('')

onMounted(async () => {
    var url = window.location.href;
    const queryParams = url.substring(url.lastIndexOf('/')+1);
    const fragment = new URLSearchParams(queryParams);
    let [accessToken, provider] = [(fragment.get('access_token') || fragment.get('code')), fragment.get('provider')]
    if(!accessToken){
        router.push({ name: 'Login' })
    }
    if(provider!.includes("#")) {
        provider = provider!.split("#")[0]
    }

    const response = await fetch(`${import.meta.env.BASE_URL}/users/auth`, {
        method: 'POST',
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify({
            provider: provider,
            token: accessToken
        })
    })

    const result = await response.json()

    if(!response.ok) {
        toast.error("Bad request")
        return
    }

    username.value = result.userName
})
</script>

<template>
    <main>
        <h1 v-if="username">Hello #{{ username }}</h1>
    </main>
</template>