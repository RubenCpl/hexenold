using DAE.BoardSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace DAE.HexSystem.Moves
{
    class Bomb<TCard, TPiece> : ActionBase<TCard, TPiece>
        where TPiece : IPiece
        where TCard : ICard
    {
        private Position _leftPosition;
        private Position _rightPosition;
        private Position _topLeftPosition;
        private Position _bottomLeftPosition;
        private Position _topRightPosition;
        private Position _bottomRightPosition;
        public override bool CanExecute(Board<Position, TPiece> board, Grid<Position> grid, TPiece piece, TCard card)
        {
            if (piece.Moved)
                return false;

            if (!board.TryGetPositionOf(piece, out var position))
                return false;

            if (!grid.TryGetCoordinateOf(position, out var coordinate))
                return false;

            return base.CanExecute(board, grid, piece, card);
        }

        public override List<Position> ValidPositions(Board<Position, TPiece> board, Grid<Position> grid, TPiece piece, Position position, TCard card)
        {
            if (!grid.TryGetCoordinateOf(position, out var coordinate))
                return new List<Position>(0);

            if (grid.TryGetPositionAt(coordinate.x, coordinate.y, out var newPosition))
            {
                return new List<Position>() { newPosition };
            }
            else
            {
                return new List<Position>(0);
            }
        }

        private Position CalculatePosition(Grid<Position> grid, (int x, int y) coordinate, int coorX, int coordY)
        {
            coordinate.x += coorX;
            coordinate.y += coordY;

            grid.TryGetPositionAt(coordinate.x, coordinate.y, out var bottomPosition);
            return bottomPosition;
        }

        public override List<Position> IsolatedPositions(Board<Position, TPiece> board, Grid<Position> grid, TPiece piece, Position position, TCard card)
        {
            if (ValidPositions(board, grid, piece, position, card) == null)
                return new List<Position>(0);

            grid.TryGetCoordinateOf(position, out (int x, int y) coordinate);

            board.TryGetPieceAt(position, out var toPiece);

            var positionList = new List<Position>();

            positionList.Add(position);

            Position rightPosition = CalculatePosition(grid, coordinate, 1, 0);
            _rightPosition = rightPosition;
            if (rightPosition != null)
                positionList.Add(rightPosition);

            Position leftPosition = CalculatePosition(grid, coordinate, -1, 0);
            _leftPosition = leftPosition;
            if (leftPosition != null)
                positionList.Add(leftPosition);

            Position botRightPosition = CalculatePosition(grid, coordinate, 0, -1);
            _bottomRightPosition = botRightPosition;
            if (botRightPosition != null)
                positionList.Add(botRightPosition);

            Position topRightPosition = CalculatePosition(grid, coordinate, 1, 1);
            _topRightPosition = topRightPosition;
            if (topRightPosition != null)
                positionList.Add(topRightPosition);

            Position bottomLeft = CalculatePosition(grid, coordinate, -1, -1);
            _bottomLeftPosition = bottomLeft;
            if (bottomLeft != null)
                positionList.Add(bottomLeft);

            Position topLeft = CalculatePosition(grid, coordinate, 0, 1);
            _topLeftPosition = topLeft;
            if (topLeft != null)
                positionList.Add(topLeft);

            return positionList;
        }

        private List<Position> AddIsolatedPositions(Position pos3, Position pos1, Position pos2, List<Position> positionList)
        {
            positionList.Add(pos1);
            positionList.Add(pos2);
            positionList.Add(pos3);

            return positionList;
        }

        public override void Execute(Board<Position, TPiece> board, Grid<Position> grid, TPiece piece, Position position, TCard card)
        {
            board.TryGetPositionOf(piece, out var pos);
            //var views = FindObjectsOfType<PositionView>();

            var positionList = IsolatedPositions(board, grid, piece, position, card);

            foreach (var p in positionList)
            {
                //p.;
                board.TryGetPieceAt(p, out var toPiece);
                if (toPiece != null)
                {
                   board.Take(toPiece);
                }
            }
        }
    }
}

