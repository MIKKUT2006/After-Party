using Assets.Entities.Player;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class PlayerHealth : MonoBehaviour
{
    public int Health = 3;
    private Animator animator;
    [SerializeField] private AudioSource deathSound;
    //[SerializeField] private ParticleSystem bloodParticles;
    //[SerializeField] private AudioClip sound;
    
    // Сообщение о смерти
    [SerializeField] private GameObject youDeadMessage;
    [SerializeField] private List<string> youDeadMessageText;
    [SerializeField] private TextMeshProUGUI youDeadMessageShowText;
    [SerializeField] private TextMeshPro Deaths;


    void Start()
    {
        animator = GetComponent<Animator>();
        youDeadMessageShowText = youDeadMessage.GetComponent<TextMeshProUGUI>();
        Deaths.text = "Deaths: " + PlayerData.Deaths;


    }
    //bool part = true;
    void Update()
    {
        // возрождение
        if (Input.GetKeyDown("r"))
        {
            //Health = 1;
            animator.SetBool("Death", false);
            deathSound.Stop();
            transform.position = PlayerData.spawnPoint;
            youDeadMessage.SetActive(false);
            Deaths.text = "Deaths: " + PlayerData.Deaths;
            PlayerData.Deaths += 1;

        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Spikes"))
        {
            animator.SetBool("Death", true);            // Установка анимации смерти
            youDeadMessage.SetActive(true);             // Вывод сообщения о смерти

            if (deathSound.isPlaying == false)          // Воспроизведение звука смерти
            {
                deathSound.Play();
                youDeadMessageShowText.text = youDeadMessageText[Random.Range(0, 10)];
            }
        }
    }
}
