using UnityEngine;
using UnityEngine.UI;

public class ProgressBarController : MonoBehaviour
{
    private Timer m_timer;
    [SerializeField] private Slider m_slider;

    private void Start()
    {
        gameObject.SetActive(false);
    }

    public void Set(float duration)
    {
        gameObject.SetActive(true);
        m_slider.maxValue = duration;
    }

    public void UpdateProgress(float duration)
    {
        if (duration <= 0)
        {
            Disable();
        }
        m_slider.value = duration;
    }


    public void Disable()
    {
        gameObject.SetActive(false);
    }
}
