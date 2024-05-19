using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Bird: MonoBehaviour
{
    private Camera mainCamera;
    public Rigidbody rb;
    private Vector3 startPosition, clampedPosition;
    public float force;
    public float maxDistance;
    public float explosionForce, explosionRadious;
    void Start()
    {
        mainCamera = Camera.main;
        rb.GetComponent<Rigidbody>();
        rb.isKinematic = true;
    }

    private void OnMouseDrag()
    {
       Vector3 dragPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        clampedPosition = dragPosition;

       float dragDistance = Vector3.Distance(startPosition, dragPosition);

       if(dragDistance > maxDistance)
        {
            clampedPosition = startPosition + (dragPosition - startPosition).normalized * maxDistance;
        }

       if (dragPosition.x > startPosition.x)
        {

        }
    }
    private void OnMouseUp()
    {
        ThrowAddForce();
    }

    private void ThrowAddForce()
    {
        rb.isKinematic = false;
        Vector3 throwVector = startPosition - clampedPosition;
        rb.AddForce(throwVector * force);
    }
    private void ThrowAddForceAtPosition()
    {
        rb.isKinematic = false;
        Vector3 throwVector = startPosition - clampedPosition;
        rb.AddForceAtPosition(throwVector * force,clampedPosition);
    }
    private void ThrowAddRelativeForce()
    {   
        rb.isKinematic = false;
        Vector3 throwVector = startPosition - clampedPosition;
        rb.AddRelativeForce(throwVector * force);
    }
    private void ThrowAddExplosionForce()
    {
        rb.isKinematic = false;
        Vector3 throwVector = startPosition - clampedPosition;
        rb.AddExplosionForce(explosionForce,this.transform.position,explosionRadious);
    }

    void Update()
    {
        
    }
}
