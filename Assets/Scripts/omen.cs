using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class omen : MonoBehaviour
{
    public int maxHealth = 20;
    public int currHealth;
   // public GameObject attackAreaObj;

    Animator omenAnim;

    public HealthBar healthBar;
    //attack
    float timeForNextPow, cooldown;


    //enemies
    public bool isHippo = false;
    public bool isGolem  = false;
    public bool isDragon  = false;
    public GameObject hippo;
    public GameObject golem;
    public GameObject dragon;


    public bool isAttack = false;

    void Start()
    {
        cooldown = 2;
        timeForNextPow = cooldown;
        omenAnim = GetComponent<Animator>();
        currHealth = maxHealth;
        healthBar.setMaxHealth(maxHealth);
    }
    void Update()
    {
           
       if(isAttack && timeForNextPow > 0)
        {
            timeForNextPow -= Time.deltaTime;
           
            //Invoke(GameObject.FindGameObjectWithTag("Golem").GetComponent<golemPath>().takeDamage(20), 2f);
           // enemy.gameObject.GetComponent<golemPath>().takeDamage(10);
        }
       else if(timeForNextPow <=0 && GameObject.FindGameObjectWithTag("Attackable"))
        {
            omenAnim.SetInteger("CanAttack", 1);
            if(isHippo)
            {
                hippo.GetComponent<hippoAction>().takeDamage(20);
            }
            else if(isGolem)
            {
                golem.GetComponent<golemPath>().takeDamage(5);
            }
            else if (isDragon)
            {
                dragon.GetComponent<dragonPk>().takeDamage(10);
            }

            // ("Hippo").GetComponent<hippoAction>().takeDamage(20);
            //GameObject.
            timeForNextPow = cooldown;
        }

    }
    public void takeDamage(int damage)
    {
        currHealth -= damage;
       // print("ME "+gameObject.name);
        healthBar.setHealth(currHealth);

        if(currHealth <= 0)
        {
            isAttack = false;
            omenAnim.SetInteger("death", 1);
            Destroy(gameObject, 2);
        }
    }
    
}
