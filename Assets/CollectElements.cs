using Assets.Entities.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectElements : MonoBehaviour
{
    [SerializeField] ParticleSystem particles;
    [SerializeField] Vector2 PlayerVector;
    //private Rigidbody2D rb2;
    void Start()
    {
        // Получаем компонент Rb2d для движения объекта
        //rb2 = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            particles.Play();
            switch (gameObject.name)
            {
                case "Light":
                    PlayerData.Light = true;
                    break;
                case "Mechanism":
                    PlayerData.Mechanism = true;
                    break;
                case "Heart":
                    PlayerData.Heart = true;
                    break;
                default:
                    break;
            }
            gameObject.SetActive(false);
        }
    }
}
