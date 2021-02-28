using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour

    
{

    Animator doorAnim;
    private bool inArea = false;
    private PlayerController pc;
    public bool haskey=false;
   
    // Start is called before the first frame update
    void Start()
    {
       
        doorAnim = transform.parent.GetComponent<Animator>();
        
        

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            inArea = true;
            
        }
    
    }

    public void openDoor()
    {
        if (inArea == true && Input.GetKeyDown(KeyCode.G))
        {

            
                doorAnim.SetBool("open", true);
            
        }
    }
    // Update is called once per frame
    void Update()
    {
        openDoor();
    }
}
