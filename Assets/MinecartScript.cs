using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinecartScript : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("MinecartDeathZone"))
        {
            transform.position = new Vector3(-157.97f, -32.27f, 0.0f);
        }
    }
}
