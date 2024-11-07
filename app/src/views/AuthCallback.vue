<script lang="ts" setup>
import { onMounted, ref } from 'vue';
import { useRouter } from 'vue-router'


const error = ref('')
const router = useRouter()

onMounted(async () => {
    const fragment = new URLSearchParams(window.location.hash.slice(1));
    const [accessToken, tokenType] = [fragment.get('access_token'), fragment.get('token_type')]

    if(!accessToken){
        router.push({ name: 'Login' })
    }

    const response = await fetch('http://localhost:5180', {
        method: 'POST',
        body: JSON.stringify({
            provider: 'discord',
            token: accessToken
        })
    })

    if(response.ok) {
        const result = await response.json()

    }
    
    error.value = "DEU RUIMMMMMMMMM"        
})
</script>

<template>
    <main>
        <h1>{{ error }}</h1>
    </main>
</template>