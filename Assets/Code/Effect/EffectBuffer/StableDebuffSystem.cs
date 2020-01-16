using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public partial class Character {
    private Dictionary<string, StableDebuffBuffer> stableDebuffDictionary = new Dictionary<string, StableDebuffBuffer> ();
    private List<IEffect> stableEffects = new List<IEffect> ();

    public int AddStableEffect (IEffect effect) {
        stableEffects.Add (effect);
        effect.Action (this);
        return stableEffects.Count - 1;
    }
    public void DisabletableEffect (IEffect effect) {
        if (!stableEffects.Contains (effect)) return;
        effect.DisableAction (this);
        stableEffects.Remove (effect);
    }

    class StableDebuffBuffer : EffectBuffer {
        public StableDebuffBuffer (Character character) : base (character) { }

        public override void AddEffect (IEffect debuff) {
            base.AddEffect (debuff);
            if (Effects.Count == 1) {
                currentTickRate = GetTickRate ();
                enumeratorProcess = BufferProcess ();
                selfCharacter.StartCoroutine (enumeratorProcess);
            }
        }

        protected override IEnumerator BufferProcess () {
            CurrentEffect.Action (selfCharacter);
            yield return waiter;
            currentTime += currentTickRate;
            CurrentEffect.DisableAction (selfCharacter);
            NextEffect ();
        }

        protected override float GetTickRate () => CurrentEffect.MaxTime;
    }
}