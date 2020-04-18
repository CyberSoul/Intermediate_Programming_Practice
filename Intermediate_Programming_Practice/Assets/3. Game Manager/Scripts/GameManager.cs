using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : SingletonTemplate<GameManager>
{
    //keep track of game state
    private string m_currentLevelName = string.Empty;
    [SerializeField] GameObject[] c_systemPrefabs;

    List<GameObject> m_instanceSystemPrefabs;
    List<AsyncOperation> m_loadOperations;

    public void Start()
    {
        DontDestroyOnLoad(gameObject); //Do not destroy our manager on some incorrect unload boot scene.

        m_instanceSystemPrefabs = new List<GameObject>();
        m_loadOperations = new List<AsyncOperation>();

        InstantiateSystemPrefabs();

        LoadLevel("Main");
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();

        foreach (var obj in m_instanceSystemPrefabs)
        {
            Destroy(obj);
        }
        m_instanceSystemPrefabs.Clear();
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

    private void InstantiateSystemPrefabs()
    {
        GameObject prefabInstance;
        foreach (var prefab in c_systemPrefabs)
        {
            prefabInstance = Instantiate(prefab);

            m_instanceSystemPrefabs.Add(prefabInstance);
        }
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
