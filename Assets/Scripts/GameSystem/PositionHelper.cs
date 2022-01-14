using DAE.BoardSystem;
using DAE.HexSystem;
using System;
using UnityEngine;

namespace DAE.GameSystem
{

    [CreateAssetMenu(menuName = "DAE/Position Helper")]
    public class PositionHelper  : ScriptableObject
    {
        public (int x, int y) ToGridPostion(Grid<Position> grid, Transform parent, Vector3 worldPosition)
        {
            var y = (float)worldPosition.z / 0.75f;
            var x = ((float)worldPosition.x + (0.433f * y )) / 0.866f;


            return ((int)x, (int)y);
        }

        public Vector3 ToWorldPosition(Grid<Position> grid, Transform parent, int x, int y)
        {
            var position = new Vector3(0, 0,0);

            position.x = (-y * 0.433f) + (x * 0.866f);

            position.z = 0.75f * y ;

            var worldPosition = position;

            return worldPosition;
        }
    }
}
