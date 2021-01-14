using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using static Enums;

public class XDriveController : MonoBehaviour
{
    public WheelCollider wheelFL; // A
    public WheelCollider wheelBL; // B
    public WheelCollider wheelBR; // C
    public WheelCollider wheelFR; // D

    public float maxMotorTorque;
    public float brakeTorque;

    public void FixedUpdate()
    {
        float driveVertical;
        float driveHorizontal;
        float driveRotation;

#if UNITY_EDITOR
        driveVertical = Input.GetKey(KeyCode.W) ? 1 : (Input.GetKey(KeyCode.S) ? -1 : 0);
        driveHorizontal = Input.GetKey(KeyCode.D) ? 1 : (Input.GetKey(KeyCode.A) ? -1 : 0);
        driveRotation = Input.GetKey(KeyCode.RightArrow) ? 1 : (Input.GetKey(KeyCode.LeftArrow) ? -1 : 0);
#else
        driveVertical = Input.GetAxis("Oculus_CrossPlatform_PrimaryThumbstickVertical");
        driveHorizontal = Input.GetAxis("Oculus_CrossPlatform_PrimaryThumbstickHorizontal");
        driveRotation = Input.GetAxis("Oculus_CrossPlatform_SecondaryThumbstickHorizontal");
#endif
        wheelFL.steerAngle = -45;
        wheelBL.steerAngle = 45;
        wheelBR.steerAngle = -45;
        wheelFR.steerAngle = 45;

        wheelFL.motorTorque = (driveHorizontal + driveVertical + driveRotation) * maxMotorTorque;
        wheelBL.motorTorque = (driveHorizontal - driveVertical - driveRotation) * maxMotorTorque;
        wheelBR.motorTorque = (driveHorizontal + driveVertical - driveRotation) * maxMotorTorque;
        wheelFR.motorTorque = (driveHorizontal - driveVertical + driveRotation) * maxMotorTorque;

        if(Mathf.Abs(driveVertical) < float.Epsilon && Mathf.Abs(driveHorizontal) < float.Epsilon && Mathf.Abs(driveRotation) < float.Epsilon)
        {
            wheelFL.brakeTorque = brakeTorque;
            wheelBL.brakeTorque = brakeTorque;
            wheelBR.brakeTorque = brakeTorque;
            wheelFR.brakeTorque = brakeTorque;
        }
        else
        {
            wheelFL.brakeTorque = 0;
            wheelBL.brakeTorque = 0;
            wheelBR.brakeTorque = 0;
            wheelFR.brakeTorque = 0;
        }

        print("FL: " + wheelFL.motorTorque + ", BL: " + wheelBL.motorTorque + ", BR: " + wheelBR.motorTorque + ", FR: " + wheelFR.motorTorque);
    }
}