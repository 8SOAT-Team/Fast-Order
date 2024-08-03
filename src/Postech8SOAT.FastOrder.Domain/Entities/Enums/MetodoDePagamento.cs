namespace Postech8SOAT.FastOrder.Domain.Entities.Enums;

public enum MetodoDePagamento
{
    Nenhum = 0,
    Pix = 1 << 1,
    Cartao = 1 << 2,
    Master = 1 << 3,
    Visa = 1 << 4,
}