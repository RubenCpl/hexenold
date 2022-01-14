using DAE.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace DAE.BoardSystem
{
    public class Grid<TPosition>
    {

        public float Radius { get; }


        public Grid(float radius)
        {
            Radius = radius;
        }

        private BidirectionalDictionary<(int x, int y), TPosition> _positions = new BidirectionalDictionary<(int, int), TPosition>();
      
        public bool TryGetPositionAt(int x, int y, out TPosition position)
            => _positions.TryGetValue((x, y), out position);


        public bool TryGetCoordinateOf(TPosition position, out (int x, int y) coordinate)
            => _positions.TryGetKey(position, out coordinate);

        public void Register(int x, int y, TPosition position)
        {

#if UNITY_EDITOR
#endif
            _positions.Add((x, y), position);

        }
    }
}
