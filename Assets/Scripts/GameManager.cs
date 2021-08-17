using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{

    [SerializeField]
    private TextAsset chart;

    [SerializeField]
    private GameObject noteBoard;

    [SerializeField]
    private Material[] noteMaterials;

    [SerializeField]
    private float[] noteXPositions = { -1.8f, -0.6f, 0.6f, 1.8f };

    [SerializeField]
    private float noteTimeOffset = 0.150f;

    [SerializeField]
    private GameObject notePrefab;

    [Header("DEBUG")]
    [SerializeField]
    private float debugSetTimeElapsed = 0.0f;

    [SerializeField]
    private bool debugExecuteTimer = false;

    private MovingBillboard billboard;

    void Start() {
        billboard = noteBoard.GetComponent<MovingBillboard>();

        string[] chartLines = Regex.Split(chart.text, "\r\n|\r|\n");
        foreach (string chartLine in chartLines) {
            string[] chartData = Regex.Split(chartLine, "\\s");
            string timeRaw = chartData[0];
            string[] timeParts = timeRaw.Split(':');
            float time = System.Convert.ToSingle(timeParts[0]) * 60 + System.Convert.ToSingle(timeParts[1]) + noteTimeOffset;
            int note = System.Convert.ToInt32(chartData[1]);
            Vector3 position = new Vector3(
                noteXPositions[note],
                0f,
                0f + billboard.movement.z * time * -1
            );
            GameObject obj = Instantiate(notePrefab, position, Quaternion.identity);
            obj.transform.parent = noteBoard.transform;
            Transform renderer = obj.transform.GetChild(0);
            renderer.gameObject.GetComponent<MeshRenderer>().material = noteMaterials[note];
        }

        AudioManager.Instance.Play();
    }

    // Update is called once per frame
    void Update() {
        if (debugExecuteTimer) {
            noteBoard.transform.position = new Vector3(
                billboard.initialPosition.x + billboard.movement.x * debugSetTimeElapsed,
                billboard.initialPosition.y + billboard.movement.y * debugSetTimeElapsed,
                billboard.initialPosition.z + billboard.movement.z * debugSetTimeElapsed
            );
            AudioManager.Instance.SetTime(debugSetTimeElapsed);
            debugExecuteTimer = false;
        }
    }
}
