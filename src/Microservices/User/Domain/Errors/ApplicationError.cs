
using Domain.Shared;

namespace Domain.Errors
{
    public static class ApplicationError
    {
        public static class JWTError
        {

            public static readonly Error TokenNull = new Error
            (
                "JWTGenerator.Create",
                "token is null"
            );
        }

        public static class User
        {

            public static readonly Error UserNotFound = new Error
            (
                "GetUserQuery.Handle",
                "user with same id not found"
            );
        }

        public static class Dictionary
        {
            public static readonly Error DictionariesWasNotFound = new Error
           (
               "GetDictionaries.Handle",
               "Не нашлось словарей"
           );
        }
    }
}
