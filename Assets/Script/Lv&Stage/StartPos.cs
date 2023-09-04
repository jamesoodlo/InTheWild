using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartPos : MonoBehaviour
{
    public string sceneName;

    void OnEnable() 
    {
        SceneManager.sceneLoaded += SceneLoaded;
    }

    void OnDisable() 
    {
        SceneManager.sceneLoaded -= SceneLoaded;
    }

    public async void  SceneLoaded(Scene scene, LoadSceneMode mode)
    {
        scene = SceneManager.GetActiveScene();
        sceneName = scene.name;

        if(sceneName == "Lobby" || sceneName == "LV1" || sceneName == "LV2" || sceneName == "LV3" || sceneName == "LV4" || sceneName == "LV5" || sceneName == "LV6")
        {   
            FindStartPos();
            if(transform.position != GameObject.FindWithTag("StartPos").transform.position)
            {
                StartCoroutine(findPosition());
            }
            Debug.Log("Level Loaded");
            Debug.Log(scene.name);
            Debug.Log(mode);  
        }  
    }

    private void FindStartPos()
    {
        this.transform.position = GameObject.FindWithTag("StartPos").transform.position;
    }

    private IEnumerator findPosition()
    {
        yield return new WaitForSeconds(0.2f);
        this.transform.position = GameObject.FindWithTag("StartPos").transform.position;
    }
    
}
