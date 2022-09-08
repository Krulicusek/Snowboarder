using Assets.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float torqueAmmount = 1f;
    [SerializeField] private float boostSpeed = 50f;
    [SerializeField] private float baseSpeed = 20f;

    private Rigidbody2D rigid;
    private SurfaceEffector2D surfaceEffector2D;
    private GroundDetector detector;
    private ButterflyDetectorBack butterflyDetectorBack;
    private ButterflyDetectorFront butterflyDetectorFront;
    private float timeInAir = 0;
    private float timeInButterfly = 0;
    private float previousRotation = 0;
    private bool  isRotating = false;
    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        surfaceEffector2D = FindObjectOfType<SurfaceEffector2D>();
        detector = transform.Find("GroundDetector").GetComponent<GroundDetector>();
        butterflyDetectorBack = transform.Find("ButterflyDetectorBack").GetComponent<ButterflyDetectorBack>();
        butterflyDetectorFront = transform.Find("ButterflyDetectorFront").GetComponent<ButterflyDetectorFront>();
    }

    // Update is called once per frame
    private void Update()
    {
        RotatePlayer();
        RespondToBoost();
        CalculatePointsForFlying();
        CalculatePointsForButterfly();
    }
    private void CalculatePointsForButterfly()
    {
        if ((butterflyDetectorBack.IsTouchingGround && !butterflyDetectorFront.IsTouchingGround)
            || butterflyDetectorFront.IsTouchingGround && !butterflyDetectorBack.IsTouchingGround)
        {
            timeInButterfly += Time.deltaTime;
            if (timeInButterfly >= 0.5)
            {
                ScoreManager.instance.AddPointsForButterfly(timeInButterfly);
                timeInButterfly = Time.deltaTime;
            }
        }
        else if (detector.IsTouchingGround)
        {
            timeInButterfly = Time.deltaTime;
        }
        else
        {
            timeInButterfly += Time.deltaTime;
        }
    }
    private void CalculatePointsForFlying()
    {
        if (!detector.IsTouchingGround)
        {
            timeInAir += Time.deltaTime;
            if (timeInAir >= 0.5)
            {
                ScoreManager.instance.AddPointsForFlying(timeInAir);
                timeInAir = Time.deltaTime;
            }
        }
        else
        {
            timeInAir = Time.deltaTime;
        }
    }
    private void RespondToBoost()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            surfaceEffector2D.speed = boostSpeed;
        }
        else
        {
            surfaceEffector2D.speed = baseSpeed;
        }
    }

    private void RotatePlayer()
    {        
        if (Input.GetKey(KeyCode.A))
        {
            rigid.AddTorque(torqueAmmount);
            isRotating = true;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            rigid.AddTorque(-torqueAmmount);
            isRotating = true;
        }
        else
        {
            isRotating = false;
        }
        if (isRotating)
        {
            if (!detector.IsTouchingGround)
            {
                ScoreManager.instance.AddPointsForRotation(Math.Abs(rigid.rotation - previousRotation));
                previousRotation = rigid.rotation;
            }
        }
    }

}
