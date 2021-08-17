using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class KeyTrigger : MonoBehaviour
{
    public KeyCode triggerKey;
    private Queue<GameObject> currentlyCollidingNotes;

    private ParticleSystem fxOnTrigger;

    void Start() {
        currentlyCollidingNotes = new Queue<GameObject>();
        fxOnTrigger = GetComponent<ParticleSystem>();
    }

    void Update() {
        if (currentlyCollidingNotes.Count > 0 && Input.GetKeyDown(triggerKey)) {
            fxOnTrigger.Play();

            GameObject currentlyCollidingNote = currentlyCollidingNotes.Dequeue();
            Destroy(currentlyCollidingNote);
        }
    }

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Note") {
            currentlyCollidingNotes.Enqueue(other.gameObject);
        }
    }
    void OnTriggerExit(Collider other) {
        if (other.gameObject.tag == "Note") {
            currentlyCollidingNotes.Dequeue();
        }
    }


}
