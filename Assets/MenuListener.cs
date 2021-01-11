using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuListener : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            KeyTile.firstBeat = true;
            BeatManager.Reset();
            BeatRecorder.beat = -1;
            BeatSpawner.beatmarkerIndex = 0;
            FeedbackManager.Reset();
            SceneManager.LoadScene(0);
            return;
        }
    }
}
