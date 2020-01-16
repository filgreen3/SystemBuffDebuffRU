using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Character : MonoBehaviour {
    public static Character CharacterInstance;

    void Start () {
        CharacterInstance = this;
        //AddRegenerationCycle ();
    }

}