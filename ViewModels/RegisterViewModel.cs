using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RegistrationApp.ViewModels;

public class RegisterViewModel : IValidatableObject
{
    [Required(ErrorMessage = "Аты-жөніңізді міндетті түрде енгізіңіз")]
    [StringLength(50, MinimumLength = 3, ErrorMessage = "Аты-жөні 3 пен 50 таңба аралығында болуы керек")]
    [Display(Name = "Аты-жөні")]
    public string FullName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Email міндетті түрде енгізіңіз")]
    [EmailAddress(ErrorMessage = "Жарамды Email мекенжайын енгізіңіз")]
    [StringLength(100, ErrorMessage = "Email 100 таңбадан аспауы керек")]
    [Display(Name = "Email")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Жасыңызды енгізіңіз")]
    [Range(14, 100, ErrorMessage = "Жас 14 пен 100 жас аралығында болуы керек")]
    [Display(Name = "Жасы")]
    public int? Age { get; set; }

    [Required(ErrorMessage = "Телефон нөмірін енгізіңіз")]
    [RegularExpression(@"^\+?[0-9]{10,15}$", ErrorMessage = "Телефон нөмірі 10-15 цифрдан тұруы керек (мысалы: +77001234567)")]
    [Display(Name = "Телефон нөмірі")]
    public string Phone { get; set; } = string.Empty;

    [Required(ErrorMessage = "Құпиясөзді енгізіңіз")]
    [StringLength(30, MinimumLength = 6, ErrorMessage = "Құпиясөз 6 мен 30 таңба аралығында болуы керек")]
    [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d).+$", ErrorMessage = "Құпиясөзде кемінде бір әріп және бір цифр болуы керек")]
    [DataType(DataType.Password)]
    [Display(Name = "Құпиясөз")]
    public string Password { get; set; } = string.Empty;

    [Required(ErrorMessage = "Құпиясөзді қайта енгізіңіз")]
    [Compare(nameof(Password), ErrorMessage = "Құпиясөздер бір-біріне сәйкес келмейді")]
    [DataType(DataType.Password)]
    [Display(Name = "Құпиясөзді растау")]
    public string ConfirmPassword { get; set; } = string.Empty;

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (!string.IsNullOrWhiteSpace(Password) && !string.IsNullOrWhiteSpace(FullName))
        {
            var cleanName = FullName.Trim();
            if (cleanName.Length >= 3 && Password.Contains(cleanName, System.StringComparison.OrdinalIgnoreCase))
            {
                yield return new ValidationResult(
                    "Құпиясөздің ішінде аты-жөніңіз болмауы керек",
                    new[] { nameof(Password) }
                );
            }
        }
    }
}
