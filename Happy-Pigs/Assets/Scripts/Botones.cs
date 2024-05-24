using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Botones : MonoBehaviour
{
    private Bird bird;
    public enum ForceType
    {
        AddForce,
        AddForceImpulse,
        AddForceAtPosition,
        AddRelativeForce,
        AddExplosionForce
    }

    public ForceType forceType;

    public void ButtonAddForce()
    {
        forceType = ForceType.AddForce;
        bird.force = 50;
    }
    public void ButtonAddForceImpulse()
    {
        forceType = ForceType.AddForceImpulse;
        bird.force = 1;
    }
    public void ButtonAddForceAtPosition()
    {
        forceType = ForceType.AddForceAtPosition;
        bird.force = 50;
    }
    public void ButtonAddForceRelativeForce()
    {
        forceType = ForceType.AddRelativeForce;
        bird.force = 50;
    }

    public void ButtonReset()
    {
        SceneManager.LoadScene("Juego"); //Reinicia la escena al seleccionar otro tipo de furza
    }
}
