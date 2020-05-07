using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;
public class TableSpriteCards : MonoBehaviour
{

    public string[] StringTableCards = new string[5];
    public GameObject[] PlayersCard;
    public GameObject[] TableCards;
    public Sprite[] CardSprites;

    PlayersCardController PlayerCardController;
    void Start()
    {
        PlayerCardController = gameObject.GetComponent<PlayersCardController>();
    }


    void SetPlayersCard()
    {
        for (int i = 0; i < PlayerCardController.Players.Count; i++)
        {
            for (int mcrds = 0; mcrds < PlayerCardController.Players[i].MainCards.Count; mcrds++)
            {
                for (int scards = 0; scards < CardSprites.Length; scards++)
                {
                    if (PlayerCardController.Players[i].MainCards[mcrds] == CardSprites[scards].name)
                    {
                        PlayersCard[i].transform.GetChild(mcrds).GetComponent<Image>().sprite = CardSprites[scards];
                    }
                }
            }
        }
    }

    void SetStringTableCards()
    {
        for (int i = 0; i < 5; i++)
        {
            StringTableCards[i] = PlayerCardController.ShuffledCards[i];
        }
    }

    public void DisplayTableCards()
    {
        SetPlayersCard();
        SetStringTableCards();
        for (int i = 0; i < CardSprites.Length; i++)
        {
            for (int b = 0; b < TableCards.Length; b++)
            {
                if (CardSprites[i].name == StringTableCards[b])
                {
                    TableCards[b].GetComponent<Image>().sprite = CardSprites[i];
                }
            }
        }
    }

}
