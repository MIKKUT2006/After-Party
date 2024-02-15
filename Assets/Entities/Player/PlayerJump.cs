using Assets.Entities.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    public Rigidbody2D rb; // Физика для игрока
    public float JumpForce = 17.0f; // Скорость прыжка игрока
    public bool OnGround; // Проверка на нахождение на земле
    public Animator Animation;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Jump") == 1 && PlayerData.OnGround == true && Animation.GetBool("Death") != true && PlayerData.Panic == false)
        {
            if (PlayerData.gravityRotate == false)
            {
                rb.AddForce(new Vector2(0, JumpForce), ForceMode2D.Impulse);
            }
            else
            {
                rb.AddForce(new Vector2(0, -JumpForce), ForceMode2D.Impulse);
            }

            PlayerData.OnGround = false;
        }
    }
    Rigidbody2D PlatformRb;

    // Здесь раньше был прыжок не через OnTrigger**
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            PlayerData.OnGround = false;
            Animation.SetBool("onGround", false);
        }

    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            PlayerData.Planer = false;
            PlayerData.OnGround = true;
            Animation.SetBool("onGround", true);
            //Debug.Log("Земля");
        }
    }

    
}
