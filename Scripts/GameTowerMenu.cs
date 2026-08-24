using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections.Generic;

public class GameTowerMenu : MonoBehaviour
{
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private GameObject victoryPanel;
    [SerializeField] private GameObject defeatPanel;

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip defeatSound;

    public static bool paused = false;
    public static bool gameFinished = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if ((victoryPanel != null && victoryPanel.activeSelf) ||
                (defeatPanel != null && defeatPanel.activeSelf))
            {
                return;
            }

            PauseButton();
        }
    }

    public void ChangeTowerPanelVisibilityButton(Animator animator)
    {
        animator.SetBool("Visible", !animator.GetBool("Visible"));
    }

    public void SpeedUp(TMP_Text buttonLabel)
    {
        if(Time.timeScale < 3f)
        {
            Time.timeScale += 1f;
        }
        else 
        {
            Time.timeScale = 1f;
        }
        buttonLabel.text = Time.timeScale + "x";
    }

    public void SwitchUIObjects(Transform elements)
    {
        foreach(Transform element in elements)
        {
            element.gameObject.SetActive(!element.gameObject.activeSelf);
        }
    }

    public void PauseButton()
    {
        paused = !paused;
        Time.timeScale = paused ? 0f : 1f;

        if (pausePanel != null)
        {
            pausePanel.SetActive(paused);
        }
    }

    public void ResumeButton()
    {
        paused = false;
        Time.timeScale = 1f;

        if (pausePanel != null)
        {
            pausePanel.SetActive(false);
        }
    }

    public void ShowDefeat()
    {
        gameFinished = true;
        paused = false;

        Time.timeScale = 0f;

        if (defeatPanel != null)
        {
            defeatPanel.SetActive(true);
        }
        
        if (audioSource != null && defeatSound != null)
        {
            audioSource.PlayOneShot(defeatSound);
        }
    }

    public void DefeatBackToMenu()
    {
        Time.timeScale = 1f;
        paused = false;
        gameFinished = false;

        SceneManager.LoadScene(0);
    }

    public void BackToMenuButton()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
        paused = false;
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        paused = false;
        gameFinished = false;

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void VictoryBackToMenu()
    {
        Time.timeScale = 1f;
        paused = false;
        gameFinished = false;

        SceneManager.LoadScene(0);
    }
}
