using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletBehaviour : MonoBehaviour
{
    float countDown=3f;
    // Start is called before the first frame update
    void Start()
    {
     
    }

    // Update is called once per frame
    void Update()
    {
        if (countDown > 0)
        {
            countDown -= Time.fixedDeltaTime;
        }
        if (countDown <= 0)
        {
            Destroy(gameObject);
        }
    }
}
