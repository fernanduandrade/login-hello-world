

interface ImportMetaEnv {
    VITE_GOOGLE_REDIRECT: string
    VITE_GITHUB_REDIRECT: string
    VITE_GOOGLE_DISCORD: string
    VITE_API_URL: string
    VITE_GOOGLE_CLIENT_ID: string
    VITE_GOOGLE_DNS_REDIRECT: string
}

interface ImportMeta {
    readonly env: ImportMetaEnv
}