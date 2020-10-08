using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardManager : MonoBehaviour
{

    public Transform parentObject;
    public GameObject keyTilePrefab;

    private string[] keys = {"Q", "W", "E", "R", "T", "Y", "U", "I", "O", "P"
                             ,"A", "S", "D", "F", "G", "H", "J", "K", "L"
                             ,"Z", "X", "C", "V", "B", "N", "M"};

    private Color32[] keyColors = { new Color32(255, 255, 255, 255), new Color32(171, 26, 0, 255), new Color32(222, 152, 0, 255)};

    private KeyTile[] keyTiles = new KeyTile[26];

    public float offsetX;

    // Start is called before the first frame update
    void Start()
    {
        Vector3 newPos = transform.position;

        LayerMask keyTileLayer = LayerMask.GetMask("KeyTileLayer");

        int colorIndex = 0;
        Color32 color = keyColors[colorIndex];

        for (int i = 0; i < keys.Length; i++)
        {
            KeyTile keyTile = Instantiate(keyTilePrefab, newPos, Quaternion.Euler(90, 0, 0), parentObject).GetComponent<KeyTile>();
            keyTile.Initialise(keys[i].ToCharArray()[0], keyColors[colorIndex]);
            if (i == 9 || i == 18)
            {
                colorIndex++;
            }
            keyTiles[i] = keyTile;


            newPos.x += offsetX;

            if (i == 9)
            {
                newPos = transform.position;
                newPos.x += 2;
                newPos.z -= keyTile.transform.localScale.y * 3;
            }

            if (i == 18)
            {
                newPos = transform.position;
                newPos.x += 4;
                newPos.z -= keyTile.transform.localScale.y * 6;
            }
        }
    }

    public KeyTile GetKeyTile(int index)
    {
        return keyTiles[index];
    }

    // Update is called once per frame
    void Update()
    {

    }
}
