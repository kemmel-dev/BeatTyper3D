using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class KeyTile : MonoBehaviour
{

    public GameObject beatCirclePrefab;
    public Color32 activeColorTile, inactiveColorTile, activeColorText, inactiveColorText;


    public KeyCode keyCode;
    private LayerMask discoveredMask, undiscoveredMask, discoveredLayer;
    private SpriteRenderer spriteRenderer;
    private TextMeshPro textMeshPro;

    private static bool firstBeat = true;

    private CircleRenderer circle;

    private void Update()
    {
        Beat nextBeat = BeatManager.GetNextBeat();
        if (nextBeat)
        {
            BeatCircle attachedBeatCircle = nextBeat.GetBeatCircle();
            if (attachedBeatCircle)
            {
                attachedBeatCircle.SetActive();
            }
        }

        Beat beatPresent = CheckForBeat(false);
        if (beatPresent)
        {
            if (firstBeat)
            {
                Connector.Enable();
                firstBeat = false;
            }
            beatPresent.gameObject.layer = discoveredLayer;
            BeatCircle beatCircle = Instantiate(beatCirclePrefab, this.transform).GetComponent<BeatCircle>();
            beatCircle.SetTarget(beatPresent.transform);
            if (beatPresent.holdable)
            {
                beatCircle.holdable = true;
            }
            beatPresent.AttachBeatCircle(beatCircle);
            beatPresent.AttachKeyTile(this);
        }

        if (Input.GetKeyDown(keyCode))
        {
            beatPresent = CheckForBeat(true);
            if (beatPresent)
            {
                float distanceToBeat = Vector3.Distance(beatPresent.transform.position, this.transform.position);
                if (distanceToBeat < BeatSpawner.beatHitDistance * 1.5f)
                {
                    BeatManager.HitBeat(distanceToBeat);
                    return;
                }
            }
            BeatManager.MissBeat(false);
        }
        if (Input.GetKey(keyCode))
        {
            spriteRenderer.enabled = true;
            textMeshPro.enabled = true;
            spriteRenderer.material.color = activeColorTile;
            textMeshPro.color = activeColorText;
        }
        else if (Input.GetKeyUp(keyCode))
        {
            spriteRenderer.enabled = false;
            textMeshPro.enabled = false;
            spriteRenderer.material.color = inactiveColorTile;
            textMeshPro.color = inactiveColorText;
        }

    }

    public void Show()
    {
        spriteRenderer.enabled = true;
        textMeshPro.enabled = true;
        circle.Show();
    }

    public void Hide()
    {
        spriteRenderer.enabled = false;
        textMeshPro.enabled = false;
        circle.Hide();
    }

    public Beat CheckForBeat(bool discovered)
    {
        RaycastHit hit;

        if (discovered)
        {
            Beat nextBeat = BeatManager.GetNextBeat();
            if (nextBeat.transform.position.x == transform.position.x)
            {
                if (nextBeat.transform.position.z == transform.position.z)
                {
                    return nextBeat;
                }
            }
            return null;
        }
        else
        {
            if (Physics.Raycast(new Ray(transform.position, Vector3.up), out hit, 10f, undiscoveredMask))
            {
                Beat hitBeat = hit.collider.GetComponent<Beat>();
                return hitBeat;
            }
        }
        return null;
    }

    // Start is called before the first frame update
    public void Initialise(char key)
    {
        circle = transform.Find("Circle").GetComponent<CircleRenderer>();
        this.gameObject.name = key.ToString();
        keyCode = GetKeyCode(key);
        KeyboardManager.AddKeyTile(key, this);

        textMeshPro = transform.Find("Text").GetComponent<TextMeshPro>();
        textMeshPro.text = key.ToString();
        textMeshPro.color = inactiveColorText;

        spriteRenderer = transform.Find("Tile").GetComponent<SpriteRenderer>();
        spriteRenderer.material.color = inactiveColorTile;

        discoveredLayer = LayerMask.NameToLayer("Discovered");
        discoveredMask = LayerMask.GetMask("Discovered");
        undiscoveredMask = LayerMask.GetMask("Undiscovered");

        Hide();
    }

    public KeyCode GetKeyCode(char key)
    {
        return (KeyCode) (key + 32);
    }
}
