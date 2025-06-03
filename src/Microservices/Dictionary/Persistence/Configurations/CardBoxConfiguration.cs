using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    internal sealed class CardBoxConfiguration : IEntityTypeConfiguration<CardBox>
    {
        public void Configure(EntityTypeBuilder<CardBox> builder) {
            builder
                .ToTable("card_box")
                .HasKey(x => x.Id);

            builder
                .Property(x => x.Id)
                .HasColumnName("id");

            builder
                .Property(x => x.TotalWords)
                .HasColumnName("total_words");
                //.HasComputedColumnSql("SELECT COUNT(*) FROM public.card_box as cb JOIN (SELECT card_plan_id, (now() - dr.learn_status_changed_at) as date FROM public.dictionary as d JOIN public.dictionary_row as dr on dr.dictionary_id = d.id WHERE d.card_plan_id = [TotalWords]) as dd on cb.card_plan_id = dd.card_plan_id WHERE date > make_interval(days=>cb.period);", stored: true);

            builder
                .Property(x => x.TotalWordsCountedAt)
                .HasColumnName("total_words_counted_at");

            builder
                .Property(x => x.Period)
                .HasColumnName("period");

            builder
                .Property(x => x.ViewedAt)
                .HasColumnName("viewed_at");

            builder
                .Property(x => x.LearnStatus)
                .HasColumnName("learn_status");

            builder
                .Property(x => x.CardPlanId)
                .HasColumnName("card_plan_id");
        }
    }
}
