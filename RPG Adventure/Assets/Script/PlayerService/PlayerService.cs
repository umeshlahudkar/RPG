using UnityEngine;

public class PlayerService : GenericSingleton<PlayerService>
{
    [SerializeField] PlayerSO playerSO;
    [SerializeField] Transform spwanPosition;
    public PlayerView activePlayer { get; private set; }

    private void Start()
    {
        CreatePlayer();
    }

    public void CreatePlayer()
    {
        PlayerModel model = new PlayerModel(playerSO);
        PlayerController playerController = new PlayerController(model, playerSO.playerView, spwanPosition.position);
        activePlayer = playerController.playerView;
    }
}
