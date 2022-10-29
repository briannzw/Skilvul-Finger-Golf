using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowBall : MonoBehaviour
{
    public Ball ball;
    public float speed = 1;
    public bool IsMoving => transform.position != ball.Position;
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, ball.Position, Time.deltaTime * speed);

        if (ball.IsMoving) return;

        if(Vector3.Distance(transform.position, ball.Position) < 0.1f)
        {
            transform.position = ball.Position;
        }
    }
}
