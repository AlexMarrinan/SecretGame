using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    void Awake()
    {
        if (instance == null){
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }else{
            //Debug.Log("destroyed new manager");
            Destroy(this.gameObject);
        }
    }
    void Start(){

    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public void LoadScene(string sceneName, string spawnName){
        StartCoroutine(LoadSceneCoroutine(sceneName, spawnName));
    }
    //Finds the correct spanwpoint when loading a scene,
    //Needs to be in singleton class so it can operate whne scene triggers are loaded/unloaded
    private IEnumerator LoadSceneCoroutine(string sceneName, string spawnName){
        Debug.Log("Loading scene");
       
       AsyncOperation loading = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
        
        while (!loading.isDone){
            //Debug.Log(SceneManager.GetActiveScene().name);
            //Debug.Log("Loading scene " + sceneName + "");
            yield return null;
        }

        CameraController.instance.SetXLock(false); 
        CameraController.instance.SetZLock(false);
        Debug.Log("Loading done!");
        Debug.Log(SceneManager.GetActiveScene().name);
        // Do anything after proper scene has been loaded
        //Find spawns after scene finished loading
        var spawns = FindObjectsOfType<PlayerSpawn>();

        foreach (PlayerSpawn ps in spawns){
            if (ps.spawnName == spawnName){
                PlayerController.instance.SetPostition(ps.transform.position);
                if (ps.activeObjects != null){
                    Debug.Log("Set objects active");
                    ps.activeObjects.SetActive(true);
                }
                break;
            }
        }

        yield return null;
    }
}
