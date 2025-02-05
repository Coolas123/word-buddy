using System.ComponentModel.DataAnnotations;

namespace Domain.Common.Enums
{
    public enum Language
    {
        [Display(Name ="Русский")]
        Russian,
        [Display(Name = "Английский")]
        English
    }
}
