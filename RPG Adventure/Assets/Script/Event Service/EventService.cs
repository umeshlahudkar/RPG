using System;

public class EventService : GenericSingleton<EventService>
{
    public event Action OnPlayerDied;
    public event Action OnEnemyDead;
    public event Action OnLevelUp;
    
    public void InvokeOnPlayerDiedEvent()
    {
        OnPlayerDied?.Invoke();
    }

    public void InvokeOnEnemyDeadEvent()
    {
        OnEnemyDead?.Invoke();
    }

    public void InvokeOnLevelUpEvent()
    {
        OnLevelUp?.Invoke();
    }
}
