using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BallHandler : MonoBehaviour
{
    [SerializeField] private GameObject ballPrefab;
    [SerializeField] private Rigidbody2D ballPivot;
    [SerializeField] private float delayTime = 0.5f;
    [SerializeField] private float respawnDelay = 1f;

    private Camera mainCamera;
    private bool isDragging;
    private Rigidbody2D currentBallRigidbody2D;
    private SpringJoint2D currentBallSpringJoint;


    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
        SpawnBall();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentBallRigidbody2D == null)
        {
            return;
        }

        if (!Touchscreen.current.primaryTouch.press.isPressed)
        {
            if (isDragging)
            {
                LaunchBall();
            }

            isDragging = false;

            return;
        }

        isDragging = true;

        currentBallRigidbody2D.isKinematic = true;

        Vector2 touchPosition = Touchscreen.current.primaryTouch.position.ReadValue();

        Vector3 worldPosition = mainCamera.ScreenToWorldPoint(touchPosition);

        //Debug.Log("Touch screen here: " + worldPosition);

        currentBallRigidbody2D.position = worldPosition;



    }

    private void SpawnBall()
    {
        GameObject ballInstance = Instantiate(ballPrefab, ballPivot.position, Quaternion.identity);

        currentBallRigidbody2D = ballInstance.GetComponent<Rigidbody2D>();
        currentBallSpringJoint = ballInstance.GetComponent<SpringJoint2D>();

        currentBallSpringJoint.connectedBody = ballPivot;
    }

    private void LaunchBall()
    {
        currentBallRigidbody2D.isKinematic = false;
        currentBallRigidbody2D = null;

        Invoke(nameof(DetachBall),delayTime);


    }

    private void DetachBall()
    {
        currentBallSpringJoint.enabled = false;
        currentBallSpringJoint = null;

        Invoke(nameof(SpawnBall), respawnDelay);
    }
}
