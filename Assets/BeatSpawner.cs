﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatSpawner : MonoBehaviour
{

    public GameObject beatPrefab;

    public float beatTempo;
    public static float beatDuration;

    private char[] keys = KeyboardManager.keys;

    // Start is called before the first frame update
    void Start()
    {
        beatDuration = beatTempo / 60f;
        SpawnBeats();
    }

    public float GetBeatduration()
    {
        return beatDuration;
    }

    public void SpawnBeats()
    {

        float yPos = 17 + beatDuration / 2;

        for (int i = 0; i < 4; i++)
        {
            SpawnBeat('A', yPos++);
            yPos++;
            SpawnBeat('S', yPos++);
            yPos++;
            SpawnBeat('D', yPos++);
            yPos++;
            SpawnBeat('F', yPos++);
            yPos++;
            SpawnBeat('R', yPos++);
            yPos++;
            SpawnBeat('E', yPos++);
            yPos++;
            SpawnBeat('W', yPos++);
            yPos++;
            SpawnBeat('Q', yPos++);
            yPos++;
        }

        for (int i = 0; i < 4; i++)
        {
            SpawnBeat('I', yPos++);
            yPos++;
            SpawnBeat('J', yPos++);
            yPos++;
            SpawnBeat('L', yPos++);
            yPos++;
            SpawnBeat('M', yPos++);
            yPos++;
            SpawnBeat('S', yPos++);
            yPos++;
            SpawnBeat('X', yPos++);
            yPos++;
            SpawnBeat('F', yPos++);
            yPos++;
            SpawnBeat('V', yPos++);
            yPos++;
        }

        for (int i = 0; i < 2; i++)
        {
            SpawnBeat('I', yPos++);
            yPos++;
            SpawnBeat('J', yPos++);
            yPos++;
            SpawnBeat('L', yPos++);
            yPos++;
            SpawnBeat('M', yPos++);
            yPos++;
        }

        for (int i = 0; i < 2; i++)
        {
            SpawnBeat('A', yPos++);
            yPos++;
            SpawnBeat('S', yPos++);
            yPos++;
            SpawnBeat('D', yPos++);
            yPos++;
            SpawnBeat('F', yPos++);
            yPos++;
            SpawnBeat('Q', yPos++);
            yPos++;
            SpawnBeat('W', yPos++);
            yPos++;
            SpawnBeat('E', yPos++);
            yPos++;
            SpawnBeat('R', yPos++);
            if (i != 1)
            {
                yPos++;
            }
        }

        Debug.Log(yPos - beatDuration / 2);


        for (int i = 0; i < 4; i++)
        {
            SpawnBeat('A', yPos++);
            SpawnBeat('S', yPos++);
            SpawnBeat('D', yPos++);
            SpawnBeat('F', yPos++);
            SpawnBeat('Q', yPos++);
            SpawnBeat('W', yPos++);
            SpawnBeat('E', yPos++);
            SpawnBeat('R', yPos++);
        }

        for (int i = 0; i < 4; i++)
        {
            SpawnBeat('Z', yPos++);
            SpawnBeat('X', yPos++);
            SpawnBeat('C', yPos++);
            SpawnBeat('V', yPos++);
            SpawnBeat('A', yPos++);
            SpawnBeat('S', yPos++);
            SpawnBeat('D', yPos++);
            SpawnBeat('F', yPos++);
        }

        for (int i = 0; i < 4; i++)
        {
            SpawnBeat('H', yPos++);
            SpawnBeat('J', yPos++);
            SpawnBeat('K', yPos++);
            SpawnBeat('L', yPos++);
            SpawnBeat('U', yPos++);
            SpawnBeat('I', yPos++);
            SpawnBeat('O', yPos++);
            SpawnBeat('P', yPos++);
        }

        for (int i = 0; i < 4; i++)
        {
            SpawnBeat('H', yPos++);
            SpawnBeat('J', yPos++);
            SpawnBeat('K', yPos++);
            SpawnBeat('L', yPos++);
            SpawnBeat('U', yPos++);
            SpawnBeat('I', yPos++);
            SpawnBeat('O', yPos++);
            SpawnBeat('P', yPos++);
        }

        for (int i = 0; i < 4; i++)
        {
            SpawnBeat('A', yPos++);
            SpawnBeat('S', yPos++);
            SpawnBeat('D', yPos++);
            SpawnBeat('F', yPos++);
            SpawnBeat('Q', yPos++);
            SpawnBeat('W', yPos++);
            SpawnBeat('E', yPos++);
            SpawnBeat('R', yPos++);
        }




    }

    public void SpawnBeat(char key, float yPos)
    {
        KeyTile keyTile = KeyboardManager.keyTiles[key];
        Vector3 newPos = new Vector3(keyTile.transform.position.x, yPos, keyTile.transform.position.z);
        Instantiate(beatPrefab, newPos, Quaternion.identity, this.transform);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y - beatDuration * Time.deltaTime, transform.position.z);
    }
}