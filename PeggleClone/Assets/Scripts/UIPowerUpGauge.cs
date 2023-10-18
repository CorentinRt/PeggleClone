using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UIPowerUpGauge : MonoBehaviour
{
    [SerializeField] Slider _slider;
    [SerializeField] float _duration;

    public void StartGaugeAnimation(bool Fill) => StartCoroutine(GaugeAnimation(_duration, Fill ? 1 : 0));
    // 1 = Fill
    // 0 = De-fill

    IEnumerator GaugeAnimation(float duration, int direction)
    {
        _slider.value = 1 - direction;

        float timeElapsed = 0;
        while (timeElapsed < duration)
        {
            _slider.value = Mathf.Abs(1 - direction - timeElapsed / duration);
            
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        _slider.value = direction;
    }
}
