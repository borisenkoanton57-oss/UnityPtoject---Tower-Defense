using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    public GameObject exitPanel;
    public GameObject settingsPanel;
    public void PlayButtonPressed()
    {
        SceneManager.LoadScene(1);
    }

    public void ExitButtonPressed()
    {
        exitPanel.SetActive(true);
    }

     public void ConfirmExit()
    {
        Application.Quit();
    }

    public void CancelExit()
    {
        exitPanel.SetActive(false);
    }

    public void OpenSettings()
    {
        settingsPanel.SetActive(true);
    }

    public void CloseSettings()
    {
        settingsPanel.SetActive(false);
    }
}
