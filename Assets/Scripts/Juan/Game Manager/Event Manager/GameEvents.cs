using System;

public static class GameEvents
{
    public static event Action OnPlayerCaught;
    public static event Action OnTimeRunOut;
    public static event Action OnGameWon;

    public static void PlayerCaught()
    {
        OnPlayerCaught?.Invoke();
    }

    public static void TimeRunOut()
    {
        OnTimeRunOut?.Invoke();
    }

    public static void GameWon()
    {
        OnGameWon?.Invoke();
    }

}
