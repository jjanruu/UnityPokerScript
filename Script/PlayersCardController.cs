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

    public List<string> player0 = new List<string>();
    public List<string> player1 = new List<string>();
    List<string> player2 = new List<string>();
    List<string> player3 = new List<string>();
    List<string> player4 = new List<string>();
    List<string> player5 = new List<string>();
    List<string> player6 = new List<string>();
    List<string> player7 = new List<string>();
    List<string> player8 = new List<string>();

    string[] SeatURL = new string[9];
    public string AllSeatsURL;
    public ShuffleCardsController GetShuffleCardsController;
    public string ServerUrl;

    void Start()
    {
        GetShuffleCardsController = gameObject.GetComponent<ShuffleCardsController>();
        //Invoke("SetPlayerCards", .25f);
    }


    public void SetPlayerCards()
    {
        RemoveAllCards();
        ShuffledCards = GetShuffleCardsController.SetOfCards;
        for (int i = 0; i < 5; i++)
        {
            player0.Add(ShuffledCards[i]);
            player1.Add(ShuffledCards[i]);
            player2.Add(ShuffledCards[i]);
            player3.Add(ShuffledCards[i]);
            player4.Add(ShuffledCards[i]);
            player5.Add(ShuffledCards[i]);
            player6.Add(ShuffledCards[i]);
            player7.Add(ShuffledCards[i]);
            player8.Add(ShuffledCards[i]);
        }

        for (int i = 5; i < 7; i++)
        {
            player0.Add(ShuffledCards[i]);
        }
        for (int i = 7; i < 9; i++)
        {
            player1.Add(ShuffledCards[i]);
        }
        for (int i = 9; i < 11; i++)
        {
            player2.Add(ShuffledCards[i]);
        }
        for (int i = 11; i < 13; i++)
        {
            player3.Add(ShuffledCards[i]);
        }
        for (int i = 13; i < 15; i++)
        {
            player4.Add(ShuffledCards[i]);
        }
        for (int i = 15; i < 17; i++)
        {
            player5.Add(ShuffledCards[i]);
        }
        for (int i = 17; i < 19; i++)
        {
            player6.Add(ShuffledCards[i]);
        }
        for (int i = 19; i < 21; i++)
        {
            player7.Add(ShuffledCards[i]);
        }
        for (int i = 21; i < 23; i++)
        {
            player8.Add(ShuffledCards[i]);
        }

        SeatURL[0] = string.Join(",", player0);
        SeatURL[1] = string.Join(",", player1);
        SeatURL[2] = string.Join(",", player2);
        SeatURL[3] = string.Join(",", player3);
        SeatURL[4] = string.Join(",", player4);
        SeatURL[5] = string.Join(",", player5);
        SeatURL[6] = string.Join(",", player6);
        SeatURL[7] = string.Join(",", player7);
        SeatURL[8] = string.Join(",", player8);
        SetSeatWebsite();
    }

    void SetSeatWebsite()
    {
        //SeatURL = SeatURL;
        AllSeatsURL = "";
        for (int i = 0; i < SeatURL.Length; i++)
        {
            AllSeatsURL += SeatURL[i] + "/";
            //SeatURL[i] = "https://poker-computation.herokuapp.com/Api/v1/Poker/" + SeatURL[i];
            SeatURL[i] = ServerUrl + SeatURL[i];
        }

        AllSeatsURL = AllSeatsURL.Remove(AllSeatsURL.Length - 1, 1);
        //AllSeatsURL = "https://poker-computation.herokuapp.com/Api/v1/Poker/" + AllSeatsURL;
        AllSeatsURL = ServerUrl + AllSeatsURL;
    }

    void RemoveAllCards()
    {
        player0.Clear();
        player1.Clear();
        player2.Clear();
        player3.Clear();
        player4.Clear();
        player5.Clear();
        player6.Clear();
        player7.Clear();
        player8.Clear();
    }
}
