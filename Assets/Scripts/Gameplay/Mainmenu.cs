using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Mainmenu : MonoBehaviour
{
    public GameObject helpPanel; // Kéo HelpPanel từ Inspector vào đây

    public void Loadgame()
    {
        SceneManager.LoadScene("Game");
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void ShowHelp()
    {
        if (helpPanel != null)
            helpPanel.SetActive(true);
    }

    public void CloseHelp()
    {
        if (helpPanel != null)
            helpPanel.SetActive(false);
    }
}
