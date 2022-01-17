using DAE.BoardSystem;
using DAE.HexSystem.Moves;
using DAE.Commons;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace DAE.HexSystem
{
    public class ActionManager<TCard, TPiece>
        where TPiece : IPiece
        where TCard : ICard
    {

        private MultiValueDictionary<CardType, IMove<TPiece, TCard>> _actions = new MultiValueDictionary<CardType, IMove<TPiece, TCard>>();

        private readonly Board<Position, TPiece> _board;
        private readonly Grid<Position> _grid;

        public ActionManager(Board<Position, TPiece> board, Grid<Position> grid)
        {
            _board = board;
            _grid = grid;

            InitializeActions();
        }

        public List<Position> ValidPositionFor(TPiece piece, Position position, TCard card)
        {

             return _actions[card.CardType]
                .Where( m => m.CanExecute(_board, _grid, piece, card) )
                .SelectMany(m => m.ValidPositions(_board, _grid, piece, position, card))
                .ToList();
        }

        public List<Position> IsolatedValidPositionFor(TPiece piece, Position position, TCard card)
        {

            return _actions[card.CardType]
               .Where(m => m.CanExecute(_board, _grid, piece, card))
               .SelectMany(m => m.IsolatedPositions(_board, _grid, piece, position, card))
               .ToList();
        }

        public void Action(TPiece piece, Position position, TCard card)
        {
            _actions[card.CardType]
                .Where(m => m.CanExecute(_board, _grid, piece, card))
                .First(m => m.IsolatedPositions(_board, _grid, piece, position, card).Contains(position))
                .Execute(_board, _grid, piece, position, card);
        }

        private void InitializeActions()
        {

            _actions.Add(CardType.LaserBeam, new LaserBeam<TCard, TPiece>());

            _actions.Add(CardType.Teleport, new Teleport<TCard, TPiece>());

            _actions.Add(CardType.Slash, new Slash<TCard, TPiece>());

            _actions.Add(CardType.Push, new Push<TCard, TPiece>());

            _actions.Add(CardType.Bomb, new Bomb<TCard, TPiece>());
        }

    }
}
