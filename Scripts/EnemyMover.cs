using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMover : MonoBehaviour
{
    [SerializeField] Vector3 movementVector = new Vector3(10f, 10f, 10f);
    Vector3 offset;
    [SerializeField] float sinePeriod = 2f;
    [SerializeField] Vector3 stopPosition;

    float movementFactor; //0 = not moved 1 = fully moved.

    bool enemyFacingRight;
    float enemyDirection; 

    Vector3 direction;
    Vector3 localDirection;
    Vector3 lastPosition;

    Rigidbody2D rb;

    Vector3 startingPosition;
    // Start is called before the first frame update
    void Start()
    {
        startingPosition = transform.position;
        rb = GetComponent<Rigidbody2D>();
       
    }

    // Update is called once per frame
    void Update()
    {
        
        
        if (sinePeriod <=Mathf.Epsilon) { return; }
        else if (Vector3.Distance(transform.position, stopPosition) <= 0.5f)
            {           // works if you make sure all axis are correct and its only moving on 1 axis
                return; 
            }    
            float sineCycles = Time.timeSinceLevelLoad / sinePeriod; // grows continually from 0
            const float tau = Mathf.PI * 2; // equals about 6.28
            float rawSinWave = Mathf.Sin(sineCycles * tau); // goes from -1 to +1
            movementFactor = rawSinWave / 2f + 0.5f;//changes it to 0 to 1

            offset = movementVector * movementFactor;
            transform.position = startingPosition + offset;

        direction = transform.position - lastPosition;
        localDirection = transform.InverseTransformDirection(direction);
        lastPosition = transform.position;

        if (direction.x > 0 && !enemyFacingRight)
        {
           enemyDirection = 180f;
           FlipRotation();
        } if (direction.x < 0 && enemyFacingRight) 
        {
            enemyDirection = 0f;
            FlipRotation();
        }
        Debug.Log(direction); 
    }

    void FlipRotation()
    {
        enemyFacingRight = !enemyFacingRight;
        transform.localRotation = Quaternion.Euler(0f, enemyDirection, 0f);
    }
}
