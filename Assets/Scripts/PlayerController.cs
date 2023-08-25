using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D player;
    
    private Vector2 initialPos;
    private float initialGrav;
    public float speed = 3f;

    // jetpack
    private bool hasJetpack;
    private int initialBoost = 1; 
    private int remainingBoost = 0;
    
    // grapple
    private bool hasGrapple;
    private int initialDashes = 1;
    private int remainingDashes = 0;
    public float dashSpeed = 150f;
    public float dashDuration = 0.5f;
    private bool isDashing = false;


    // item tooltips
    private bool isPaused;
    // public GameObject tooltipJetpack;
    public GameObject tooltip;

    private void Start()
    {
        player = GetComponent<Rigidbody2D>();
        initialPos = player.position;
        initialGrav = player.gravityScale;
        
        // Check GameManager for saved jetpack state
        if (GameManager.Instance.remainingBoost > 0)
        {
            AcquireJetpack(GameManager.Instance.remainingBoost);
        }
        // Check GameManager for saved grapple state
        if (GameManager.Instance.remainingDashes  > 0)
        {
            AcquireGrapple(GameManager.Instance.remainingDashes);
            // GameManager.Instance.UpgradeGrapple(GameManager.Instance.remainingDashes);
        }
    }

    // Update is called once per frame
    void Update()
    {

        float horizontalInput = Input.GetAxis("Horizontal");
        //Debug.Log(GameManager.Instance.snared);
        if (isPaused && Input.GetKeyDown(KeyCode.Space))
        {
            ResumeGame();
            return;
        }

        if (GameManager.Instance.snared == false)
        {
            if (!isDashing)
            {
                // Horizontal movement
                float moveX = Input.GetAxis("Horizontal");
                player.velocity = new Vector2(moveX * speed, player.velocity[1]);

                // For jetpack ability
                if ((Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)) && remainingBoost > 0 && hasJetpack)
                {
                    player.velocity = new Vector2(moveX * speed * Time.deltaTime, player.velocity[1] * 2);
                    remainingBoost--;
                    GameManager.Instance.remainingBoost = remainingBoost;
                }
            }
            
            // For grapple ability
            if (remainingDashes > 0 && Input.GetKeyDown(KeyCode.LeftShift))
            {
                StartCoroutine(Dash(horizontalInput));
            }

        

        }
        
    }
    
    public void AcquireJetpack(int boosts)
    {
        hasJetpack = true;
        remainingBoost = boosts;
        GameManager.Instance.remainingBoost = boosts;
    }

    public void UpgradeJetpack(int newInitialBoost)
    {
        initialBoost = newInitialBoost;
        remainingBoost = newInitialBoost;
        GameManager.Instance.remainingBoost = newInitialBoost;
    }

    public void AcquireGrapple(int dashes)
    {
        hasGrapple = true;
        remainingDashes = dashes;
        GameManager.Instance.remainingDashes = dashes;
    }

    public void UpgradeGrapple(int newDashes)
    {
        initialDashes = newDashes;
        remainingDashes =  newDashes;
        GameManager.Instance.remainingDashes = newDashes;
    }


    IEnumerator Dash(float direction)
    {
        if (direction == 0)
        {
            yield break; // Exit if no direction is being held
        }

        isDashing = true;
        player.velocity = new Vector2(direction * dashSpeed, player.velocity.y);
        
        yield return new WaitForSeconds(dashDuration);

        remainingDashes--;
        GameManager.Instance.remainingDashes = remainingDashes;
        
        isDashing = false;
    }

    public void PausePlayerMovement() {
        //player.velocity = new Vector2(0, 0);
        //player.gravityScale = 0;
        Time.timeScale = 0;
    }

    public void ResetPlayer() {
        player.position = initialPos;
        player.velocity = new Vector2(0, 0);

        //player.gravityScale = initialGrav;
        remainingBoost = initialBoost;
        remainingDashes = initialDashes;
        Time.timeScale = 1;
    }

    public void PauseGame()
    {
        isPaused = true;
        Time.timeScale = 0f;
        // tooltip.SetActive(true);
    }

    public void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1f;
        // tooltipJetPack.SetActive(false);
        tooltip.SetActive(false);
    }

}
