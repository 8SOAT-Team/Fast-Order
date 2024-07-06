using System.ComponentModel.DataAnnotations;

namespace Postech8SOAT.FastOrder.WebAPI.DTOs;

public class CategoriaDTO
{
    [Required(ErrorMessage = "O nome é requerido.")]
    [MinLength(3)]
    [MaxLength(100)]
    public string? Nome { get; set; }

    [Required(ErrorMessage = "O descrição é requerido.")]
    [MinLength(3)]
    [MaxLength(100)]
    public string? Descricao { get; set; }
}
