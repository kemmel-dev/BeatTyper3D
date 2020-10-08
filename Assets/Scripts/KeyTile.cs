using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class KeyTile : MonoBehaviour
{

    private TextMeshPro textMeshPro;
    private Color32 color;
    private KeyCode keyCode;
    private LayerMask noteMask;
    private MeshRenderer meshRenderer;

    public float hitRange;
    public float maxRange = 100;

    public byte opacityInactive, opacityActive;

    public void Initialise(char character, Color32 color)
    {
        textMeshPro = GetComponentInChildren<TextMeshPro>();
        meshRenderer = GetComponent<MeshRenderer>();
        this.color = color;
        meshRenderer.material.color = color;
        keyCode = GetKeyCodeFromCharacter(character);
        SetText(character.ToString());
        hitRange = 5f;
        noteMask = LayerMask.GetMask("NoteLayer");

        MakeInactive();
    }

    private void Update()
    {
        bool keyPressed = Input.GetKeyDown(keyCode);
        Note note = CheckForNoteInRange(hitRange);
        if (note)
        {
            float distance = Vector3.Distance(transform.position, note.transform.position);

            note.MakeActive();

            //Debug.DrawRay(transform.position, Vector3.up, UnityEngine.Color.yellow, 2);
            //Debug.DrawLine(transform.position, note.transform.position, UnityEngine.Color.red, 2);
            
            if (keyPressed)
            {
                note.Hit();
            }
            MakeInactive();
            return;
        }

        if (keyPressed)
        {
            note = CheckForNoteInRange(maxRange);
            if (note)
            {
                note.Miss();
                MakeInactive();
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Note collidingNote = collision.gameObject.GetComponent<Note>();
        if (collidingNote)
        {
            collidingNote.Miss();
            MakeInactive();
        }
    }

    public Note CheckForNoteInRange(float range)
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.up, out hit, range, noteMask))
        {
            Note note = hit.collider.GetComponent<Note>();
            if (note)
            {
                return note;
            }
        }
        return null;
    }

    public void MakeActive()
    {
        color.a = opacityActive;
        meshRenderer.material.color = color;
    }

    public void MakeInactive()
    {
        color.a = opacityInactive;
        meshRenderer.material.color = color;
    }


    public void SetText(string text)
    {
        textMeshPro.text = text;
    }

    public void SetKeyCode(KeyCode keyCode)
    {
        this.keyCode = keyCode;
    }

    public Color32 Color
    {
        get
        {
            return color;
        }
        set
        {
            color = value;
        }
    }


    private KeyCode GetKeyCodeFromCharacter(char character)
    {
        int charNo = (int)character + (97 - 65);
        return (KeyCode)charNo;
    }

}

