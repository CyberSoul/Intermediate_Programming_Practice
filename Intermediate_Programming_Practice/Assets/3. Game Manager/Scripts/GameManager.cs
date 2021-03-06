﻿using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : SingletonTemplate<GameManager>
{
    public enum GameState
    {
        PREGAME,
        RUNNING,
        PAUSED
    }

    [SerializeField] GameObject[] c_systemPrefabs;
    [SerializeField] public Events.EventGameState OnGameStateChanged;

    private string m_currentLevelName = string.Empty;

    List<GameObject> m_instanceSystemPrefabs;
    List<AsyncOperation> m_loadOperations;

    GameState m_currentGameState = GameState.PREGAME;
    public GameState CurrentGameState
    {
        get { return m_currentGameState; }
        private set { m_currentGameState = value; }
    }

    public void Start()
    {
        DontDestroyOnLoad(gameObject); //Do not destroy our manager on some incorrect unload boot scene.

        m_instanceSystemPrefabs = new List<GameObject>();
        m_loadOperations = new List<AsyncOperation>();

        InstantiateSystemPrefabs();

        UIManager.Instance.OnMainMenuFadeComplete.AddListener(HandleNainMenuFadeComplete);
    }

    private void Update()
    {
        if (Instance.CurrentGameState == GameState.PREGAME)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
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

            if (m_loadOperations.Count == 0)
            {
                UpdateState(GameState.RUNNING);
            }
        }

        Debug.Log("Load complete");
    }

    void OnUnloadOperationComplete(AsyncOperation a_operation)
    {
        Debug.Log("Unload complete");
    }
    
    void HandleNainMenuFadeComplete(bool a_fadeOut)
    {
        if (!a_fadeOut)
        {
            UnloadLevel(m_currentLevelName);
        }
    }

    void UpdateState(GameState a_newState)
    {
        GameState prevGameState = m_currentGameState;
        m_currentGameState = a_newState;

        switch (m_currentGameState)
        {
            case GameState.PREGAME:
                Time.timeScale = 1;
                break;

            case GameState.RUNNING:
                Time.timeScale = 1;
                break;

            case GameState.PAUSED:
                Time.timeScale = 0;
                break;

            default:
                break;
        }

        OnGameStateChanged.Invoke(prevGameState, m_currentGameState);
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

    public void StartGame()
    {
        LoadLevel("Main");
    }

    public void TogglePause()
    {
        UpdateState(m_currentGameState == GameState.PAUSED ? GameState.RUNNING : GameState.PAUSED);

    }

    public void RestartGame()
    {
        UpdateState(GameState.PREGAME);
    }

    public void QuitGame()
    {
        //Do something before close. Like save.

        Application.Quit();
    }
}
