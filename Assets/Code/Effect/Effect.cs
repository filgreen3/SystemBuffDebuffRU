using System.Reflection;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu (fileName = "NewEffect", menuName = "Create Effect", order = 66)]
public class Effect : ScriptableObject, IEffect {

    [SerializeField]
    private string effectTag = "Standart";
    public string EffectTag => effectTag;

    [SerializeField]
    private float point = 0;
    public float Point => point;

    [SerializeField]
    private float actionTime = 0;
    public float ActionTime => actionTime;

    [SerializeField]
    private TimeTypeEnum timeType = 0;
    public TimeTypeEnum TimeType => timeType;

    public bool Buff => Point > 0;

    public float MaxTime { get => ActionTime; }
    public float Time => MaxTime / Point;

    [SerializeField]
    private bool usePercent = false;
    public bool UsePercent => usePercent;

    public TargetPropertyEnum TargetProperty;

    public void Action (Character character) {
        PropertyInfo fildtoset = typeof (Character).GetProperty (TargetProperty.ToString ());
        var value = (float) fildtoset.GetValue (character);

        if (Buff && timeType == TimeTypeEnum.Uniform) {
            if (usePercent) {
                PropertyInfo fildtoset2 = typeof (Character).GetProperty (((int) TargetProperty + 2).ToString ());
                var value2 = (float) fildtoset2.GetValue (character);
                fildtoset.SetValue (character, value + value2 * (point / 100f));
            } else
                fildtoset.SetValue (character, value + 1 * Mathf.Sign (point));
        } else {
            if (usePercent) {
                PropertyInfo fildtoset2 = typeof (Character).GetProperty ((TargetProperty + 2).ToString ());
                var value2 = (float) fildtoset2.GetValue (character);
                fildtoset.SetValue (character, value + value2 * (point / 100f));
            } else
                fildtoset.SetValue (character, value + Point);
        }
    }

    public void DisableAction (Character character) {
        if (TimeType == TimeTypeEnum.Stable) {
            PropertyInfo fildtoset = typeof (Character).GetProperty (TargetProperty.ToString ());
            var value = (float) fildtoset.GetValue (character);
            fildtoset.SetValue (character, value - Point);
        }
    }

}