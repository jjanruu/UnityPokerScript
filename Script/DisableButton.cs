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
        Invoke("EnableButton", .2f);
    }

    public void DisableButtonClick()
    {
        
        //CheckEnable();
    }

    void EnableButton()
    {
        gameObject.GetComponent<Button>().enabled = true;
    }
}
