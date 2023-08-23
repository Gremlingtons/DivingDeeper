using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snare : MonoBehaviour
{


    Rigidbody2D player;
    private float duration = 2.0f;
    private bool isActive = false;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter2D(Collider2D col)
    {
        player = GetComponent<Rigidbody2D>();
        
        if (col.tag == "Snare" && isActive == false)
        {
            isActive = true;
            Destroy(col.gameObject);
            StartCoroutine(EffectWearOff(duration));  //start the time function
        }
    }
    IEnumerator EffectWearOff(float waitTime)
    {
        // restrict movement
        GameManager.Instance.snared = true;
        // wait
        yield return new WaitForSeconds(waitTime);

        // free movement
        GameManager.Instance.snared = false;
        isActive = false;
    }
}
