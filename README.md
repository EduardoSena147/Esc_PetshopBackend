# 🐾 PetShop Backend - Sistema de Agendamento

![.NET](https://img.shields.io/badge/.NET-6.0-purple)
![PostgreSQL](https://img.shields.io/badge/PostgreSQL-15-blue)
![Entity Framework](https://img.shields.io/badge/Entity%20Framework-Core-orange)
![JWT](https://img.shields.io/badge/JWT-Authentication-yellow)
![Swagger](https://img.shields.io/badge/Swagger-Documentation-green)

Backend completo para um sistema de agendamento de serviços em petshop, desenvolvido em .NET 6 com arquitetura limpa e escalável.

## ✨ Funcionalidades

### 🔐 Autenticação e Autorização
- **Sistema JWT** com tokens seguros
- **Múltiplos perfis** (Administrador, Gerente, Funcionário, Cliente)
- **Proteção de endpoints** com autorização por roles
- **Hash de senhas** com algoritmos seguros

### 👥 Gestão de Usuários
- Cadastro de usuários com diferentes níveis de acesso
- CRUD completo de usuários
- Validação de email e username únicos
- Ativação/desativação de contas

### 🐶 Gestão de Clientes
- Cadastro automático ao registrar usuário como cliente
- Dados completos (endereço, contato, documentos)
- Sistema de cadastro pendente/completo
- Validação de endereço e documentos

### 🏷️ Catálogo de Serviços
- Cadastro de tipos de animais (cães, gatos, aves, etc.)
- Categorização de serviços
- Gestão de preços e disponibilidade

### 📅 Sistema de Agendamentos
- Agendamento de serviços por clientes
- Controle de horários disponíveis
- Confirmação de agendamentos
- Lembretes automáticos (futuro)

## 🛠️ Tecnologias Utilizadas

- **.NET 6** - Framework principal
- **Entity Framework Core** - ORM e gestão de banco
- **PostgreSQL** - Banco de dados relacional
- **JWT Bearer** - Autenticação por tokens
- **AutoMapper** - Mapeamento entre entidades e DTOs
- **Swagger/OpenAPI** - Documentação da API
- **BCrypt.Net** - Criptografia de senhas
- **Dapper** - Consultas SQL de alta performance

## 📊 Monitoramento e Logs
- **Serilog** para logging estruturado
- **Health Checks** para monitoramento da API
- **Exception Handling** global com middleware customizado

## 🔒 Segurança
- **HTTPS** obrigatório em produção
- **CORS** configurado para origens específicas
- **Rate Limiting** para prevenção de ataques
- **SQL Injection** prevenido via parameters
- **XSS Protection** com sanitização de inputs

## 📈 Próximas Funcionalidades
- [ ] Sistema de notificações por email
- [ ] Integração com gateway de pagamento
- [ ] API para mobile apps
- [ ] Dashboard administrativo
- [ ] Relatórios e analytics
- [ ] Sistema de fidelidade

---

**Desenvolvido com ❤️ para amantes de animais** 🐕🐈🐦

*Este projeto faz parte do sistema de gestão PetShop - Tornando o cuidado com pets mais fácil e eficiente!*
