using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Postech8SOAT.FastOrder.WebAPI.DTOs;

public class ProdutoDTO
{
    [DisplayName("Id")]
    public Guid? Id { get; private set; }

    [Required(ErrorMessage = "O nome é requerido.")]
    [MinLength(3)]
    [MaxLength(100)]
    [DisplayName("Nome")]
    public string? Nome { get; set; }

    [Required(ErrorMessage = "A descrição é requerida.")]
    [MinLength(5)]
    [MaxLength(200)]
    [DisplayName("Descrição")]
    public string? Descricao { get; set; }

    [Required(ErrorMessage = "O preço é requerido.")]
    [Column(TypeName = "decimal(18,2)")]
    [DisplayFormat(DataFormatString = "{0:C2}")]
    [DataType(DataType.Currency)]
    [DisplayName("Preço")]
    public decimal Preco { get;  set; }

    [Required]
    [DisplayName("Categoria")]
    public Guid CategoriaId { get; set; }

    [MaxLength(300)]
    [DisplayName("Imagem do produto.")]
    public string? Imagem { get;  set; }

    public void SetId(Guid id)
    {
        Id = id;
    }    
}
