using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPlayerInArea : MonoBehaviour
{
    public PatrolController patrollController;
    public int playerLayer;

    private void OnTriggerStay2D(Collider2D collision)
    {
        patrollController.attack = collision.gameObject.layer == playerLayer;
    }
}
