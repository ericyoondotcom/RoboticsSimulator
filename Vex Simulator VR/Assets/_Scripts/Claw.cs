using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Claw : MonoBehaviour
{
    bool closingClaw = false;
    public HingeJoint leftClaw;
    public HingeJoint rightClaw;
    public float clawSpeed;

    void Start()
    {
        
    }

    void FixedUpdate()
    {
#if UNITY_EDITOR
        if (Input.GetKey(KeyCode.RightArrow))
        {
            closingClaw = true;
        }else if (Input.GetKey(KeyCode.LeftArrow))
        {
            closingClaw = false;
        }
#else
        if (Input.GetAxis("SecondaryIndexTrigger"))
        {
            closingClaw = true;
        }
        else if (Input.GetAxis("SecondaryHandTrigger"))
        {
            closingClaw = false;
        }
#endif
        

        var leftMotor = leftClaw.motor;
        leftMotor.targetVelocity = clawSpeed * (closingClaw ? 1 : -1);
        leftClaw.motor = leftMotor;
        var rightMotor = rightClaw.motor;
        rightMotor.targetVelocity = clawSpeed * (closingClaw ? -1 : 1);
        rightClaw.motor = rightMotor;
        
    }
}
