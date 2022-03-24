using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Command
{
    public class Command_Play_WithSelecting : Command_Play
    {
        public Command_Play_WithSelecting(bool isPlayer, int gameId, int indexTo, int selectedCard) : base(isPlayer, gameId, indexTo)
        {
            this.selectedCard = selectedCard;
        }


        [SerializeField] private int selectedCard;

        protected override void RunAbility_OnPlay(ICardInfo playingCard)
        {
            var ability = playingCard.AbilityBook.AbilityGetter();
            if (ability is ISelectableAbility selectable)
            {
                selectable.Set(selectedCard);
                ((IAbilityBase)selectable).Run(playingCard);
            }
            else
            {
                throw new System.Exception("ISelectableAbilityではないアビリティーを持っています");
            }
        }
    }
}
