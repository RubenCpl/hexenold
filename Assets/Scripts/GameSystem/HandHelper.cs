using DAE.BoardSystem;
using DAE.HexSystem;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAE.GameSystem
{
    public class HandHelper : MonoBehaviour
    {
        public int MaxHand = 5;

        [SerializeField]
        public int cardsPerType = 3;

        private int _cardCounter;

        [SerializeField]
        public GameObject TeleportPrefab;

        [SerializeField]
        public GameObject LaserPrefab;

        [SerializeField]
        public GameObject SlashPrefab;

        [SerializeField]
        public GameObject PushPrefab;

        private List<GameObject> _cards = new List<GameObject>();


        public void LoadCardDeck()
        {
            for (int i = 0; i < cardsPerType; i++)
                _cards.Add(TeleportPrefab);

            for (int i = 0; i < cardsPerType; i++)
                _cards.Add(SlashPrefab);

            for (int i = 0; i < cardsPerType; i++)
                _cards.Add(PushPrefab);

            for (int i = 0; i < cardsPerType; i++)
                _cards.Add(LaserPrefab);

            _cards.Shuffle();
        }

        public void GenerateCard()
        {
            var cards = FindObjectsOfType<Card>();
            if (cards.Length < MaxHand)
            {
                CreateCard();
            }
        }

        private void CreateCard()
        {
            if (_cardCounter != _cards.Count)
            {
                GameObject teleportCard = Instantiate(_cards[_cardCounter]);
                teleportCard.transform.SetParent(this.gameObject.transform, false);
                _cardCounter++;
            }
        }
    }

    public static class MyExtensions
    {
        private static readonly System.Random rng = new System.Random();

        //Fisher - Yates shuffle
        public static void Shuffle<T>(this IList<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
    }

}
