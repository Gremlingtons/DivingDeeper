using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D player;
    private Vector2 initialPos;
    private float initialGrav;
    private int initialBoost = 2; // number of uses when pressing down key
    private int remainingBoost;
    public float speed = 3f;

    private void Start()
    {
        player = GetComponent<Rigidbody2D>();
        initialPos = player.position;
        initialGrav = player.gravityScale;
        remainingBoost = initialBoost;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(GameManager.Instance.snared);

        if (GameManager.Instance.snared == false)
        {
            // Horizontal movement
            float moveX = Input.GetAxis("Horizontal");
            player.velocity = new Vector2(moveX * speed, player.velocity[1]);

            // For pickup ability
            if ((Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)) && remainingBoost > 0)
            {
                player.velocity = new Vector2(moveX * speed * Time.deltaTime, player.velocity[1] * 2);
                remainingBoost--;
            }
        }
        
    }

    public void PausePlayerMovement() {
        player.velocity = new Vector2(0, 0);
        player.gravityScale = 0;
    }

    public void ResetPlayer() {
        player.position = initialPos;
        player.gravityScale = initialGrav;
        remainingBoost = initialBoost;
    }
}
