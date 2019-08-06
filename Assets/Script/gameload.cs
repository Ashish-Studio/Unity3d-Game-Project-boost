using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gameload : MonoBehaviour
{
     int level;
    // Start is called before the first frame update
    void Start()
    {
        level = SceneManager.GetActiveScene().buildIndex;
        StartCoroutine(Example());
        SceneManager.LoadScene(level+1);
    }

    IEnumerator Example()
    {
       
        yield return new WaitForSeconds(4);
        
    }
}
