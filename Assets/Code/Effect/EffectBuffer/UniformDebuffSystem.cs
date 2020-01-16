using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public partial class Character {
    private Dictionary<string, UniformDebuffBuffer> uniformDebuffDictionary = new Dictionary<string, UniformDebuffBuffer> ();

    public class UniformDebuffBuffer : EffectBuffer {
        public UniformDebuffBuffer (Character character) : base (character) { }

        public override void AddEffect (IEffect debuff) {
            base.AddEffect (debuff);
            if (Effects.Count == 1) {
                enumeratorProcess = BufferProcess ();
                selfCharacter.StartCoroutine (enumeratorProcess);
            }
        }

        protected override float GetTickRate () => 1;

        protected override IEnumerator BufferProcess () {
            while (true) {
                yield return waiter;
                if (Effects.Count > 0) {
                    CurrentEffect.Action (selfCharacter);
                    currentTime += 1;

                    if (CurrentEffect.MaxTime - currentTime < 0.05) {
                        NextEffect ();
                    }
                }
            }
        }
    }
}