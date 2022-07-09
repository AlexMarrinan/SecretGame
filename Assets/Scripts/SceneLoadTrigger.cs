using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneLoadTrigger : MonoBehaviour
{
    public string sceneName;
    public string spawnName;
    
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            LoadScene();
        }
    }

    public void OnEvent(){
        SceneManager.LoadScene(sceneName);
    }

    public void LoadScene(){
        if (SceneManager.GetActiveScene().name != sceneName)
        {
            GameManager.instance.LoadScene(sceneName, spawnName);
        }
    }
}