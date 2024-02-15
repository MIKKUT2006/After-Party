using Assets.Entities.Player;
using DialogueEditor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcDialogue : MonoBehaviour
{
    public NPCConversation lonerDialogue;
    [SerializeField] private bool hintBtnClick;
    [SerializeField] private GameObject pressEHint;
    private bool isDeath = false;


    void Update()
    {
        // Условие на то, что мы нажали кнопку для старта диалога
        if (Input.GetKeyDown("e") && hintBtnClick == true && isDeath == false)
        {
            ConversationManager.Instance.StartConversation(lonerDialogue);
            PlayerData.isDialogue = true;

            if (PlayerData.Boomstick == true)
            {
                ConversationManager.Instance.SetBool("Boomstick", true);
            }

            // Если мы разговариваем с БОЛЬЮ и при этом собрали кристалл, открывается новая ветка диалогов
            if (lonerDialogue.name == "Pain" && PlayerData.HappyCrystal == true)
            {
                ConversationManager.Instance.SetBool("HappyCrystal", true);
            }

            if (lonerDialogue.name == "Heart" && PlayerData.isGoodEnding == true)
            {
                ConversationManager.Instance.SetBool("GoodEnding", true);
            }
        }

        
    }

    // проверка на вход в триггер диалога
    private void OnTriggerStay2D(Collider2D collision)
    {
        // Проверка, что с НПС столкнулся именно игрок (Проверка через тег игрока)
        if (collision.CompareTag("Player"))
        {
            pressEHint.SetActive(true);
            hintBtnClick = true;
        }
    }

    // Проверка на выход из триггера диалога
    private void OnTriggerExit2D(Collider2D collision)
    {
        // Скрываем подсказку нажатия диалога
        hintBtnClick = false;
        pressEHint.SetActive(false);

        // Если диалог активен, то мы его закрываем
        if (lonerDialogue.enabled == true)
        {
            lonerDialogue.enabled = false;
        }
    }
}
