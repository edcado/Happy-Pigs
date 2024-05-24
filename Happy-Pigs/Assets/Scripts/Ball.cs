
using UnityEngine;

public class Ball : MonoBehaviour
{
	public Rigidbody2D rb;
	[HideInInspector] public CircleCollider2D col;
	[HideInInspector] public GameManager gm;

	[HideInInspector] public Vector3 pos { get { return transform.position; } }

	void Awake ()
	{
		rb = GetComponent<Rigidbody2D>();
		col = GetComponent<CircleCollider2D> ();
		gm = GetComponent<GameManager>();
	}
    private void Start()
    {
		//Vuelve al objeto kinematico para que no se vea afectado por la gravedad
		rb.isKinematic = true;
    }

    #region Tipos De Fuerza
    public void ThrowAddForce(Vector2 force)
	{
		rb.AddForce( force, ForceMode2D.Force); //En este caso el valor de la fuerza va en aumento en correlacion a la masa.
															 //rb.AddForce(throwVector*force,ForceMode2D.VelocityChange); //Este caso no existe para las fuerzas en 2D. Pasa lo mismo con el caso de Acceleration.
	}
	public void ThrowAddForceImpulse(Vector2 force)
	{
		rb.AddForce( force, ForceMode2D.Impulse);  //En este caso la fuerza se induce directamente al objeto, usando tambien la masa del mismo.
	}
	public void ThrowAddForceAtPosition(Vector2 force)
	{
		rb.AddForceAtPosition(force, gm.endPoint);
	}
	public void ThrowAddRelativeForce(Vector2 force)
	{
		rb.AddRelativeForce(force);
	}
	public void ThrowAddExplosionForce(Vector2 force)
	{
		//rb.AddExplosionForce(explosionForce,this.transform.position,explosionRadious); //Este caso no existe para las fuerzas en 2D.
	}
    #endregion

    #region Activar/Desactivar RB
    public void ActivateRb ()
	{
		rb.isKinematic = false;
	}

	public void DesactivateRb ()
	{
		rb.velocity = Vector3.zero;
		rb.angularVelocity = 0f;
		rb.isKinematic = true;
	}
    #endregion
}
