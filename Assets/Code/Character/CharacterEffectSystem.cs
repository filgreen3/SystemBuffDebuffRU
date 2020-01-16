using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public partial class Character : MonoBehaviour {
    private Dictionary<string, List<Effect>> buffdebuffDictionary = new Dictionary<string, List<Effect>> ();

    private IEnumerator TimeCycle (float time, float maxTime, UnityAction<Character> action, string name) {
        var waiter = new WaitForSeconds (time);
        var timeleft = 0f;
        while (true) {
            yield return waiter;
            action.Invoke (this);
            timeleft += time;
            if (maxTime >= 0 && maxTime - timeleft < 0.05) {
                buffdebuffDictionary.Remove (name);
                yield break;
            }
        }
    }

    public void AddBuffDebuff (IEffect effect) {
        {
            switch (effect.TimeType) {
                case TimeTypeEnum.Instant:
                    effect.Action (this);
                    break;
                case TimeTypeEnum.Uniform:
                    if (effect.Buff) {
                        StartCoroutine (TimeCycle (
                            effect.ActionTime / (effect.Point * effect.ActionTime),
                            effect.MaxTime,
                            effect.Action,
                            effect.EffectTag));
                    } else {
                        if (!uniformDebuffDictionary.ContainsKey (effect.EffectTag))
                            uniformDebuffDictionary.Add (effect.EffectTag, new UniformDebuffBuffer (this));
                        uniformDebuffDictionary[effect.EffectTag].AddEffect (effect);
                    }
                    break;
                case TimeTypeEnum.Stable:
                    if (effect.Buff) {
                        effect.Action (this);
                        StartCoroutine (TimeCycle (effect.MaxTime, effect.MaxTime, effect.DisableAction, effect.EffectTag));
                    } else {
                        if (!stableDebuffDictionary.ContainsKey (effect.EffectTag)) {
                            stableDebuffDictionary.Add (effect.EffectTag, new StableDebuffBuffer (this));
                        }
                        stableDebuffDictionary[effect.EffectTag].AddEffect (effect);
                    }
                    break;
            }
        }
    }

    public void DisableAllEffects () {
        StopAllCoroutines ();
        buffdebuffDictionary.Clear ();
        uniformDebuffDictionary.Clear ();
        stableDebuffDictionary.Clear ();
        // AddRegenerationCycle ();
    }

    private void AddRegenerationCycle () {
        StartCoroutine (TimeCycle (0.5f, -1,
            (obj) => {
                if (Healty < MaxHealty)
                    Healty += 3;
                Mana += 3;
            },
            "Regeneration"));

    }

}