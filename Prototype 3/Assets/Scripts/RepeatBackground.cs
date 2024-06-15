using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    private Vector3 startPos; // lặp nền

    private float repeatWidth;// làm nền smooth


    // Start is called before the first frame update
    void Start()
    {
            startPos = transform.position;
            repeatWidth = GetComponent<BoxCollider>().size.x/2;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x < startPos.x -repeatWidth) { 
            transform.position = startPos;
        }
    }
}
