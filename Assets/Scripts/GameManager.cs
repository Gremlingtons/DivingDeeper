using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Singleton
    public static GameManager Instance;
    [SerializeField] GameObject obstacles;
    [SerializeField] TMP_Text moneyText;
    [SerializeField] GameObject loseScreen;
    [SerializeField] GameObject winScreen;
    [SerializeField] TextMeshProUGUI tooltipGrapple;


    [Tooltip("How much money does the player have?")]
    public int money = 0;
    public bool snared = false;
    public int totalBoost = 0;
    public int totalDashes = 0;

    public TextMeshProUGUI tooltipJetpack; // Tooltip text

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TryAgain()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().ResetPlayer();
        GameObject.FindGameObjectWithTag("LoseScreen").GetComponent<LoseScreen>().ShowLoseScreen(false);
        foreach(Transform child in obstacles.transform)
        {
            child.gameObject.SetActive(true);
        }
    }

    public void HardReset()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void UpdateMoney(int moneyGained)
    {
        money += moneyGained;
        moneyText.text = "$: " + money.ToString();
    }


    public void SetWinScreen(bool b)
    {
        winScreen.SetActive(b);
    }

    public void SetLoseScreen(bool b)
    {
        loseScreen.SetActive(b);
    }

    public void ShowTooltip(string message)
    {
        tooltipGrapple.text = message;
        tooltipJetpack.text = message;
        tooltipGrapple.gameObject.SetActive(true);
        tooltipJetpack.gameObject.SetActive(true);
    }

    public void HideTooltip()
    {
        tooltipGrapple.gameObject.SetActive(false);
        tooltipJetpack.gameObject.SetActive(false);
    }


}
