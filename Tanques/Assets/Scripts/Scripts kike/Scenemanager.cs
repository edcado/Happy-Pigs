using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Scenemanager : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void PlayMethod()
    {
        // esto sirve para poder guardar desde el principio las variables 

        SceneManager.LoadScene("Tanks");




    }
    public void ExitGame()
    {
        Application.Quit();
    }
    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            SceneManager.LoadScene("Win");
        }
    }
}
