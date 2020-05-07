using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System;
using UnityEngine.UI;
using SimpleJSON;

public class PlayersCardController : MonoBehaviour
{
    public List<String> ShuffledCards;
    public ShuffleCardsController GetShuffleCardsController;

    [Serializable]
    public class PlayerClass
    {
        public string Rank;
        public List<string> MainCards;
        public List<string> BestCards;
        public List<string> Cards;
        public List<int> ScoreCards;
        public List<int> PairValue;
        public int MainScore;

        public int StraightCount;
        public int TrioCount;
        public int countLowestStraight = 0;
        public int[] Suits = new int[4];
        public string TempRoyalFlush;

    }
    public List<PlayerClass> Players;

    void Start()
    {
        GetShuffleCardsController = gameObject.GetComponent<ShuffleCardsController>();
        //Invoke("SetPlayerCards", .25f);
    }


    public void SetPlayerCards()
    {
        ShuffledCards = GetShuffleCardsController.SetOfCards;
        for (int i = 0; i < 5; i++)
        {
            for (int pl = 0; pl < 2; pl++)
            {
                Players[pl].Cards.Add(ShuffledCards[i]);
            }
        }

        for (int i = 5; i < 7; i++)
        {
            Players[0].Cards.Add(ShuffledCards[i]);
            Players[0].MainCards.Add(ShuffledCards[i]);
        }
        for (int i = 7; i < 9; i++)
        {
            Players[1].Cards.Add(ShuffledCards[i]);
            Players[1].MainCards.Add(ShuffledCards[i]);
        }
    }
}
