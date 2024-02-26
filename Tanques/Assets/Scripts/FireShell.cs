using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireShell : MonoBehaviour {

    public GameObject bullet;
    public GameObject turret;
    public GameObject enemy;
    public Transform turretBase;

    float v_rotacion = 5f;
    float velocidad = 15f;
    float v_mov = 1f;

    static float delayReset = 0.2f;
    float delay = delayReset;
    void CreateBullet() {
        GameObject Shell = Instantiate(bullet, turret.transform.position, turret.transform.rotation);
        Shell.GetComponent<Rigidbody>().velocity = velocidad * turretBase.forward;
    }

    float? RotarTorreta()
    {
        float? angulo = CalcularAngulo(true);
        if (angulo != null )
        {
            turretBase.localEulerAngles = new Vector3(360f - (float)angulo, 0f, 0f);
            return angulo;
        }
    }

    float? CalcularAngulo(bool bajo)
    {
        Vector3 dirObj = enemy.transform.position - this.transform.position;
        float y = dirObj.y;
        float x = dirObj.magnitude;
        float gravedad = 9.8f;
        float dentroRaiz = Mathf.Pow(velocidad,4) - gravedad*(gravedad*Mathf.Pow(x,2)+2*y+Mathf.Pow(velocidad,2));

        if(dentroRaiz >= 0)
        {
            if (bajo)
            {
                float angulo = Mathf.Pow(velocidad, 2) - Mathf.Sqrt(dentroRaiz);
                return Mathf.Atan2(angulo, gravedad * x) * Mathf.Rad2Deg;
            }
            else
            {
                float angulo = Mathf.Pow(velocidad, 2) + Mathf.Sqrt(dentroRaiz);
                return Mathf.Atan2(angulo, gravedad * x) * Mathf.Rad2Deg;
            }
        }
        else
        {
            return null;
        }

    }

    void Update() {

        delay = delay - Time.deltaTime;
        Vector3 direccion = enemy.transform.position - this.transform.position;
        Quaternion lookat = Quaternion.LookRotation(new Vector3(direccion.x, 0f, direccion.z));
        this.transform.rotation = Quaternion.Slerp(this.transform.rotation, lookat, Time.deltaTime * v_rotacion);
        float? angulo = RotarTorreta();
        if (angulo != null && delay <= 0f) 
        {
                CreateBullet();
            delay = delayReset;
        }
    }
}
