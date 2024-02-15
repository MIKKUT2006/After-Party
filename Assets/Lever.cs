using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour
{
    public GameObject forestDoor;
    [SerializeField] private AudioSource opensound;
    private bool isHorizontal = false;
    private int coordinates = 30;

    private bool isOpen = false;
    private void FixedUpdate()
    {
        if (isHorizontal == true && coordinates > 0)
        {
            
            coordinates--;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && isOpen == false)
        {
            isOpen = true;
            Destroy(forestDoor);
            Vector3 scaler = transform.localScale;

            scaler.x *= -1;

            transform.localScale = scaler;
            if(opensound.isPlaying == false)
            {
                opensound.Play();
                if (forestDoor.transform.eulerAngles.z > 0 || forestDoor.transform.eulerAngles.z < 0)
                {
                    isHorizontal = true;
                }
            }

        }
    }
}
