using System;
using UnityEngine;
using TMPro;

public class GameController : MonoBehaviour
{
    public static GameController instance;

    [Header("Coins System")]
    public int CoinsCount = 0;
    [SerializeField] TextMeshProUGUI CoinCounter_TxT;

    void Awake()
    {
        instance = this;
    }

    private void Update() {
        
    }

    private void FixedUpdate() {

        // Set the coin number on the txt
        CoinCounter_TxT.SetText(CoinsCount.ToString());
    }
}
