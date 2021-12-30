using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hippoAction : MonoBehaviour
{
    //health System
    public int maxHealth = 100;
    public int currHealth;

    public HealthBar healthBar;

    public Transform Hippo;
    public GameObject[] multipleEnemies;
    public List<Transform> EnemyList;

    private Transform nearestEnemy;
    //public GameObject effects;
    public float attackTimer = 0f;
    Animator anim;
    public bool isMove = true;

    public float speed;
    Rigidbody rig;

    //public GameObject golemiihfb;


    //colide
    private bool collideWithGolem;


    void Start()
    {
        rig = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        
       

       

        currHealth = maxHealth;
        healthBar.setMaxHealth(maxHealth);

    }

    void FixedUpdate()
    {
        //golemiihfb.GetComponent<golemPath>().isMove = false;
        foreach(Transform enemy in EnemyList)
        {
            if(enemy == null)
            {
                EnemyList.Remove(enemy);

            }
        }
        if(EnemyList.Count == 0)
        {
            anim.SetInteger("idle", 1);
            anim.SetInteger("attack", 0);
            anim.SetInteger("run", 0);
        }
        float minimumDistance = Mathf.Infinity;
        if (nearestEnemy != null)
        {
            nearestEnemy.GetComponent<MeshRenderer>().material.color = Color.green;
        }
        nearestEnemy = null;
        // Transform minmEnemy = null;
        foreach (Transform enemy in EnemyList)
        {
            float distance = Vector3.Distance(Hippo.position, enemy.position);

            if (distance < minimumDistance)
            {
                minimumDistance = distance;
                // minmEnemy = enemy;
                nearestEnemy = enemy;

            }
        }
        if (nearestEnemy == null)
        {
            return;
        }
       // print("NE " + nearestEnemy.gameObject.name);
        //  print(minimumDistance + "Sf" + EnemyList.Count);


        if (minimumDistance <= 45f || (collideWithGolem && minimumDistance < 80f))
        {


            if (attackTimer <= 0f)
            {
                //  print("im going here");
                attackTimer = 1f;
               // effects.SetActive(false);
            }

            if (attackTimer > 0f)
            {
                // print("im going here too");
                attackTimer -= Time.deltaTime;
                if (attackTimer <= 0f)
                {
                    anim.SetInteger("attack", 1);
                    anim.SetInteger("run", 0);

                 //   effects.SetActive(true);
                    nearestEnemy.GetComponent<omen>().takeDamage(5);
                  //  print("Length:" + EnemyList.Count);
                  //  print(nearestEnemy.gameObject.name);
                    if (nearestEnemy.GetComponent<omen>().currHealth == 0)
                    {
                        //   print("ok last");
                        anim.SetInteger("attack", 0);
                        anim.SetInteger("run", 0);
                        EnemyList.Remove(nearestEnemy);
                        //attackTimer = 1f;
                    }

                    if(EnemyList.Count == 0)
                    {
                        print("Game over you won");
                    }
                }
            }

            // yield return new WaitForSeconds(2);

        }
        else
        {
            Vector3 pos = Vector3.MoveTowards(transform.position, nearestEnemy.position, speed * Time.fixedDeltaTime);



            rig.MovePosition(pos);
            transform.LookAt(nearestEnemy);
            anim.SetInteger("run", 1);



        }


        nearestEnemy.GetComponent<MeshRenderer>().material.color = Color.red;
       // print("Golem " + anim.GetInteger("run") + " " + anim.GetInteger("attack") + " " + anim.GetInteger("idle"));
        //  Debug.Log("Nearest Enemy: " + nearestEnemy + "; Distance: " + minimumDistance);
        //  print(EnemyList.IndexOf(nearestEnemy));
    }

    public void takeDamage(int damage)
    {
        currHealth -= damage;
        // print("ME "+gameObject.name);
        healthBar.setHealth(currHealth);

        if (currHealth <= 0)
        {
            anim.SetInteger("death", 1);
            // gameObject.GetComponent<Collider>().enabled = false;
            Destroy(gameObject, 1);

        }
    }



    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.name == "Golem")
        {
            collideWithGolem = true;
        }
        
    }
    private void OnCollisionExit(Collision collision)
    {
       if(collision.gameObject.name == "Golem")
        {
            collideWithGolem = false;
        }
    }
}
