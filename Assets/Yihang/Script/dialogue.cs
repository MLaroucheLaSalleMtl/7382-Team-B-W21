using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dialogue : MonoBehaviour
{
    [SerializeField] private GameObject reminder;
    [SerializeField] private GameObject nokeyReminder;


    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        reminder.SetActive(false);
        nokeyReminder.SetActive(false);
        gameManager = GameManager.instance;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            reminder.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            reminder.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.inDoorArea == true && gameManager.hasKey == false)
        {

            nokeyReminder.SetActive(true);
        }
        else if (gameManager.inDoorArea == false)
        {
            nokeyReminder.SetActive(false);
        }
        
    }
}
