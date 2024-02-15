using Assets.Entities.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyOpen : MonoBehaviour
{
    public GameObject Door;
    //private bool isOpen = false;
    public TMPro.TextMeshPro m_TextMeshPro;
    public GameObject keyName;
    public string keyNameText;
    public string needKeyText;

    private bool isKey;
    private void Start()
    {
        keyNameText = keyName.name;

        // При запуске игры открываем открытые ранее двери
        for (int i = 0; i < PlayerData.Doorname.Count; i++)
        {
            if (PlayerData.Doorname[i] == Door.name)
            {
                Destroy(Door);
                m_TextMeshPro.gameObject.SetActive(false);
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        ListItemFind(PlayerData.keysTags);
        if (isKey == true && collision.gameObject.CompareTag("Player"))
        {
            PlayerData.keysTags.Remove(keyNameText);
            //PlayerData.cityKey = false;
            m_TextMeshPro.gameObject.SetActive(false);
            PlayerData.Doorname.Add(Door.name);
            Destroy(Door);
            //isOpen = true;
        }
        else
        {
            m_TextMeshPro.gameObject.SetActive(true);
            m_TextMeshPro.text = needKeyText;
        }
    }

    // Поиск элемента списка
    private void ListItemFind(List<string> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            if (list[i] == keyNameText)
            {
                isKey =  true;
            }
            else
            {
                isKey = false;
            }
        }
        
    }
}
