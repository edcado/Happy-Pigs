using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrajectoryLine : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Bird _birdScript;
    [SerializeField] private Transform _birdSpawnPoint;

    [Header("Trajectory Line Smoothness/Length")]
    [SerializeField] private int _segmentCount = 50;
    [SerializeField] private float _curveLength = 3.5f;

    private Vector2[] _segments;
    private LineRenderer _lineRenderer;

    private float birdSpeed;
    private float birdGravity;
    void Start()
    {
        //Inicializa los segmentos
        _segments = new Vector2[_segmentCount];

        _lineRenderer = GetComponent<LineRenderer>();
        _lineRenderer.positionCount = _segmentCount;
    }

    void Update()
    {
        Vector2 startPos = _birdSpawnPoint.position;
        _segments[0] = startPos;
        _lineRenderer.SetPosition(0, startPos);

        Vector2 startVelocity = _birdScript.throwVector * _birdScript.force;

        for(int i = 1; i < -_segmentCount; i++)
        {
            //
            float timeOffset = (i * Time.fixedDeltaTime * _curveLength);


            Vector2 gravityOffset = 0.5f * Physics2D.gravity * _birdScript.rb.gravityScale * Mathf.Pow(timeOffset, 2);

            //
            _segments[i] = _segments[0] + startVelocity * timeOffset + gravityOffset;
            _lineRenderer.SetPosition(i, _segments[i]);
        }
    }
}
