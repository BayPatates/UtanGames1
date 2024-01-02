using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update
public void PlayGame()
{
    SceneManager.LoadScene(1); // Load the next scene
}
public void HowToPlay()
{
    SceneManager.LoadScene(2); // Load the next scene
}
public void BackToMenu()
{
    SceneManager.LoadScene(0); // Load the next scene
}

public void QuitGame()
{
    Application.Quit();
}
}
