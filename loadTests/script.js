import http from 'k6/http';
import { SharedArray } from 'k6/data';

const data = new SharedArray('clientes data', function() {
  return JSON.parse(open('./clientes.json'));
});

// Configurações do teste
export const options = {
  stages: [
    { duration: '1m', target: 100 }, // Gradualmente sobe para 100 VUs
    { duration: '5m', target: 100 }, // Mantém 100 VUs por 5 minutos
    { duration: '1m', target: 0 },   // Gradualmente desce para 0 VUs
  ],
};

export default function () {
  // Cada VU envia um único objeto do array `Clientes`
  const index = __VU * __ITER % data.length; // Distribui os itens no array entre as VUs
  const payload = JSON.stringify(data[index]);

  const headers = { 'Content-Type': 'application/json' };
  
  const res = http.post(`${__ENV.BASE_URL}/cliente`, payload, { headers });

  // Valida a resposta (ajuste conforme o retorno esperado da API)
  if (res.status !== 200) {
    console.error(`Falha no envio do CPF: ${data[index].cpf}, Status: ${res.status}`);
  }
}
