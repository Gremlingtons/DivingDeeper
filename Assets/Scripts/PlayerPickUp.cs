using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerPickUp : MonoBehaviour
{
    [SerializeField] TMP_Text textMoney;

    // Start is called before the first frame update
    void Start()
    {
        UpdateMoneyText(0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Coin"))
        {
            GameManager.Instance.money++;
            Destroy(col.gameObject);
            UpdateMoneyText(GameManager.Instance.money);
        }

    }

    void UpdateMoneyText(int amount)
    {
        textMoney.text = "$: " + amount.ToString();
    }
}
