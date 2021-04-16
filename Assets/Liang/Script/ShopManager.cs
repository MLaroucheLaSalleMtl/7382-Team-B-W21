using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    //Liang

    public int[,] shopItems = new int[4, 4];
    //public int coins;
    public Text coinsTxt;
    public GameManager gameManager;
    public GameObject player;
    //public GameObject Panel;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.instance;
        coinsTxt.text = "Coins: " + gameManager.coin.ToString();
        //coins = gameManager.coin;

        //Item ID
        shopItems[1, 1] = 1;
        shopItems[1, 2] = 2;
        shopItems[1, 3] = 3;

        //Item Price
        shopItems[2, 1] = 10;
        shopItems[2, 2] = 20;
        shopItems[2, 3] = 30;

        //Item Quantity
        shopItems[3, 1] = 0;
        shopItems[3, 2] = 0;
        shopItems[3, 3] = 0;

    }

    
    public void Buy()
    {
        //gameManager.canShoot = false;
        //Trying to implement a more smooth UI, but did not find a solution for it.

        GameObject ButtonRef = GameObject.FindGameObjectWithTag("Event").GetComponent<EventSystem>().currentSelectedGameObject;
        
        if (gameManager.coin >= shopItems[2, ButtonRef.GetComponent<ButtonInfo>().itemID])
        {
            gameManager.coin -= shopItems[2, ButtonRef.GetComponent<ButtonInfo>().itemID];
            //gameManager.coin = coins;
            
            if(shopItems[1, ButtonRef.GetComponent<ButtonInfo>().itemID] == 1)
            {
                gameManager.SetInvincible();
                ButtonRef.SetActive(false);
                
            }
            else if (shopItems[1, ButtonRef.GetComponent<ButtonInfo>().itemID] == 2)
            {
                player.GetComponent<PlayerController>().SetUnlimitBullet();
                ButtonRef.SetActive(false);
                
            }
            else if (shopItems[1, ButtonRef.GetComponent<ButtonInfo>().itemID] == 3)
            {
                gameManager.SetFull();
                ButtonRef.SetActive(false);
                
            }
            //shopItems[3, ButtonRef.GetComponent<ButtonInfo>().itemID]++;
            coinsTxt.text = "Coins: " + gameManager.coin.ToString();
            //ButtonRef.GetComponent<ButtonInfo>().quantityTxt.text = shopItems[3, ButtonRef.GetComponent<ButtonInfo>().itemID].ToString();
            //ButtonRef.SetActive(false);
        }
    }
}
