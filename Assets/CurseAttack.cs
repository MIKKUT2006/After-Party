using Assets.Entities.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurseAttack : MonoBehaviour
{
    public Animator anim;
    Transform Playerpos;
    public Transform curseTransform;
    public AudioSource attackSound;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (anim.GetBool("Attack") == true && PlayerData.CurseDeath == false)
        {
            if (curseTransform.position.x < Playerpos.position.x)
            {
                Vector3 scaler = curseTransform.localScale;

                scaler.x = 1;

                curseTransform.localScale = scaler;
            }
            else
            {
                Vector3 scaler = curseTransform.localScale;

                scaler.x = -1;

                curseTransform.localScale = scaler;
            }
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            anim.SetBool("Attack", true);
            Playerpos = collision.gameObject.transform;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            anim.SetBool("Attack", false);
        }
    }

    public void Attack()
    {
        attackSound.Play();
    }
}
