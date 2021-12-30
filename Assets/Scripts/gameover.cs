using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameover : MonoBehaviour
{
    public List<Transform> EnemyList;
    public List<Transform> PlayerList;

    public GameObject gameoverPanel;
    public GameObject win;
    public GameObject lose;
    public GameObject bgSound;


    private void Update()
    {
        int c = 0;
        int d = 0;
        foreach(Transform enemy in EnemyList)
        {
           if(enemy != null)
            {
                c++;
            }
          
        }
        if(c==0)
        {
            print("you win game over");
            gameoverPanel.SetActive(true);
            win.SetActive(true);
            bgSound.SetActive(false);

        }

        foreach (Transform player in PlayerList)
        {
            if (player != null)
            {
                d++;
            }

        }
        if (d == 0)
        {
            gameoverPanel.SetActive(true);
            lose.SetActive(true);
            bgSound.SetActive(false);
            print("you lose game over");
        }

    }
}
