using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonInfo : MonoBehaviour
{

    public int itemID;
    public Text priceTxt;
    public Text quantityTxt;
    public GameObject shopManager;

    // Update is called once per frame
    void Update()
    {
        priceTxt.text = "Price: " + shopManager.GetComponent<ShopManager>().shopItems[2, itemID].ToString();
        quantityTxt.text = shopManager.GetComponent<ShopManager>().shopItems[3, itemID].ToString();
    }
}
