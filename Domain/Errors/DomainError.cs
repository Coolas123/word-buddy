using Domain.Shared;

namespace Domain.Errors
{
    public static class DomainError
    {
        public static class UserError {

            public static readonly Error EmailIsArleadyUsed = new Error
            (
                "User.Create",
                "The email is arleady used"
            );
        }

        public static class DictionaryError
        {

            public static readonly Error WordIsEmpty = new Error
            (
                "DictionaryRow.Create",
                "Пустое значение"
            );

            public static readonly Error TranslationIsEmpty = new Error
            (
                "DictionaryRow.Create",
                "Пустое значение"
            );

            public static readonly Error WordIsLong = new Error
            (
                "DictionaryRow.Create",
                "Слишком длинное значение"
            );

            public static readonly Error TranslationIsLong = new Error
           (
               "DictionaryRow.Create",
               "Слишком длинное значение"
           );
        }
    }
}
