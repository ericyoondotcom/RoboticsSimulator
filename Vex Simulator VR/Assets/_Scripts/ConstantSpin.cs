using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Borrowed from Kartfinity
public class ConstantSpin : MonoBehaviour
{
    public Vector3 spinPerSecond;

    private void Update()
    {
        transform.Rotate(spinPerSecond * Time.deltaTime);
    }

}
