using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using cakeslice;

using static Enums;

[RequireComponent(typeof(Rigidbody))]
public class ClawGrabbable : MonoBehaviour
{
    public Collider collider;

    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other)
    {
        Claw claw = other.GetComponent<Claw>();
        if (claw == null)
        {
            return;
        }
        claw.ClawGrabbableEntered(this);
    }

    private void OnTriggerExit(Collider other)
    {
        Claw claw = other.GetComponent<Claw>();
        if (claw == null)
        {
            return;
        }
        claw.ClawGrabbableExited(this);
    }

    public void OnGrabStart()
    {
        rb.isKinematic = true;
        if (collider) collider.enabled = false;
    }
    public void OnGrabEnd()
    {
        rb.isKinematic = false;
        if (collider) collider.enabled = true;
    }

}
