using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.ViewModels
{
    public sealed class DictionaryRowViewModel
    {
        public string WordText { get; set; } = null!;
        public string WordTranslation { get; set; } = null!;
        public LearnStatus LearnStatus { get; set; }
    }
}
