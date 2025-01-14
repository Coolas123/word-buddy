using System.ComponentModel.DataAnnotations;

namespace Domain.Enums
{
    public enum Country
    {
        [Display(Name = "Россия")]
        Russia = 1,
        [Display(Name = "США")]
        USA = 2,
    }
}
