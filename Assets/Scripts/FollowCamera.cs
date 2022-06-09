using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public Transform target;
    public float smoothing;

    Vector3 offset;
    void Start()
    {
        offset = transform.position - target.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 posCamera = target.position + offset;

        transform.position = Vector3.Lerp(transform.position, posCamera, smoothing * Time.deltaTime);
    }
}
