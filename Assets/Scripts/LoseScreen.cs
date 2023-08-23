using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoseScreen : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        ShowLoseScreen(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowLoseScreen(bool show)
    {
        gameObject.SetActive(show);
    }
}
