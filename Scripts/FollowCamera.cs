using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    Transform cameraTransform;
    [SerializeField] Transform player;
    [SerializeField] Vector3 cameraOffset;
    Vector3 newCameraTransform;
    [SerializeField] float leftSideClamp = -32f;
    [SerializeField] float rightSideClamp = 0f;
    
    void Start()
    {
        cameraTransform = GetComponent<Transform>();
        newCameraTransform = cameraTransform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(player.position.x > leftSideClamp && player.position.x < rightSideClamp)
        {
            newCameraTransform.x = player.position.x;
            cameraTransform.position = newCameraTransform + cameraOffset;
            
        }
        
    }
}
