using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BeatRecorder : MonoBehaviour
{

    public static int beat = 0;
    private List<KeyStroke> keyStrokes = new List<KeyStroke>();

    public AudioSource audioSource;
    public AudioClip audioClip;

    private AutoTimer beatTimer;

    float time;

    float startTime;

    public int endBeat;

    // Start is called before the first frame update
    void Start()
    {
        audioSource.Play();
        beatTimer = new AutoTimer(BeatSpawner.beatsPerSecond);
        time = 0;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (time > BeatSpawner.beatsPerSecond * beat)
        {
            beat++;
        }

        if (beat > endBeat)
        {
            FeedbackManager.SendScores();
            SceneManager.LoadScene(3);
        }
        if (Input.anyKeyDown)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                RecordToFile();
                this.enabled = false;
                return;
            }
            foreach (KeyCode key in Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKeyDown(key))
                {
                    Record(key);
                }
            }
        }
    }

    private void Record(KeyCode key)
    {
        keyStrokes.Add(new KeyStroke(key, beat));
    }

    private void RecordToFile()
    {
        string assetFolder = Application.dataPath;
        string savesFolder = Path.Combine(assetFolder, @"..\Assets\LevelRecorder\Output\");
        StreamWriter streamWriter = new StreamWriter(savesFolder + DateTime.Now.ToString().Replace('/', '-').Replace(':', '-') + ".txt");
        foreach(KeyStroke keyStroke in keyStrokes)
        {
            string line = keyStroke.Key.ToString() + '/' + keyStroke.Time.ToString();
            streamWriter.WriteLine(line);
        }
        streamWriter.Close();
    }

    private struct KeyStroke
    {
        public KeyStroke(KeyCode key, float time)
        {
            Key = key;
            Time = time;
        }

        public KeyCode Key { get; }
        public float Time { get; }
    }
}
