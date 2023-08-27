using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShatterBarrier : MonoBehaviour
{

    Rigidbody2D player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter2D(Collider2D col)
    {
        player =  GetComponent<Rigidbody2D>();
        if (col.gameObject.CompareTag("Barrier"))
        {
            if (player.velocity[1] < -40)
            {
                player.velocity = new Vector2(player.velocity[0], player.velocity[1] / 8);
                col.gameObject.SetActive(false);
                if (col.gameObject.name == "Barrier (Final)")
                {
                    GameManager.Instance.SetWinScreen(true);
                }
            }
            else
            {
                Time.timeScale = 0;
                GameManager.Instance.SetLoseScreen(true);
            }
        }
    }
}
