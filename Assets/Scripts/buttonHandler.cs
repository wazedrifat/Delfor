using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buttonHandler : MonoBehaviour
{
    public GameObject emailimage;
    public GameObject howtoPlay;
    public GameObject returnToMainMenu;
    public GameObject closeToMain;

    public void howtoPlayImg()
    {
        howtoPlay.SetActive(true);
        emailimage.SetActive(false);
    }
    public void closeButton()
    {
        emailimage.SetActive(true);
        howtoPlay.SetActive(false);
    }
    public void Nope()
    {
        returnToMainMenu.SetActive(false);
        closeToMain.SetActive(true);
    }

    public void returnMainMenu()
    {
        returnToMainMenu.SetActive(true);
        closeToMain.SetActive(false);
    }

}
