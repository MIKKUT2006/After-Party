using Assets.Entities.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulElevator : MonoBehaviour
{
    public Rigidbody2D rb;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Лифт души
        if (collision.gameObject.CompareTag("Player"))
        {
            if (PlayerData.soulElevator == false && gameObject.tag != "Panic")
            {
                PlayerData.soulElevator = true;
                rb.gravityScale = -0.3f;
            }

            if (gameObject.tag == "Panic")
            {
                PlayerData.Panic = true;
                Destroy(this.gameObject);
            }
        }

        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // Лифт души
        if (collision.gameObject.CompareTag("Player"))
        {
            if (PlayerData.soulElevator == true)
            {
                PlayerData.soulElevator = false;
            }
        }
    }
}
