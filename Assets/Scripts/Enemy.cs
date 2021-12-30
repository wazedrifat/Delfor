using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform ghost;
   // public Transform Golem;  //new code
   // public List<Transform> EnemyList;
   // private Transform nearestEnemy;

    public GameObject omenPlayer;
    public GameObject effects;
   // public omen O;
   // private float dist;
    public float speed;
    float attackTimer = 0f;
   // public float howClose;
    Rigidbody rig;
    Animator anim;
    public bool isMove = true;

    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
       // ghost = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 pos = Vector3.MoveTowards(transform.position, ghost.position, speed * Time.fixedDeltaTime);

        if(isMove)
        {
        rig.MovePosition(pos);
        transform.LookAt(ghost);
            anim.SetInteger("run", 1);
         }
        //dist = Vector3.Distance(ghost.position, transform.position);

        //  if(dist <= howClose)
        //   {
        //       transform.LookAt(ghost);
        //       GetComponent<Rigidbody>().AddForce(transform.forward * speed);
        //    }

        float distance = Vector3.Distance(transform.position, ghost.position);
     //   print(distance);
        if(distance <= 110f)
        {
            
            if(attackTimer <=0f)
            {
                attackTimer = 1f;
                effects.SetActive(false);
            }
           
            if(attackTimer > 0f)
            {
                //print("attack");
                attackTimer -= Time.deltaTime;
                if(attackTimer <=0f)
                {
                    anim.SetInteger("attack", 1);
                    effects.SetActive(true);
                    omenPlayer.GetComponent<omen>().takeDamage(5);
                    if(omenPlayer.GetComponent<omen>().currHealth == 0)
                    {
                        anim.SetInteger("attack", 0);
                        anim.SetInteger("run", 0);
                    }
                    
                }
            }
            
           // yield return new WaitForSeconds(2);
            
        }
    }

    private void OnCollisionEnter(Collision omen)
    {
        if(omen.gameObject.name == "Ghost")
        {
            isMove = false;
            anim.SetInteger("run", 0);
        }
    }
}
