using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FailTextUpdater : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<TextMeshProUGUI>().text = "Sorry, but you missed more than " + FeedbackManager.maxMisses + " times. We are looking for REAL performers...\n\n...but you're always welcome to try again!\n\n< Press escape to return to the main menu >";
    }
}
