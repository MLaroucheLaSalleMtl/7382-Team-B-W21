using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dialogue2 : MonoBehaviour
{
    [SerializeField] private GameObject reminder;
    private float countDown;
    // Start is called before the first frame update
    void Start()
    {
        reminder.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            reminder.SetActive(true);
            countDown = 3f;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (countDown > 0)
        {
            countDown -= Time.deltaTime;
        }
        else if (countDown <= 0)
        {
            reminder.SetActive(false);
        }
    }
}
