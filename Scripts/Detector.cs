using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Detector : MonoBehaviour
{
    [SerializeField] Slider slider;

    [SerializeField] float detectionSpeed = 1;
    [SerializeField] float unDetectedSpeed = 1;

    [SerializeField] GameObject gameOverScreen;

    PlayerControls pc;
    
    // Start is called before the first frame update
    void Start()
    {
        slider.value = 0; 
        gameOverScreen.SetActive(false);
        pc = FindObjectOfType<PlayerControls>();   
    }

    // Update is called once per frame
    void Update()
    {
        LosingDetection(); 
        Detected();
    }

    void OnTriggerStay2D(Collider2D other) 
    {
        if(other.CompareTag("Enemy") && !pc.hidden)
        {
            float detectionAmount = detectionSpeed * Time.fixedDeltaTime;
            slider.value += detectionAmount;
        }    
    }

    void LosingDetection()
    {
        slider.value -= unDetectedSpeed * Time.fixedDeltaTime;
    }

    void Detected()
    {
        if(slider.value >= slider.maxValue - 0.05f)
        {
            gameOverScreen.SetActive(true);
            Time.timeScale = 0f;
            Debug.Log("Detected");        
        }
    }

    public void RestartGame()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.buildIndex);
        Time.timeScale = 1f;
    }
}
