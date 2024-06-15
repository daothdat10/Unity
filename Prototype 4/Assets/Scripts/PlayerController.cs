using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 10.0f;
    private Rigidbody playerRb;
    private GameObject focalPoint; // dieu khien cam
    private float powerUpStrength = 15.0f;
    public GameObject powerupIndicator;

    public bool hasPowerup = false; // kiểm tra xem có sủ hữu powerup không 


    public PowerupType currentPowerUp = PowerupType.None;

    public GameObject rocketPrefab;

    private GameObject tmpRocket;
    private Coroutine powerupCountdown;

    //smash
    private float hangTime=0.5f;
    private float smashSpeed=10;
    private float explosionForce=10;
    private float explosionRadius = 10;


    bool smashing = false;
    float floorY;


    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("Focal Point");


       


    }

    // Update is called once per frame
    void Update()
    {
        float forwardInput = Input.GetAxis("Vertical");
        playerRb.AddForce(focalPoint.transform.forward * speed * forwardInput);
        powerupIndicator.transform.position = transform.position + new Vector3(0, -0.5f, 0);

        if(currentPowerUp == PowerupType.Rockets && Input.GetKeyDown(KeyCode.F))
        {
            LaunchRockets(); 
        }
        if (currentPowerUp == PowerupType.Smash && Input.GetKeyDown(KeyCode.Space) && !smashing)
        {

            smashing = true;
            
            StartCoroutine(Smash());
        }


    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Powerup"))
        {
            hasPowerup = true;
            currentPowerUp = other.gameObject.GetComponent<Powerups>().powerupsType;

            Destroy(other.gameObject);
            powerupIndicator.gameObject.SetActive(true);
            

            if (powerupCountdown != null)
            {

                StopCoroutine(powerupCountdown);
            }
            powerupCountdown = StartCoroutine((IEnumerator)PowerupCountdownRoutine());


        }
    }
    


    IEnumerable PowerupCountdownRoutine()
    {
        yield return new WaitForSeconds(7);
        hasPowerup = false;
        powerupIndicator.gameObject.SetActive(false);
        currentPowerUp = PowerupType.None;
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && currentPowerUp == PowerupType.PushBack)
        {

            Rigidbody enemyRigidbody = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromPlayer = collision.gameObject.transform.position - transform.position;


            enemyRigidbody.AddForce(awayFromPlayer * powerUpStrength, ForceMode.Impulse);
            Debug.Log("Player collided with: " + collision.gameObject.name + " with powerup set to " + currentPowerUp.ToString());

        }
    }
    
    void LaunchRockets()
    {
        

        foreach (var enemy in FindObjectsOfType<Enemy>())
        {
            tmpRocket = Instantiate(rocketPrefab, transform.position + Vector3.up, Quaternion.identity);
            tmpRocket.GetComponent<RocketBehaviour>().Fire(enemy.transform);
        }
    }
    IEnumerator Smash()
    {
        var enemies = FindObjectsOfType<Enemy>();
        
        floorY = transform.position.y;
        
        float jumpTime = Time.time + hangTime;

        while (Time.time < jumpTime)
        {
            
            playerRb.velocity = new Vector2(playerRb.velocity.x, smashSpeed);
            yield return null;
        }
        
        while (transform.position.y > floorY)
        {
            playerRb.velocity = new Vector2(playerRb.velocity.x, -smashSpeed * 2);
            yield return null;
        }
       
        for (int i = 0; i < enemies.Length; i++)
        {
            
            if (enemies[i] != null)
                enemies[i].GetComponent<Rigidbody>().AddExplosionForce(explosionForce, transform.position, explosionRadius, 0.0f, ForceMode.Impulse);
        }
        
        smashing = false;
    }

}
