using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Postech8SOAT.FastOrder.WebAPI.DTOs;

public class ClienteDTO
{

    [Display(Name = "CPF")]
    [Required(ErrorMessage = "CPF deve estar preenchido.")]
    [RegularExpression(@"^(\d{3}\.\d{3}\.\d{3}-\d{2})$", ErrorMessage = "CPF inválido")]
    public string? Cpf { get; set; }

    [Required(ErrorMessage = "O nome é requerido.")]
    [MinLength(3)]
    [MaxLength(100)]
    [DisplayName("Nome")]
    public string? Nome { get; set; }

    [Required(ErrorMessage = "O email deve ser informado", AllowEmptyStrings = false)]
    [EmailAddress(ErrorMessage = "Formato do email inválido")]
    public string? Email { get; set; }
}
