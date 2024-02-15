using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class toForestDoor : MonoBehaviour
{
    public GameObject Door;
    private Animator animator;
    [SerializeField] private AudioSource opensound;

    private void Start()
    {
        if (GetComponent<Animator>() != null)
        {
            animator = GetComponent<Animator>();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Crate"))
        {
            Destroy(Door);

            if (GetComponent<Animator>() != null)
            {
                animator.SetBool("On", true);
            }

            if (opensound.isPlaying == false)
            {
                opensound.Play();
            }
        }
    }
}
