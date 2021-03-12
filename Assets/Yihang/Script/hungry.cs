using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class hungry : MonoBehaviour
{

    public Slider hungrySlider;


    public void maxEnegry(float enegry)
    {
        hungrySlider.maxValue = enegry;


    }


    public void getEnegry(float enegry)
    {
        hungrySlider.value = enegry;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
