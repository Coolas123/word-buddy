
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
               "there is no dicitonaries"
           );
        }

        public static class CardPlan
        {

            public static readonly Error ThereIsNoCardPlans = new Error
            (
                "GetCardPlans.Handle",
                "there is no card plans"
            );

            public static readonly Error DictionariesWasNotFound = new Error
            (
                "GetCardPlanDictionariesIdQueryHandler.Handle",
                "there is no dictionaries"
            );

            public static readonly Error UpdateFailure = new Error
            (
                "UpdateDictionariesCardPlanCommandHandler.Handle",
                "unsuccessful update"
            );
        }

        public static class GeneratedTextHistory
        {
            public static readonly Error GeneratedTextHistoryWasNotFound = new Error
           (
               "GetGeneratedTextHistoryQuery.Handle",
               "there is no contexts"
           );
        }
    }
}
