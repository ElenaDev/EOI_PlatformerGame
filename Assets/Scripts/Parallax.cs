using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Este script va como componente en el background que queremos mover
/// </summary>
public class Parallax : MonoBehaviour
{
    public float distanceX;//esta variable representa como de lejos/cerca queremos que est� ese background
    //a valores menores significar� que el fondo est� m�s lejos (se mueve m�s lento)
    //a valores mayores el fondo est� m�s cerca y se mueve m�s r�pido
    public float smoothingX;

    Transform cam;
    Vector3 previousCamPos;

    void Awake()
    {
        cam = Camera.main.transform;//estoy cogiendo el componente transform de la c�mara que tenga la etiqueta
        //de Main Camera
        previousCamPos = cam.position;
    }

    void Update()
    {
        if(distanceX!=0)
        {
            float parallaxX = (previousCamPos.x - cam.position.x) * distanceX;
            //Calcula la posici�n a la que quiero mover el fondo
            Vector3 backGroundTargetPosX = new Vector3(transform.position.x + parallaxX, transform.position.y,
                transform.position.z);
            //Muevo el background:
            transform.position = Vector3.Lerp(transform.position, backGroundTargetPosX, smoothingX * Time.deltaTime);

            previousCamPos = cam.position;
        }
    }
}
