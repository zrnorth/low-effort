using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class KeyTrigger : MonoBehaviour
{
    public KeyCode triggerKey;
    private GameObject currentlyCollidingNote = null;

    void Update()
    {
        if (currentlyCollidingNote != null && Input.GetKeyDown(triggerKey))
        {
            Destroy(currentlyCollidingNote);
            currentlyCollidingNote = null;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Note")
        {
            if (currentlyCollidingNote != null)
            {
                Debug.LogError("Overlapping notes");
                return; // Don't thrash
            }
            currentlyCollidingNote = other.gameObject;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Note")
        {
            currentlyCollidingNote = null;
        }
    }


}