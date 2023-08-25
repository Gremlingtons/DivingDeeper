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
    private float lastDashTime;
    public float dashCooldown = 0.15f;
    public float dashDistance = 100.0f;
    private int initialDashes = 1;
    public int remainingDashes = 0;

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
        if (GameManager.Instance.remainingDashes > 0)
        {
            AcquireGrapple(GameManager.Instance.remainingDashes);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(GameManager.Instance.snared);
        if (isPaused && Input.GetKeyDown(KeyCode.Space))
        {
            ResumeGame();
            return;
        }

        if (GameManager.Instance.snared == false)
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

            // For grapple ability
            if (remainingDashes > 0 && Input.GetKeyDown(KeyCode.D))
            {
                if (Time.time - lastDashTime < dashCooldown)
                {
                    DashRight(Vector2.right);
                }
                lastDashTime = Time.time;
            }
            if (remainingDashes > 0 && Input.GetKeyDown(KeyCode.A))
            {
                if (Time.time - lastDashTime < dashCooldown)
                {
                    DashLeft(Vector2.left);
                }
                lastDashTime = Time.time;
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
        remainingDashes = dashes;
        GameManager.Instance.remainingDashes = dashes;
    }

    public void UpgradeGrapple(int newDashes)
    {
        initialDashes = newDashes;
        remainingDashes =  newDashes;
        GameManager.Instance.remainingDashes = newDashes;
    }


    private void DashLeft(Vector2 direction)
    {
        transform.position = new Vector2(transform.position.x - (direction.x * (-1 * dashDistance)) , transform.position.y);
        remainingDashes--;
        GameManager.Instance.remainingDashes = remainingDashes;
    }

    private void DashRight(Vector2 direction)
    {
        transform.position = new Vector2(transform.position.x + (direction.x * dashDistance), transform.position.y);
        remainingDashes--;
        GameManager.Instance.remainingDashes = remainingDashes;
    }

    public void PausePlayerMovement() {
        //player.velocity = new Vector2(0, 0);
        //player.gravityScale = 0;
        Time.timeScale = 0;
    }

    public void ResetPlayer() {
        player.position = initialPos;
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
