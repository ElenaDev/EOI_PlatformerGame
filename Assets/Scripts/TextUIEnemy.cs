using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextUIEnemy : MonoBehaviour
{
    public int speed;
    void Start()
    {
        
    }

    void Update()
    {
        transform.Translate(Vector2.up * speed * Time.deltaTime);
    }
}
