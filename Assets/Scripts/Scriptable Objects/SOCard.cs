using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Card", menuName ="Card")]
public class SOCard : ScriptableObject
{
    public string nameCard;
    public int healthCard;
    public float forceCard;

    public Sprite imageCard;
}
