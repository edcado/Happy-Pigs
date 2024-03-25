using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineForceOscar : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float stopVelocity = .05f;
    [SerializeField] private float shootPower;
    [SerializeField] private float jumpForce;
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private int maxPoints = 20;

    [Header("Booleans")]
    [SerializeField] private bool isIdle;
    [SerializeField] private bool isAiming;

    [Header("Colission info")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private LayerMask WhatIsGround;

    [Header("Components")]
    [SerializeField] private Rigidbody body;
    [SerializeField] private Camera mainCamera;

    private void Awake()
    {
        body = GetComponent<Rigidbody>();
        mainCamera = Camera.main;
        isAiming = false;
        lineRenderer.enabled = false;
    }

    private void Update()
    {
        if (body.velocity.magnitude < stopVelocity)
        {
            Stop();
        }
        ProcessAim();
        if (IsGroudDetected())
            Debug.Log("Estoy tocando suelo");
    }

    private void Stop()
    {
        body.velocity = Vector3.zero;
        body.angularVelocity = Vector3.zero;
        isIdle = true;
    }

    private bool IsGroudDetected() => Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, WhatIsGround);




    private void OnMouseDown()
    {
        if (isIdle)
        {
            isAiming = true;
        }
    }

    private void ProcessAim()
    {
        if (!isAiming || !isIdle)
        {
            return;
        }

        UpdateLineRendererPositions();

        if (Input.GetMouseButtonUp(0))
        {
            Vector3? worldPoint = CastMouseClick();
            if (worldPoint.HasValue)
            {
                Debug.Log("Disparo bola");
                ShootBall(worldPoint.Value);
            }
        }
    }

    private void UpdateLineRendererPositions()
    {
        List<Vector3> points = new List<Vector3>();
        points.Add(transform.position);
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            Vector3 direction = (hit.point - transform.position).normalized;
            float distance = Vector3.Distance(transform.position, hit.point);

            for (int i = 1; i <= maxPoints; i++)
            {
                float t = i / (float)maxPoints;
                Vector3 point = CalculateParabolicPoint(transform.position, direction, distance, t);
                points.Add(point);
            }
        }

        lineRenderer.positionCount = points.Count;
        lineRenderer.SetPositions(points.ToArray());
        lineRenderer.enabled = true;
        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.1f;
        lineRenderer.material.color = Color.green;
    }

    private Vector3 CalculateParabolicPoint(Vector3 origin, Vector3 direction, float distance, float t)
    {
        float maxHeight = jumpForce / 8f;
        float totalTime = Mathf.Sqrt(2 * maxHeight / Physics.gravity.magnitude);
        float horizontalDistance = distance;
        float initialHeight = maxHeight * (1 - (2 * t - 1) * (2 * t - 1));
        float displacementY = initialHeight - 0.5f * Physics.gravity.magnitude * totalTime * t;
        float displacementXZ = horizontalDistance * t;
        Vector3 point = origin + (direction * displacementXZ) + (Vector3.up * displacementY);
        return point;
    }

    private void ShootBall(Vector3 worldPoint)
    {
        
        isAiming = false;
        lineRenderer.enabled = false;
        float distance = Vector3.Distance(transform.position, worldPoint);
        float adjustedShootPower = (shootPower * distance) / 2f;
        Vector3 horizontalWorldPoint = new Vector3(worldPoint.x, transform.position.y, worldPoint.z);
        Vector3 direction = (horizontalWorldPoint - transform.position).normalized;
        float strength = Vector3.Distance(transform.position, horizontalWorldPoint);
        body.AddForce(direction * strength * adjustedShootPower);
        float adjustedJumpForce = (jumpForce * distance) / 8f;
        body.AddForce(Vector3.up * adjustedJumpForce, ForceMode.Impulse);
        
    }

    private Vector3? CastMouseClick()
    {
        RaycastHit hit;
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            return hit.point;
        }
        else
        {
            return null;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(groundCheck.position, new Vector3 (groundCheck.position.x, groundCheck.position.y - groundCheckDistance, groundCheck.position.z));
    }
    
}
