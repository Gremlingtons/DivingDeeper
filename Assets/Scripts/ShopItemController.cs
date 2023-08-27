using UnityEngine;
using TMPro;

public class ShopItem : MonoBehaviour
{
    // Enum to represent the different types of shop items
    public enum ItemType
    {
        DownwardBooster,
        GrapplingHook
    }

    // The type of the shop item
    [SerializeField]
    private ItemType itemType;

    // The name of the shop item
    [SerializeField]
    private string itemName;

    // The current level of the shop item
    [SerializeField]
    private int level = 1;

    // The maximum level the shop item can reach
    private const int maxLevel = 5;

    // A description for the shop item
    [SerializeField]
    private string description;

    // The base cost for the shop item
    [SerializeField]
    private int baseCost = 10;

    // Prefab for displaying item details
    [SerializeField]
    private GameObject textBoxPrefab;

    // Reference to the sprite renderer of the shop item for visual effects like fading
    //[SerializeField]
    //private SpriteRenderer itemSpriteRenderer;

    // A reference to the currently displayed text box for the item details
    private GameObject currentTextBox;

    // Method to calculate the cost of the item based on its current level
    private int GetCurrentCost()
    {
        return baseCost * level;
    }

    // Method to handle the purchase of the shop item
    private bool PurchaseItem()
    {
        // Check if player has enough money and if item isn't at max level
        if (GameManager.Instance.money >= GetCurrentCost() && level < maxLevel)
        {
            // Increase the ability uses
            if (itemType == ItemType.DownwardBooster) 
            { 
                GameManager.Instance.UpgradeJetpack(); 
            }
            if (itemType == ItemType.GrapplingHook) 
            { 
                GameManager.Instance.UpgradeGrapple(); 
            }

            // Deduct the cost from the player's money
            GameManager.Instance.UpdateMoney(-GetCurrentCost());

            // Increase the level of the item
            level++;

            // Update the displayed details for the shop item
            UpdateItemInfoText();

            // If the item has reached max level, fade its appearance
            if (level == maxLevel)
            {
                FadeItem();
            }
            return true;
        }
        return false;
    }

    // Trigger when player enters the shop item's area
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the colliding object is the player
        if (other.CompareTag("Player"))
        {
            // Display the details of the shop item
            DisplayItemInfo();
        }
    }

    // Trigger when player exits the shop item's area
    private void OnTriggerExit2D(Collider2D other)
    {
        // Check if the exiting object is the player
        if (other.CompareTag("Player"))
        {
            // Hide the displayed details of the shop item
            HideItemInfo();
        }
    }

    // Method to display the shop item's details
    private void DisplayItemInfo()
    {
        // Instantiate the text box prefab to display the details
        currentTextBox = Instantiate(textBoxPrefab, transform.position + new Vector3(0, 3f, 0), Quaternion.identity);

        // Update the text in the displayed text box
        UpdateItemInfoText();
    }

    // Method to update the displayed text with the shop item's details
    private void UpdateItemInfoText()
    {
        // Check if the text box is currently being displayed
        if (currentTextBox)
        {
            // Get the TMP_Text component from the text box
            TMP_Text textComponent = currentTextBox.GetComponentInChildren<TMP_Text>();

            // Set the text to display the shop item's details
            textComponent.text = $"{itemName} (Level {level})\n{description}\nCost: {GetCurrentCost()}\n 'Space' to Buy";
        }
    }

    // Method to hide the displayed shop item's details
    private void HideItemInfo()
    {
        // Check if the text box is currently being displayed
        if (currentTextBox)
        {
            // Destroy the displayed text box
            Destroy(currentTextBox);
        }
    }

    // Method to fade the appearance of the shop item when it's fully upgraded
    private void FadeItem()
    {
        // Set the color of the shop item's sprite to a faded appearance
        //itemSpriteRenderer.color = new Color(0.5f, 0.5f, 0.5f, 0.5f);
    }

    // Update method called every frame
    private void Update()
    {
        // Check if the player presses the spacebar while viewing the shop item's details
        if (Input.GetKeyDown(KeyCode.Space) && currentTextBox)
        {
            // Attempt to purchase the shop item
            PurchaseItem();
        }
    }
}