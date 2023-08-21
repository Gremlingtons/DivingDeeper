using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Camera cam;
    public Transform target;
    public Vector3 offset;
    public float smoothTime = 0.3f;
    private Vector3 velocity = Vector3.zero;

    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            Vector3 targetPosition = target.position + offset;
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
        }
        
        cam = GetComponent<Camera>();
        if  (cam.transform.position[1] > -100) {
            cam.orthographicSize =  8;
        }
        else
        {
            cam.orthographicSize = 8 * (cam.transform.position[1] * -1) / 100;
        }
        
      
    }

    
}
