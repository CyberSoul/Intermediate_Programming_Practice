using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] Animation m_mainMenuAnimator;
    [SerializeField] AnimationClip m_fadeOutAnimation;
    [SerializeField] AnimationClip m_fadeInAnimation;

    public void OnFadeOutComplete()
    {
        UIManager.Instance.SetDummyCameraActive(false);
        Debug.Log("FadeOut Complete");
    }

    public void OnFadeInComplete()
    {
        UIManager.Instance.SetDummyCameraActive(true);
        Debug.Log("FadeIn Complete");
    }

    public void FadeIn()
    {
        m_mainMenuAnimator.Stop();
        m_mainMenuAnimator.clip = m_fadeInAnimation;
        m_mainMenuAnimator.Play();
    }

    public void FadeOut()
    {
        m_mainMenuAnimator.Stop();
        m_mainMenuAnimator.clip = m_fadeOutAnimation;
        m_mainMenuAnimator.Play();
    }
}
