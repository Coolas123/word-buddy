using Application.Abstractions.Messaging;
using Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Application.Dictionaries.Commands.CreateDictionary
{
    public sealed class CreateDictionaryCommand : ICommand<Guid>
    {
        [Display(Name ="Заголовок")]
        public string? Title { get; set; }
        [Display(Name = "Описание словаря")]
        public string? Description { get; set; }
        [Display(Name = "Язык слова")]
        public Language? WordLanguage { get; set; }
        [Display(Name = "Язык перевода")]
        public Language? TranslationLanguage { get; set; }

        public DateTime? CreatedAt { get; set; }

        public Guid UserId { get; set; }
    }
}
