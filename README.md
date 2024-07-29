# fast-order

Breve descrição do que é o projeto e seu propósito.

## Pré-requisitos
- Docker
    - [Instruções para instalar o Docker - Windows](https://docs.docker.com/desktop/install/windows-install/)   
    - [Instruções para instalar o Docker - Linux](https://docs.docker.com/desktop/install/linux-install/)
    - [Instruções para instalar o Docker - MacOs](https://docs.docker.com/desktop/install/mac-install/)
- Docker Compose
    - [Instruções para instalar o Docker Compose](https://docs.docker.com/compose/install/)


## Executando
1. Abra o "Docker Desktop"

2. Clone o repositório:
   ```bash
   git clone https://github.com/8SOAT-Team/fast-order.git

3. Acesse a raíz do projeto
   ```bash
   cd fast-order
   cd Fast-Order

4. Construa e inicie os containers Docker  
   ```bash
   docker-compose up --build
  
## Documentação da API
A documentação completa da API pode ser encontrada em https://localhost:57399/swagger/index.html

## Uso

A API deve estar rodando em https://localhost:57399/.

Será iniciado um container com o postgres, rodando na porta 5432 e expondo na porta 5433.
Outro container será iniciado com o adminer na porta 8080 auxiliando na visualização do schema de banco de dados.


## Teste
Para rodar os testes, use:
```bash
docker-compose run api npm test
```

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
