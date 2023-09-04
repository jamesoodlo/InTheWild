using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class loadLevel : MonoBehaviour
{
    
    public int iLevelToLoad;
    public string sLevelToLoad;

    public bool useIntegerToLoadLevel = false;

    private void OnTriggerEnter(Collider collision)
    {
        
        if (collision.gameObject.tag == "Player")
        {
            LoadScene();
        }
    }

    public async void LoadScene()
    {
        if(useIntegerToLoadLevel)
        {
            SceneManager.LoadSceneAsync(iLevelToLoad, LoadSceneMode.Single);
        }
        else
        {
            SceneManager.LoadSceneAsync(sLevelToLoad, LoadSceneMode.Single);
        }
    }
}
