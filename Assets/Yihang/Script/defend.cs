using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class defend : MonoBehaviour
{
    public Slider defendSlider;


    public void MaxDefend(float dValue)
    {
        defendSlider.maxValue = dValue;


    }


    public void getDefend(float dValue)
    {
       defendSlider.value = dValue;
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
