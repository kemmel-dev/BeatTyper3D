using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardSpawner : MonoBehaviour
{

    public GameObject keyTilePrefab;
    public Transform parentTransform;

    public float xOffset;
    public float offsetXRow1, offsetXRow2, offsetXRow3;
    public float offsetZRow1, offsetZRow2, offsetZRow3;


    // Start is called before the first frame update
    void Awake()
    {
        float xPos = parentTransform.position.x + offsetXRow1;
        float zPos = parentTransform.position.z + offsetZRow1;
        foreach (char key in KeyboardManager.keys)
        {
            KeyTile keyTile = Instantiate(keyTilePrefab, new Vector3(xPos, 0, zPos), Quaternion.Euler(90, 0, 0), parentTransform).GetComponent<KeyTile>();
            keyTile.Initialise(key);

            xPos += xOffset;

            if (key == 'P')
            {
                xPos = parentTransform.position.x + offsetXRow2;
                zPos = parentTransform.position.z + offsetZRow2;
            }
            if (key == 'L')
            {
                xPos = parentTransform.position.x + offsetXRow3;
                zPos = parentTransform.position.z + offsetZRow3;
            }
        }
    }
}
