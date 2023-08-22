using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;
    public float speed = 3f;
    private int usage = 2; // number of uses when pressing down key

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // Horizontal movement
        float moveX = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveX * speed, rb.velocity[1]);

        // For pickup ability
        if ((Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)) && usage > 0)
        {
            rb.velocity = new Vector2(moveX * speed * Time.deltaTime, rb.velocity[1] * 2);
            usage--;
        }
    }
}
