using DAE.BoardSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace DAE.HexSystem.Moves
{
    class LaserBeam<TCard, TPiece> : ActionBase<TCard, TPiece>
        where TPiece : IPiece
        where TCard : ICard
    {
        private List<Position> _bottomPositions;
        private List<Position> _topPositions;
        private List<Position> _topLeftPositions;
        private List<Position> _bottomLeftPositions;
        private List<Position> _topRightPositions;
        private List<Position> _bottomRightPositions;
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
            if (!board.TryGetPositionOf(piece, out var pos))
                return new List<Position>(0);

            if (!grid.TryGetCoordinateOf(pos, out var coordinate))
                return new List<Position>(0);

            var positionList = new List<Position>();

            List<Position> topPositions = AddPositions(grid, coordinate, 1, 0);
            _topPositions = topPositions;
            foreach (var p in topPositions)
                positionList.Add(p);

            List<Position> bottomPositions = AddPositions(grid, coordinate, -1, 0);
            _bottomPositions = bottomPositions;
            foreach (var p in bottomPositions)
                positionList.Add(p);

            List<Position>  bottomRightPositions = AddPositions(grid, coordinate, -1, -1);
            _bottomRightPositions = bottomRightPositions;
            foreach (var p in bottomRightPositions)
                positionList.Add(p);

            List<Position> topRightPositions = AddPositions(grid, coordinate, 0, -1);
            _topRightPositions = topRightPositions;
            foreach (var p in topRightPositions)
                positionList.Add(p);


            List<Position> bottomLeftPositions = AddPositions(grid, coordinate, 1, 1);
            _bottomLeftPositions = bottomLeftPositions;
            foreach (var p in bottomLeftPositions)
                positionList.Add(p);

            List<Position> topLeftPositions = AddPositions(grid, coordinate, 0, 1);
            _topLeftPositions = topLeftPositions;
            foreach (var p in topLeftPositions)
                positionList.Add(p);

            return positionList;
        }

        private List<Position> AddPositions(Grid<Position> grid, (int x, int y) coordinate, int v1, int v2)
        {
            var positionList = new List<Position>();
            for (int i = 1; i < 8; i++)
            {
                int v3 = i * v1;
                int v4 = i * v2;

                var calculatedPosition = CalculatePosition(grid, coordinate, v3, v4);
                if (calculatedPosition != null)
                positionList.Add(calculatedPosition);
            }
            return positionList;
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

            board.TryGetPieceAt(position, out var toPiece);

            var positionList = new List<Position>();

            if (_topPositions.Contains(position))
                positionList = _topPositions;

            if (_bottomPositions.Contains(position))
                positionList = _bottomPositions;

            if (_topLeftPositions.Contains(position))
                positionList = _topLeftPositions;

            if (_topRightPositions.Contains(position))
                positionList = _topRightPositions;

            if (_bottomLeftPositions.Contains(position))
                positionList = _bottomLeftPositions;

            if (_bottomRightPositions.Contains(position))
                positionList = _bottomRightPositions;

            return positionList;
        }
        public override void Execute(Board<Position, TPiece> board, Grid<Position> grid, TPiece piece, Position position, TCard card)
        {
            board.TryGetPositionOf(piece, out var pos);

            var positionList = IsolatedPositions(board, grid, piece, position, card);

            foreach (var p in positionList)
            {
                board.TryGetPieceAt(p, out var toPiece);
                if (toPiece != null)
                {
                    if (toPiece.PieceType == PieceType.Enemy)
                        board.Take(toPiece);
                }
            }
        }
    }
}
