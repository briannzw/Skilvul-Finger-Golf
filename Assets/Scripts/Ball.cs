using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public Rigidbody rb;

    public Vector3 Position => rb.position;

    public bool IsMoving => rb.velocity != Vector3.zero;

    public bool IsTeleporting => isTeleporting;

    Vector3 lastPos;
    bool isTeleporting;

    private void Awake()
    {
        if (rb == null) rb = GetComponent<Rigidbody>();
        isTeleporting = false;
        lastPos = transform.position;
    }
    public void AddForce(Vector3 force)
    {
        if (force != Vector3.zero)
        {
            rb.isKinematic = false;
            lastPos = transform.position;
            rb.AddForce(force, ForceMode.Impulse);
        }
    }

    private void FixedUpdate()
    {
        if (rb.velocity != Vector3.zero && rb.velocity.magnitude < .2f)
        {
            rb.velocity = Vector3.zero;
            rb.isKinematic = true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (isTeleporting) return;
        if (collision.gameObject.CompareTag("Out"))
        {
            StartCoroutine(DelayedTeleport());
        }
    }

    IEnumerator DelayedTeleport()
    {
        isTeleporting = true;
        yield return new WaitForSeconds(3);
        rb.isKinematic = true;
        transform.position = lastPos;
        isTeleporting = false;
    }
}
