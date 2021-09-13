using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Player stats
    public float speed = 5.0f;

    // Game objects for powerups
    public GameObject powerupIndicatorManager;
    public GameObject homingRocket;
    private GameObject tempRocket;
    private PowerupManager powerupManager;

    // Pound Powerup stats
    private float poundPowerupStrength = 20.0f;
    private float poundSpeed = 5;
    private bool onGround = true;
    private bool canGroundPound = true;
    private float maxPoundDistance = 20.0f;
    
    //Bounce power up stats
    private float bouncePowerupStrength = 10.0f;

    // Player movement objects
    private Rigidbody playerRb;
    private GameObject focalPoint;

    // Start is called before the first frame update
    void Start()
    {
        powerupManager = powerupIndicatorManager.GetComponent<PowerupManager>();
        playerRb = GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("Focal Point");
    }

    // Update is called once per frame
    void Update()
    {
        HandleMovement();
        HandlePowerupTrigger();
    }

    // Handle vertical movement
    void HandleMovement()
    {
        float verticalInput = Input.GetAxis("Vertical");
        playerRb.AddForce(focalPoint.transform.forward * verticalInput * speed);
        powerupIndicatorManager.transform.position = transform.position + new Vector3(0, -0.5f, 0);
    }


    // Handle the player triggered powerups (rockets, pound, etc.)
    void HandlePowerupTrigger()
    {
        // Handle the space bar powerup triggers
        if(Input.GetKeyDown(KeyCode.Space))
        {
            // If the powerup is rockets shoot them else if ground pound trigger the ground pound attack
            if (powerupManager.currentPowerup == PowerupManager.PowerupTypes.Rockets)
            {
                foreach (var enemy in FindObjectsOfType<Enemy>())
                {
                    tempRocket = Instantiate(homingRocket, transform.position + Vector3.up, Quaternion.identity);
                    tempRocket.GetComponent<HomingRocket>().SetTarget(enemy.transform);
                }
            }
            else if (powerupManager.currentPowerup == PowerupManager.PowerupTypes.Pound)
            {
                // If it's on ground then jump ow ground pound
                if (onGround)
                {
                    playerRb.AddForce(Vector3.up * poundPowerupStrength, ForceMode.Impulse);
                    onGround = false;
                }
                else if (canGroundPound)
                {
                    playerRb.AddForce(Vector3.down * poundPowerupStrength * poundSpeed, ForceMode.Impulse);
                    canGroundPound = false;
                }
            }
        }
    }


    void OnTriggerEnter(Collider other)
    {
        Destroy(other.gameObject);

        if (other.CompareTag("Launch Powerup"))
        {
            powerupManager.TurnOnPowerup(PowerupManager.PowerupTypes.Bounce);
        }
        else if (other.CompareTag("Rocket Powerup"))
        {
            powerupManager.TurnOnPowerup(PowerupManager.PowerupTypes.Rockets);
        }
        else if(other.CompareTag("Pound Powerup"))
        {
            powerupManager.TurnOnPowerup(PowerupManager.PowerupTypes.Pound);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        // If the player hits an enemy and has the bounce indicator then shoot the enemy awy
        if (collision.gameObject.CompareTag("Enemy") && powerupManager.currentPowerup == PowerupManager.PowerupTypes.Bounce)
        {
            Rigidbody enemyRb = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromPlayer = collision.gameObject.transform.position - transform.position;
            enemyRb.AddForce(awayFromPlayer * bouncePowerupStrength, ForceMode.Impulse);
        }
        
        // If player is on 
        if(collision.gameObject.CompareTag("Ground") && powerupManager.currentPowerup == PowerupManager.PowerupTypes.Pound && !onGround)
        {
            onGround = true;
            canGroundPound = true;

            float forceMult; 
            foreach(var enemy in FindObjectsOfType<Enemy>())
            {
                forceMult = maxPoundDistance - Vector3.Distance(transform.position, enemy.transform.position);

                enemy.rbEnemy.AddForce(Vector3.back * maxPoundDistance, ForceMode.Impulse);
            }
        }
    }
}
