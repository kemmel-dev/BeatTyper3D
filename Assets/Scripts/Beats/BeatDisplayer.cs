using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[ExecuteAlways]
public class BeatDisplayer : MonoBehaviour
{
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, new Vector3(1f, 1f, 1f));
    }
}
