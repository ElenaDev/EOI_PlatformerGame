using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Este script va como componente en el background que queremos mover
/// </summary>
public class Parallax : MonoBehaviour
{
    public float distanceX;//esta variable representa como de lejos/cerca queremos que esté ese background
    //a valores menores significará que el fondo está más lejos (se mueve más lento)
    //a valores mayores el fondo está más cerca y se mueve más rápido
    public float smoothingX;

    Transform cam;
    Vector3 previousCamPos;

    void Awake()
    {
        cam = Camera.main.transform;//estoy cogiendo el componente transform de la cámara que tenga la etiqueta
        //de Main Camera
        previousCamPos = cam.position;
    }

    void Update()
    {
        if(distanceX!=0)
        {
            float parallaxX = (previousCamPos.x - cam.position.x) * distanceX;
            //Calcula la posición a la que quiero mover el fondo
            Vector3 backGroundTargetPosX = new Vector3(transform.position.x + parallaxX, transform.position.y,
                transform.position.z);
            //Muevo el background:
            transform.position = Vector3.Lerp(transform.position, backGroundTargetPosX, smoothingX * Time.deltaTime);

            previousCamPos = cam.position;
        }
    }
}
