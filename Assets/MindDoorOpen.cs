using Assets.Entities.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MindDoorOpen : MonoBehaviour
{
    [SerializeField] GameObject mindDoor;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && PlayerData.Crystals.Count == 7)
        {
            Destroy(mindDoor);
        }
    }
}
