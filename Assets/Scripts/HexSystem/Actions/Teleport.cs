using DAE.BoardSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace DAE.HexSystem.Moves
{
    class Teleport<TCard, TPiece> : ActionBase<TCard, TPiece>
        where TPiece : IPiece
        where TCard : ICard
    {
        public override bool CanExecute(Board<Position, TPiece> board, Grid<Position> grid, TPiece piece, TCard card)
        {
            return base.CanExecute(board, grid, piece, card);
        }

        public override List<Position> ValidPositions(Board<Position, TPiece> board, Grid<Position> grid, TPiece piece, Position position, TCard card)
        {
            if (!grid.TryGetCoordinateOf(position, out var coordinate))
                return new List<Position>(0);

            if (grid.TryGetPositionAt(coordinate.x, coordinate.y, out var newPosition) && IsEmptyCoordinate(board, grid, piece, coordinate.x, coordinate.y))
            {
                return new List<Position>() { newPosition };
            }   else
            {
                return new List<Position>(0);
            }
        }

        public override List<Position> IsolatedPositions(Board<Position, TPiece> board, Grid<Position> grid, TPiece piece, Position position, TCard card)
        {
            if (!board.TryGetPositionOf(piece, out var pos))
                return new List<Position>(0);

            if (!grid.TryGetCoordinateOf(position, out var coordinate))
                return new List<Position>(0);


            if (grid.TryGetPositionAt(coordinate.x, coordinate.y, out var newPosition) && IsEmptyCoordinate(board, grid, piece, coordinate.x, coordinate.y))
                return new List<Position>() { newPosition };
            else
                return new List<Position>(0);

        }



        private bool IsEmptyCoordinate(Board<Position, TPiece> board, Grid<Position> grid, TPiece piece, int x, int y)
        {
            if (!grid.TryGetPositionAt(x, y, out var position))
                return false;

            if (board.TryGetPieceAt(position, out _))
                return false;

            return true;
        }

        public override void Execute(Board<Position, TPiece> board, Grid<Position> grid, TPiece piece, Position position, TCard card)
        {
                board.Move(piece, position);
        }
    }
}


