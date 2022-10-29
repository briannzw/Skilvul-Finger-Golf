using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public Ball ball;
    public Transform arrowTransform;
    public Image aim;
    public LineRenderer lineRenderer;
    public TMP_Text shootText;
    public LayerMask ballLayer;
    public LayerMask rayLayer;
    public FollowBall camPivot;
    public Camera cam;
    public Vector3 camSensitivity;
    public float shootForce;

    Vector3 lastMousePos;
    Vector3 mouseDelta;
    Vector3 current;
    Vector3 last;

    Vector3 forceVector;
    Vector3 forceDir;
    float forceMagnitude;
    float forceFactor;

    Renderer[] arrowRenderers;

    Ray ray;

    float angle;
    float ballDistance;
    bool isShooting = false;

    public int shootCount;

    RectTransform aimRect;

    private void Start()
    {
        ballDistance = Vector3.Distance(cam.transform.position, ball.Position) + 1;
        arrowTransform.gameObject.SetActive(false);
        arrowRenderers = arrowTransform.gameObject.GetComponentsInChildren<Renderer>();
        aimRect = aim.GetComponent<RectTransform>();
    }

    private void Update()
    {
        if (Input.GetMouseButton(1))
        {
            CameraMove();
        }

        lastMousePos = Input.mousePosition;

        if (ball.IsMoving || ball.IsTeleporting)
        {
            aim.gameObject.SetActive(false);
            lineRenderer.enabled = false;
            return;
        }

        if (!camPivot.IsMoving && !aim.gameObject.activeSelf)
        {
            aim.gameObject.SetActive(true);
            aimRect.anchoredPosition = cam.WorldToScreenPoint(ball.Position);
        }

        if (transform.position != ball.Position)
        {
            transform.position = ball.Position;

            aim.gameObject.SetActive(true);
            aimRect.anchoredPosition = cam.WorldToScreenPoint(ball.Position);
        }

        if (Input.GetMouseButtonDown(0))
        {
            ray = cam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, ballDistance, ballLayer))
            {
                isShooting = true;
                arrowTransform.gameObject.SetActive(true);
                lineRenderer.enabled = true;
            }
        }

        if(Input.GetMouseButton(0) && isShooting)
        {
            ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if(Physics.Raycast(ray, out hit, ballDistance * 2, rayLayer))
            {
                forceVector = ball.Position - hit.point;
                forceDir = forceVector.normalized;
                forceMagnitude = forceVector.magnitude;
                forceMagnitude = Mathf.Clamp(forceMagnitude, 0, 5);
                forceFactor = forceMagnitude / 5;
            }

            transform.LookAt(transform.position + new Vector3(forceDir.x, 0, forceDir.z));
            arrowTransform.localScale = new Vector3(1 + 0.5f * forceFactor,
                1 + 0.5f * forceFactor, 1 + 1.5f * forceFactor * 2);

            foreach(Renderer rend in arrowRenderers)
            {
                rend.material.color = Color.Lerp(Color.white, Color.red, forceFactor);
            }

            aimRect.anchoredPosition = Input.mousePosition;

            Vector3 ballScreenPos = cam.WorldToScreenPoint(ball.Position);
            lineRenderer.SetPositions(new Vector3[]{ballScreenPos, Input.mousePosition});
        }

        if (Input.GetMouseButtonUp(0) && isShooting == true)
        {
            ball.AddForce(forceDir * shootForce * forceFactor);
            shootCount++;
            shootText.text = "Shoot Count : " + shootCount;
            forceFactor = 0;
            forceDir = Vector3.zero;
            isShooting = false;
            arrowTransform.gameObject.SetActive(false);
        }
    }

    void CameraMove()
    {
        current = cam.ScreenToViewportPoint(Input.mousePosition);
        last = cam.ScreenToViewportPoint(lastMousePos);
        mouseDelta = current - last;

        Vector3 followPos = ball.transform.position;

        camPivot.transform.RotateAround(followPos, Vector3.up, mouseDelta.x * camSensitivity.x);
        camPivot.transform.RotateAround(followPos, cam.transform.right, -mouseDelta.y * camSensitivity.y);

        angle = Vector3.SignedAngle(Vector3.up, cam.transform.up, cam.transform.right);

        if (angle < -20)
        {
            camPivot.transform.RotateAround(followPos, cam.transform.right, -20 - angle);
        }
        else if (angle > 75)
        {
            camPivot.transform.RotateAround(followPos, cam.transform.right, 75 - angle);
        }
    }
}
