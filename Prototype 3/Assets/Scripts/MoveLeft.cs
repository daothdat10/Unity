using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    private float speed = 30;

    private float leftBound = -15;

    private PlayerController playerControllerScript;// dừng xuất hiện trướng ngại vật khi game over
    // Start is called before the first frame update
    void Start()
    {
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerControllerScript.gameOver == false) {
            transform.Translate(Vector3.left * Time.deltaTime * speed);
        }


        if(transform.position.x<leftBound && gameObject.CompareTag("obstade"))
        {

            Destroy(gameObject);

        }
    }
}

