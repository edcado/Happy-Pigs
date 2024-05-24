using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
public class Bird: MonoBehaviour
{
    private Camera mainCamera;
    public Rigidbody2D rb;
    private Vector3 startPosition, clampedPosition;
    public float force;
    public float maxDistance;
    public float explosionForce, explosionRadious;
    public Vector3 throwVector;
    private Vector3 racoonStartPosition;
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
        racoonStartPosition = transform.position;
    }

    private void OnMouseDrag()
    {
       Vector3 dragPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);//Guarda en una variable la posicion inicial del cursor para calcular la trayectoria del lanzamiento
        clampedPosition = dragPosition;

       float dragDistance = Vector3.Distance(startPosition, dragPosition);//Guarda la posicion final del raton

       if(dragDistance > maxDistance)//Si la distancia entre la posicion final e inicial es mayor que la maxima se aplica la maxima
        {
            clampedPosition = startPosition + (dragPosition - startPosition).normalized * maxDistance;
        }

       if (dragPosition.x > startPosition.x)//Si la distancia entre la posicion final e inicial es menor que la maxima se aplica la calculada
        {

        }
    }
    private void OnMouseUp()
    {
        switch (forceType)//Dependiendo del tipo de Fuerza seleccionado se aplica un tipo de fuerza
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
    private void Throw()
    {
        rb.isKinematic = false;
        throwVector = startPosition - clampedPosition;

        float resetTime = 5f;
        Invoke("Reset", resetTime);
    }

    private void ThrowAddForce()
    {
        Throw();
        rb.AddForce(throwVector * force, ForceMode2D.Force); //En este caso el valor de la fuerza va en aumento en correlacion a la masa.
        //rb.AddForce(throwVector*force,ForceMode2D.VelocityChange); //Este caso no existe para las fuerzas en 2D. Pasa lo mismo con el caso de Acceleration.
    }
    private void ThrowAddForceImpulse() 
    {
        Throw();
        rb.AddForce(throwVector * force, ForceMode2D.Impulse);  //En este caso la fuerza se induce directamente al objeto, usando tambien la masa del mismo.
    }
    private void ThrowAddForceAtPosition()
    {
        Throw();
        rb.AddForceAtPosition(throwVector * force,clampedPosition);
    }
    private void ThrowAddRelativeForce()
    {
        Throw();
        rb.AddRelativeForce(throwVector * force);
    }
    private void ThrowAddExplosionForce()
    {
        //rb.AddExplosionForce(explosionForce,this.transform.position,explosionRadious); //Este caso no existe para las fuerzas en 2D.
    }

    public void Reset()
    {
        transform.position = racoonStartPosition;
        rb.isKinematic = true;
        rb.velocity = new Vector2(0,0);
        mainCamera.GetComponent<CameraMovement>().ResetPosition();
    }

    #region Buttons
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

    public void ButtonReset()
    {
        SceneManager.LoadScene("Juego"); //Reinicia la escena al seleccionar otro tipo de furza
    }
    #endregion
}
