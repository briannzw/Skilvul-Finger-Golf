using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuCamera : MonoBehaviour
{
    public Transform camTransform;
    public Transform targetTransform;

    public float speed = 2f;

    void Update()
    {
        camTransform.RotateAround(targetTransform.position, Vector3.up, speed * Time.deltaTime);
    }

    public void QuitGame()
    {
        Application.Quit(0);
    }
}
