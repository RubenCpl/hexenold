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
    class StartScreenState : GameStateBase
    {
        private ActionManager<Card, Piece> _actionManager;

        private GameObject _gameOver;



        public StartScreenState(StateMachine<GameStateBase> stateMachine, ActionManager<Card, Piece> actionManager) : base(stateMachine)
        {
            _actionManager = actionManager;


        }

        //internal override void Dropped(Position position, Piece piece, Piece[] pieces, PositionView[] views, Card[] cards, GameObject hand)
        //{

        //    foreach (var card in cards)
        //    {
        //        if (card.CardActive == true)
        //        {
        //            _actionManager.Action(piece, position, card);

        //            foreach (var view in views)
        //            {
        //                view.Model.Deactivate();
        //            }
        //            RemoveCard(cards, hand);
        //        }
        //    }
        //}


        //    private void RemoveCard(Card[] cards, GameObject hand)
        //    {
        //        foreach (var card in cards)
        //        {
        //            card.CardDestory();
        //        }

        //        hand.GetComponent<HandHelper>().GenerateCard();
        //    }


        //    internal override void Hovered(Position position, Piece piece, Piece[] pieces, PositionView[] views, Card[] cards, GameObject hand)
        //    {
        //        bool isolate = false;
        //        foreach (var view in views)
        //        {
        //            view.Model.Deactivate();
        //        }

        //        foreach (var card in cards)
        //        {
        //            if (card.CardActive == true)
        //            {
        //                var positions = _actionManager.ValidPositionFor(piece, position, card);

        //                foreach (var pos in positions)
        //                {
        //                    if (position == pos)
        //                        isolate = true;
        //                }

        //                if (isolate != true)
        //                    foreach (var pos in positions)
        //                        if (pos != null)
        //                            pos.Activate();

        //                if (isolate == true)
        //                {
        //                    var isolatedPos = _actionManager.IsolatedValidPositionFor(piece, position, card);

        //                    foreach (var iPos in isolatedPos)
        //                    {
        //                        if (iPos != null)
        //                            iPos.Activate();
        //                    }
        //                }
        //            }
        //        }
        //    }
        //}
    }
}
