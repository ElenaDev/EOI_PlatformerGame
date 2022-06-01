using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardManager : MonoBehaviour
{
   /* public Image imageCardUI;
    public TextMeshProUGUI textNameCardUI;
    public TextMeshProUGUI textHealthCardUI;
    public TextMeshProUGUI textForceCardUI;*/

    //array de scriptable objects
    public SOCard[] cards;//aquí metemos el scriptable object
    public Transform dataContainer;//El elemento de la interfaz que contiene el horizontal layout group
    //las cartas tienen que ir como hijas de este gameobject
    public GameObject cardPrefab;
    void Start()
    {
        //Asignación de los datos del scriptable object a los elementos de la interfaz:
        /* imageCardUI.sprite = card.imageCard;
         imageCardUI.preserveAspect = true;//para que conserve el aspect ratio de la imagen

         textNameCardUI.text = card.nameCard;
         textHealthCardUI.text = card.healthCard.ToString();//Tenemos que poner el ToString() porque la variable
         //healthCard es tipo int y queremos mostrarla por un text
         textForceCardUI.text = card.forceCard.ToString();*/
        CardCreator();
    }
    void CardCreator()
    {
        for(int i=0; i < cards.Length; i++)
        {
            //Instanciamos la carta
            GameObject cardClone = Instantiate(cardPrefab);
            //Ponemos la carta como hija del dataContainer
            cardClone.transform.SetParent(dataContainer);

            //
            CardUI cardUI = cardClone.GetComponent<CardUI>();
            cardUI.imageCardUI.sprite = cards[i].imageCard;
            cardUI.imageCardUI.preserveAspect = true;
            cardUI.textNameCardUI.text = cards[i].nameCard;
            cardUI.textHealthCardUI.text = cards[i].healthCard.ToString();
            cardUI.textForceCardUI.text = cards[i].forceCard.ToString();
            //

            /*cardClone.GetComponent<Image>().sprite = cards[i].imageCard;
            cardClone.GetComponent<Image>().preserveAspect = true;
            cardClone.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = cards[i].nameCard;
            cardClone.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = cards[i].healthCard.ToString();
            cardClone.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = cards[i].forceCard.ToString();*/
        }
    }

}
