using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class uiHandler : MonoBehaviour
{
   public void touchdownplay()
    {
        SceneManager.LoadScene("New");
    }
    public void battleMode()
    {
        SceneManager.LoadScene("battleMode");
    }
    public void charachters()
    {
        SceneManager.LoadScene("characterShowcase");
    }
    public void mainMenu()
    {
        SceneManager.LoadScene("UIMain");
    }
    
    public void settings()
    {

    }
    public void quit()
    {
        Application.Quit();
    }
}
