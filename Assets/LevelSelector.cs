using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelector : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SwitchToLevelEasy()
    {
        SceneManager.LoadScene("LevelEasy");
    }

    public void SwitchToLevelMedium()
    {
        SceneManager.LoadScene("LevelMedium");
    }

    public void SwitchToLevelHard()
    {
        SceneManager.LoadScene("LevelHard");
    }
}
