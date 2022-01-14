using DAE.BoardSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAE.HexSystem
{
    interface IMove<TPiece, TCard>
        where TPiece : IPiece
        where TCard : ICard
    {
        bool CanExecute(Board<Position, TPiece> board, Grid<Position> grid, TPiece piece, TCard card);

        void Execute(Board<Position, TPiece> board, Grid<Position> grid, TPiece piece, Position position, TCard card);

        List<Position> ValidPositions(Board<Position, TPiece> board, Grid<Position> grid, TPiece piece, Position position, TCard card);

        List<Position> IsolatedPositions(Board<Position, TPiece> board, Grid<Position> grid, TPiece piece, Position position, TCard card);

    }
}
