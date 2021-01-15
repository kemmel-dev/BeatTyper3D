using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DeathCounter
{

    public static int numDeaths = 0;

    public static void AddDeath()
    {
        numDeaths++;
    }

    public static void ResetDeaths()
    {
        numDeaths = 0;
    }
}
