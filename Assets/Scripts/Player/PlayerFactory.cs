using UnityEngine;

public class PlayerFactory
{
    public Player BuildPlayer(GameContext gameContext, PlayerData playerData)
    {
        GameObject playerRoot = GameObject.Instantiate(playerData.playerPrefab);
        if(playerData.playerPrefab != null)
        {
            if (playerRoot.TryGetComponent<Player>(out Player player))
            {
                player.initialize(gameContext, playerData);
                return player;
            }
            return null;
        }
        return null;
    }

    public void DestoryStage(GameObject playerRoot)
    {
        if (playerRoot != null)
        {
            GameObject.Destroy(playerRoot);
        }
    }
}