# Fluxo esperado

## Identificação do cliente
GET /cliente?cpf=41128255871

## Incluir cliente se necessário

POST /cliente 
{
  "cpf": "246.661.954-18",
  "nome": "string",
  "email": "user@example.com"
}

## Criar o pedido

POST /pedido
{
  "clienteId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "itensDoPedido": [
    {
      "produtoId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
      "quantidade": 0
    }
  ]
}

## Iniciar o pagamento do pedido

POST /pagamento/pedido/287032C7-ACFF-4A53-84E8-08DCAAB5919D
{
  "metodoDePagamento": 0
}

## Confirmar o pagamento do pedido

PATCH /pagamento/287032C7-ACFF-4A53-84E8-08DCAAB5919D
{
  "status": "autorizado"
}

## Atualize o status do pedido 
PUT /pedido/287032C7-ACFF-4A53-84E8-08DCAAB5919D/status
{
  "novoStatus": 1
}

# Administrativo

## Listar categorias disponíveis

GET /produto/categoria

## Cadastrar um novo produto

POST /produto 
{
  "nome": "string",
  "descricao": "string",
  "preco": 0,
  "categoriaId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "imagem": "string"
}