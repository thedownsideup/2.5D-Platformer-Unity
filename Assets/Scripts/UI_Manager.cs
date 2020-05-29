using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Manager : MonoBehaviour
{

    [SerializeField]
    private Text _coinsText;
    [SerializeField]
    private Text _livesText;

    void Start () 
    {
        _coinsText.text = "Coins: " + 0;
        _livesText.text = "Lives: " + 3;
    }

    public void UpdateCoins(int coins)
    {
        _coinsText.text = "Coins: " + coins;
    }

    public void UpdateLives(int lives)
    {
        _livesText.text = "Lives: " + lives;
    }
}
