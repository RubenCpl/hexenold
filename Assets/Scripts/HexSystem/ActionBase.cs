using DAE.BoardSystem;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAE.HexSystem
{
    abstract class ActionBase<TCard, TPiece> : IMove<TPiece, TCard>
        where TPiece : IPiece
        where TCard : ICard
    {


        public virtual bool CanExecute(Board<Position, TPiece> board, Grid<Position> grid, TPiece piece, TCard card)
        {
            return true;
        }

        public virtual void Execute(Board<Position, TPiece> board, Grid<Position> grid, TPiece piece, Position position, TCard card) { }

        public abstract List<Position> ValidPositions(Board<Position, TPiece> board, Grid<Position> grid, TPiece piece, Position position, TCard card);
        public abstract List<Position> IsolatedPositions(Board<Position, TPiece> board, Grid<Position> grid, TPiece piece, Position position, TCard card);

    }
}
