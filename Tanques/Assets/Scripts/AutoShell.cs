using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoShell : MonoBehaviour
{
    public GameObject explosion;
    public Rigidbody rb;


    void OnCollisionEnter(Collision col)
    {

        if (col.gameObject.tag == "tank")
        {
            GameObject exp = Instantiate(explosion, this.transform.position, Quaternion.identity);
            Destroy(exp, 0.5f);
            Destroy(this.gameObject);
        }
    }
    private void Start()
    {
        rb=gameObject.GetComponent<Rigidbody>();
    }

    private void Update()
    {
        this.transform.forward = rb.velocity;
    }
}
