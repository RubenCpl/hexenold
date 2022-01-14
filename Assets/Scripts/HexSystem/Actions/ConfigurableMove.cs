using DAE.BoardSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAE.HexSystem.Moves
{
    class ConfigurableMove<TCard, TPiece> : ActionBase<TCard, TPiece>
        where TPiece : IPiece
        where TCard : ICard
    {

        public delegate List<Position> PositionCollector(Board<Position, TPiece> board, Grid<Position> grid, TPiece piece, TCard card);


        private PositionCollector _positionCollector;

        public ConfigurableMove(PositionCollector positionCollector)
        {
            _positionCollector = positionCollector;
        }


        public override List<Position> ValidPositions(Board<Position, TPiece> board, Grid<Position> grid, TPiece piece, Position position, TCard card)
            => _positionCollector(board, grid, piece, card);

        public override List<Position> IsolatedPositions(Board<Position, TPiece> board, Grid<Position> grid, TPiece piece, Position position, TCard card)
            => _positionCollector(board, grid, piece, card);
    }
}
