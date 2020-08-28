using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float health =50f;
    //public GameObject bloodEffect;

    private void Update()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void TakeDamage(float damage)
    {
        //Instantiate(bloodEffect, transfor.position, Quaternion.identity);
        Debug.Log("damage did: " + damage);
        Debug.Log("Enemy health: " + health);
        health -= damage;
    }
}
