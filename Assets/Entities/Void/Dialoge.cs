using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DialogueEditor;
using System;
using Assets.Entities.Player;

public class Dialoge : MonoBehaviour

{
    public string[] lines; // Массив строк с ответами
    public float speedText; // Скорость текста
    /*public Text dialoge;*/ // Текст диалога
    public TextMeshPro text;
    public int index; // Индекс выбранного ответа
    public NPCConversation myConversation;
    [SerializeField] private bool hintBtnClick;
    [SerializeField] private GameObject pressEHint;
    private bool isDialoge = false;

    public Animator anim;
    void Start()
    {
        pressEHint.SetActive(false);
        //text.text = string.Empty;
    }

    // Update is called once per frame
    void Update()
    {
        if (isDialoge == true)
        {
            // Ставим индекс нужной строки текста
            index = 0;  
            // Начинаем куратину
            StartCoroutine(TypeLine());
        }
        if (Input.GetKeyDown("e") && hintBtnClick == true)
        {
            ConversationManager.Instance.StartConversation(myConversation);
            PlayerData.isDialogue = true;

            if (PlayerData.Boomstick == true)
            {
                ConversationManager.Instance.SetBool("Boomstick", true);
            }
        }
    }

    IEnumerator TypeLine()
    {
        foreach (char c in lines[index].ToCharArray())
        {
            text.text += c;
            yield return new WaitForSeconds(speedText);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            pressEHint.SetActive(true);
            hintBtnClick = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        hintBtnClick = false;
        pressEHint.SetActive(false);
    }

    public void Death()
    {
        anim.SetBool("Death", true);
    }
}
