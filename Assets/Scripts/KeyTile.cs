using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;

public class KeyTile : MonoBehaviour
{
    public GameObject beatCirclePrefab;

    private KeyCode keyCode;

    public Color32 activeColorTile, inactiveColorTile, activeColorText, inactiveColorText;

    private SpriteRenderer spriteRenderer;
    private TextMeshPro textMeshPro;

    private LayerMask discovered, undiscovered;

    private Dictionary<GameObject, BeatCircle> attachedBeatCircles = new Dictionary<GameObject, BeatCircle>();

    private static bool firstBeat;

    private void OnCollisionEnter(Collision other)
    {
        MissBeat(other.gameObject);
    }

    private void Update()
    {
        GameObject nextBeat = BeatSpawner.beats[0];
        if (nextBeat)
        {
            Vector3 nextBeatPos = nextBeat.transform.position;
            nextBeatPos.y = 0;
            if (transform.position == nextBeatPos)
            {
                if (attachedBeatCircles.ContainsKey(nextBeat))
                {
                    attachedBeatCircles[nextBeat].SetActive();
                }
            }
        }

        GameObject beatPresent = CheckForBeatUndiscovered();

        if (beatPresent)
        {
            if (!firstBeat)
            {
                Connector.Enable();
                firstBeat = true;
            }
            beatPresent.layer = discovered;
            BeatCircle beatCircle = Instantiate(beatCirclePrefab, this.transform).GetComponent<BeatCircle>();
            beatCircle.SetTarget(beatPresent.transform);
            attachedBeatCircles.Add(beatPresent, beatCircle);
        }

        if (Input.GetKeyDown(keyCode))
        {
            beatPresent = CheckForBeatDiscovered();
            if (beatPresent)
            {
                if (Vector3.Distance(beatPresent.transform.position, this.transform.position) < BeatCircle.maxDistanceToNote / 2)
                {
                    HitBeat(beatPresent);
                    return;
                }
            }
            MissBeat(null);
            return;
        }
        if (Input.GetKey(keyCode))
        {
            spriteRenderer.material.color = activeColorTile;
            textMeshPro.color = activeColorText;
            return;
        }
        else if (Input.GetKeyUp(keyCode))
        {
            spriteRenderer.material.color = inactiveColorTile;
            textMeshPro.color = inactiveColorText;
            return;
        }

    }

    private void HitBeat(GameObject beatPresent)
    {
        BeatCircle attachedBeatCircle = attachedBeatCircles[beatPresent];
        Destroy(beatPresent);
        Destroy(attachedBeatCircle.gameObject);
        FeedbackManager.Hit();
        BeatSpawner.beats.RemoveAt(0);
    }

    private void MissBeat(GameObject beatPresent)
    {
        if (beatPresent)
        {
            BeatCircle attachedBeatCircle = attachedBeatCircles[beatPresent];
            if (attachedBeatCircle)
            {
                Destroy(attachedBeatCircle.gameObject);
            }
            Destroy(beatPresent);
            BeatSpawner.beats.RemoveAt(0);
        }
        FeedbackManager.Miss();
    }

    public GameObject CheckForBeatDiscovered()
    {
        RaycastHit hit;
        if (Physics.Raycast(new Ray(transform.position, Vector3.up), out hit, 10, LayerMask.GetMask("Discovered")))
        {
            return hit.collider.gameObject;
        }
        return null;
    }

    public GameObject CheckForBeatUndiscovered()
    {
        RaycastHit hit;
        if (Physics.Raycast(new Ray(transform.position, Vector3.up), out hit, 10, undiscovered))
        {
            return hit.collider.gameObject;
        }
        return null;
    }

    // Start is called before the first frame update
    public void Initialise(char key)
    {
        this.gameObject.name = key.ToString();
        keyCode = GetKeyCode(key);
        KeyboardManager.AddKeyTile(key, this);

        textMeshPro = transform.Find("Text").GetComponent<TextMeshPro>();
        textMeshPro.text = key.ToString();
        textMeshPro.color = inactiveColorText;

        spriteRenderer = transform.Find("Tile").GetComponent<SpriteRenderer>();
        spriteRenderer.material.color = inactiveColorTile;

        discovered = LayerMask.NameToLayer("Discovered");
        undiscovered = LayerMask.GetMask("Undiscovered");

    }

    public KeyCode GetKeyCode(char key)
    {
        return (KeyCode) (key + 32);
    }
}
