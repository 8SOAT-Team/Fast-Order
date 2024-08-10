# fast-order

É uma implementação dos domínios de negócio de uma lanchonete utilizando arquitetura hexagonal, disponibilizando um Driven webapi.

O projeto implementa os fluxos descritos no [Event Storming (miro board)](https://miro.com/app/board/uXjVK5PtxF0=/?share_link_id=847017542278) 

## Categorias do negócio

O requisito de negócio determina quais categorias um produto pode ser alocado. 
Este projeto trata a categoria como uma entidade, portanto as mesmas são referenciadas por Ids, mas não há atualmente gestão de categorias, apenas listagem.
O mapeamento das categorias segue:
```json
[
  {
    "id": "07c470aa-606f-4792-849a-02433c121117",
    "nome": "Bebida",
    "descricao": "Bebidas"
  },
  {
    "id": "b553a212-9930-4e5a-a780-138a0a4a0b78",
    "nome": "Sobremesa",
    "descricao": "Sobremesas"
  },
  {
    "id": "0194d8c4-2d04-4172-a63a-4d381eadf729",
    "nome": "Acompanhamento",
    "descricao": "Acompanhamentos"
  },
  {
    "id": "6224b6c0-26e9-42fa-8b04-dc0e9fd6b971",
    "nome": "Lanche",
    "descricao": "Lanches"
  }
]
```


## Pré-requisitos
- Docker
    - [Instruções para instalar o Docker - Windows](https://docs.docker.com/desktop/install/windows-install/)   
    - [Instruções para instalar o Docker - Linux](https://docs.docker.com/desktop/install/linux-install/)
    - [Instruções para instalar o Docker - MacOs](https://docs.docker.com/desktop/install/mac-install/)
- Docker Compose
    - [Instruções para instalar o Docker Compose](https://docs.docker.com/compose/install/)


## Executando
1. Tenha certeza de que o Docker está rodando, você pode fazer isso abrindo o Docker Desktop

2. Clone o repositório:
   ```bash
   git clone https://github.com/8SOAT-Team/Fast-Order.git

3. Acesse a raíz do projeto
   ```bash
   cd Fast-Order

4. Construa e inicie os containers Docker  
   ```bash
   docker-compose up --build
  
## Documentação da API
A documentação completa da API pode ser encontrada em https://localhost:57399 (permita em seu navegador conexão não confiável)

## Uso

A API deve estar rodando em https://localhost:57399/

Será iniciado um container com o MS SqlServer, rodando na porta 11433 e expondo na porta 11433.

# Contribuindo

## Rodando as migrações

- Após realizar alterações nos modelos de dados, gere a migração
   ```bash
   dotnet ef migrations add <NomeDaMigracao> --startup-project Postech8SOAT.FastOrder.WebAPI --project Postech8SOAT.FastOrder.Infra.Data
   ```

- Caso precise desfazer a migração (antes de atualizar o banco de dados)
   ```bash
   dotnet ef migrations remove --startup-project Postech8SOAT.FastOrder.WebAPI --project Postech8SOAT.FastOrder.Infra.Data
   ```

Ao iniciar a aplicação WEB API todas as migrações pendentes são aplicadas, fazendo com que não seja necessário rodar o comando ```ef database update```

# Fluxo de utilização esperado

[Fluxo de execução esperado](flow.md)

## Licença
Este projeto está licenciado sob a Licença MIT - veja o arquivo LICENSE para mais detalhes.

## Autores
### Fiap turma 8SOAT - Grupo 7

- André Bessa - RM357159
- Fernanda - RM357346
- Felipe Bergmann - RM357042
- Gilberto Loubach - RM357416
- Victor Oliver - RM357451
