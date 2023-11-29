﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PGGE;
using System.Runtime.InteropServices.WindowsRuntime;

public class PlayerMovement : MonoBehaviour
{
    [HideInInspector]
    public CharacterController mCharacterController;
    public Animator mAnimator;

    public float mWalkSpeed = 1.5f;
    public float mRotationSpeed = 50.0f;
    public bool mFollowCameraForward = false;
    public float mTurnRate = 10.0f;

#if UNITY_ANDROID
    public FixedJoystick mJoystick;
#endif

    private float hInput;
    private float vInput;
    private float speed;
    private float acceleration = 3f;
    public bool flying = false;
    private bool crouch = false;
    public float mGravity = -30.0f;
    public float mJumpHeight = 1.0f;

    private Vector3 mVelocity = new Vector3(0.0f, 0.0f, 0.0f);

    void Start()
    {
        mCharacterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        //HandleInputs();
        //Move();
    }

    private void FixedUpdate()
    {
        ApplyGravity();
    }

    public void HandleInputs()
    {
        // We shall handle our inputs here.
    #if UNITY_STANDALONE
        hInput = Input.GetAxis("Horizontal");
        vInput = Input.GetAxis("Vertical");
#endif

#if UNITY_ANDROID
        hInput = 2.0f * mJoystick.Horizontal;
        vInput = 2.0f * mJoystick.Vertical;
#endif
        //if holding down leftshift
        if (Input.GetKey(KeyCode.LeftShift))
        {
            //and speed is below or equals to double of mWalkSpeed
            if (speed <= mWalkSpeed * 2)
            {
                //increase speed by acceleration times time.delta time
                //this increases the speed gradually
                speed += acceleration * Time.deltaTime;
            } 
        }
        // if not holding down leftshift
        else
        {
            //and speed is bigger then walkspeed
            if (speed >= mWalkSpeed)
            {
                //decrease speed by acceleration times time.delta time
                //this decreases the speed gradually
                speed -= acceleration * Time.deltaTime;
            }
                
        }

        speed = Mathf.Clamp(speed, mWalkSpeed, mWalkSpeed * 2);

        //if (Input.GetKey(KeyCode.LeftShift))
        //{
        //    speed = mWalkSpeed * 2;
        //}
        //else
        //{
        //    speed = mWalkSpeed;
        //}

        //if press space
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //toggle the flying bool 
            flying = !flying;
            //set the flying bool in the animator to the same as flying bool
            mAnimator.SetBool("Flying", flying);
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            crouch = !crouch;
            Crouch();
            Debug.Log(crouch);
        }
    }

    public void Move()
    {
        if (crouch) return;

        // We shall apply movement to the game object here.
        if (mAnimator == null) return;
        if (mFollowCameraForward)
        {
            // rotate Player towards the camera forward.
            Vector3 eu = Camera.main.transform.rotation.eulerAngles;
            transform.rotation = Quaternion.RotateTowards(
                transform.rotation,
                Quaternion.Euler(0.0f, eu.y, 0.0f),
                mTurnRate * Time.deltaTime);
        }
        else
        {
            transform.Rotate(0.0f, hInput * mRotationSpeed * Time.deltaTime, 0.0f);
        }

        Vector3 forward = transform.TransformDirection(Vector3.forward).normalized;
        forward.y = 0.0f;

        mCharacterController.Move(forward * vInput * speed * Time.deltaTime);
        mAnimator.SetFloat("PosX", 0);
        mAnimator.SetFloat("PosZ", vInput * speed / (2.0f * mWalkSpeed));

        
    }

    

    private Vector3 HalfHeight;
    private Vector3 tempHeight;
    void Crouch()
    {
        mAnimator.SetBool("Crouch", crouch);
        if(crouch)
        {
            tempHeight = CameraConstants.CameraPositionOffset;
            HalfHeight = tempHeight;
            HalfHeight.y *= 0.5f;
            CameraConstants.CameraPositionOffset = HalfHeight;
        }
        else
        {
            CameraConstants.CameraPositionOffset = tempHeight;
        }
    }

    void ApplyGravity()
    {
        // apply gravity.
        mVelocity.y += mGravity * Time.deltaTime;
        if (mCharacterController.isGrounded && mVelocity.y < 0)
            mVelocity.y = 0f;
    }
}
