using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterPropertyManager : MonoBehaviour {

    public Character MyCharacter;
    public RectTransform HealtyBar, HealtyMaxBar;
    public RectTransform ManaBar, ManaMaxBar;

    public Text HealtyText, ManaText;

    private void Start () { MyCharacter.OnUpdateStats.AddListener (UpdateDate); UpdateDate (); }

    [ContextMenu ("Update Character Property")]
    private void UpdateDate () {
        HealtyBar.localScale = Vector3.up + Vector3.right * (MyCharacter.Healty / MyCharacter.MaxHealty);
        ManaBar.localScale = Vector3.up + Vector3.right * (MyCharacter.Mana / MyCharacter.MaxMana);

        HealtyMaxBar.localScale = Vector3.up + Vector3.right * (MyCharacter.MaxHealty / 100f);
        ManaMaxBar.localScale = Vector3.up + Vector3.right * (MyCharacter.MaxMana / 100f);

        HealtyText.text = $"{MyCharacter.MaxHealty}|{MyCharacter.Healty}";
        ManaText.text = $"{MyCharacter.MaxMana}|{MyCharacter.Mana}";
    }
    public void CallDisableAllEffects () {
        MyCharacter.DisableAllEffects ();
        EffectButton.OnDisableAll.Invoke ();
    }

}