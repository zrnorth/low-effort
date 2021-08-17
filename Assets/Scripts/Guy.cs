using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guy : Singleton<Guy>
{

    [SerializeField]
    private GameObject[] renderers;

    [SerializeField]
    private float scaleAmplitude = 0.0125f;

    [SerializeField]
    private float scaleFrequency = 1.0f;

    [SerializeField]
    private float scaleFrequencyIntense = 100.0f;

    [SerializeField]
    private float rotationRangeIntense = 1.0f;

    [SerializeField]
    private float rotationFrequencyIntense = 70.0f;

    private int activeRenderer = 0;

    private bool isIntenseMode = false;

    private Vector3 initScale;

    [Header("Debug Settings")]
    [SerializeField]
    private bool debugRandomizeNormal = false;
    [SerializeField]
    private bool debugRandomizeIntense = false;

    // Start is called before the first frame update
    void Start() {
        initScale = transform.localScale;
        RandomizePose();
    }

    // Update is called once per frame
    void Update() {
        for (int i = 0; i < renderers.Length; i++) {
            GameObject renderer = renderers[i];
            renderer.SetActive(i == activeRenderer);
        }

        float frequencyScalar = isIntenseMode ? scaleFrequencyIntense : scaleFrequency;

        transform.localScale = new Vector3(
            initScale.x + Mathf.Sin(Time.timeSinceLevelLoad * frequencyScalar) * scaleAmplitude,
            initScale.y,
            initScale.z
        );

        if (isIntenseMode) {
            transform.rotation = Quaternion.Euler(
                1.0f,
                1.0f,
                Mathf.Sin(Time.timeSinceLevelLoad * rotationFrequencyIntense) * rotationRangeIntense
            );
        } else {
            transform.rotation = Quaternion.Euler(1.0f, 1.0f, 1.0f);
        }

        // DEBUG
        if (debugRandomizeNormal) {
            RandomizePose();
            SetIntenseMode(false);
            debugRandomizeNormal = false;
        }
        if (debugRandomizeIntense) {
            RandomizePose();
            SetIntenseMode(true);
            debugRandomizeIntense = false;
        }
    }

    public void RandomizePose() {
        int newActive = Random.Range(0, renderers.Length);
        if (newActive == activeRenderer) {
            RandomizePose();
        } else {
            activeRenderer = newActive;
        }
    }

    public void SetIntenseMode(bool intense) {
        isIntenseMode = intense;
    }
}
