using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField]
    private GameObject noteBoard;

    [Header("DEBUG")]
    [SerializeField]
    private float debugSetTimeElapsed = 0.0f;

    [SerializeField]
    private bool debugExecuteTimer = false;

    void Start() {

    }

    // Update is called once per frame
    void Update() {
        if (debugExecuteTimer) {
            MovingBillboard movingBillboard = noteBoard.GetComponent<MovingBillboard>();
            noteBoard.transform.position = new Vector3(
                movingBillboard.initialPosition.x + movingBillboard.movement.x * debugSetTimeElapsed,
                movingBillboard.initialPosition.y + movingBillboard.movement.y * debugSetTimeElapsed,
                movingBillboard.initialPosition.z + movingBillboard.movement.z * debugSetTimeElapsed
            );
            AudioManager.Instance.SetTime(debugSetTimeElapsed);
            debugExecuteTimer = false;
        }
    }
}
