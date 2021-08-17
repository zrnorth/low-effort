using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note : MonoBehaviour
{
    public bool isIntense = false;

    private bool didMiss = false;

    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        if (transform.position.z < -1f && didMiss == false) {
            GameManager.Instance.numMissed++;
            didMiss = true;
        }
        if (transform.position.z < -3f) {
            Destroy(this.gameObject);
        }
    }
}
