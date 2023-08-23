using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Singleton
    public static GameManager Instance;

    [Tooltip("How much money does the player have?")]
    public int money = 0;
    public bool snared = false;

    private void Awake()
    {
        if (GameManager.Instance == null)
        {
            Instance = this;
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

    public void TryAgain() {
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().ResetPlayer();
        GameObject.FindGameObjectWithTag("LoseScreen").GetComponent<LoseScreen>().ShowLoseScreen(false);
    }
}
