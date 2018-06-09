using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickup : MonoBehaviour {
    [SerializeField] AudioClip coinPickupSFX;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        AudioSource.PlayClipAtPoint(coinPickupSFX, Camera.main.transform.position);
        GameSession gameSession = FindObjectOfType<GameSession>();
        if(gameSession)
        {
            gameSession.AddToScore(100);
        }
        
        Destroy(this.gameObject);
    }
}
