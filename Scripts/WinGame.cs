using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WinGame : MonoBehaviour
{
    Image image;
    PlayerControls pc;
    Color transparentBG;
    Color transparentText;
    [SerializeField] float colourSpeed = 1f;

    [SerializeField] TMP_Text winText;

    [SerializeField] GameObject button;

    [SerializeField] float buttonTimer;
    [SerializeField] AudioSource audioSource;
    
    // Start is called before the first frame update
    void Start()
    {
        image = GetComponentInChildren<Image>();
        image.enabled = false;
        transparentBG = Color.white;
        transparentBG.a = 0;
        image.color = transparentBG;
        winText.enabled = false;
        winText.color = transparentText;
        transparentText = Color.black;
        transparentText.a = 0;
        pc = FindObjectOfType<PlayerControls>();

        button.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(pc.wonGame)
        {
           audioSource.Stop();
           Invoke("BackGroundTransition", 1f);            
        }

        if(image.color.a >= 1)
        {
            Invoke("TextTransition", 1f);
            Invoke("ActivateButton", buttonTimer);
        }
    }

    void BackGroundTransition()
    {
        image.enabled = true;
        transparentBG.a += Time.deltaTime * colourSpeed;
        image.color = transparentBG;
    }

    void TextTransition()
    {
        winText.enabled = true;
        transparentText.a += Time.deltaTime * colourSpeed;
        winText.color = transparentText;
    }

    void ActivateButton()
    {
        button.SetActive(true);
    }
}
