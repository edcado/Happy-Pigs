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
    public enum ForceType
    {
        AddForce,
        AddForceAtPosition,
        AddRelativeForce,
        AddExplosionForce
    }

    public ForceType forceType;
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
        switch (forceType)
        {
            case ForceType.AddForce:
                ThrowAddForce();
                break;
            case ForceType.AddForceAtPosition:
                ThrowAddForceAtPosition();
                break;
            case ForceType.AddRelativeForce:
                ThrowAddRelativeForce();
                break;
            case ForceType.AddExplosionForce:
                ThrowAddExplosionForce();
                break;
            default:
                Debug.LogWarning("Unknown force type selected");
                break;
        }
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




    public void ButtonAddForce()
    {
        forceType = ForceType.AddForce;
    }
    public void ButtonAddForceAtPosition()
    {
        forceType = ForceType.AddForceAtPosition;
    }
    public void ButtonAddForceRelativeForce()
    {
        forceType = ForceType.AddRelativeForce;
    }


    void Update()
    {
        
    }
}
