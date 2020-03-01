using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using cakeslice;

using static Enums;

[RequireComponent(typeof(Rigidbody))]
public class Grabbable : MonoBehaviour
{
    public Outline outline;

    [System.NonSerialized]
    public OculusGrab grabber;

    Dictionary<Side, bool> isClosest;
    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        outline.enabled = false;
        isClosest = new Dictionary<Side, bool>()
        {
            {Side.Left, false },
            {Side.Right, false }
        };
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        OculusGrab grabScript = other.GetComponent<OculusGrab>();
        if(grabScript == null)
        {
            return;
        }
        grabScript.GrabbableEnteredRange(this);
    }

    private void OnTriggerExit(Collider other)
    {
        OculusGrab grabScript = other.GetComponent<OculusGrab>();
        if (grabScript == null)
        {
            return;
        }
        grabScript.GrabbableOutOfRange(this);
    }

    public void OnClosestStateChanged(bool closest, Side hand)
    {
        isClosest[hand] = closest;
        outline.enabled = isClosest[Side.Left] || isClosest[Side.Right];
    }

    public void InternalOnGrabStart(OculusGrab grabber)
    {
        this.grabber = grabber;
        rb.isKinematic = true;
    }
    public void InternalOnGrabEnd(OculusGrab grabber)
    {
        this.grabber = null;
        rb.isKinematic = false;
    }

    public virtual void OnGrabStart() { }
    public virtual void OnGrabEnd() { }

}
