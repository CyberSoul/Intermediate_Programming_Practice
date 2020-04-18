using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // What level the game is currently loaded
    //Load and unload game level
    //keep track of game state
    //generate other persistent systems
    private string m_currentLevelName = string.Empty;

    List<AsyncOperation> m_loadOperations;

    public void Start()
    {
        DontDestroyOnLoad(gameObject); //Do not destroy our manager on some incorrect unload boot scene.

        m_loadOperations = new List<AsyncOperation>();
        LoadLevel("Main");
    }

    //Load and unload game level
    void OnLoadOperationComplete(AsyncOperation a_operation)
    {
        if (m_loadOperations.Contains(a_operation))
        {
            m_loadOperations.Remove(a_operation);
            
            //dispatch message
            //transition between scenes
        }

        Debug.Log("Load complete");
    }

    void OnUnloadOperationComplete(AsyncOperation a_operation)
    {
        Debug.Log("Unload complete");
    }

    public void LoadLevel(string a_levelname)
    {
        //SceneManager.LoadScene(a_levelname);
        AsyncOperation ao = SceneManager.LoadSceneAsync(a_levelname, LoadSceneMode.Additive);
        if (ao == null)
        {
            Debug.LogError($"[GameManager] unable to load level {a_levelname}");
            return;
        }
        ao.completed += OnLoadOperationComplete;
        m_loadOperations.Add(ao);
        m_currentLevelName = a_levelname;
    }

    public void UnloadLevel(string a_levelName)
    {
        AsyncOperation ao = SceneManager.UnloadSceneAsync(a_levelName);
        ao.completed += OnUnloadOperationComplete;
    }
}
