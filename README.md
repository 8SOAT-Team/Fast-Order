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

```bash
cp .env.example .env
```

Atualize as variáveis de ambiente no arquivo .env conforme necessário.

## Como Rodar o Projeto
Construa e inicie os containers Docker:

```bash
docker-compose up --build
```

A API deve estar rodando em http://localhost:5011.

Será iniciado um container com o postgres, rodando na porta 5432 e expondo na porta 5433.
Outro container será iniciado com o adminer na porta 8080 auxiliando na visualização do schema de banco de dados.

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

- André Bessa - RM357159
- Fernanda - RM357346
- Felipe Bergmann - RM357042
- Gilberto Loubach - RM357416
- Victor Oliver - RM357451
