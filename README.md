# 🚗 MecHub - Sistema de Gestão para Oficina Mecânica

## 📌 Sobre o projeto

O **MecHub** é um sistema web desenvolvido em **ASP.NET Core MVC** com o objetivo de gerenciar rotinas operacionais de uma oficina mecânica, permitindo o controle de:

- usuários do sistema
- clientes
- veículos
- mecânicos
- serviços
- ordens de serviço
- itens vinculados às ordens de serviço
- autenticação local e externa (Google)

O projeto foi idealizado para simular um ambiente real de oficina, aplicando conceitos de desenvolvimento backend, arquitetura MVC, autenticação e relacionamento entre entidades no banco de dados.

---

# 🎯 Objetivos do projeto

Este sistema foi desenvolvido com foco em:

- praticar desenvolvimento profissional com ASP.NET Core MVC
- aplicar boas práticas de organização de código
- implementar autenticação híbrida
- trabalhar com Entity Framework Core
- modelar relacionamentos reais entre entidades
- separar responsabilidades entre Model, ViewModel e Controller
- preparar uma base escalável para futuras melhorias

---

## 🚀 Tecnologias Utilizadas

### 🧠 Backend
- ![.NET](https://img.shields.io/badge/.NET-5C2D91?style=for-the-badge&logo=.net&logoColor=white)
- ![C#](https://img.shields.io/badge/CSharp-239120?style=for-the-badge&logo=c-sharp&logoColor=white)
- ![ASP.NET Core MVC](https://img.shields.io/badge/ASP.NET_Core_MVC-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)
- ![Entity Framework Core](https://img.shields.io/badge/Entity_Framework_Core-68217A?style=for-the-badge&logo=.net&logoColor=white)

### 🎨 Front-end
- ![Razor](https://img.shields.io/badge/Razor-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)
- ![HTML5](https://img.shields.io/badge/HTML5-E34F26?style=for-the-badge&logo=html5&logoColor=white)
- ![CSS3](https://img.shields.io/badge/CSS3-1572B6?style=for-the-badge&logo=css3&logoColor=white)
- ![JavaScript](https://img.shields.io/badge/JavaScript-F7DF1E?style=for-the-badge&logo=javascript&logoColor=black)
- ![Bootstrap](https://img.shields.io/badge/Bootstrap-7952B3?style=for-the-badge&logo=bootstrap&logoColor=white)

### 🗄️ Banco de Dados
- ![MySQL](https://img.shields.io/badge/MySQL-00758F?style=for-the-badge&logo=mysql&logoColor=white)
- ![Pomelo](https://img.shields.io/badge/Pomelo-6DB33F?style=for-the-badge&logo=mysql&logoColor=white)

### 🔧 Ferramentas
- ![GitHub](https://img.shields.io/badge/GitHub-181717?style=for-the-badge&logo=github&logoColor=white)
- ![GitHub Copilot](https://img.shields.io/badge/GitHub_Copilot-8957E5?style=for-the-badge&logo=github-copilot&logoColor=white)
- ![VS Code](https://img.shields.io/badge/VS_Code-007ACC?style=for-the-badge&logo=visual-studio-code&logoColor=white)

### 🔐 Autenticação
- ![Authentication Cookies](https://img.shields.io/badge/Auth-Cookies-orange?style=for-the-badge)
- ![Google Authentication](https://img.shields.io/badge/Google_Auth-4285F4?style=for-the-badge&logo=google&logoColor=white)

### 🏛️ Arquitetura
- ![MVC](https://img.shields.io/badge/Architecture-MVC-blue?style=for-the-badge)

---

# 🏗️ Estrutura do projeto

O projeto está organizado seguindo o padrão MVC:

```bash
MecHub/
│
├── Controllers/
├── Models/
├── ViewModels/
├── Views/
├── Data/
├── wwwroot/
└── Program.cs
