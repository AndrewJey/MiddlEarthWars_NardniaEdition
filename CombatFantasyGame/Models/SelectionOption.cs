namespace CombatFantasyGame.Models;

public class SelectionOption<T>
{
    public SelectionOption(T value, string displayName)
    {
        Value = value;
        DisplayName = displayName;
    }

    public T Value { get; }

    public string DisplayName { get; }

    public override string ToString() => DisplayName;
}
