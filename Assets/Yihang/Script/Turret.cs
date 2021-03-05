using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    Rigidbody2D turretRigid;
    private float shootRate = 1f;
    private float nextShootTime;
    private GameManager gameManager;



    [SerializeField] private Transform player;
    [SerializeField] private float followRange=5;
    [SerializeField] private GameObject enemyBullet;

    // Start is called before the first frame update
    void Start()
    {
        turretRigid = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        gameManager = GameManager.instance;

    }

    private void turretShoot()
    {
        if (gameManager.isLoose == false)
        {
            float distanceFromPlayer = Vector2.Distance(player.position, transform.position);

   
            Vector2 rotateAngle = (transform.position-player.position).normalized;

            if (distanceFromPlayer <= followRange && nextShootTime < Time.time)
            {
                transform.right = rotateAngle;
                GameObject _enemyBullet = Instantiate(enemyBullet, turretRigid.position, Quaternion.identity);
                nextShootTime = Time.time + shootRate;

            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        turretShoot();
    }
}
