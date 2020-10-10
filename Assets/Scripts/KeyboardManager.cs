using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardManager : MonoBehaviour
{

    public static char[] keys = {'Q', 'W', 'E', 'R', 'T', 'Y', 'U', 'I', 'O', 'P',
                           'A', 'S', 'D', 'F', 'G', 'H', 'J', 'K', 'L',
                           'Z', 'X', 'C', 'V', 'B', 'N', 'M'};

    public static KeyboardManager instance;

    public static Dictionary<char, KeyTile> keyTiles = new Dictionary<char, KeyTile>();

    public static void AddKeyTile(char key, KeyTile keyTile)
    {
        keyTiles.Add(key, keyTile);
    }
}
