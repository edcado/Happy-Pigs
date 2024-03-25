using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawnobject : MonoBehaviour
{
    public GameObject Spawnpoint;
    public GameObject player;
    private Rigidbody rb; 
    // Start is called before the first frame update
    void Start()
    {
        rb = player.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            player.transform.position = Spawnpoint.transform.position;
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero; 
        }
    }
}
