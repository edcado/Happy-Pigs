using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineForce : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float stopVelocity = .05f;
    [SerializeField] private float maxShootPower = 50f;
    [SerializeField] private float maxChargeTime = 2f;
    [SerializeField] private float jumpForce;
    [SerializeField] private int maxPoints = 20;

    [Header("Booleans")]
    [SerializeField] private bool isIdle;
    [SerializeField] private bool isAiming;
    public bool isShooting = false;

    [Header("Components")]
    [SerializeField] private Rigidbody body;
    [SerializeField] private Camera mainCamera;

    [SerializeField] private float shootPower = 0f;
    [SerializeField] private float shootAngle = 45f; // �ngulo inicial del disparo
    private Vector3 shootDirection;

    private void Awake()
    {
        body = GetComponent<Rigidbody>();
        mainCamera = Camera.main;
        isAiming = false;
    }

    private void Update()
    {
        if (body.velocity.magnitude < stopVelocity)
        {
            Stop();
        }

        if (!isAiming && !isIdle)
        {
            return;
        }

        if (isAiming)
        {
            ProcessAim();
        }

        if (!isAiming && isShooting)
        {
            FollowBall();
        }
    }

    private void FollowBall()
    {
        Vector3 offset = new Vector3(0f, 10f, -15f); // Ajusta estos valores seg�n tu preferencia

        Vector3 newPosition = transform.position + offset;

        // Interpola suavemente la posici�n actual de la c�mara hacia la nueva posici�n
        mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, newPosition, Time.deltaTime * 5f);

        // Asegura que la c�mara est� siempre mirando hacia la pelota
        mainCamera.transform.LookAt(transform.position);
    }

    private void Stop()
    {
        body.velocity = Vector3.zero;
        body.angularVelocity = Vector3.zero;
        isIdle = true;
    }

    private void OnMouseDown()
    {
        if (isIdle)
        {
            isAiming = true;
            StartCoroutine(ChargeShot());
        }
    }

    private IEnumerator ChargeShot()
    {
        float chargeTime = 0f;
        while (isAiming && chargeTime < maxChargeTime)
        {
            chargeTime += Time.deltaTime;
            shootPower = Mathf.Clamp(chargeTime / maxChargeTime * maxShootPower, 0f, maxShootPower);
            yield return null;
        }
    }

    private void OnMouseUp()
    {
        if (isAiming)
        {
            isAiming = false;
            isShooting = true;
            ShootBall();
        }
    }

    private void ProcessAim()
    {
        UpdateRaycastVisualization();

        // Incrementa el �ngulo con las teclas Q y E (o cualquier otra tecla que desees)
        if (Input.GetKey(KeyCode.Q))
        {
            shootAngle += 1f;
            shootAngle = Mathf.Clamp(shootAngle, 0f, 90f); // Limita el �ngulo entre 0 y 90 grados
        }
        else if (Input.GetKey(KeyCode.E))
        {
            shootAngle -= 1f;
            shootAngle = Mathf.Clamp(shootAngle, 0f, 90f); // Limita el �ngulo entre 0 y 90 grados
        }

        if (Input.GetMouseButtonUp(0))
        {
            isAiming = false;
            isShooting = true;
            ShootBall();
        }
    }

    private void UpdateRaycastVisualization()
    {
        Vector3 origin = transform.position;
        Vector3 direction = Quaternion.Euler(shootAngle, 0, 0) * Vector3.forward; // Calcula la direcci�n del rayo con el �ngulo
        Ray ray = new Ray(origin, direction);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, maxShootPower))
        {
            // Si golpea algo, muestra la l�nea hasta ese punto
            Debug.DrawLine(origin, hit.point, Color.green);
        }
        else
        {
            // Si no golpea nada, muestra la l�nea hasta la m�xima distancia
            Vector3 endPoint = origin + direction.normalized * maxShootPower;
            Debug.DrawLine(origin, endPoint, Color.green);
        }
    }

    private void ShootBall()
    {
        body.velocity = Vector3.zero;
        body.angularVelocity = Vector3.zero;

        Vector3 origin = transform.position;
        // Calcula la direcci�n del disparo en los ejes x, y y z
        Vector3 direction = Quaternion.Euler(shootAngle, 0, 0) * transform.forward;

        Ray ray = new Ray(origin, direction);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, maxShootPower))
        {
            shootDirection = direction.normalized;
            float distance = hit.distance;

            Vector3 forceDirection = ray.direction.normalized;
            Vector3 force = forceDirection * (distance / maxShootPower * shootPower);
            body.AddForce(force, ForceMode.Impulse);
        }

        body.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }
}
