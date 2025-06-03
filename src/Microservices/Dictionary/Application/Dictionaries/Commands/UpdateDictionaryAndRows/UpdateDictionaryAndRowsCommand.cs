using Application.Abstractions.Messaging;
using Application.Dictionaries.Commands.UpdateDictionary;
using Application.DictionaryRow.Commands.CreateWord;
using Application.DictionaryRow.Commands.UpdateWord;
using Application.DictionaryRow.Commands.UpdateWord.UpdateWords;
using Domain.Entities;

namespace Application.Dictionaries.Commands.UpdateDictionaryAndRows
{
    public sealed class UpdateDictionaryAndRowsCommand : ICommand
    {
        public UpdateDictionaryRowsCommand UpdateDictionaryRowsCommand { get; set; } = null!;
        public CreateDictionaryRowsCommand CreateDictionaryRowsCommand { get; set; } = null!;
        public UpdateDictionaryCommand UpdateDictionaryCommand { get; set; } = null!;

        public static UpdateDictionaryAndRowsCommand Create(Dictionary dictionary) {
            var viewWords = new List<UpdateDictionaryRowCommand>(dictionary.DictionaryRows.Count);

            foreach (var word in dictionary.DictionaryRows) {
                var imPathBase = @"../Application/img/";
                byte[] imgByte = null;
                if (!string.IsNullOrEmpty(word.ImgPath)) {
                    imgByte = File.ReadAllBytes(imPathBase + word.Id);
                }
                viewWords.Add(new UpdateDictionaryRowCommand
                {
                    Id = word.Id,
                    WordText = word.WordText,
                    LearnStatus = word.LearnStatus,
                    LearnStatusChangedAt = word.LearnStatusChangedAt,
                    WordTranslation = word.WordTranslation,
                    WordContexts = word.WordContexts,
                    ImgBase64 = imgByte == null ? "" : Convert.ToBase64String(imgByte),
                    NoteText = word.NoteText,

                });
            }

            return new UpdateDictionaryAndRowsCommand
            {
                UpdateDictionaryCommand = new UpdateDictionaryCommand
                {
                    Title = dictionary.Title,
                    Description = dictionary.Description,
                    LastViewedAt = dictionary.LastViewedAt,
                    WordLanguage = dictionary.WordLanguage,
                    TranslationLanguage = dictionary.TranslationLanguage,
                    Id = dictionary.Id
                },
                UpdateDictionaryRowsCommand = new UpdateDictionaryRowsCommand
                {
                    DictionaryRows = viewWords,
                    DictionaryId = dictionary.Id
                },
                CreateDictionaryRowsCommand = new CreateDictionaryRowsCommand
                {
                    DictionaryId = dictionary.Id,
                    DictionaryRows = new()
                }
            };
        }
    }
}
