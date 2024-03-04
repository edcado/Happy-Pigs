using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineForce : MonoBehaviour
{
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private float stopVelocity = .05f;
    [SerializeField] private float shotPower;
    private bool isIdle;
    private bool isAiming;

    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        isAiming = false;
        lineRenderer.enabled = false;
    }
    private void Update()
    {
        if(rb.velocity.magnitude < stopVelocity)
        {
            Stop();
        }
        ProcessAim();
    }

    private void OnMouseDown()
    {
        if (isIdle)
        {
            isAiming = true;
        }
    }
    private void ProcessAim()
    {
        if(!isAiming || !isIdle)
        {
            return;
        }
        Vector3? worldPoint = CastMousClickRay();

        if (!worldPoint.HasValue)
        {
            return;
        }
        Drawline(worldPoint.Value);
        if (Input.GetMouseButtonUp(0))
        {
            shoot(worldPoint.Value);
        }
    }
    private void shoot(Vector3 worldPoint)
    {
        isAiming = false;
        lineRenderer.enabled = false;

        Vector3 horizontalWorldPoint = new Vector3(worldPoint.x, transform.position.y, worldPoint.z);
        float strength = Vector3.Distance(transform.position, horizontalWorldPoint);
        Vector3 direction = (horizontalWorldPoint - transform.position).normalized;

        rb.AddForce(direction * strength * shotPower);
    }
    private void Drawline(Vector3 wroldPoint)
    {
        Vector3[] position =
        {
            transform.position,
            wroldPoint
        };
        lineRenderer.SetPositions(position);
        lineRenderer.enabled = true;
    }
    private Vector3? CastMousClickRay()
    {
        Vector3 screenMousePosFar = new Vector3(
            Input.mousePosition.x,
            Input.mousePosition.y,
            Camera.main.farClipPlane
            );
        Vector3 screenMousePosNear = new Vector3(
            Input.mousePosition.x,
            Input.mousePosition.y,
            Camera.main.nearClipPlane
            );
        Vector3 worldmousePosFar = Camera.main.ScreenToWorldPoint(screenMousePosFar);
        Vector3 worldmousePosNear = Camera.main.ScreenToWorldPoint(screenMousePosNear);
        RaycastHit hit;

        if(Physics.Raycast(worldmousePosNear, worldmousePosFar - worldmousePosNear, out hit, float.PositiveInfinity))
        {
            return hit.point;
        }
        else
        {
            return null;
        }
    }
    private void Stop()
    {
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        isIdle = true; 
    }
}
