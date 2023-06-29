using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinController : MonoBehaviour
{
    [SerializeField] private float destroyTime;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other) {

        if (other.gameObject.CompareTag("Player"))
        {
            GameController.instance.CoinsCount += 1; 
            Debug.Log($"Coins: {GameController.instance.CoinsCount}");
            Destroy(this.gameObject, destroyTime);
        }
    }
}
