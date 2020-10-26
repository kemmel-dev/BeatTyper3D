using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;

public class BeatSpawner : MonoBehaviour
{

    public GameObject beatPrefab;

    public int bpm;
    public static float beatsSpeedPerSecond;
    public static float beatHitDistance;
    public static float beatsPerSecond;

    public string level;

    public TempoTile tempoTile;

    public TextMeshPro tempoText;

    public List<float> beatMarkers = new List<float>();
    public List<string> tapTempo = new List<string>();
    public static int beatmarkerIndex = 0;

    private int oldBeat;

    // Start is called before the first frame update
    void Awake()
    {
        beatsSpeedPerSecond = bpm / 60f;
        beatsPerSecond = 60f / bpm;
        beatHitDistance = beatsSpeedPerSecond / 3;
        SpawnBeats(level);
        SpawnTempoBeats();
        oldBeat = BeatRecorder.beat;
    }

    private void SpawnTempoBeats()
    {
        foreach (float beatMarker in beatMarkers)
        {
            SpawnTempoBeat(beatMarker);
        }
    }

    private void Update()
    {
        int currentBeat = BeatRecorder.beat;
        if (oldBeat != currentBeat)
        {
            tempoTile.Tick();
            oldBeat++;
        }

        
        if (beatmarkerIndex < beatMarkers.Count)
        {
            if (currentBeat == (int)beatMarkers[beatmarkerIndex])
            {
                beatmarkerIndex++;
            }
        }


        if (beatmarkerIndex < tapTempo.Count)
        {
            tempoText.text = "Tempo:\n" + tapTempo[beatmarkerIndex];
            transform.position = new Vector3(transform.position.x, transform.position.y - beatsSpeedPerSecond * Time.deltaTime, transform.position.z);

        }
        else
        {
            tempoText.text = "Tempo: 0";
        }
    }

    public float GetBeatduration()
    {
        return beatsSpeedPerSecond;
    }

    public void SpawnBeats(string level)
    {
        string assetFolder = Application.dataPath;
        string savesFolder = Path.Combine(assetFolder, @"..\Assets\LevelRecorder\Input\");
        StreamReader streamReader = new StreamReader(savesFolder + level + ".txt");

        Regex regex = new Regex("[A-Z]/[0-9]+");

        string line = streamReader.ReadLine();
        while (line != null)
        {
            if (regex.IsMatch(line.Trim()))
            {
                string[] parts = line.Split('/');
                SpawnBeat(parts[0][0], float.Parse(parts[1]) - 1 + BeatSpawner.beatHitDistance);
            }
            line = streamReader.ReadLine();
        }
        streamReader.Close();
    }

    public void SpawnBeat(char key, float yPos)
    {
        KeyTile keyTile = KeyboardManager.keyTiles[key];
        Vector3 newPos = new Vector3(keyTile.transform.position.x, yPos, keyTile.transform.position.z);
        BeatManager.AddBeat(Instantiate(beatPrefab, newPos, Quaternion.identity, this.transform).GetComponent<Beat>());
    }

    public void SpawnTempoBeat(float yPos)
    {
        Vector3 newPos = new Vector3(tempoTile.transform.position.x, yPos, tempoTile.transform.position.z);
        Instantiate(beatPrefab, newPos, Quaternion.identity, this.transform).GetComponent<Beat>();
    }

}
