using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Postech8SOAT.FastOrder.WebAPI.DTOs;

public class CategoriaDTO
{
    public CategoriaDTO(Guid? id, string nome, string descricao)
    {
        Id = id;
        Nome = nome;
        Descricao = descricao;
    }

    [DisplayName("Id")]
    public Guid? Id { get; private init; }

    [Required(ErrorMessage = "O nome é requerido.")]
    [MinLength(3)]
    [MaxLength(100)]
    public string Nome { get; init; } = null!;

    [Required(ErrorMessage = "O descrição é requerido.")]
    [MinLength(3)]
    [MaxLength(100)]
    public string Descricao { get; init; } = null!;
}
