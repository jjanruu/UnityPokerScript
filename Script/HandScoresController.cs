using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System;
using UnityEngine.UI;
using SimpleJSON;
using System.Linq;

public class HandScoresController : MonoBehaviour
{

    JSONNode PlayerSeasonCards;
    public string CardsResponse;

    string AllSeatsURL;

   
    int CountPlayers = 9;
    public List<int> Scores;
    public List<string> Rank;
    public Text[] RankText = new Text[2];

    public List<GameObject> PlayerWinner;

    SpriteCards SpriteCards;
    PlayersCardController PlayerCardController;
    ShuffleCardsController ShuffleCardsController;
    void Start()
    {
        PlayerCardController = gameObject.GetComponent<PlayersCardController>();
        SpriteCards = gameObject.GetComponent<SpriteCards>();
        ShuffleCardsController = gameObject.GetComponent<ShuffleCardsController>();
        GetCardsResponse();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GetCardsResponse()
    {
        //button
        ShuffleCardsController.AddSetOfCards();
        DisableWinner();
        PlayerCardController.SetPlayerCards();
        DelayGetRankScores();
    }

    void DelayGetRankScores()
    {
        StartCoroutine(GetRankScores());
    }

    IEnumerator GetRankScores()
    {
        AllSeatsURL = PlayerCardController.AllSeatsURL;

        using (UnityWebRequest webRequest = UnityWebRequest.Get(AllSeatsURL))
        {
            yield return webRequest.SendWebRequest();
            if (webRequest.isNetworkError || webRequest.isHttpError)
            {
                Debug.Log(webRequest.error);
            }
            else
            {
                //Debug.Log(webRequest.downloadHandler.text);

                CardsResponse = webRequest.downloadHandler.text;
                PlayerSeasonCards = SimpleJSON.JSON.Parse(CardsResponse);

                Scores.Clear();
                Rank.Clear();

                for (int i = 0; i < PlayerSeasonCards.Count; i++)
                {
                    Scores.Add(PlayerSeasonCards[i]["score"].AsInt);
                    Rank.Add(PlayerSeasonCards[i]["rank"].Value.ToString());
                }
                SerRankText();
                RemoveOtherScores();
                //Scores.Sort();
                SpriteCards.displayMyCards();
                CheckWinner();
            }
        }
    }

    void SerRankText()
    {
        for (int i = 0; i < RankText.Length; i++)
        {
            RankText[i].text = "RankCard: " + Rank[i];
        }
    }

    void RemoveOtherScores()
    {

        for (int i = 2; i < PlayerSeasonCards.Count; i++)
        {
            Scores[i] = 9999;
        }
    }

    void CheckWinner()
    {
        //lowest score wins;
        if (Scores[0] > Scores[1])
        {
            PlayerWinner[1].SetActive(true);
        }
        else if(Scores[0] == Scores[1])
        {
            PlayerWinner[2].SetActive(true);
        }
        else
        {
            PlayerWinner[0].SetActive(true);
        }
        Invoke("DelayEnableButton", 1.5f);
        //DisableWinner();
    }

    void DisableWinner()
    {
        for (int i = 0; i < PlayerWinner.Count; i++)
        {
            PlayerWinner[i].SetActive(false);
        }

    }

    void DelayEnableButton()
    {
        ButtonRetry.enabled = true;
    }
    public Button ButtonRetry;

}
