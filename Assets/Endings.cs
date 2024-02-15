using Assets.Entities.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Endings : MonoBehaviour
{
    [SerializeField] GameObject GoodText;
    [SerializeField] GameObject BadText;
    [SerializeField] AudioSource BadOst;
    [SerializeField] AudioSource GoodOst;


    private void Start()
    {
        if (PlayerData.isGoodEnding == true)
        {
            GoodText.SetActive(true);
            GoodOst.Play();
        }
        else
        {
            BadText.SetActive(true);
            BadOst.Play();
        }
    }
}
