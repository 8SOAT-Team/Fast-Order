# language: pt-BR
Funcionalidade: Gerenciamento de Clientes

  Como um sistema de gerenciamento de clientes
  Eu quero garantir que os clientes sejam criados, alterados e validados corretamente

  Cenário: Criar um cliente válido
    Dado que o CPF do cliente é "765.273.200-00"
    E o nome do cliente é "João da Silva"
    E o email do cliente é "joao@example.com"
    Quando eu criar o cliente
    Então o cliente deve ser válido   

  Cenário: Retornar exceção quando CPF for nulo ou vazio
    Dado que o CPF do cliente é ""
    E o nome do cliente é "João da Silva"
    E o email do cliente é "joao@example.com"
    Quando eu tentar criar o cliente
    Então uma exceção de validação deve ser lançada

  Cenário: Retornar exceção quando CPF for inválido
    Dado que o CPF do cliente é "765.273.200-0X"
    E o nome do cliente é "Nome válido"
    E o email do cliente é "email@example.com"
    Quando eu tentar criar o cliente
    Então uma exceção de validação deve ser lançada

  Cenário: Retornar exceção quando nome for inválido
    Dado que o CPF do cliente é "765.273.200-00"
    E o nome do cliente é "jo"
    E o email do cliente é "email@example.com"
    Quando eu tentar criar o cliente
    Então uma exceção de validação deve ser lançada

  Cenário: Alterar nome de um cliente
    Dado que o cliente foi criado com CPF "765.273.200-00", nome "João da Silva" e email "joao@example.com"
    E o novo nome do cliente é "João Silva"
    Quando eu alterar o nome do cliente
    Então o nome do cliente deve ser "João Silva"

  Cenário: Alterar email de um cliente
    Dado que o cliente foi criado com CPF "765.273.200-00", nome "João da Silva" e email "joao@example.com"
    E o novo email do cliente é "joao.da.silva@example.com"
    Quando eu alterar o email do cliente para "joao.da.silva@example.com"
    Então o email do cliente deve ser "joao.da.silva@example.com"
