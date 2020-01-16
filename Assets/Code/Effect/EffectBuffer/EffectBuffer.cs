using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EffectBuffer {

    public EffectBuffer (Character character) {
        selfCharacter = character;
        currentTickRate = 1f;
    }

    protected List<IEffect> Effects = new List<IEffect> ();
    protected List<float> timeStart = new List<float> ();

    protected int idCurrentEffect;
    protected IEffect CurrentEffect => Effects[idCurrentEffect];

    protected IEnumerator enumeratorProcess;
    protected Character selfCharacter;

    protected float currentTime;
    protected WaitForSeconds waiter;

    private float myCurrentTickRate;
    protected float currentTickRate {
        get => myCurrentTickRate;
        set {
            value = Mathf.Abs (value);
            myCurrentTickRate = value;
            waiter = new WaitForSeconds (value);
        }
    }

    public virtual void AddEffect (IEffect debuff) {
        Effects.Add (debuff);
        timeStart.Add (currentTime);
    }
    protected virtual void NextEffect () {
        var prevTime = CurrentEffect.MaxTime;
        var maxValuePoint = float.MinValue;
        for (int i = timeStart.Count - 1; i >= 0; i--) {

            if (currentTime - timeStart[i] >= Effects[i].MaxTime) {
                if (idCurrentEffect > i) idCurrentEffect--;
                timeStart.RemoveAt (i);
                Effects.RemoveAt (i);
                continue;
            }

            var localValue = Mathf.Abs (Effects[i].Point);
            if (localValue > maxValuePoint) {
                maxValuePoint = localValue;
                idCurrentEffect = i;
            }
        }

        if (Effects.Count == 0) {
            selfCharacter.StopCoroutine (enumeratorProcess);
            currentTime = 0;
        } else {
            var timeMinus = timeStart[idCurrentEffect];
            for (int i = timeStart.Count - 1; i >= 0; i--) {
                timeStart[i] -= timeMinus;

            }
            currentTime = prevTime - timeMinus;
            currentTickRate = GetTickRate ();
        }
    }
    protected abstract float GetTickRate ();
    protected abstract IEnumerator BufferProcess ();

}