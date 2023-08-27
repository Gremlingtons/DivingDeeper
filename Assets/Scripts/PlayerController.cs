using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    Rigidbody2D player;
    
    private Vector2 initialPos;
    private float initialGrav;
    public float speed = 3f;

    // jetpack
    public GameObject jetpackShop;
    private bool hasJetpack;
    private int remainingBoost = 0;
    [SerializeField] TextMeshProUGUI boostText;

    // grapple
    public GameObject grappleShop;
    private bool hasGrapple;
    private int remainingDashes = 0;
    public float dashSpeed = 150f;
    public float dashDuration = 0.5f;
    private bool isDashing = false;
    [SerializeField] TextMeshProUGUI dashText;


    // item tooltips
    private bool isPaused;
    
    // public GameObject tooltipJetpack;
    

    private void Start()
    {
        player = GetComponent<Rigidbody2D>();
        initialPos = player.position;
        initialGrav = player.gravityScale;
        jetpackShop.SetActive(false);
        grappleShop.SetActive(false);
        dashText.gameObject.SetActive(false);
        boostText.gameObject.SetActive(false);
        
        //// Check GameManager for saved jetpack state
        //if (GameManager.Instance.remainingBoost > 0)
        //{
        //    AcquireJetpack(GameManager.Instance.remainingBoost);
        //    jetpackShop.SetActive(true);
        //}
        //// Check GameManager for saved grapple state
        //if (GameManager.Instance.remainingDashes  > 0)
        //{
        //    AcquireGrapple(GameManager.Instance.remainingDashes);
        //    grappleShop.SetActive(true);
        //    // GameManager.Instance.UpgradeGrapple(GameManager.Instance.remainingDashes);
        //}
    }

    // Update is called once per frame
    void Update()
    {

        float horizontalInput = Input.GetAxis("Horizontal");
        //Debug.Log(GameManager.Instance.snared);
        if (isPaused && Input.GetKeyDown(KeyCode.Space))
        {
            GameManager.Instance.HideTooltip();
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
                    boostText.text = $"Boost: {remainingBoost}";
                }
            }
            
            // For grapple ability
            if (remainingDashes > 0 && Input.GetKeyDown(KeyCode.LeftShift) && hasGrapple)
            {
                StartCoroutine(Dash(horizontalInput));
            }

        

        }
        
    }
    
    public void AcquireJetpack(int boosts)
    {
        hasJetpack = true;
        jetpackShop.SetActive(true);
        boostText.gameObject.SetActive(true);
        remainingBoost = boosts;
    }

    public void ResetJetpackUses()
    {
        remainingBoost = GameManager.Instance.totalBoost;
        boostText.text = $"Boost: {remainingBoost}";
    }
    

    public void AcquireGrapple(int dashes)
    {
        hasGrapple = true;
        grappleShop.SetActive(true);
        dashText.gameObject.SetActive(true);
        remainingDashes = dashes;
    }

    public void ResetGrappleUses()
    {
        remainingDashes = GameManager.Instance.totalDashes;
        dashText.text = $"Dash: {remainingDashes}";
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
        dashText.text = $"Dash: {remainingDashes}";

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
        remainingBoost = GameManager.Instance.totalBoost;
        remainingDashes = GameManager.Instance.totalDashes;
        dashText.text = $"Dash: {remainingDashes}";
        boostText.text = $"Boost: {remainingBoost}";
        Time.timeScale = 1;
    }

    public void PauseGame()
    {
        isPaused = true;
        Time.timeScale = 0f;
        
    }

    public void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1f;
    }

}
