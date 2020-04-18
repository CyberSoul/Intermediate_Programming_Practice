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
    }
    
    void HandleResumeClicked()
    {
        GameManager.Instance.TogglePause();
    }
}
