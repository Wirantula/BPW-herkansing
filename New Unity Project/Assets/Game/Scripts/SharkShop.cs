using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharkShop : MonoBehaviour
{
    //check for collision with player
    //check for E key
    //check for coin
    //remove coin from player
    //update inventory display
    //play win sound
    //give gun
    //debug "Get out of here!"

    [SerializeField]
    private AudioClip _youWinAudio;

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player" && Input.GetKeyDown(KeyCode.E))
        {
            Player player = other.GetComponent<Player>();
            if (player != null && player.CoinPickedup == true)
            {
                player.CoinPickedup = false;
                UIManager uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
                if (uiManager != null)
                {
                    uiManager.CoinGiven();
                }
                AudioSource.PlayClipAtPoint(_youWinAudio, transform.position, 1f);
                //give gun
                player.EnableWeapons();
            }
            else
            {
                UIManager uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
                uiManager.PopUpMsg();
            }
        }
    }

}
