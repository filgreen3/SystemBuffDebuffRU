using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public partial class Character : MonoBehaviour {
    public UnityEvent OnUpdateStats = new UnityEvent ();

    [SerializeField]
    private float healty, mana, maxHealty, maxMana;

    public float Healty {
        get => healty;
        set {
            healty = Mathf.Clamp (value, 0, MaxHealty);
            OnUpdateStats.Invoke ();
        }
    }
    public float Mana {
        get => mana;
        set {
            mana = Mathf.Clamp (value, 0, MaxMana);
            OnUpdateStats.Invoke ();
        }
    }

    public float MaxHealty {
        get => maxHealty;
        set {
            maxHealty = value;
            Healty = Healty;
            OnUpdateStats.Invoke ();
        }
    }
    public float MaxMana {
        get => maxMana;
        set {
            maxMana = value;
            Mana = Mana;
            OnUpdateStats.Invoke ();
        }
    }

}