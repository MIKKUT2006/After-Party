using Assets.Entities.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepairFactory : MonoBehaviour
{
    [SerializeField] GameObject UseText;
    [SerializeField] GameObject Door;
    private bool Repair = false;
    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown("e") && Repair == true)
        {
            // Чиним фабрику и открывается дверь
            gameObject.GetComponent<Animator>().SetBool("Repair", true);
            Destroy(Door);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && PlayerData.BlueWire == true && PlayerData.WhiteWire == true && PlayerData.RedWire == true) 
        {
            Repair = true;
            UseText.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && PlayerData.BlueWire == true && PlayerData.WhiteWire == true && PlayerData.RedWire == true)
        {
            Repair = true;
            UseText.SetActive(false);
        }
    }
}
