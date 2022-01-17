using DAE.HexSystem;
using DAE.StateSystem;
using DAE.GameSystem;
using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAE.GameSystem.GameStates
{
    class GamePlayState : GameStateBase
    {
        private ActionManager<Card, Piece> _actionManager;



        public GamePlayState( StateMachine<GameStateBase> stateMachine, ActionManager<Card, Piece> actionManager) : base(stateMachine)
        {
            _actionManager = actionManager;


        }

        internal override void Dropped(Position position, Piece piece)
        {
            var cards = FindObjectsOfType<Card>();
            foreach (var card in cards)
            {
                if (card.CardActive == true)
                {
                    _moveManager.Action(piece, position, card);

                    var views = FindObjectsOfType<PositionView>();
                    foreach (var view in views)
                    {
                        view.Model.Deactivate();
                    }
                    RemoveCard();
                }
            }
        };
    }

        //internal override void Hovered(Position position)
        //{

        //}
    }
}
