using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bloodEffect : MonoBehaviour
{
    public float timer = 1;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
