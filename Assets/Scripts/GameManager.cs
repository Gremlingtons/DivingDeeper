using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Singleton
    public static GameManager Instance;
    [SerializeField] GameObject obstacles;

    [Tooltip("How much money does the player have?")]
    public int money = 0;
    public bool snared = false;

    private void Awake()
    {
        if (Instance == null)
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

    public void TryAgain()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().ResetPlayer();
        GameObject.FindGameObjectWithTag("LoseScreen").GetComponent<LoseScreen>().ShowLoseScreen(false);
        foreach(Transform child in obstacles.transform)
        {
            child.gameObject.SetActive(true);
        }
    }
}
