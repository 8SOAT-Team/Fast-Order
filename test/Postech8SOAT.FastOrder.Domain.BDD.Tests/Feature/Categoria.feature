# language: pt-BR
Funcionalidade: Gerenciamento de Categorias
  Para garantir a consistência dos dados
  Como um desenvolvedor
  Quero validar as regras de negócio ao criar uma categoria

Cenário: Deve retornar exceção quando o nome for nulo ou vazio
    Dado que o nome da categoria é ""
    E a descrição da categoria é "Teste"
    Quando eu tentar criar uma categoria
    Então uma exceção do tipo "DomainExceptionValidation" deve ser lançada

Cenário: Deve retornar exceção quando o nome tiver menos de 3 caracteres
    Dado que o nome da categoria é "Te"
    E a descrição da categoria é "Teste"
    Quando eu tentar criar uma categoria
    Então uma exceção do tipo "DomainExceptionValidation" deve ser lançada

Cenário: Deve criar categoria com sucesso
    Dado que o nome da categoria é "Teste"
    E a descrição da categoria é "Teste"
    Quando eu criar uma categoria
    Então a categoria deve ser criada com sucesso