using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
            transform.position += new Vector3(-1, 0,0);
        if (Input.GetKeyDown(KeyCode.D))
            transform.position += new Vector3(1, 0, 0);
        if (Input.GetKeyDown(KeyCode.W))
            transform.position += new Vector3(0, 1, 0);
        if (Input.GetKeyDown(KeyCode.S))
            transform.position += new Vector3(0, -1, 0);
    }
}
