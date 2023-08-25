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

    private const float SHIFT_THRESHOLD = 3f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        cam = GetComponent<Camera>();
        if (target != null)
        {
            Vector3 cameraCenter = target.position + offset;
            Vector3 cameraOffset = new Vector3(0, (cam.orthographicSize - (0.25f * cam.orthographicSize)) * -1 , -10);
            Vector3 targetPosition = target.position;
            // Shift camera downwards if the player is falling
            if (Mathf.Abs(target.GetComponent<Rigidbody2D>().velocity[1]) <= SHIFT_THRESHOLD)
            {
                targetPosition = cameraCenter;
            }
            else
            {
                targetPosition = targetPosition + cameraOffset;
            }
            
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
        }
        
        
        if  (cam.transform.position[1] > -70) {
            cam.orthographicSize =  8;
        }
        else if (cam.transform.position[1] <= -60 & cam.transform.position[1] > -240)
        {
            cam.orthographicSize = 8 * (cam.transform.position[1] * -1) / 70;
        }
         else 
        {
            cam.orthographicSize = 8  * (240/70);
        }
        
      
    }

    
}
