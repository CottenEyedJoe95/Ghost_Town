using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    [SerializeField] float xSpeed = 1f;
    [SerializeField] float ySpeed = 1f;

    [SerializeField] float hidingTime = 3f;

    TriggerInteraction triggerInteraction;
    Animator animator;

    bool interavtive;
    bool win;
    public bool wonGame;
    public bool hidden;
    bool hiding;
    
    float xMovement;
    float yMovement;
    float shipDirection;
    bool shipFacingRight;

    void Start() 
    {
        interavtive = false;
        win = false;  
        wonGame = false;
        hidden = false;
        hiding = false; 

        animator = GetComponentInChildren<Animator>(); 
    }
    
    void Update() 
    {
        ProcessMovement();
        InteractWithObject();    
    }
    
    void ProcessMovement()
    {
        if(!hidden)
        {
            xMovement = Input.GetAxis("Horizontal");
            float xOffset = (xMovement * Time.deltaTime) * xSpeed;
            float newXPosition = transform.localPosition.x + xOffset;

            yMovement = Input.GetAxis("Vertical");
            float yOffset = (yMovement * Time.deltaTime) * ySpeed;
            float newYPosition = transform.localPosition.y + yOffset;

            transform.localPosition = new Vector2(newXPosition, newYPosition);
        }
        

        if (xMovement > 0 && !shipFacingRight)
        {
           shipDirection = 0f;
           FlipShip();
        } if (xMovement < 0 && shipFacingRight) 
        {
            shipDirection = 180f;
            FlipShip();
        }

        void FlipShip()
        {
            shipFacingRight = !shipFacingRight;
            transform.localRotation = Quaternion.Euler(0f, shipDirection, 0f);
        }
    } 

    void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.CompareTag("Interactive"))
        {
            triggerInteraction = other.GetComponent<TriggerInteraction>();
            interavtive = true;
        } 

        if(other.CompareTag("Win"))
        {
            triggerInteraction = other.GetComponent<TriggerInteraction>();
            interavtive = true;
            win = true;
        }  
    }

    private void OnTriggerExit2D(Collider2D other) 
    {
        if(other.CompareTag("Interactive"))
        {
            interavtive = false;
        } 
        
        if(other.CompareTag("Hide"))
        {
            hiding = false;
        }     
    }

    private void OnTriggerStay2D(Collider2D other) 
    {
        if(other.CompareTag("Hide"))
        {
            hiding = true;
        }
        else {hiding = false;} 
    }
    
    void InteractWithObject()
    {
        if(Input.GetKeyDown(KeyCode.Space) && interavtive && !win && !hiding)
        {
            triggerInteraction.PlayInteraction();            
        }
        
        if(Input.GetKeyDown(KeyCode.Space) && win && interavtive)
        {
            triggerInteraction.PlayInteraction();
            wonGame = true;            
        }

        if(Input.GetKeyDown(KeyCode.Space) && hiding)
        {
            Hide();           
        }

    }

    void Hide()
    {       
        hidden = true;
        animator.PlayInFixedTime("Ghost_Hiding");
        Invoke("Appear", hidingTime);
    } 

    void Appear()
    {
        hidden = false;
        hiding = false;
        animator.PlayInFixedTime("Ghost_Hiding 1");

    }  
}
