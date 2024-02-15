using Assets.Entities.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurseScript : MonoBehaviour
{
    private void Start()
    {
        if (PlayerData.CurseDeath == true)
        {
            PlayerData.CurseDeath = true;
            gameObject.transform.GetChild(0).gameObject.layer = LayerMask.NameToLayer("Plants");
            gameObject.transform.GetChild(1).gameObject.layer = LayerMask.NameToLayer("Plants");
            gameObject.transform.GetChild(5).gameObject.GetComponent<BoxCollider2D>().enabled = true;
            gameObject.GetComponent<Animator>().SetBool("Death", true);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Crate"))
        {
            PlayerData.CurseDeath = true;
            gameObject.transform.GetChild(0).gameObject.layer = LayerMask.NameToLayer("Plants");
            gameObject.transform.GetChild(1).gameObject.layer = LayerMask.NameToLayer("Plants");
            gameObject.transform.GetChild(5).gameObject.GetComponent<BoxCollider2D>().enabled = true;
            gameObject.GetComponent<Animator>().SetBool("Death", true);
        }
    }
}
