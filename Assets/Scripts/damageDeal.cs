using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class damageDeal : MonoBehaviour
{
    public int maxHealth;
    public int currHealth;

    public HealthBar healthBar;

    public void takeDamage(int damage)
    {
        currHealth -= damage;
        // print("ME "+gameObject.name);
        healthBar.setHealth(currHealth);

        if (currHealth == 0)
        {
            
            //anim.SetInteger("death", 1);
            // gameObject.GetComponent<Collider>().enabled = false;
            Destroy(gameObject, 2);

        }
    }
}
