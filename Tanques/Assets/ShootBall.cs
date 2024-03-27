using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ShootBall : MonoBehaviour
{
    [SerializeField] private TrajectoryLine trajectoryLine;

    [SerializeField] private GameObject GolfBall;

    [SerializeField] private Transform Ball;

    [SerializeField, Min(1)] private float GolfBallMass;

    [SerializeField] private float BaseSpeed = 3;


    [SerializeField, Min(3)] private float MaxTimeClick = 5f;
    private float TimeCount;

    [SerializeField] private float shootDelay = 1f;
    private bool isWaiting = false;

    private float initialVelocity;

    private Time clickTime;

    private UnityEvent OnShoot;
    void Start()
    {

    }


    void Update()
    {
        trajectoryLine.ShowTrajectoryLine(Ball.position,shootForce());
        if (Input.GetKeyDown(KeyCode.Mouse0))
            shootForce();
        if (Input.GetKeyUp(KeyCode.Mouse0))
            shootForce();
        
    }

    private void TimeController()
    {
        TimeCount = 0;
        TimeCount += Time.deltaTime;
    }


    private Vector3 shootForce()
    {
        if (TimeCount < MaxTimeClick)
        {
            Vector3 InitialSpeed = new Vector3 (BaseSpeed * TimeCount, 0);
            return InitialSpeed;
        else
        {
            Vector3 InitialSpeed = new Vector3 (BaseSpeed * MaxTimeClick, 0);
            return InitialSpeed;
        }
    }

    
}
