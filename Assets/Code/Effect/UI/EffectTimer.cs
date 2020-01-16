using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EffectTimer : MonoBehaviour {
    public Image Circle;
    public Text InfoText;

    public void StartCircle (Effect effect) {
        Circle.color = ((int) effect.TargetProperty % 2 == 0) ? Color.red : Color.blue;
        InfoText.text = $"{effect.Point}\n{effect.TimeType}";
        if (effect.MaxTime > 0)
            StartCoroutine (CircleProcess (effect.MaxTime));
    }
    private IEnumerator CircleProcess (float maxTime) {
        var waiter = new WaitForSeconds (0.05f);
        float currentTime = 0;
        while (currentTime <= maxTime) {
            Circle.fillAmount = 1 - (currentTime / maxTime);
            currentTime += 0.05f;
            yield return waiter;
        }
        Destroy (gameObject);
    }
}