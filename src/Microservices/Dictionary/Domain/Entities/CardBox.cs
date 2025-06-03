using Domain.Enums;
using Domain.Primitives;

namespace Domain.Entities
{
    public class CardBox : Entity
    {
        public Guid CardPlanId {  get; private set; }
        public LearnStatus LearnStatus { get; private set; }
        public int TotalWords {get;private set;}
        public DateTime TotalWordsCountedAt { get; private set; }
        public int? Period { get; private set; }
        public DateTime ViewedAt { get; private set; }

        public CardBox(
            Guid id,
            Guid cardPlanId,
            LearnStatus learnStatus,
            int totalWords,
            DateTime totalWordsCountedAt,
            int? period,
            DateTime viewedAt) : base(id) 
        {
            LearnStatus = learnStatus;
            CardPlanId = cardPlanId;
            TotalWords = totalWords;
            TotalWordsCountedAt = totalWordsCountedAt;
            Period = period;
            ViewedAt = viewedAt;
        }

        public static CardBox CreateByDefault(Guid cardPlanId,LearnStatus learnStatus, int TotalWords,int? period) {
            return new CardBox(
                Guid.NewGuid(),
                cardPlanId,
                learnStatus,
                TotalWords,
                DateTime.Now.ToUniversalTime(),
                period,
                DateTime.Now.ToUniversalTime());
        } 

        public CardBox SetTotalWords(int totalWords) {
            if(TotalWords > 0)
                TotalWords = totalWords;
            return this;
        }
    }
}
