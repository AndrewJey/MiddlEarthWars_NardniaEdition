namespace CombatFantasyGame.Models;

public enum Race
{
    Human,
    Elf,
    Orc,
    Beast
}

public enum ElfElement
{
    None,
    Fire,
    Earth,
    Air,
    Water
}

public enum HumanWeapon
{
    Shotgun,
    SniperRifle
}

public enum OrcWeapon
{
    Axe,
    Hammer
}

public enum BeastWeapon
{
    Fists,
    Sword
}

public enum BattleActionType
{
    Attack,
    Heal,
    MoveForward,
    MoveBack
}

public enum BattleResultType
{
    None,
    Player1Wins,
    Player2Wins,
    Draw
}

public enum GameLanguage
{
    Spanish,
    English,
    Portuguese,
    Japanese
}
