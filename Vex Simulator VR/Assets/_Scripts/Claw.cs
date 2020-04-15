using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Claw : MonoBehaviour
{
    bool closingClaw = false;
    public float clawAnimTime;
    public Vector3 rotationStart;
    public Vector3 rotationEnd;
    public Transform leftClaw;
    public Transform rightClaw;
    public float controllerTriggerThreshold = 0.7f;

    float t = 0;
    List<(ClawGrabbable grabbable, Transform oldParent)> insideClaw = new List<(ClawGrabbable grabbable, Transform oldParent)>();

    void Start()
    {
        
    }

    void FixedUpdate()
    {
#if UNITY_EDITOR
        if (Input.GetKey(KeyCode.RightArrow) && !closingClaw)
        {
            StartClosingClaw();
        }
        else if (Input.GetKey(KeyCode.LeftArrow) && closingClaw)
        {
            StartOpenClaw();
        }
#else
        if (OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTrigger) > controllerTriggerThreshold && !closingClaw)
        {
            StartClosingClaw();
        }
        else if (OVRInput.Get(OVRInput.Button.One, OVRInput.Controller.RTouch) && closingClaw)
        {
            StartOpenClaw();
        }
#endif

        if (closingClaw)
        {
            leftClaw.localRotation = Quaternion.Lerp(Quaternion.Euler(rotationStart), Quaternion.Euler(rotationEnd), t / clawAnimTime);
            rightClaw.localRotation = Quaternion.Lerp(Quaternion.Euler(-rotationStart), Quaternion.Euler(-rotationEnd), t / clawAnimTime);
        }
        else
        {
            leftClaw.localRotation = Quaternion.Lerp(Quaternion.Euler(rotationEnd), Quaternion.Euler(rotationStart), t / clawAnimTime);
            rightClaw.localRotation = Quaternion.Lerp(Quaternion.Euler(-rotationEnd), Quaternion.Euler(-rotationStart), t / clawAnimTime);
        }
        t += Time.fixedDeltaTime;
        
    }

    public void ClawGrabbableEntered(ClawGrabbable other)
    {
        insideClaw.Add((other, other.transform.parent));
    }
    public void ClawGrabbableExited(ClawGrabbable other)
    {
        insideClaw.Remove((other, other.transform.parent));
    }

    void StartClosingClaw()
    {
        closingClaw = true;
        t = 0;
        foreach(var obj in insideClaw)
        {
            obj.grabbable.OnGrabStart();
            obj.grabbable.gameObject.transform.parent = transform;
        }
    }
    void StartOpenClaw()
    {
        closingClaw = false;
        t = 0;
        foreach (var obj in insideClaw)
        {
            obj.grabbable.OnGrabEnd();
            obj.grabbable.gameObject.transform.parent = obj.oldParent;
        }
        insideClaw.Clear();
    }
}
