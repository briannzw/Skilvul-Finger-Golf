using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aim : MonoBehaviour
{
    public float speed;
    float timer;
    float z;

    private void Update()
    {
        if (timer > 1)
            timer = 0;

        timer += Time.deltaTime * speed;

        z = Mathf.Lerp(0, 360, timer) % 360;

        transform.localRotation = Quaternion.Euler(0, 0, z);
    }
}
