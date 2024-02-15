using Assets.Entities.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiveBlueWire : MonoBehaviour
{
    // Метод для выдачи синиго провода игроку
    [SerializeField] GameObject Crystal;
    public void BlueWireGive()
    {
        PlayerData.BlueWire = true;
        Crystal.SetActive(true);
    }
}
