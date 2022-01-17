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
    class EndScreenState : GameStateBase
    {

        private GameObject _gameOver;

        public EndScreenState(StateMachine<GameStateBase> stateMachine, ActionManager<Card, Piece> actionManager, GameObject gameOver) : base(stateMachine)
        {
            _gameOver = gameOver;
        }
        public override void OnEnter()
        {
            _gameOver.SetActive(true);
        }

    }
}

