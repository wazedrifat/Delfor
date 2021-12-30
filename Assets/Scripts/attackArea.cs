using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attackArea : MonoBehaviour
{

    public GameObject omenModel;
    public GameObject OmenNenemy;

  
    // public golemPath golemInit;
    //omen OMEN;

    private void Start()
    {
        // golemInit = OmenNenemy.GetComponent<golemPath>();
    }

    private void Update()
    {
        // if(isAttack)
        // {
        //
        //  }

    }


    private void OnTriggerEnter(Collider enemy)
    {

        if (enemy.gameObject.tag == "Attackable")
        {
            OmenNenemy = enemy.gameObject;
            omenModel.transform.LookAt(enemy.gameObject.transform);
            //omenModel.GetComponent<Animator>().SetInteger("CanAttack", 1);
            omenModel.GetComponent<omen>().isAttack = true;
            if(enemy.gameObject.name == "Golem")
            {
                omenModel.GetComponent<omen>().isGolem = true;
            }

            else if(enemy.gameObject.name == "Hippo")
            {
                omenModel.GetComponent<omen>().isHippo = true;
            }
            else if (enemy.gameObject.name == "dragon_black")
            {
                omenModel.GetComponent<omen>().isDragon = true;
            }


            //while(omenModel.GetComponent<omen>().currHealth != 0)
            // {
            //     enemy.gameObject.GetComponent<golemPath>().takeDamage(10);
            // }



            //  OMEN.isAttack = true;
            //   if(OmenNenemy.GetComponent<golemPath>().currHealth == 0)
            // {
            //     omenModel.GetComponent<Animator>().SetInteger("CanAttack", 0);
            //  }
        }

        
    }
    private void OnCollisionExit(Collision enemy)
    {
        if (enemy.gameObject.tag == "Attackable")
        {
            
            // OmenNenemy = enemy.gameObject;
            //omenModel.transform.LookAt(enemy.gameObject.transform);
            //omenModel.GetComponent<Animator>().SetInteger("CanAttack", 1);
            omenModel.GetComponent<omen>().isAttack = false;

        }


    }
}
