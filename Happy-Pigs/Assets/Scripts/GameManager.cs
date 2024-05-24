
using UnityEngine;

public class GameManager : MonoBehaviour
{
	#region Singleton class: GameManager

	public static GameManager Instance;

	void Awake ()
	{
		if (Instance == null) {
			Instance = this;
		}
	}

	#endregion

	Camera cam;

	public float explosionForce, explosionRadious;
	private Vector3 racoonStartPosition;
	public Ball ball;
	public Trajectory trajectory;
	[SerializeField] float pushForce = 4f;

	bool isDragging = false;

	Vector2 startPoint;
	public Vector2 endPoint;
	Vector2 direction;
	Vector2 force;
	float distance;
	public enum ForceType
	{
		AddForce,
		AddForceImpulse,
		AddForceAtPosition,
		AddRelativeForce,
		AddExplosionForce
	}

	public ForceType forceType;
	//---------------------------------------
	void Start ()
	{
		cam = Camera.main;
		racoonStartPosition = ball.transform.position;

	}

	void Update ()
	{
		if (Input.GetMouseButtonDown (0)) {
			isDragging = true;
			OnDragStart ();
		}
		if (Input.GetMouseButtonUp (0)) {
			isDragging = false;
			OnDragEnd ();
		}

		if (isDragging) {
			OnDrag ();
		}
	}

	//-Drag--------------------------------------
	void OnDragStart ()
	{
		ball.DesactivateRb();
		startPoint = cam.ScreenToWorldPoint (Input.mousePosition);

		trajectory.Show ();
	}

	void OnDrag ()
	{
		endPoint = cam.ScreenToWorldPoint (Input.mousePosition);
		distance = Vector2.Distance (startPoint, endPoint);
		direction = (startPoint - endPoint).normalized;
		force = direction * distance * pushForce;

		trajectory.UpdateDots (ball.pos, force);
	}

	void OnDragEnd ()
	{
		//push the ball
		ball.ActivateRb ();

		ball.ThrowAddForceImpulse(force);

		trajectory.Hide();

		float resetTime = 5f;
		Invoke("Reset", resetTime);
	}


	private void OnMouseUp()
	{
        switch (forceType)//Dependiendo del tipo de Fuerza seleccionado se aplica un tipo de fuerza
        {
            case ForceType.AddForce:
                ball.ThrowAddForce(force);
                break;
            case ForceType.AddForceImpulse:
				ball.ThrowAddForceImpulse(force);
				break;
            case ForceType.AddForceAtPosition:
                ball.ThrowAddForceAtPosition(force);
                break;
            case ForceType.AddRelativeForce:
                ball.ThrowAddRelativeForce(force);
                break;
            case ForceType.AddExplosionForce:
                ball.ThrowAddExplosionForce(force);
                break;
            default:
				ball.ThrowAddForce(force);
				break;
        }
    }


	public void Reset()
	{
		ball.rb.transform.rotation = Quaternion.Euler(0,0,0);
		ball.rb.angularVelocity = 0;
		ball.transform.position = racoonStartPosition;
		ball.rb.velocity = new Vector3(0, 0,0);
		ball.rb.isKinematic = true;
		cam.GetComponent<CameraMovement>().ResetPosition();
	}
}
