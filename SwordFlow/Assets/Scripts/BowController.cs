using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowController : MonoBehaviour
{
    public Transform FirePoint;
    public GameObject Arrow;

    private float ShootTrigger;

    private void Start()
    {
        ShootTrigger = 0f;
    }
    void Update()
    {
        if (ShootTrigger > 0.5f)
        {
            ShootTrigger = 0f;
            Shoot();
        }

        ShootTrigger += Time.deltaTime;
    }

    private void Shoot()
    {
        Instantiate(Arrow, FirePoint.position, FirePoint.rotation);
    }
}
