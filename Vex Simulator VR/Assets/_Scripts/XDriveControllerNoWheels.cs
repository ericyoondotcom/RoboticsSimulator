using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using static Enums;

[RequireComponent(typeof(Rigidbody))]
public class XDriveControllerNoWheels : MonoBehaviour
{
    public float maxSpeed;
    public float maxTorque;
    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
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

        rb.AddForce(driveVertical * maxSpeed * Time.fixedDeltaTime, 0, -driveHorizontal * maxSpeed * Time.fixedDeltaTime);
        rb.angularVelocity = driveRotation * maxTorque * Time.fixedDeltaTime * Vector3.up;
    }
}