using System.ComponentModel.DataAnnotations;

namespace Domain.Enums
{
    public enum Language
    {
        [Display(Name ="Русский")]
        Russian,
        [Display(Name = "Английский")]
        English
    }
}
