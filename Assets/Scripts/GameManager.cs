using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

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

    [SerializeField]
    private TMP_Text scoreLabel, scoreText;

    [SerializeField]
    private TMP_Text endOfGameScoreLabel, endOfGameScoreText;

    [SerializeField]
    private Button playAgainButton;

    [SerializeField]
    private GameObject[] hintLabels;

    private MovingBillboard billboard;

    private int score = 0;

    public int numMissed = 0;

    void Start() {
        billboard = noteBoard.GetComponent<MovingBillboard>();

        string[] chartLines = Regex.Split(chart.text, "\r\n|\r|\n");
        for (int i = 0; i < chartLines.Length; i++) {
            Debug.Log(i);
            string chartLine = chartLines[i];
            string[] chartData = Regex.Split(chartLine, "\\s");
            string timeRaw = chartData[0];
            string[] timeParts = timeRaw.Split(':');
            float time = System.Convert.ToSingle(timeParts[0]) * 60 + System.Convert.ToSingle(timeParts[1]) + noteTimeOffset;
            int note = System.Convert.ToInt32(chartData[1]);
            string mode = chartData.Length >= 3 ? chartData[2] : "";
            Vector3 position = new Vector3(
                noteXPositions[note],
                0f,
                0f + billboard.movement.z * time * -1
            );
            GameObject obj = Instantiate(notePrefab, position, Quaternion.identity);
            obj.transform.parent = noteBoard.transform;
            if (mode == "i") {
                obj.GetComponent<Note>().isIntense = true;
            }
            Transform renderer = obj.transform.GetChild(0);
            renderer.gameObject.GetComponent<MeshRenderer>().material = noteMaterials[note];
        }

        AudioManager.Instance.Play();
    }

    // Update is called once per frame
    void Update() {
        if (numMissed > 2) {
            AudioManager.Instance.SetTrack(ActiveTrack.Good);
        } else {
            AudioManager.Instance.SetTrack(ActiveTrack.Bad);
        }

        if (debugExecuteTimer) {
            noteBoard.transform.position = new Vector3(
                billboard.initialPosition.x + billboard.movement.x * debugSetTimeElapsed,
                billboard.initialPosition.y + billboard.movement.y * debugSetTimeElapsed,
                billboard.initialPosition.z + billboard.movement.z * debugSetTimeElapsed
            );
            AudioManager.Instance.SetTime(debugSetTimeElapsed);
            debugExecuteTimer = false;
        }

        if (AudioManager.Instance.GetTime() > 60f * 3) {
            EndGame();
        }
    }
    void EndGame() {
        Destroy(scoreLabel.gameObject);
        Destroy(scoreText.gameObject);

        endOfGameScoreLabel.gameObject.SetActive(true);
        endOfGameScoreText.text = score.ToString();
        endOfGameScoreText.gameObject.SetActive(true);
        playAgainButton.gameObject.SetActive(true);
    }


    public void ScorePoint() {
        score++;
        scoreText.text = score.ToString();

        if (score > 0) {
            foreach (GameObject hintLabel in hintLabels) {
                Destroy(hintLabel);
            }
        }
    }
}
