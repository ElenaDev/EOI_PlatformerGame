using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : MonoBehaviour
{
    public int turnSpeed;
    public float forceAmount;
    public int direction;

    Rigidbody2D rb2D;

    private void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        if (Random.value < 0.5f) direction = 1;
        else direction = -1;

        rb2D.AddForce(Vector3.up * forceAmount);
        rb2D.AddForce(Vector3.right * forceAmount / 4 * direction);
    }

    void Update()
    {
        transform.Rotate(Vector3.up * turnSpeed * Time.deltaTime);
    }
}
