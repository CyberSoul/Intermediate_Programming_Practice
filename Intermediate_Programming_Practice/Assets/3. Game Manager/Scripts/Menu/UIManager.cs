using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : SingletonTemplate<UIManager>
{
    [SerializeField] MainMenu m_mainMenu;
    [SerializeField] PauseMenu m_pauseMenu;
    [SerializeField] Camera m_dummyCamera;

    private void Start()
    {
        GameManager.Instance.OnGameStateChanged.AddListener(HandleGameStateChanged);
    }

    public void HandleGameStateChanged(GameManager.GameState a_prevState, GameManager.GameState a_currentStae)
    {
        m_pauseMenu.gameObject.SetActive(a_currentStae == GameManager.GameState.PAUSED);
    }

    private void Update()
    {
        if (GameManager.Instance.CurrentGameState != GameManager.GameState.PREGAME)
        {
            return;
        }

        if ( Input.GetKeyDown(KeyCode.Space) )
        {
            GameManager.Instance.StartGame();
        }

    }

    public void SetDummyCameraActive(bool a_value)
    {
        m_dummyCamera.gameObject.SetActive(a_value);
    }
}
