using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class ShuffleCardsController : MonoBehaviour
{
    public List<String> SetOfCards;

    void Start()
    {
        AddSetOfCards();

    }

    public void AddSetOfCards()
    {
        if(SetOfCards.Count != 0)
        {
            SetOfCards.Clear();
        }

        for (int i = 1; i < 14; i++)
        {
            if (SetOfCards.Count < 52)
            {
                string CardNumber = i.ToString();
                if (CardNumber == 1.ToString())
                {
                    CardNumber = "Z";
                }
                else if (CardNumber == 10.ToString())
                {
                    CardNumber = "V";
                }
                else if (CardNumber == 11.ToString())
                {
                    CardNumber = "W";
                }
                else if (CardNumber == 12.ToString())
                {
                    CardNumber = "X";
                }
                else if (CardNumber == 13.ToString())
                {
                    CardNumber = "Y";
                    i = 0;
                }
            
                if (SetOfCards.Count < 13)
                {
                    SetOfCards.Add(CardNumber + "H");
                }
                else if (SetOfCards.Count >= 13 && SetOfCards.Count < 26)
                {
                    SetOfCards.Add(CardNumber + "D");
                }
                else if (SetOfCards.Count >= 26 && SetOfCards.Count < 39)
                {
                    SetOfCards.Add(CardNumber + "S");
                }
                else if (SetOfCards.Count >= 39)
                {
                    SetOfCards.Add(CardNumber + "C");
                }
            }
        }
        RandomSetOfCards();
    }


    public void RandomSetOfCards()
    {
        System.Random randomGenerator = new System.Random();
        SetOfCards = SetOfCards.OrderBy(x => randomGenerator.Next(1, 1000)).ToList();
    }
}
