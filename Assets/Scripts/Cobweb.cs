using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cobweb : MonoBehaviour
{

    Rigidbody2D player;
    float slowFactor = 0.2f;
    float currentSpeed = 0;
    float slowSpeed = 0;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // private void OnTriggerEnter2D(Collider2D col) 
    // {
    //     player = GetComponent<Rigidbody2D>();
    //     if (col.gameObject.CompareTag("Cobweb"))
    //     {
    //         player.gravityScale = 0;
    //         player.velocity = new Vector2(player.velocity[0], player.velocity[1] * slowFactor);
    //     }
    // }


    // private void OnTriggerExit2D(Collider2D col)
    // {
    //     player = GetComponent<Rigidbody2D>();
    //     if (col.gameObject.CompareTag("Cobweb"))
    //     {
    //         player.gravityScale = 1;

    //     }
    // }

    private void OnTriggerEnter2D(Collider2D col)
    {
        player = GetComponent<Rigidbody2D>();
        if (col.gameObject.CompareTag("Cobweb"))
        {
            currentSpeed = player.velocity[1];
            slowSpeed = currentSpeed * slowFactor;
        }
    }

    void OnTriggerStay2D(Collider2D col)
    {
        player = GetComponent<Rigidbody2D>();
        if (col.gameObject.CompareTag("Cobweb"))
        {
            player.velocity = new Vector2(player.velocity[0], slowSpeed);
        }
    }

}
