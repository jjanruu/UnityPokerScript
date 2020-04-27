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

    // Update is called once per frame
    void Update()
    {
        
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
                    CardNumber = "A";
                }
                else if (CardNumber == 10.ToString())
                {
                    CardNumber = "T";
                }
                else if (CardNumber == 11.ToString())
                {
                    CardNumber = "J";
                }
                else if (CardNumber == 12.ToString())
                {
                    CardNumber = "Q";
                }
                else if (CardNumber == 13.ToString())
                {
                    CardNumber = "K";
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
        AddDoubleQuotes();
        RandomSetOfCards();
    }

    void AddDoubleQuotes()
    {
        for (int i = 0; i < SetOfCards.Count; i++)
        {
            var tempSetOfCards = SetOfCards[i];
            tempSetOfCards = "\"" + tempSetOfCards + "\"";
            SetOfCards[i] = tempSetOfCards;
        }
    }

    public void RandomSetOfCards()
    {
        System.Random randomGenerator = new System.Random();
        SetOfCards = SetOfCards.OrderBy(x => randomGenerator.Next(1, 1000)).ToList();
    }
}
