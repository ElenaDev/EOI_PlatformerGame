using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI textCoinUI;//El texto que est� en la interfaz para mostrar las monedas
    
    int numCoins; //El n� de monedas que lleva el player

    #region Singleton
    //Creaci�n del singleton, es decir, la clase GameManager solo va a tener una instancia
    public static GameManager gameManager;
    private void Awake()
    {
        gameManager = this;
    }
    #endregion

    public void AddCoins()
    {
        numCoins++;
        textCoinUI.text = "" + numCoins;
    }
}
