using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Lever : MonoBehaviour
{
    public GameObject forestDoor;
    [SerializeField] private AudioSource opensound;
    private bool isHorizontal = false;
    private int coordinates = 30;
    private Vector2 newdoorPosition;

    private bool isOpen = false;
    private void FixedUpdate()
    {
        //if (isHorizontal == true && coordinates > 0)
        //{

        //    coordinates--;
        //}

        if (isOpen == true)
        {
            if (forestDoor.transform.position.x != newdoorPosition.x)
            {
                forestDoor.transform.position = new Vector2(0.05f * Time.deltaTime, forestDoor.transform.position.y);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && isOpen == false)
        {
            newdoorPosition.x = forestDoor.transform.position.x + 0.4f;
            isOpen = true;
            //Destroy(forestDoor);
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
