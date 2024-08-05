# Fluxo de utilização esperado

## Administrativo

### Listar categorias disponíveis

```bash
curl -X 'GET' \
  'https://localhost:57399/produto/categoria' \
  -H 'accept: */*'
```

### Cadastrar um novo produto

```bash
curl -X 'POST' \
  'https://localhost:57399/produto' \
  -H 'accept: */*' \
  -H 'Content-Type: application/json' \
  -d '{
  "nome": "X-Delicious",
  "descricao": "O lanche mais delicioso da casa!",
  "preco": 10.95,
  "categoriaId": "6224b6c0-26e9-42fa-8b04-dc0e9fd6b971",
  "imagem": "/path/images/x-delicious.png"
}'
```

**Resposta**

```bash
{
  "id": "bef68fa5-0c9a-49de-fb8e-08dcb4eae33f",
  "nome": "X-Delicious",
  "descricao": "O lanche mais delicioso da casa!",
  "preco": 10.95,
  "categoriaId": "6224b6c0-26e9-42fa-8b04-dc0e9fd6b971",
  "imagem": "/path/images/x-delicious.png"
}
```

## Identificação do cliente
Utilize este endpoint para obter o cadastro de um cliente já conhecido

```bash
curl -X 'GET' \
  'https://localhost:57399/cliente?cpf=41128255871' \
  -H 'accept: */*'
```

## Incluir cliente se necessário

```bash
curl -X 'POST' \
  'https://localhost:57399/cliente' \
  -H 'accept: */*' \
  -H 'Content-Type: application/json' \
  -d '{
  "cpf": "41128255871",
  "nome": "João Silva",
  "email": "joao.silva@example.com"
}'
```

**Resposta**
```bash
{
  "id": "c5e6c683-7676-47bd-892f-b9f1f28362e1",
  "cpf": "41128255871",
  "nome": "João Silva",
  "email": "joao.silva@example.com"
}
```

## Criar o pedido

```bash
curl -X 'POST' \
  'https://localhost:57399/pedido' \
  -H 'accept: */*' \
  -H 'Content-Type: application/json' \
  -d '{
  "clienteId": "c5e6c683-7676-47bd-892f-b9f1f28362e1",
  "itensDoPedido": [
    {
      "produtoId": "bef68fa5-0c9a-49de-fb8e-08dcb4eae33f",
      "quantidade": 1
    }
  ]
}'
```

**Resposta**
```bash
{
  "id": "9cbb1cad-862f-4298-b323-7a0112cb1817",
  "dataPedido": "2024-08-05T01:08:53.7456141+00:00",
  "statusPedido": "Recebido",
  "clienteId": "c5e6c683-7676-47bd-892f-b9f1f28362e1",
  "cliente": null,
  "itensDoPedido": [
    {
      "id": "56c1e4c4-4469-4e53-1968-08dcb4eb2c90",
      "produtoId": "bef68fa5-0c9a-49de-fb8e-08dcb4eae33f",
      "quantidade": 1
    }
  ],
  "valorTotal": 10.95
}
```

## Iniciar o pagamento do pedido

```bash
curl -X 'POST' \
  'https://localhost:57399/pagamento/pedido/9cbb1cad-862f-4298-b323-7a0112cb1817' \
  -H 'accept: */*' \
  -H 'Content-Type: application/json' \
  -d '{
  "metodoDePagamento": "Pix"
}'
```

**Resposta**

```bash
{
  "id": "ba27a3b9-cdbf-4344-a60f-f99eba0d1dd9",
  "pedidoId": "9cbb1cad-862f-4298-b323-7a0112cb1817",
  "pagamentoExternoId": null,
  "status": "Pendente",
  "metodoDePagamento": "Pix",
  "valorTotal": 10.95
}
```

## Confirmar o pagamento do pedido

```bash
curl -X 'PATCH' \
  'https://localhost:57399/pagamento/ba27a3b9-cdbf-4344-a60f-f99eba0d1dd9' \
  -H 'accept: */*' \
  -H 'Content-Type: application/json' \
  -d '{
  "status": "Autorizado"
}'
```

## Atualize o status do pedido 

```bash
curl -X 'PUT' \
  'https://localhost:57399/pedido/9cbb1cad-862f-4298-b323-7a0112cb1817/status' \
  -H 'accept: */*' \
  -H 'Content-Type: application/json' \
  -d '{
  "novoStatus": "EmPreparacao"
}'
```

## Liste o Pedido

```bash
curl -X 'GET' \
  'https://localhost:7291/pedido/9cbb1cad-862f-4298-b323-7a0112cb1817' \
  -H 'accept: */*'
```