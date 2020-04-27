using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DisableButton : MonoBehaviour
{
    public GameObject[] winner;
   
    void Start()
    {
        gameObject.GetComponent<Button>().enabled = false;
    }

    public void DisableButtonClick()
    {
        gameObject.GetComponent<Button>().enabled = false;
        //CheckEnable();
    }

    void CheckEnable()
    {
        int countWinner = 0;
        for (int i = 0; i < winner.Length; i++)
        {
            if(winner[i].activeInHierarchy == false)
            {
                countWinner++;
                if(countWinner == 3)
                {
                    gameObject.GetComponent<Button>().enabled = true;
                    countWinner = 0;
                }
            }
        }
        
    }
}
