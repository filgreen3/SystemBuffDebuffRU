using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class EffectButton : MonoBehaviour {
    public Effect MyEffect;
    public EffectTimer TimerPrefub;
    private Button button;

    public static UnityEvent OnDisableAll = new UnityEvent ();

    private bool isSetEffect;

    private void Start () {
        button = GetComponent<Button> ();
        button.onClick.AddListener (Action);
        OnDisableAll.AddListener (DestroyAllTimer);
    }

    private void Action () {
        if (isSetEffect) {
            DisableStableEffect ();
            return;
        }
        if (MyEffect.TimeType != TimeTypeEnum.Stable && MyEffect.MaxTime > 0)
            Character.CharacterInstance.AddBuffDebuff (MyEffect);
        else {
            isSetEffect = true;
            Character.CharacterInstance.AddStableEffect (MyEffect);
        }

        if (MyEffect.TimeType == TimeTypeEnum.Instant) return;
        var timer = Instantiate (TimerPrefub, transform);
        timer.StartCircle (MyEffect);
    }

    private void DisableStableEffect () {
        isSetEffect = false;
        Character.CharacterInstance.DisabletableEffect (MyEffect);
        DestroyAllTimer ();
    }

    private void DestroyAllTimer () {
        isSetEffect = false;
        Character.CharacterInstance.DisabletableEffect (MyEffect);
        while (transform.childCount > 1) {
            var child = transform.GetChild (transform.childCount - 1);
            child.SetParent (null);
            Destroy (child.gameObject);
        }
    }

}