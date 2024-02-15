using Assets.Entities.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateGravity : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (PlayerData.gravityRotate == false)
            {
                PlayerData.gravityRotate = true;
            }
            else
            {
                PlayerData.gravityRotate = false;
            }
        }
    }
}
