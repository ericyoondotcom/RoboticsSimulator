using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SineHover : MonoBehaviour
{
    public float amount;
    public float time;

    float yPosOriginal = 0;
    float t;

    void Start()
    {
        yPosOriginal = transform.position.y;
    }


    void Update()
    {
        float sine = amount * Mathf.Sin(Mathf.PI * 2 * t / time);
        transform.position = new Vector3(transform.position.x, yPosOriginal + sine, transform.position.z);
        t += Time.deltaTime;
        t %= time;
    }
}
