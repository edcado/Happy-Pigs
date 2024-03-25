using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Controller : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private Transform target;
    private Vector3 previousPosition;

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            previousPosition = cam.ScreenToViewportPoint(Input.mousePosition);
        }

        if (Input.GetMouseButton(1))
        {
            Vector3 direction = previousPosition - cam.ScreenToViewportPoint(Input.mousePosition);

            cam.transform.position = target.position;

            cam.transform.Rotate(new Vector3(1, 0, 0), direction.y * 180);
            cam.transform.Rotate(new Vector3(0 ,1 ,0), -direction.x * 180, Space.World);
            cam.transform.Translate(new Vector3(0, 0, -10));

            previousPosition = cam.ScreenToViewportPoint(Input.mousePosition);
        }
    }
}
