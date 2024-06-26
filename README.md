# fast-order

Breve descrição do que é o projeto e seu propósito.

## Pré-requisitos

- Docker
- Docker Compose

## Instalação

1. Clone o repositório:
   ```bash
   git clone https://github.com/seu-usuario/seu-projeto.git
   cd seu-projeto
2. Certifique-se de que o Docker está instalado:
  - Instruções para instalar o Docker
  - Instruções para instalar o Docker Compose
    
## Configuração
Crie um arquivo .env baseado no arquivo .env.example:

bash
Copiar código
cp .env.example .env
Atualize as variáveis de ambiente no arquivo .env conforme necessário.

## Como Rodar o Projeto
Construa e inicie os containers Docker:

```bash
docker-compose up --build
```
A API deve estar rodando em http://localhost:8080.

## Uso
### Exemplos de Endpoints
```
GET /api/v1/resource
```
Descrição: Retorna uma lista de recursos.
Exemplo de resposta:

```json

[
  {
    "id": 1,
    "name": "Recurso 1"
  }
]
```

```
POST /api/v1/resource
```

Descrição: Cria um novo recurso.
Exemplo de corpo de requisição:
```json
{
  "name": "Novo Recurso"
}
```

## Teste
Para rodar os testes, use:
```bash
docker-compose run api npm test
```

## Documentação da API
A documentação completa da API pode ser encontrada em http://localhost:8080/docs.

## Contribuição
### Fork o projeto
Crie uma branch para sua feature (git checkout -b feature/nova-feature)
Commit suas alterações (git commit -m 'Adiciona nova feature')
Push para a branch (git push origin feature/nova-feature)
Abra um Pull Request

## Licença
Este projeto está licenciado sob a Licença MIT - veja o arquivo LICENSE para mais detalhes.

## Autores

André Bessa - RM357159
Fernanda - RM357346
Gilberto Loubach - RM357416
Felipe Bergmann - RM357042
Victor Oliver - RM357451
