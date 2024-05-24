using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    //Script Del Enemigo
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Si el entra en colision con el mapache destruye al enemigo
        bool isBird = collision.gameObject.GetComponent<Ball>();

        if (isBird)
            Destroy(gameObject);
        

        //Si es golpeado por arriba con cualquier cosa destruye al enemigo
        bool isCrushed = collision.contacts[0].normal.y < -0.5f;

        if (isCrushed)
            Destroy(gameObject);

    }

}


