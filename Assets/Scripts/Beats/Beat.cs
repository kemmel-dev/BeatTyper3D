using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beat : MonoBehaviour
{

    private KeyTile correspondingTile;
    private BeatCircle attachedBeatCircle;

    public void Update()
    {
        if (transform.position.y < 0)
        {
            attachedBeatCircle.MarkLate();
        }
    }

    public void AttachBeatCircle(BeatCircle beatCircle)
    {
        attachedBeatCircle = beatCircle;
    }

    public void AttachKeyTile(KeyTile keyTile)
    {
        correspondingTile = keyTile;
    }

    public BeatCircle GetBeatCircle()
    {
        return attachedBeatCircle;
    }

    public KeyTile GetKeyTile()
    {
        return correspondingTile;
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
