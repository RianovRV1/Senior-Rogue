using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseMovement : MonoBehaviour {

    public float speed = 10000f;
    public Vector3 target;

    void Start()
    {
        target = transform.position;
    }

    void Update()
    {

        
        // the setting of Z helps the camera detect where the mouse is in world space and correctly sets the target transformation to move towards
        
        if (Input.GetMouseButtonDown(0))
        {
            target = Input.mousePosition;
            target.z = 60;
            target = Camera.main.ScreenToWorldPoint(target);
            target.z = 0;

        }
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
    }

}
