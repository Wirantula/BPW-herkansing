using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{

    [SerializeField]
    private AudioClip _coinSound;

    //check for collision (ontrigger)
    void OnTriggerStay(Collider other)
    {
        //check if player
        //check for E press
        if (other.tag == "Player" && Input.GetKeyDown(KeyCode.E))
        {
            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                player.CoinPickedup = true;
                AudioSource.PlayClipAtPoint(_coinSound, transform.position, 1f);
                UIManager uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();

                if (uiManager != null)
                {
                    uiManager.CoinPickedUp();
                }

                Destroy(this.gameObject);
                //give player the coin
                //play coin sound!
                //destroy the coin
            }
        }
    }
    





}
