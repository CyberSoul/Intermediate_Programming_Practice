using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : SingletonTemplate<UIManager>
{
    [SerializeField] MainMenu m_mainMenu;
    [SerializeField] Camera m_dummyCamera;

    private void Update()
    {
        if (GameManager.Instance.CurrentGameState != GameManager.GameState.PREGAME)
        {
            return;
        }

        if ( Input.GetKeyDown(KeyCode.Space) )
        {
            GameManager.Instance.StartGame();
            //m_mainMenu.FadeOut();
        }
    }

    public void SetDummyCameraActive(bool a_value)
    {
        m_dummyCamera.gameObject.SetActive(a_value);
    }
}
