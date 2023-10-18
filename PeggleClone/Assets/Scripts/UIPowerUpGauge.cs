using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UIPowerUpGauge : MonoBehaviour
{
    [SerializeField] Slider _slider;
    [SerializeField] float _duration;

    public void StartGaugeAnimation(bool Fill)
    {
        if (Fill) StartCoroutine(GaugeAnimationFill(_duration));
        else StartCoroutine(GaugeAnimationDepleate(_duration));
    }

    IEnumerator GaugeAnimationFill(float duration)
    {
        _slider.value = 0;

        float timeElapsed = 0;
        while (timeElapsed < duration)
        {
            _slider.value = timeElapsed / duration;
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        _slider.value = 1f;
    }
    IEnumerator GaugeAnimationDepleate(float duration)
    {
        _slider.value = 1f;

        float timeElapsed = 1;
        while (timeElapsed > duration)
        {
            _slider.value = timeElapsed / duration;
            timeElapsed -= Time.deltaTime;
            yield return null;
        }

        _slider.value = 0f;
    }
}
