using System.ComponentModel.DataAnnotations;

namespace Cifrofoto.Coursework.Models;

public class LoginInput
{
    [Required(ErrorMessage = "Введите пароль сотрудника")]
    [StringLength(256, ErrorMessage = "Пароль слишком длинный")]
    [DataType(DataType.Password)]
    [Display(Name = "Пароль сотрудника")]
    public string Password { get; set; } = "";
}
