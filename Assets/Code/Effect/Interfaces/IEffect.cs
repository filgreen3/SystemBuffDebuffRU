public interface IEffect {
    bool Buff { get; }
    string EffectTag { get; }
    float MaxTime { get; }
    float Time { get; }
    float Point { get; }
    float ActionTime { get; }

    TimeTypeEnum TimeType { get; }
    bool UsePercent { get; }

    void Action (Character character);
    void DisableAction (Character character);
}
