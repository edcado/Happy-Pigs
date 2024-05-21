using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Bird: MonoBehaviour
{
    private Camera mainCamera;
    public Rigidbody2D rb;
    private Vector3 startPosition, clampedPosition;
    public float force;
    public float maxDistance;
    public float explosionForce, explosionRadious;
    public enum ForceType
    {
        AddForce,
        AddForceImpulse,
        AddForceAtPosition,
        AddRelativeForce,
        AddExplosionForce
    }

    public ForceType forceType;
    void Start()
    {
        mainCamera = Camera.main;
        rb.GetComponent<Rigidbody2D>();
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
            case ForceType.AddForceImpulse:
                ThrowAddForceImpulse();
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
        rb.AddForce(throwVector * force, ForceMode2D.Force); //En este caso el valor de la fuerza va en aumento en correlacion a la masa.
        //rb.AddForce(throwVector*force,ForceMode2D.VelocityChange); //Este caso no existe para las fuerzas en 2D. Pasa lo mismo con el caso de Acceleration.
    }
    private void ThrowAddForceImpulse() 
    {
        rb.isKinematic=false;
        Vector3 throwVector = startPosition - clampedPosition;
        rb.AddForce(throwVector * force, ForceMode2D.Impulse);  //En este caso la fuerza se induce directamente al objeto, usando tambien la masa del mismo.
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
        //rb.AddExplosionForce(explosionForce,this.transform.position,explosionRadious);
    }




    public void ButtonAddForce()
    {
        forceType = ForceType.AddForce;
        force = 50;
    }
    public void ButtonAddForceImpulse()
    {
        forceType = ForceType.AddForceImpulse;
        force = 1;
    }
    public void ButtonAddForceAtPosition()
    {
        forceType = ForceType.AddForceAtPosition;
        force = 50;
    }
    public void ButtonAddForceRelativeForce()
    {
        forceType = ForceType.AddRelativeForce;
        force = 50;
    }


    void Update()
    {
        
    }
}
