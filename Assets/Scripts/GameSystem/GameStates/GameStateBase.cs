﻿using DAE.HexSystem;
using DAE.StateSystem;
using DAE.GameSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace DAE.GameSystem.GameStates
{
    class GameStateBase : IState<GameStateBase>
    {
        public StateMachine<GameStateBase> StateMachine { get; }

        public GameStateBase(StateMachine<GameStateBase> stateMachine)
        {
            StateMachine = stateMachine;
        }

        public virtual void OnEnter()
        {   
        }

        public virtual void OnExit()
        {    
        }

        internal virtual void Dropped(Position position, Piece piece, Piece[] pieces, PositionView[] views, Card[] cards, GameObject hand)
        {
            
        }

        internal virtual void Hovered (Position position, Piece piece, Piece[] pieces, PositionView[] views, Card[] cards, GameObject hand)
        {
            
        }
    }
}
