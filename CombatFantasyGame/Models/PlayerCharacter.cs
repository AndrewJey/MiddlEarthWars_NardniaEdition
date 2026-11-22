namespace CombatFantasyGame.Models;

public class PlayerCharacter
{
    public string PlayerName { get; set; } = string.Empty;

    public Race Race { get; set; }

    public ElfElement Element { get; set; } = ElfElement.None;

    public HumanWeapon? HumanWeapon { get; set; }

    public OrcWeapon? OrcWeapon { get; set; }

    public BeastWeapon? BeastWeapon { get; set; }

    public int MaxHealth { get; set; }

    public int CurrentHealth { get; set; }

    public bool IsDead => CurrentHealth <= 0;

    // Estados de daño prolongado / curación
    public int BleedTurnsRemaining { get; set; }

    public int BleedDamagePerTurn { get; set; }

    public double PendingHealPercentNextTurn { get; set; }

    public void ApplyBleed()
    {
        if (BleedTurnsRemaining <= 0 || BleedDamagePerTurn <= 0)
        {
            return;
        }

        CurrentHealth -= BleedDamagePerTurn;
        BleedTurnsRemaining--;
    }

    public void ApplyPendingHeal()
    {
        if (PendingHealPercentNextTurn <= 0)
        {
            return;
        }

        var lostHealth = MaxHealth - CurrentHealth;
        if (lostHealth <= 0)
        {
            PendingHealPercentNextTurn = 0;
            return;
        }

        var healAmount = (int)Math.Round(lostHealth * PendingHealPercentNextTurn);
        CurrentHealth = Math.Min(MaxHealth, CurrentHealth + healAmount);
        PendingHealPercentNextTurn = 0;
    }
}

