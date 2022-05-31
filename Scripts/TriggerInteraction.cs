using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerInteraction : MonoBehaviour
{
    Animator animator;
    AudioSource audioSource;
    [SerializeField] AudioClip audioClip;
    [SerializeField] string animationString;
    
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void PlayInteraction()
    {
        animator.PlayInFixedTime(animationString);
        audioSource.PlayOneShot(audioClip);
    }


}
