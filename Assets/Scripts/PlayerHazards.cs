using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHazards : MonoBehaviour
{
    Rigidbody2D player;

    // Cobweb variables
    float slowFactor = 0.2f;
    float currentSpeed = 0;
    float slowSpeed = 0;

    // Snare variables
    private float duration = 2.0f;
    private bool isActive = false;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        currentSpeed = player.velocity[1];
        switch (col.tag)
        {
            case "Cobweb":
                slowSpeed = currentSpeed * slowFactor;
                break;
            case "Snare":
                if (!isActive)
                {
                    isActive = true;
                    //Destroy(col.gameObject);
                    col.gameObject.SetActive(false);
                    StartCoroutine(EffectWearOff(duration));  //start the time function
                }
                break;
            case "Breakable":
                if (currentSpeed > 5f)
                {
                    player.velocity = new Vector2(player.velocity[0], player.velocity[1] / 8);
                    //Destroy(col.gameObject);
                }
                break;
        }
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Cobweb"))
        {
            player.velocity = new Vector2(player.velocity[0], slowSpeed);
        }
    }

    IEnumerator EffectWearOff(float waitTime)
    {
        // restrict movement
        GameManager.Instance.snared = true;
        // wait
        yield return new WaitForSeconds(waitTime);

        // free movement
        GameManager.Instance.snared = false;
        isActive = false;
    }
}
