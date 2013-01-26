using Game.Logic.Model;
namespace Game.Logic.Impl
{
    using System;
    using System.Linq;
    using System.Data;
    using System.Data.Objects;
    using System.Data.EntityClient;
    using Model;
    using DynamicModel;
    using Game.Core;
    /**
     * Es el contenedor de cartas
     **/
    internal class CardContainer : ICardContainer
    {
        public CardContainer()
        {

        }

        public ICard GetCard(int id)
        {
            return RequestContext.Model<Entities>().Cards.First(card => card.Id == id);
        }

        public ICard SaveCard(IUserSession session, ICard newCard)
        {
            CardEffect effect = new CardEffect
            {
                Affected = (int)newCard.Effect.Affected,
                CardAttackChange = newCard.Effect.CardAttackChange,
                CardAttackMultiplier = newCard.Effect.CardAttackMultiplier,
                Description = newCard.Effect.Description,
                DisableOpponentEffect = newCard.Effect.DisableOpponentEffect,
                EffectTiming = (int)newCard.Effect.EffectTiming,
                LifePointsChange = newCard.Effect.LifePointsChange,
                Name = newCard.Effect.Name,
                ProbabilityOfEffect = newCard.Effect.ProbabilityOfEffect
            };

            Card created = new Card
            {
                Name = newCard.Name,
                ImageUrl = newCard.ImageUrl,
                Effect = effect,
                AttackPoints = newCard.AttackPoints,
                DefensePoints = newCard.DefensePoints
            };

            RequestContext.Model<Entities>().AddToCards(created);
            RequestContext.Model<Entities>().SaveChanges();

            return created;
        }
    }
}
