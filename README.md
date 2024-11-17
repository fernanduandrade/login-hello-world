# Login Hello World 🌍

Um projeto simples de **login** desenvolvido para demonstrar o uso de diferentes provedores de autenticação e integração com backends modernos. 🚀

---

## 📝 Sobre o projeto

O **Login Hello World** é um exemplo funcional de como implementar múltiplos provedores de login, como Google, Discord, GitHub, e outros, usando Vue.js no frontend e Minimal APIs com .NET no backend. Ele utiliza **OAuth** para autenticação e demonstra práticas recomendadas para lidar com redirecionamentos, manipulação de callbacks, e integração de provedores com um enum para facilitar a escalabilidade.

### 🛠 Tecnologias utilizadas

- **Frontend**
  - [Vue.js](https://vuejs.org/)
  - Vue Router
  - Fetch Api
- **Backend**
  - [Minimal APIs com .NET](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/minimal-apis)
  - Integração com provedores OAuth
- **Outras**
  - PostgreSQL para armazenamento de usuários
  - Docker para desenvolvimento

---

## 📂 Estrutura do projeto

```plaintext
.
├── frontend/
│   ├── src/
│   │   ├── common/
            ├──components/
            ├──logic/
│   │   └── views/
├── backend/
│   ├── Handlers/
│   ├── Models/
│   ├── Services/
│   └── Program.cs
└── README.md
