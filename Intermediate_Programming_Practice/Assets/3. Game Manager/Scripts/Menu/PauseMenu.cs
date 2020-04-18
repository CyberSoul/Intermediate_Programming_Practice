using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] Button m_resumeBtn;
    [SerializeField] Button m_restartBtn;
    [SerializeField] Button m_quitBtn;

    private void Start()
    {
        m_resumeBtn.onClick.AddListener(HandleResumeClicked);
        m_restartBtn.onClick.AddListener(HandleRestartClicked);
        m_quitBtn.onClick.AddListener(HandleQuitClicked);
    }
    
    void HandleResumeClicked()
    {
        GameManager.Instance.TogglePause();
    }

    void HandleRestartClicked()
    {
        GameManager.Instance.RestartGame();
    }

    void HandleQuitClicked()
    {
        GameManager.Instance.QuitGame();
        Debug.Log("Quit");
    }
}
