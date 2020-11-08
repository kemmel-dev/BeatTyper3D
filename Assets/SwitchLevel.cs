using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchLevel : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            SceneManager.LoadScene(2);
            return;
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
            return;
        }
        if (Input.GetMouseButton(0) || Input.GetMouseButton(1))
        {
            return;
        }
        if (Input.anyKey)
        {
            SceneManager.LoadScene("LevelSelect");
            return;
        }
    }
}
