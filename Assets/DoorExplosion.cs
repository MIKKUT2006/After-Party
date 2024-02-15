using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorExplosion : MonoBehaviour
{
    public GameObject door;
    public ParticleSystem explosionParticle;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bomb"))
        {
            Vector3 bombPosition = collision.transform.position;
            Destroy(door);
            Instantiate(explosionParticle, bombPosition, new Quaternion());
            Destroy(collision);
        }
    }
}

