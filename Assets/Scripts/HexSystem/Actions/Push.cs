using DAE.BoardSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace DAE.HexSystem.Moves
{
    class Push<TCard, TPiece> : ActionBase<TCard, TPiece>
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
            if (!board.TryGetPositionOf(piece, out var pos))
                return new List<Position>(0);

            if (!grid.TryGetCoordinateOf(pos, out var coordinate))
                return new List<Position>(0);

            var positionList = new List<Position>();

            Position rightPosition = CalculatePosition(grid, coordinate, 1, 0);
            _rightPosition = rightPosition;
            positionList.Add(rightPosition); 

            Position leftPosition = CalculatePosition(grid, coordinate, -1, 0);
            _leftPosition = leftPosition;
            positionList.Add(leftPosition);

            Position botRightPosition = CalculatePosition(grid, coordinate, 0, -1);
            _bottomRightPosition = botRightPosition;
            positionList.Add(botRightPosition); 

            Position topRightPosition = CalculatePosition(grid, coordinate, 1, 1);
            _topRightPosition = topRightPosition;
            positionList.Add(topRightPosition); 

            Position bottomLeft = CalculatePosition(grid, coordinate, -1, -1);
            _bottomLeftPosition = bottomLeft;
            positionList.Add(bottomLeft); 

            Position topLeft = CalculatePosition(grid, coordinate, 0, 1);
            _topLeftPosition = topLeft;
            positionList.Add(topLeft); 

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


            if (position == _rightPosition)
                positionList = AddIsolatedPositions(_rightPosition, _topRightPosition, _bottomRightPosition, positionList);

            if (position == _leftPosition)
                positionList = AddIsolatedPositions(_leftPosition, _topLeftPosition, _bottomLeftPosition, positionList);

            if (position == _topLeftPosition)
                positionList = AddIsolatedPositions(_topLeftPosition, _leftPosition, _topRightPosition, positionList);

            if (position == _topRightPosition)
                positionList = AddIsolatedPositions(_topRightPosition, _rightPosition, _topLeftPosition, positionList);

            if (position == _bottomLeftPosition)
                positionList = AddIsolatedPositions(_bottomLeftPosition, _leftPosition, _bottomRightPosition, positionList);

            if (position == _bottomRightPosition)
                positionList = AddIsolatedPositions(_bottomRightPosition, _rightPosition, _bottomLeftPosition, positionList);

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
            var positionList = IsolatedPositions(board, grid, piece, position, card);

            foreach (var p in positionList)
            {
                board.TryGetPieceAt(p, out var toPiece);

                //if (toPiece == null)
                //    return;

                if (toPiece != null && toPiece.PieceType == PieceType.Enemy)
                    if (p == _rightPosition)
                        MoveEnemies(board, grid, p, toPiece, 1, 0);


                if (toPiece != null && toPiece.PieceType == PieceType.Enemy)
                    if (p == _leftPosition)
                        MoveEnemies(board, grid, p, toPiece, -1, 0);


                if (toPiece != null && toPiece.PieceType == PieceType.Enemy)
                    if (p == _bottomLeftPosition)
                        MoveEnemies(board, grid, p, toPiece, -1, -1);

                if (toPiece != null && toPiece.PieceType == PieceType.Enemy)
                    if (p == _bottomRightPosition)
                        MoveEnemies(board, grid, p, toPiece, 0, -1);

                if (toPiece != null && toPiece.PieceType == PieceType.Enemy )
                    if (p == _topLeftPosition)
                        MoveEnemies(board, grid, p, toPiece, 0, 1);

                if (toPiece != null && toPiece.PieceType == PieceType.Enemy)
                    if (p == _topRightPosition)
                        MoveEnemies(board, grid, p, toPiece, 1, 1);
            }
        }

        private static void MoveEnemies(Board<Position, TPiece> board, Grid<Position> grid, Position p, TPiece toPiece, int c1, int c2)
        {
            grid.TryGetCoordinateOf(p, out var coo);
            coo.x += c1;
            coo.y += c2;
            grid.TryGetPositionAt(coo.x, coo.y, out var pushPosition);
            board.Move(toPiece, pushPosition);

            if (pushPosition == null)
            {
                board.Take(toPiece);
            }
        }
    }
}

