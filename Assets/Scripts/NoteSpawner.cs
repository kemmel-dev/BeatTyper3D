using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class NoteSpawner : MonoBehaviour
{

    public GameObject notePrefab;
    public Transform parentObject;

    private AutoTimer timer;
    private KeyboardManager keyboardManager;

    public float spawnHeight;
    public float spawnTime;

    // Start is called before the first frame update
    void Start()
    {
        timer = new AutoTimer(spawnTime);
        keyboardManager = GameObject.FindGameObjectWithTag("KeyboardManager").GetComponent<KeyboardManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (timer.IsReached(Time.time))
        {
            int nextIndex = Random.Range(0, 26);
            KeyTile keyTile = keyboardManager.GetKeyTile(nextIndex);
            Vector3 keyTilePosition = keyTile.transform.position;
            keyTilePosition.y += spawnHeight;
            Note note = Instantiate(notePrefab, keyTilePosition, Quaternion.identity, parentObject).GetComponent<Note>();
            note.Start();
            note.noteLine.Start();
            note.noteLine.SetColor(keyTile.Color);
            note.noteLine.SetWidth(2);

            timer = new AutoTimer(Time.time + Random.Range(1f, 2.5f));
        }
    }
}
