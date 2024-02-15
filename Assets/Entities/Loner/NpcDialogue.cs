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
        // ������� �� ��, ��� �� ������ ������ ��� ������ �������
        if (Input.GetKeyDown("e") && hintBtnClick == true && isDeath == false)
        {
            ConversationManager.Instance.StartConversation(lonerDialogue);
            PlayerData.isDialogue = true;

            if (PlayerData.Boomstick == true)
            {
                ConversationManager.Instance.SetBool("Boomstick", true);
            }

            // ���� �� ������������� � ����� � ��� ���� ������� ��������, ����������� ����� ����� ��������
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

    // �������� �� ���� � ������� �������
    private void OnTriggerStay2D(Collider2D collision)
    {
        // ��������, ��� � ��� ���������� ������ ����� (�������� ����� ��� ������)
        if (collision.CompareTag("Player"))
        {
            pressEHint.SetActive(true);
            hintBtnClick = true;
        }
    }

    // �������� �� ����� �� �������� �������
    private void OnTriggerExit2D(Collider2D collision)
    {
        // �������� ��������� ������� �������
        hintBtnClick = false;
        pressEHint.SetActive(false);

        // ���� ������ �������, �� �� ��� ���������
        if (lonerDialogue.enabled == true)
        {
            lonerDialogue.enabled = false;
        }
    }
}
