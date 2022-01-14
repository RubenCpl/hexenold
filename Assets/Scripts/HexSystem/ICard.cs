using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DAE.BoardSystem;


namespace DAE.HexSystem
{

        public interface ICard
        {
            CardType CardType { get; }

            bool CardActive { get; }
    }
}