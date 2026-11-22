namespace CombatFantasyGame.Models;

public class GameSession
{
    public PlayerCharacter Player1 { get; }

    public PlayerCharacter Player2 { get; }

    /// <summary>
    /// 0 = jugador 1, 1 = jugador 2.
    /// </summary>
    public int CurrentPlayerIndex { get; private set; }

    /// <summary>
    /// 0 = cuerpo a cuerpo, &gt; 0 = distancia.
    /// </summary>
    public int Distance { get; set; } = 2;

    public int TurnNumber { get; private set; } = 1;

    public GameSession(PlayerCharacter player1, PlayerCharacter player2)
    {
        Player1 = player1;
        Player2 = player2;
        CurrentPlayerIndex = 0;
    }

    public GameSession(
        PlayerCharacter player1,
        PlayerCharacter player2,
        int currentPlayerIndex,
        int distance,
        int turnNumber) : this(player1, player2)
    {
        CurrentPlayerIndex = currentPlayerIndex == 0 ? 0 : 1;
        Distance = distance;
        TurnNumber = turnNumber <= 0 ? 1 : turnNumber;
    }

    public PlayerCharacter CurrentPlayer => CurrentPlayerIndex == 0 ? Player1 : Player2;

    public PlayerCharacter OtherPlayer => CurrentPlayerIndex == 0 ? Player2 : Player1;

    public void NextTurn()
    {
        CurrentPlayerIndex = CurrentPlayerIndex == 0 ? 1 : 0;
        TurnNumber++;

        // Al iniciar turno se aplican efectos continuos al jugador activo.
        CurrentPlayer.ApplyBleed();
        CurrentPlayer.ApplyPendingHeal();
    }
}
