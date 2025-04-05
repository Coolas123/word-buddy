using Application.Abstractions.Messaging;
using Domain.Common.Enums;
using System.ComponentModel.DataAnnotations;

namespace Application.Dictionaries.Commands.UpdateDictionary
{
    public sealed class UpdateDictionaryCommand : ICommand
    {
        public Guid? UserId { get; set; }
        public Guid? Id { get; set; }
        [Display(Name = "Заголовок")]
        public string? Title { get; set; }
        [Display(Name = "Описание словаря")]
        public string? Description { get; set; }
        [Display(Name = "Язык слова")]
        public Language? WordLanguage { get; set; }
        [Display(Name = "Язык перевода")]
        public Language? TranslationLanguage { get; set; }
        public DateTime? LastViewedAt { get; set; }
    }
}
