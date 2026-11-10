using System.ComponentModel.DataAnnotations;

namespace Cifrofoto.Coursework.Models;

public class Client
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Укажите ФИО клиента")]
    [StringLength(120)]
    [Display(Name = "ФИО")]
    public string Name { get; set; } = "";

    [Required(ErrorMessage = "Укажите телефон клиента")]
    [Phone(ErrorMessage = "Проверьте номер телефона")]
    [StringLength(30)]
    [Display(Name = "Телефон")]
    public string Phone { get; set; } = "";

    [EmailAddress(ErrorMessage = "Проверьте адрес email")]
    [StringLength(150)]
    [Display(Name = "Email")]
    public string? Email { get; set; }
}
