using Assets.Entities.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ambient : MonoBehaviour
{
    //// Лист (список) эмбиентов
    //public List<AudioClip> AmbientArray = new List<AudioClip>();

    public AudioSource AmbientMusic;
    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        if (PlayerData.AmbientMusicStop == true)
        {
            if (AmbientMusic.volume > 0)
            {
                AmbientMusic.volume -= 0.01f * Time.deltaTime;
            }
            else
            {
                PlayerData.AmbientMusicStop = false;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && PlayerData.AmbientMusicStop == false)
        {
            switch (gameObject.tag)
            {
                case "Fabric":
                    AmbientMusic.clip = PlayerData.Ambientlist[0];
                    AmbientMusic.pitch = 1f;
                    break;
                case "Forest":
                    AmbientMusic.clip = PlayerData.Ambientlist[1];
                    break;
                case "Plains":
                    AmbientMusic.clip = PlayerData.Ambientlist[2];
                    break;
                case "Mind":
                    AmbientMusic.clip = PlayerData.Ambientlist[3];
                    break;
                default:
                    break;
            }
            PlayerData.AmbientMusicStop = false;
            AmbientMusic.volume = 0.1f;
            AmbientMusic.Play();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerData.AmbientMusicStop = true;
        }
    }
}
