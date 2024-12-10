using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.ViewModels
{
    public sealed class DictionaryRowViewModel
    {
        public Word? Word { get; set; }
        public Translation? Translation { get; set; }
    }
}
