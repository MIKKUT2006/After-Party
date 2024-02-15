using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.XR;

public class MovingDrone : MonoBehaviour
{
    public GameObject Drone;
    public Rigidbody2D DroneRb;
    public int Flip = 1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    float Xvelocity;
    private void FixedUpdate()
    {
        Xvelocity = Flip * 5;
        DroneRb.velocity = new Vector2(Flip * 1, DroneRb.velocity.y);
    }

    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer != LayerMask.NameToLayer("Spikes"))
        {
            if (Flip == 1)
            {
                Flip = -1;
                //Drone.transform.Rotate(new Vector3(0,180,0));
                Drone.transform.localScale = new Vector3(-12, 12, 12);
            }
            else
            {
                Flip = 1;
                //Drone.transform.Rotate(new Vector3(0, 180, 0));
                Drone.transform.localScale = new Vector3(12, 12, 12);
            }
        }
    }
}
