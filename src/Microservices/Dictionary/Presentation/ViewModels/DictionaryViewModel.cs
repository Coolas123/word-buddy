using Domain.Common.Enums;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.ViewModels
{
    public sealed class DictionaryViewModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public Language WordLanguage { get; set; }
        public Language TranslationLanguage { get; set; }
        public DateTime LastViewedAt { get; set; }
        public IEnumerable<DictionaryRowViewModel> DictionaryRows{ get; set; } = Enumerable.Empty<DictionaryRowViewModel>();
        
        private DictionaryViewModel(Dictionary dictionary, IEnumerable<DictionaryRowViewModel> viewRows) {
            Id = dictionary.Id;
            Title = dictionary.Title;
            Description = dictionary.Description;
            WordLanguage = dictionary.WordLanguage;
            TranslationLanguage = dictionary.TranslationLanguage;
            LastViewedAt = dictionary.LastViewedAt;
            DictionaryRows = viewRows;
        }

        public static DictionaryViewModel Create(Dictionary dictionary) {
            var viewRows = new List<DictionaryRowViewModel>(dictionary.DictionaryRows.Count());

            foreach (var row in dictionary.DictionaryRows) {
                viewRows.Add(new DictionaryRowViewModel { 
                    WordText = row.WordText,
                    WordTranslation = row.WordTranslation,
                    LearnStatus = row.LearnStatus
                });
            }

            return new DictionaryViewModel(dictionary, viewRows);
        }
    }
}
