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
            GameManager.Instance.ScorePoint();
            GameManager.Instance.numMissed = 0;
            fxOnTrigger.Play();

            GameObject currentlyCollidingNote = currentlyCollidingNotes.Dequeue();
            Note note = currentlyCollidingNote.GetComponent<Note>();
            Guy guy = Guy.Instance;
            guy.SetIntenseMode(note && note.isIntense);
            guy.RandomizePose();
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
