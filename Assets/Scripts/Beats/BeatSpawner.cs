using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatSpawner : MonoBehaviour
{

    public GameObject beatPrefab;

    public float beatTempo;
    public static float beatDuration;


    // Start is called before the first frame update
    void Start()
    {
        beatDuration = beatTempo / 60f;
        SpawnBeats();
    }

    private void LateUpdate()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y - beatDuration * Time.deltaTime, transform.position.z);
    }

    public float GetBeatduration()
    {
        return beatDuration;
    }

    public void SpawnBeats()
    {

        float yPos = 16 + beatDuration /4;

        SpawnBeat('A', yPos++);
        SpawnBeat('S', yPos++);
        SpawnBeat('D', yPos++);
        SpawnBeat('F', yPos++);
        SpawnBeat('R', yPos++);




    }

    public void SpawnBeat(char key, float yPos)
    {
        KeyTile keyTile = KeyboardManager.keyTiles[key];
        Vector3 newPos = new Vector3(keyTile.transform.position.x, yPos, keyTile.transform.position.z);
        BeatManager.AddBeat(Instantiate(beatPrefab, newPos, Quaternion.identity, this.transform).GetComponent<Beat>());
    }

}
