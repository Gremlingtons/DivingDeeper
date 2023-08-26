using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GrappleItem : MonoBehaviour
{
    public int initialDashes = 1;
    // public GameObject tooltip;
    //public TextMeshProUGUI tooltipText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerController player = other.GetComponent<PlayerController>();
            if (player != null)
            { 
                GameManager.Instance.ShowTooltip("You've picked up a grapple! While holding a or d, press SHIFT to use. (press SPACE to resume)");
                
                player.AcquireGrapple(initialDashes);
            }
            Destroy(gameObject);

            // pause and display tooltip
            player.PauseGame();
        }
    }
}
