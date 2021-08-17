using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingBillboard : MonoBehaviour
{
    public Vector3 movement;
    void Update()
    {
        transform.position = new Vector3(transform.position.x + (movement.x * Time.deltaTime),
                                         transform.position.y + (movement.y * Time.deltaTime),
                                         transform.position.z + (movement.z * Time.deltaTime));
    }
}
