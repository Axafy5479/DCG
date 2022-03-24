using Position;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Command
{
    public class Command_Initialize : CommandBase
    {
        public Command_Initialize(bool isPlayer, InitializeData data) : base(isPlayer)
        {
            this.initialData = data;
        }

        [SerializeField] private InitializeData initialData;

        protected override void _execute()
        {
            TurnManager.Initialize(initialData.FirstAttack);
            new Ability_DI();


            new HeroController(true, initialData.HeroHpPlayer);
            new HeroController(false, initialData.HeroHpRival);

            PositionLocator.LI.Register(new Deck(true));
            PositionLocator.LI.Register(new Deck(false));

            PositionLocator.LI.Register(new Hand(true));
            PositionLocator.LI.Register(new Hand(false));

            PositionLocator.LI.Register(new Field(true));
            PositionLocator.LI.Register(new Field(false));

            PositionLocator.LI.Register(new Discard(true));
            PositionLocator.LI.Register(new Discard(false));

            new CardDataBase(initialData.AllCards.ToList());
        }

        public void SetInitialDeck()
        {
            PositionLocator.LI.Resolve<Deck>(true).MakeInitialDeck(initialData.DeckData_Player);
            PositionLocator.LI.Resolve<Deck>(false).MakeInitialDeck(initialData.DeckData_Rival);



        }

        public void StartDraw()
        {
            PositionLocator.LI.Resolve<Deck>(false).Draw();
            PositionLocator.LI.Resolve<Deck>(true).Draw();
        }

        public void GameStart()
        {
            TurnManager.TI.GameStart();
        }



        [Serializable]
        public struct InitializeData
        {
            public InitializeData(CardBook[] allCards,CardBook[] deckData_Player, CardBook[] deckData_Rival, bool firstAttack,int heroHpPlayer,int heroHpRival)
            {
                this.deckData_Player = deckData_Player;
                this.deckData_Rival = deckData_Rival;
                this.firstAttack = firstAttack;
                this.heroHpPlayer = heroHpPlayer;
                this.heroHpRival = heroHpRival;
                this.allCards = allCards;
            }


            [SerializeField] private CardBook[] deckData_Player;
            [SerializeField] private CardBook[] deckData_Rival;
            [SerializeField] private bool firstAttack;
            [SerializeField] private int heroHpPlayer;
            [SerializeField] private int heroHpRival;
            [SerializeField]private CardBook[] allCards;

            public CardBook[] DeckData_Player => deckData_Player;
            public CardBook[] DeckData_Rival => deckData_Rival;
            public bool FirstAttack => firstAttack;

            public int HeroHpPlayer { get => heroHpPlayer;}
            public int HeroHpRival { get => heroHpRival; }
            public CardBook[] AllCards => allCards;
        }
    }

}
