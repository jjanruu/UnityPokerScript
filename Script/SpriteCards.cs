using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;
public class SpriteCards : MonoBehaviour
{
    string[] StringPlayer1Cards = new string[2];
    string[] StringPlayer2Cards = new string[2];
    string[] StringTableCards = new string[5];

    public GameObject[] Player1CardsUI;
    public GameObject[] Player2CardsUI;

    public GameObject[] TableCards;

    public Sprite[] CardSprites;

    PlayersCardController PlayerCardController;
    void Start()
    {
        PlayerCardController = gameObject.GetComponent<PlayersCardController>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void SetStringPlayer1()
    {
        StringPlayer1Cards[0] = PlayerCardController.ShuffledCards[5];
        StringPlayer1Cards[1] = PlayerCardController.ShuffledCards[6];
    }

    void SetStringPlayer2()
    {
        StringPlayer2Cards[0] = PlayerCardController.ShuffledCards[7];
        StringPlayer2Cards[1] = PlayerCardController.ShuffledCards[8];
    }
    void SetStringTableCards()
    {
        for (int i = 0; i < 5; i++)
        {
            StringTableCards[i] = PlayerCardController.ShuffledCards[i];
        }
    }

    public void displayMyCards()
    {
        SetStringPlayer1();
        SetStringPlayer2();
        SetStringTableCards();

        Regex reg = new Regex("[*'\",_&#^@]");

        for (int i = 0; i < CardSprites.Length; i++)
        {
            for (int a = 0; a < StringPlayer1Cards.Length; a++)
            {

                if (CardSprites[i].name == reg.Replace(StringPlayer1Cards[a], string.Empty))
                {
                    Player1CardsUI[a].GetComponent<Image>().sprite = CardSprites[i];
                }

                if (CardSprites[i].name == reg.Replace(StringPlayer2Cards[a], string.Empty))
                {
                    Player2CardsUI[a].GetComponent<Image>().sprite = CardSprites[i];
                }
            }

            for (int b = 0; b < TableCards.Length; b++)
            {
                if (CardSprites[i].name == reg.Replace(StringTableCards[b], string.Empty))
                {
                    TableCards[b].GetComponent<Image>().sprite = CardSprites[i];
                }
            }
        }
    }

}
