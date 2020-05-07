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
    public Button ButtonRetry;
    public Text[] RankText = new Text[2];

    public List<GameObject> PlayerWinner;

    TableSpriteCards TableSpriteCards;
    PlayersCardController PlayerCardController;
    ShuffleCardsController ShuffleCardsController;

    public string[] SpecialCards = new string[] { "Z", "Y", "X", "W", "V" };
    string[] SuitCards = new string[] { "S", "C", "H", "D"};


    int HighCardScore = 1000;
    int OnePairScore = 2000;
    int TwoPairsScore = 3000;
    int ThreeOfAKindScore = 4000;
    int StraightScore = 5000;
    int FullHouseScore = 6000;
    int FlushScore = 7000;
    int FourOfAKind = 8000;

    int StraightFlushScore = 9000;
    int RoyalFlushScore = 10000;

    public bool isShuffle;
    void Start()
    {
        PlayerCardController = gameObject.GetComponent<PlayersCardController>();
        TableSpriteCards = gameObject.GetComponent<TableSpriteCards>();
        ShuffleCardsController = gameObject.GetComponent<ShuffleCardsController>();

        Invoke("ComputeCards",.2f);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //ComputeCards();
        }       
    }

    public void GetCardsResponse()
    {
        //button
        //ShuffleCardsController.AddSetOfCards();
        DisableWinner();
        PlayerCardController.SetPlayerCards();
        ComputeCards();
    }

    public void ComputeCards()
    {
        ClearScore();
        if(isShuffle == true)
        {
            ShuffleCardsController.RandomSetOfCards();
        }

        PlayerCardController.SetPlayerCards();
       
        TableSpriteCards.DisplayTableCards();


        SortCards();

        SetCardValue();
        CheckHighCard();

        CheckPair(0);
        CheckPair(1);

        CheckStraight();
        CheckFlush();
        CheckStraightFlush();
        CheckRoyalFlush();


        SetRankText();
        CheckWinner();
    }

    void ClearScore()
    {
        for (int i = 0; i < PlayerCardController.Players.Count; i++)
        {
            PlayerCardController.Players[i].MainScore = 0;
            PlayerCardController.Players[i].PairValue.Clear();
            PlayerCardController.Players[i].Cards.Clear();
            PlayerCardController.Players[i].MainCards.Clear();
        }
    }

    void SortCards()
    {
        for (int i = 0; i < PlayerCardController.Players.Count; i++)
        {
            PlayerCardController.Players[i].Cards.Sort();
        }
    }


    void Kicker(int RankScore)
    {
        var OtherPlayer = 1;
        if (isFourOfAKind != true)
        {
            for (int i = 0; i < PlayerCardController.Players.Count; i++)
            {
                if (i > OtherPlayer)
                {
                    OtherPlayer += i;
                }
                if (PlayerCardController.Players[0].MainScore == PlayerCardController.Players[1].MainScore)//compare winner score
                {
                    //Debug.Log("pl" + 01 + " " + PlayerCardController.Players[0].MainScore);

                    if (PlayerCardController.Players[OtherPlayer].ScoreCards.Count == 7)
                    {
                        for (int kicker = 6; kicker >= 2; kicker--)
                        {
                            if (PlayerCardController.Players[i] != PlayerCardController.Players[OtherPlayer])
                            {
                                if (PlayerCardController.Players[i].ScoreCards[kicker] == PlayerCardController.Players[OtherPlayer].ScoreCards[kicker])
                                {
                                    PlayerCardController.Players[i].MainScore = RankScore + PlayerCardController.Players[i].ScoreCards[kicker];
                                    PlayerCardController.Players[OtherPlayer].MainScore = RankScore + PlayerCardController.Players[i].ScoreCards[kicker];
                                }
                                else
                                {
                                    PlayerCardController.Players[i].MainScore = RankScore + PlayerCardController.Players[i].ScoreCards[kicker];
                                    PlayerCardController.Players[OtherPlayer].MainScore = RankScore + PlayerCardController.Players[OtherPlayer].ScoreCards[kicker];
                                    kicker = 0;
                                }
                            }
                        }
                    }
                }
            }
        }
    }

    void KickerIncrement(int RankScore)
    {
        var OtherPlayer = 1;
        for (int i = 0; i < PlayerCardController.Players.Count; i++)
        {
            if (i > OtherPlayer)
            {
                OtherPlayer += i;
            }

            if (PlayerCardController.Players[0].MainScore == PlayerCardController.Players[1].MainScore)//compare winner score
            {
                if (PlayerCardController.Players[0].MainScore >= FlushScore && PlayerCardController.Players[1].MainScore >= FlushScore)
                {
                    if (PlayerCardController.Players[OtherPlayer].ScoreCards.Count == 7)
                    {
                        for (int kicker = 0; kicker < PlayerCardController.Players[i].BestCards.Count; kicker++)
                        {
                            if (PlayerCardController.Players[i] != PlayerCardController.Players[OtherPlayer])
                            {
                                var temp = PlayerCardController.Players[i].BestCards[kicker].Substring(0, 1);
                                var temp1 = PlayerCardController.Players[OtherPlayer].BestCards[kicker].Substring(0, 1);
                                if (temp == "V")
                                {
                                    temp = "10";
                                }
                                else if (temp == "W")
                                {
                                    temp = "11";
                                }
                                else if (temp == "X")
                                {
                                    temp = "12";
                                }
                                else if (temp == "Y")
                                {
                                    temp = "13";
                                }
                                else if (temp == "Z")
                                {
                                    temp = "14";
                                }

                                if (temp1 == "V")
                                {
                                    temp1 = "10";
                                }
                                else if (temp1 == "W")
                                {
                                    temp1 = "11";
                                }
                                else if (temp1 == "X")
                                {
                                    temp1 = "12";
                                }
                                else if (temp1 == "Y")
                                {
                                    temp1 = "13";
                                }
                                else if (temp == "Z")
                                {
                                    temp1 = "14";
                                }

                                if (Convert.ToInt32(temp) == Convert.ToInt32(temp1))
                                {
                                    PlayerCardController.Players[i].MainScore = RankScore + Convert.ToInt32(temp);
                                    PlayerCardController.Players[OtherPlayer].MainScore = RankScore + Convert.ToInt32(temp);
                                }
                                else
                                {
                                    PlayerCardController.Players[i].MainScore = RankScore + Convert.ToInt32(temp);
                                    PlayerCardController.Players[OtherPlayer].MainScore = RankScore + Convert.ToInt32(temp1);
                                    kicker = 5;
                                }
                            }
                        }
                    }
                }
            }
        }
    }

    void SetCardValue()
    {

        for (int i = 0; i < PlayerCardController.Players.Count; i++)
        {

            PlayerCardController.Players[i].ScoreCards.Clear();
            for (int crds = 0; crds < PlayerCardController.Players[i].Cards.Count; crds++)
            {
                var tempPlayerCards = PlayerCardController.Players[i].Cards[crds].Substring(0, 1);
                int tempPlayerScores;


                if (int.TryParse(tempPlayerCards, out tempPlayerScores))
                {
                    PlayerCardController.Players[i].ScoreCards.Add(tempPlayerScores);
                }
                else
                {
                    if (tempPlayerCards.Contains("V"))
                    {
                        PlayerCardController.Players[i].ScoreCards.Add(10);
                    }
                    else if (tempPlayerCards.Contains("W"))
                    {
                        PlayerCardController.Players[i].ScoreCards.Add(11);
                    }
                    else if (tempPlayerCards.Contains("X"))
                    {
                        PlayerCardController.Players[i].ScoreCards.Add(12);
                    }
                    else if (tempPlayerCards.Contains("Y"))
                    {
                        PlayerCardController.Players[i].ScoreCards.Add(13);
                    }
                    else if (tempPlayerCards.Contains("Z"))
                    {
                        PlayerCardController.Players[i].ScoreCards.Add(14);
                    }
                }
            }
        }
    }

    
    void CheckHighCard()
    {
        for (int i = 0; i < PlayerCardController.Players.Count; i++)
        {

            //Debug.Log("Player:" + i + " High Card");
            isHighCard = true;
            PlayerCardController.Players[i].Rank = "High Card";
            PlayerCardController.Players[i].ScoreCards.Sort();

            PlayerCardController.Players[i].MainScore = HighCardScore + (PlayerCardController.Players[i].ScoreCards[PlayerCardController.Players[i].ScoreCards.Count - 1] * 10);


        }
    }

    int tempFourOfAKind = 0;
    int tempThreeOfAKind = 0;
    int tempOnePair = 0;
    bool isHighCard = false, isOnePair = false, isThreeOfAKind = false, isTwoPair = false, isFourOfAKind = false, isFullHouse = false;
    void CheckPair(int GetPlayer)
    {
         tempFourOfAKind = 0;
         tempThreeOfAKind = 0;
         tempOnePair = 0;
        //PlayerCardController.Players[GetPlayer].MainScore = 0;
        isOnePair = false; isThreeOfAKind = false; isTwoPair = false; isFourOfAKind = false; isFullHouse = false;


        var dict = new Dictionary<int, int>();

        foreach (var value in PlayerCardController.Players[GetPlayer].ScoreCards)
        {
            if (dict.ContainsKey(value))
            {
                dict[value]++;
                if (dict[value] == 2)//number of fair
                {
                    //Debug.Log("One Pair");
                    isOnePair = true;

                }
                else if (dict[value] == 3)
                {
                    //Debug.Log("Three of a kind");
                    isThreeOfAKind = true;
                    isOnePair = false;
                }
                else if (dict[value] == 4)
                {
                    //Debug.Log("Four of a kind");
                    isFourOfAKind = true;
                    isOnePair = false;
                    isThreeOfAKind = false;
                }
            }
            else
            {
                dict[value] = 1;
            }
        }

        foreach (var pair in dict)
        {
            if (PlayerCardController.Players[GetPlayer].StraightCount < 5)
            {
                if (pair.Value >= 2)
                {
                    if (pair.Value == 3)
                    {
                        tempThreeOfAKind = pair.Key;
                    }
                    if (pair.Value == 4)
                    {
                        tempFourOfAKind = pair.Key;
                    }

                    var tempPairValue = pair.Key;
                    var lastIndex = 0;
                    PlayerCardController.Players[GetPlayer].PairValue.Add(tempPairValue);
                    

                    if (GetPlayer == 0)
                    {
                        SetCardValue();
                    }

                    for (int crds = 0; crds < PlayerCardController.Players[GetPlayer].Cards.Count; crds++)
                    {

                        if (PlayerCardController.Players[GetPlayer].Cards[crds].Substring(0, 1) == tempPairValue.ToString())
                        {
                            for (int i = 0; i < pair.Value; i++)
                            {
                                var tempPair = PlayerCardController.Players[GetPlayer].Cards[crds];
                                PlayerCardController.Players[GetPlayer].Cards.RemoveAt(crds);
                                PlayerCardController.Players[GetPlayer].Cards.Add(tempPair);
                            }
                        }
                    }
                    if (GetPlayer == 1)
                    {
                        SetCardValue();
                    }

                    if (PlayerCardController.Players[GetPlayer].PairValue.Count != 0)
                    {
                        lastIndex = PlayerCardController.Players[GetPlayer].PairValue.Count - 1;
                    }
                    if (isFourOfAKind == true)
                    {
                        Debug.Log("Player:" + GetPlayer + " Four of a Kind");
                        PlayerCardController.Players[GetPlayer].Rank = "Four of a Kind";

                        PlayerCardController.Players[GetPlayer].Cards.Sort();
                        PlayerCardController.Players[GetPlayer].ScoreCards.Sort();
                        var tempCompute4Kind = tempFourOfAKind * 10;

                        int tempKicker = 0;
                        for (int i = 0; i < PlayerCardController.Players[GetPlayer].ScoreCards.Count; i++)
                        {
                            if (PlayerCardController.Players[GetPlayer].ScoreCards[i] != tempFourOfAKind)
                            {
                                tempKicker = PlayerCardController.Players[GetPlayer].ScoreCards[i];

                            }
                        }
                        PlayerCardController.Players[GetPlayer].MainScore = FourOfAKind + tempCompute4Kind + tempKicker;
                    }

                    else if (PlayerCardController.Players[GetPlayer].PairValue.Count >= 2 && PlayerCardController.Players[GetPlayer].PairValue.Contains(tempThreeOfAKind))
                    {
                        if (PlayerCardController.Players[GetPlayer].PairValue.Count > 2)
                        {
                            PlayerCardController.Players[GetPlayer].PairValue.RemoveAt(0);
                        }

                        for (int i = 0; i < PlayerCardController.Players[GetPlayer].PairValue.Count; i++)
                        {
                            if (PlayerCardController.Players[GetPlayer].PairValue[i] != tempThreeOfAKind)
                            {
                                isFullHouse = true;
                                tempOnePair = PlayerCardController.Players[GetPlayer].PairValue[i];

                            }
                        }

                        Debug.Log("Player:" + GetPlayer + " Full House:");
                        PlayerCardController.Players[GetPlayer].Rank = "Full House";
                        var tempCompute3Kind = tempThreeOfAKind * 10;
                        var tempCompute1Pair = tempOnePair;
                        var tempTotalHouse = tempCompute1Pair + tempCompute3Kind + FullHouseScore;
                        PlayerCardController.Players[GetPlayer].MainScore = tempTotalHouse;
                    }
                    else if (isThreeOfAKind == true)
                    {
                        Debug.Log("Player:" + GetPlayer + "Three of a Kind");
                        PlayerCardController.Players[GetPlayer].Rank = "Three of a Kind";
                        PlayerCardController.Players[GetPlayer].MainScore = ThreeOfAKindScore + (PlayerCardController.Players[GetPlayer].PairValue[lastIndex] * 10);
                    }
                    else if (PlayerCardController.Players[GetPlayer].PairValue.Count >= 2)
                    {
                        isTwoPair = true;
                        Debug.Log("Player:" + GetPlayer + "Two Pairs");
                        PlayerCardController.Players[GetPlayer].Rank = "Two Pairs";

                        if (PlayerCardController.Players[GetPlayer].PairValue.Count > 2)
                        {
                            PlayerCardController.Players[GetPlayer].PairValue.RemoveAt(0);
                        }
                        var TempLastIndex = PlayerCardController.Players[GetPlayer].PairValue.Count - 1;
                         PlayerCardController.Players[GetPlayer].MainScore = TwoPairsScore + (PlayerCardController.Players[GetPlayer].PairValue[TempLastIndex] * 10);
                    }
                    else if (isOnePair == true)
                    {
                        Debug.Log("Player:" + GetPlayer + "One Pair");
                        PlayerCardController.Players[GetPlayer].Rank = "One Pair";
                        PlayerCardController.Players[GetPlayer].MainScore = OnePairScore + (PlayerCardController.Players[GetPlayer].PairValue[lastIndex] * 10);
                        //Debug.Log("pl" + GetPlayer + " " + PlayerCardController.Players[GetPlayer].MainScore);
                    }
                }
            }
        }


        if (GetPlayer == 1)//last player.
        {
            if (isFullHouse == true)
            {
                //Kicker(FullHouseScore);
            }
            else if (isThreeOfAKind == true)
            {
                Kicker(ThreeOfAKindScore);
            }
            else if (isTwoPair == true)
            {
                KickerTwoPair(TwoPairsScore);
            }
            else if (isOnePair == true)
            {
                Kicker(OnePairScore);
            }
            else if(isHighCard == true)
            {
                Kicker(HighCardScore);
            }
        }
    }

    void KickerTwoPair(int RankScore)
    {
        if(PlayerCardController.Players[0].MainScore == PlayerCardController.Players[1].MainScore)
        {
            var tempPlayer1Pair1 = 0;
            var tempPlayer2Pair1 = 0;

            tempPlayer1Pair1 = PlayerCardController.Players[0].PairValue[1];
            tempPlayer2Pair1 = PlayerCardController.Players[1].PairValue[1];

            var tempPlayer1Pair2 = 0;
            var tempPlayer2Pair2 = 0;
            tempPlayer1Pair2 = PlayerCardController.Players[0].PairValue[0];
            tempPlayer2Pair2 = PlayerCardController.Players[1].PairValue[0];

            for (int i = 0; i < PlayerCardController.Players.Count; i++)
            {
                if (tempPlayer1Pair1 == tempPlayer2Pair1)
                {
                    if (tempPlayer1Pair2 == tempPlayer2Pair2)
                    {
                        for (int sc = 0; sc < PlayerCardController.Players[i].ScoreCards.Count; sc++)
                        {
                            if (PlayerCardController.Players[i].ScoreCards[sc] != PlayerCardController.Players[i].PairValue[0] )
                            {
                                if (PlayerCardController.Players[i].ScoreCards[sc] != PlayerCardController.Players[i].PairValue[1])
                                {
                                    PlayerCardController.Players[i].ScoreCards.Sort();
                                    PlayerCardController.Players[i].MainScore = TwoPairsScore + (PlayerCardController.Players[i].ScoreCards[sc] * 10);
                                }
                            }
                        }
                    }
                    else
                    {
                        PlayerCardController.Players[i].MainScore = TwoPairsScore + (PlayerCardController.Players[i].PairValue[0] * 10);
                    }

                }
                else
                {
                    PlayerCardController.Players[i].MainScore = TwoPairsScore + (PlayerCardController.Players[i].PairValue[1] * 10);
                }
            }
        }
    }

    string FlushSuit = "";
    void CheckFlush()
    {
        FlushSuit = "";
        ClearSuit();
        if (isFourOfAKind == false)
        {
            for (int i = 0; i < PlayerCardController.Players.Count; i++)
            {
                PlayerCardController.Players[i].BestCards.Clear();
                for (int crds = 0; crds < PlayerCardController.Players[i].Cards.Count; crds++)
                {
                    for (int Scards = 0; Scards < SuitCards.Length; Scards++)
                    {

                        if (PlayerCardController.Players[i].Cards[crds].Substring(1, 1) == SuitCards[Scards])
                        {
                            PlayerCardController.Players[i].Suits[Scards]++;
                            if (PlayerCardController.Players[i].Suits[Scards] >= 5)
                            {
                                FlushSuit = PlayerCardController.Players[i].Cards[crds].Substring(1, 1);//PAIR SUIT
                            }
                        }
                    }
                }

                for (int sts = 0; sts < PlayerCardController.Players[i].Suits.Length; sts++)
                {
                    if (PlayerCardController.Players[i].Suits[sts] >= 5)
                    {
                        for (int crds = 0; crds < PlayerCardController.Players[i].Cards.Count; crds++)
                        {
                            if (PlayerCardController.Players[i].Cards[crds].Substring(1, 1) != FlushSuit)
                            {
                                var tempCards = PlayerCardController.Players[i].Cards[crds];
                                PlayerCardController.Players[i].Cards.RemoveAt(crds);
                                PlayerCardController.Players[i].Cards.Insert(6, tempCards);
                                SetCardValue();
                            }
                        }
                        for (int crds = 0; crds < PlayerCardController.Players[i].Cards.Count; crds++)
                        {
                            if(PlayerCardController.Players[i].BestCards.Count < 5)
                            {
                                if (FlushSuit == PlayerCardController.Players[i].Cards[crds].Substring(1, 1))
                                {
                                    PlayerCardController.Players[i].BestCards.Add(PlayerCardController.Players[i].Cards[crds]);
                                }
                            }
                        }

                        for (int crds = 0; crds < PlayerCardController.Players[i].Cards.Count; crds++)
                        {
                            if (PlayerCardController.Players[i].Cards[crds] == PlayerCardController.Players[i].BestCards[0])
                            {
                                if (PlayerCardController.Players[i].Suits[sts] >= 5)
                                {
                                    Debug.Log("Player:" + i + "Flush");
                                    PlayerCardController.Players[i].Rank = "Flush";
                                    PlayerCardController.Players[i].MainScore = FlushScore + (PlayerCardController.Players[i].ScoreCards[crds] * 10);
                                }
                            }
                        }

                        //Debug.Log(i + ": " + PlayerCardController.Players[i].MainScore);

                    }
                }
                if (i == 1)//last player;
                {
                    KickerIncrement(FlushScore);
                }
            }
        }
    }

    bool isStraight = false;
    void CheckStraight()
    {
        isStraight = false;


        if (isFullHouse == false || isFourOfAKind == false)
        {
            var tempDifference = 0;//indexDifference;
            var tempIndexScore = 0;
            ClearSuit();

            for (int i = 0; i < PlayerCardController.Players.Count; i++)
            {
                PlayerCardController.Players[i].countLowestStraight = 0;
                PlayerCardController.Players[i].BestCards.Clear();
                PlayerCardController.Players[i].StraightCount = 0;
                for (int scrs = 0; scrs < PlayerCardController.Players[i].ScoreCards.Count; scrs++)
                {
                    //Straight
                    PlayerCardController.Players[i].Cards = PlayerCardController.Players[i].Cards.OrderByDescending(x => x).Select(p => p).ToList();
                    PlayerCardController.Players[i].ScoreCards = PlayerCardController.Players[i].ScoreCards.OrderByDescending(x => x).Select(p => p).ToList();
                    if (PlayerCardController.Players[i].StraightCount < 5)
                    {
                        if (scrs != PlayerCardController.Players[i].ScoreCards.Count - 1)
                        {
                            tempIndexScore = scrs;
                        }
                        var tempIndexScore1 = tempIndexScore;
                        if (tempIndexScore < PlayerCardController.Players[i].ScoreCards.Count - 1)
                        {
                            tempIndexScore1++;
                        }
                        tempDifference = PlayerCardController.Players[i].ScoreCards[tempIndexScore] - PlayerCardController.Players[i].ScoreCards[tempIndexScore1];
                        //straight
                        if (tempDifference == 0)
                        {
                            tempIndexScore++;
                            //Debug.Log(tempIndexScore + " : " + tempIndexScore1 + "SP");
                        }
                        if (tempDifference == 1)
                        {
                            if (PlayerCardController.Players[i].StraightCount == 3)
                            {

                                if (!PlayerCardController.Players[i].BestCards.Contains(PlayerCardController.Players[i].Cards[tempIndexScore]) && PlayerCardController.Players[i].BestCards.Count < 4)
                                {
                                    PlayerCardController.Players[i].StraightCount += 2;
                                    PlayerCardController.Players[i].BestCards.Add(PlayerCardController.Players[i].Cards[tempIndexScore]);
                                    PlayerCardController.Players[i].BestCards.Add(PlayerCardController.Players[i].Cards[tempIndexScore1]);
                                    GetSuitPair();
                                }
                            }
                            else
                            {
                                if (!PlayerCardController.Players[i].BestCards.Contains(PlayerCardController.Players[i].Cards[tempIndexScore]) && PlayerCardController.Players[i].BestCards.Count < 5)
                                {
                                    PlayerCardController.Players[i].StraightCount++;
                                    PlayerCardController.Players[i].BestCards.Add(PlayerCardController.Players[i].Cards[tempIndexScore]);
                                }
                            }
                            //Debug.Log("StraightCount: " + PlayerCardController.Players[i].StraightCount);
                        }
                        else
                        {
                            if (tempDifference != 0)
                            {
                                //Debug.Log(i + "CLEAR");
                                PlayerCardController.Players[i].StraightCount = 0;
                                PlayerCardController.Players[i].BestCards.Clear();
                            }
                        }
                    }

                    if (PlayerCardController.Players[i].StraightCount >= 5)
                    {
                        //Debug.Log("Straight");

                        for (int crds = 0; crds < PlayerCardController.Players[i].Cards.Count; crds++)
                        {
                            if (PlayerCardController.Players[i].Cards[crds] == PlayerCardController.Players[i].BestCards[0])
                            {
                                for (int suits = 0; suits < PlayerCardController.Players[i].Suits.Length; suits++)
                                {
                                    //Debug.Log("Player:" + i + " Straight");
                                    isStraight = true;
                                    PlayerCardController.Players[i].Rank = "Straight";
                                    PlayerCardController.Players[i].MainScore = StraightScore + (PlayerCardController.Players[i].ScoreCards[crds] * 10);
                                }
                            }
                        }
                    }
                }

                if (isStraight == false)
                {
                    SetStraight();
                    for (int crds = 0; crds < PlayerCardController.Players[i].Cards.Count; crds++)
                    {
                        if (!PlayerCardController.Players[i].Cards[crds].Substring(0, 1).Contains("6"))
                        {
                            //PlayerCardController.Players[i].countLowestStraight = 0;
                            if(PlayerCardController.Players[i].TempRoyalFlush == "Z5432")
                            {
                                PlayerCardController.Players[i].Rank = "Straight";
                                PlayerCardController.Players[i].MainScore = StraightScore;
                            }
                        }
                    }
                }
            }
        }
    }

    string[] LowestStraight = new string[] { "Z", "2", "3", "4", "5" };
    void SetStraight()
    {
        GetSuitPair();
        //SetRoyalFlush();

        for (int i = 0; i < PlayerCardController.Players.Count; i++)
        {
            if (PlayerCardController.Players[i].TempRoyalFlush != "")
            {
                PlayerCardController.Players[i].TempRoyalFlush = "";
            }

            for (int crds = 0; crds < PlayerCardController.Players[i].Cards.Count; crds++)
            {
                if (!PlayerCardController.Players[i].TempRoyalFlush.Contains(PlayerCardController.Players[i].Cards[crds].Substring(0, 1)))
                {
                    if (LowestStraight.Contains(PlayerCardController.Players[i].Cards[crds].Substring(0, 1)))
                    {
                        PlayerCardController.Players[i].TempRoyalFlush += PlayerCardController.Players[i].Cards[crds].Substring(0, 1);
                    }
                }
            }
        }
    }

    void CheckStraightFlush()
    {
        var tempDifference = 0;//indexDifference;
        var tempIndexScore = 0;
        ClearSuit();

        for (int i = 0; i < PlayerCardController.Players.Count; i++)
        {
            PlayerCardController.Players[i].countLowestStraight = 0;
            PlayerCardController.Players[i].BestCards.Clear();
            PlayerCardController.Players[i].StraightCount = 0;

            for (int scrs = 0; scrs < PlayerCardController.Players[i].ScoreCards.Count; scrs++)
            {

                //Straight
                PlayerCardController.Players[i].Cards = PlayerCardController.Players[i].Cards.OrderByDescending(x => x).Select(p => p).ToList();
                PlayerCardController.Players[i].ScoreCards = PlayerCardController.Players[i].ScoreCards.OrderByDescending(x => x).Select(p => p).ToList();
                if (PlayerCardController.Players[i].StraightCount < 5)
                {
                    if (scrs != PlayerCardController.Players[i].ScoreCards.Count - 1)
                    {
                        tempIndexScore = scrs;
                    }
                    var tempIndexScore1 = tempIndexScore;
                    if (tempIndexScore < PlayerCardController.Players[i].ScoreCards.Count - 1)
                    {
                        tempIndexScore1++;
                    }

                    //Debug.Log(tempIndexScore + " : " + tempIndexScore1);
                    tempDifference = PlayerCardController.Players[i].ScoreCards[tempIndexScore] - PlayerCardController.Players[i].ScoreCards[tempIndexScore1];
                    if (tempDifference == 0)
                    {
                        if (FlushSuit == PlayerCardController.Players[i].Cards[tempIndexScore].Substring(1, 1))
                        {
                            PlayerCardController.Players[i].BestCards.Add(PlayerCardController.Players[i].Cards[tempIndexScore]);
                            PlayerCardController.Players[i].StraightCount++;
                            //Debug.Log(i + "  " + PlayerCardController.Players[i].Cards[tempIndexScore]);
                        }
                        tempIndexScore++;

                        //Debug.Log(tempIndexScore + " : " + tempIndexScore1 + "SP");
                    }
                    //diff
                    if (tempDifference == 1)
                    {
                        if (PlayerCardController.Players[i].StraightCount == 3)
                        {

                            if (!PlayerCardController.Players[i].BestCards.Contains(PlayerCardController.Players[i].Cards[tempIndexScore]) && PlayerCardController.Players[i].BestCards.Count < 4)
                            {

                                if (FlushSuit == PlayerCardController.Players[i].Cards[tempIndexScore].Substring(1, 1))
                                {
                                    PlayerCardController.Players[i].BestCards.Add(PlayerCardController.Players[i].Cards[tempIndexScore]);
                                    PlayerCardController.Players[i].StraightCount++;
                                    //Debug.Log(i + "  " + PlayerCardController.Players[i].Cards[tempIndexScore]);
                                }
                                if (FlushSuit == PlayerCardController.Players[i].Cards[tempIndexScore1].Substring(1, 1))
                                {
                                    PlayerCardController.Players[i].BestCards.Add(PlayerCardController.Players[i].Cards[tempIndexScore1]);
                                    PlayerCardController.Players[i].StraightCount++;
                                    //Debug.Log(i + "  " + PlayerCardController.Players[i].Cards[tempIndexScore1]);
                                }
                            }
                        }
                        else
                        {
                            if (!PlayerCardController.Players[i].BestCards.Contains(PlayerCardController.Players[i].Cards[tempIndexScore]) && PlayerCardController.Players[i].BestCards.Count < 5)
                            {
                                if (FlushSuit == PlayerCardController.Players[i].Cards[tempIndexScore].Substring(1, 1))
                                {
                                    PlayerCardController.Players[i].BestCards.Add(PlayerCardController.Players[i].Cards[tempIndexScore]);
                                    //Debug.Log(i + "  " + PlayerCardController.Players[i].Cards[tempIndexScore]);
                                    PlayerCardController.Players[i].StraightCount++;
                                    //if (i == 1)
                                    //{
                                    //    Debug.Log(PlayerCardController.Players[i].Cards[tempIndexScore]);
                                    //}
                                }
                            }
                        }
                    }
                    else
                    {
                        if (tempDifference != 0)
                        {
                            //Debug.Log(i + "  " + PlayerCardController.Players[i].Cards[tempIndexScore]);
                            PlayerCardController.Players[i].StraightCount = 0;
                            PlayerCardController.Players[i].BestCards.Clear();

                        }
                    }
                }
                //GetSuitPairStraightFlush();
                GetSuitPair();
                if (PlayerCardController.Players[i].StraightCount >= 5)
                {

                    for (int crds = 0; crds < PlayerCardController.Players[i].Cards.Count; crds++)
                    {
                        if (PlayerCardController.Players[i].Cards[crds] == PlayerCardController.Players[i].BestCards[0])
                        {
                            for (int suits = 0; suits < PlayerCardController.Players[i].Suits.Length; suits++)
                            {
                                if (PlayerCardController.Players[i].Suits[suits] >= 5 && PlayerCardController.Players[i].BestCards.Count == 5)
                                {
                                    Debug.Log("Player:" + i + " Straight Flush");
                                    PlayerCardController.Players[i].Rank = "Straight Flush";
                                    PlayerCardController.Players[i].MainScore = StraightFlushScore + (PlayerCardController.Players[i].ScoreCards[crds] * 10);
                                }
                            }
                        }
                    }
                }
            }

            //PlayerCardController.Players[i].BestCards.Clear();
            if(PlayerCardController.Players[i].MainScore <= StraightFlushScore)
            {
                for (int crds = 0; crds < PlayerCardController.Players[i].Cards.Count; crds++)
                {
                    for (int lw = 0; lw < LowestStraight.Length; lw++)
                    {
                        if (PlayerCardController.Players[i].Cards[crds].Contains("6" + FlushSuit))
                        {
                            PlayerCardController.Players[i].countLowestStraight = 0;
                        }

                        if (PlayerCardController.Players[i].Cards[crds].Substring(0, 1).Contains(LowestStraight[lw]))
                        {
                            PlayerCardController.Players[i].BestCards.Add(PlayerCardController.Players[i].Cards[crds]);
                            PlayerCardController.Players[i].countLowestStraight++;
                            GetSuitPair();
                            if (PlayerCardController.Players[i].Suits.Contains(5))
                            {
                                if (PlayerCardController.Players[i].countLowestStraight >= 5)
                                {
                                    PlayerCardController.Players[i].Rank = "Straight Flush";
                                    PlayerCardController.Players[i].MainScore = StraightFlushScore;
                                }
                            }
                        }
                    }
                }
            }
        }
    }

    void CheckRoyalFlush()
    {
        SetRoyalFlush();
        GetSuitPair();

        for (int i = 0; i < PlayerCardController.Players.Count; i++)
        {
            //PlayerCardController.Players[i].StraightCount = 0;
            // 
            if (PlayerCardController.Players[i].TempRoyalFlush == TempRoyalFlush)
            {
                Debug.Log("Player:" + i + "Royal Flush");
                PlayerCardController.Players[i].Rank = "Royal Flush";
                PlayerCardController.Players[i].MainScore = RoyalFlushScore;
            }
        }
    }

    string TempRoyalFlush = "ZYXWV";
    string[] StringRoyalFlush = new string[] { "Z", "Y", "X", "W", "V" };
    void SetRoyalFlush()
    {
        for (int i = 0; i < PlayerCardController.Players.Count; i++)
        {
            if(PlayerCardController.Players[i].TempRoyalFlush != "")
            {
                PlayerCardController.Players[i].TempRoyalFlush = "";
            }

            for (int crds = 0; crds < PlayerCardController.Players[i].Cards.Count; crds++)
            {

                if (PlayerCardController.Players[i].Cards[crds].Substring(1,1) == FlushSuit)
                {
                    if (StringRoyalFlush.Contains(PlayerCardController.Players[i].Cards[crds].Substring(0, 1)))
                    {
                        if(!PlayerCardController.Players[i].TempRoyalFlush.Contains(PlayerCardController.Players[i].Cards[crds].Substring(0, 1)))
                        {
                            PlayerCardController.Players[i].TempRoyalFlush += PlayerCardController.Players[i].Cards[crds].Substring(0, 1);
                        }

                        if(!PlayerCardController.Players[i].BestCards.Contains(PlayerCardController.Players[i].Cards[crds]))
                        {
                            PlayerCardController.Players[i].BestCards.Add(PlayerCardController.Players[i].Cards[crds]);
                        }
                    }
                }
            }
        }
    }

    void GetSuitPair()
    {
        ClearSuit();
        for (int i = 0; i < PlayerCardController.Players.Count; i++)
        {
            for (int Scards = 0; Scards < SuitCards.Length; Scards++)
            {
                for (int bstcrd = 0; bstcrd < PlayerCardController.Players[i].BestCards.Count; bstcrd++)
                {

                    if (PlayerCardController.Players[i].BestCards[bstcrd].Substring(1, 1) == SuitCards[Scards])
                    {
                        //Debug.Log(PlayerCardController.Players[i].BestCards[bstcrd]);
                        PlayerCardController.Players[i].Suits[Scards]++;
                    }
                }
            }
        }
    }


    void SetSpecialCards()
    {
        for (int i = 0; i < PlayerCardController.Players.Count; i++)
        {

            for (int crds = 0; crds < PlayerCardController.Players[i].Cards.Count; crds++)
            {
                var tempPlayerCards = PlayerCardController.Players[i].Cards[crds].Substring(0, 1);

                if (tempPlayerCards == "V")
                {
                    PlayerCardController.Players[i].ScoreCards[crds] = 10;
                }
                else if (tempPlayerCards == "W")
                {
                    PlayerCardController.Players[i].ScoreCards[crds] = 11;
                }
                else if (tempPlayerCards == "X")
                {
                    PlayerCardController.Players[i].ScoreCards[crds] = 22;
                }
                else if (tempPlayerCards == "Y")
                {
                    PlayerCardController.Players[i].ScoreCards[crds] = 13;
                }
                else if (tempPlayerCards == "Z")
                {
                    PlayerCardController.Players[i].ScoreCards[crds] = 14;
                }

            }
        }
    }

    void ClearSuit()
    {
        for (int i = 0; i < PlayerCardController.Players.Count; i++)
        {
            for (int Scards = 0; Scards < SuitCards.Length; Scards++)
            {
                PlayerCardController.Players[i].Suits[Scards] = 0;
            }
        }
    }

    void SetRankText()
    {
        for (int i = 0; i < RankText.Length; i++)
        {
            for (int pl = 0; pl < PlayerCardController.Players.Count; pl++)
            {
                if(i == pl)
                {
                    RankText[i].text = PlayerCardController.Players[pl].Rank;
                }
            }
        }
        
    }

    void CheckWinner()
    {
        DisableWinner();
        
        if (PlayerCardController.Players[0].MainScore < PlayerCardController.Players[1].MainScore)
        {
            PlayerWinner[1].SetActive(true);
        }
        else if (PlayerCardController.Players[0].MainScore == PlayerCardController.Players[1].MainScore)
        {
            PlayerWinner[2].SetActive(true);
        }
        else
        {
            PlayerWinner[0].SetActive(true);
        }
    }

    void DisableWinner()
    {
        for (int i = 0; i < PlayerWinner.Count; i++)
        {
            PlayerWinner[i].SetActive(false);
        }
    }
}
