# fast-order

É uma implementação dos domínios de negócio de uma lanchonete utilizando clean-architecture, disponibilizando um Driven webapi.

O projeto implementa os fluxos descritos no [Event Storming (miro board)](https://miro.com/app/board/uXjVK5PtxF0=/?share_link_id=847017542278) 

Para maiores detalhes gravamos um [video de apresentação com o fluxo de execução esperado](https://www.youtube.com/watch?v=2OUjhxdt0Hk)


## Pré-requisitos
- Docker
    - [Instruções para instalar o Docker - Windows](https://docs.docker.com/desktop/install/windows-install/)   
    - [Instruções para instalar o Docker - Linux](https://docs.docker.com/desktop/install/linux-install/)
    - [Instruções para instalar o Docker - MacOs](https://docs.docker.com/desktop/install/mac-install/)
- Docker Compose
    - [Instruções para instalar o Docker Compose](https://docs.docker.com/compose/install/)
- Kubernetes
  - [Habilitar Kubernetes com Docker Desktop](https://docs.docker.com/desktop/kubernetes/)

## Executando

1. Abra o "Docker Desktop"

2. Clone o repositório:
   ```bash
   git clone https://github.com/8SOAT-Team/Fast-Order.git

3. Acesse a pasta de manifestos
   ```bash
   cd Fast-Order
   cd manifestos

4. Aplique os arquivos ".yaml" utilizando o kubectl
    ```bash
    kubectl apply -f fo-configmap.yaml,fo-pvc.yaml,fo-svc.yaml,fo-deployment.yaml,fo-hpa.yaml,fo-db-svc.yaml,fo-db-deployment.yaml

5. Aguarde os deployments estarem disponíveis     
    ```bash
    kubectl get deployments --watch

## Documentação da API
Disponibilizamos uma [collection do postman](/docs/postman/Postech8SOAT.FastOrder.WebAPI.postman_collection.json) com todos os endpoints utilizados na apresentação.

## Uso
As APIs devem estar rodando em http://localhost:31500/

# Desenho da Arquitetura 
<p align="center"><img src="https://github.com/8SOAT-Team/Fast-Order/blob/main/manifestos/arquitetura.png"></p>

## Licença
Este projeto está licenciado sob a Licença MIT - veja o arquivo LICENSE para mais detalhes.

## Autores
### Fiap turma 8SOAT - Grupo 7

- André Bessa - RM357159
- Fernanda Beato - RM357346
- Felipe Bergmann - RM357042
- Gilberto Loubach - RM357416
- Victor Oliver - RM357451
