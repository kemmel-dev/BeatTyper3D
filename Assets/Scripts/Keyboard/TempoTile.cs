using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempoTile : MonoBehaviour
{

    public GameObject beatCirclePrefab;

    private SpriteRenderer tile1, tile2;

    private void Start()
    {
        tile1 = transform.Find("Tile 1").GetComponent<SpriteRenderer>();
        tile2 = transform.Find("Tile 2").GetComponent<SpriteRenderer>();
        tile2.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        Beat nextTempoBeat = CheckForBeat();
        if (nextTempoBeat)
        {
            nextTempoBeat.gameObject.layer = LayerMask.NameToLayer("Discovered");
            BeatCircle beatCircle = Instantiate(beatCirclePrefab, this.transform).GetComponent<BeatCircle>();
            beatCircle.SetTarget(nextTempoBeat.transform);
            nextTempoBeat.AttachBeatCircle(beatCircle);
        }
    }

    public void Tick()
    {
        tile1.enabled = !tile1.enabled;
        tile2.enabled = !tile2.enabled;
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(other.gameObject.GetComponent<Beat>().GetBeatCircle().gameObject);
        Destroy(other.gameObject);
    }

    public Beat CheckForBeat()
    {
        RaycastHit hit;

        if (Physics.Raycast(new Ray(transform.position, Vector3.up), out hit, 10f, LayerMask.GetMask("Undiscovered")))
        {
            Beat hitBeat = hit.collider.GetComponent<Beat>();
            return hitBeat;
        }
        return null;
    }
}
