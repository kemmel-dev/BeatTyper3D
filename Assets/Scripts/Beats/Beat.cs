using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beat : MonoBehaviour
{

    private BeatCircle attachedBeatCircle;

    public void AttachBeatCircle(BeatCircle beatCircle)
    {
        attachedBeatCircle = beatCircle;
    }

    public BeatCircle GetBeatCircle()
    {
        return attachedBeatCircle;
    }

    public void Hit()
    {
        Destroy(this.gameObject);
    }

    public void Miss()
    {
        Destroy(this.gameObject);
    }

    public Vector3 GetKeyTilePosition()
    {
        Vector3 thisPos = transform.position;
        thisPos.y  = 0;
        return thisPos;
    }
}
