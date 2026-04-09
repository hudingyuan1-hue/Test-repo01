using System;

[Serializable]
public class Member
{
    public string Name;

    public float MaxAbility;

    public float CurrentAbility;

    public int AddScore;

    public int DeadScore;

    [NonSerialized] public int Score;
}
