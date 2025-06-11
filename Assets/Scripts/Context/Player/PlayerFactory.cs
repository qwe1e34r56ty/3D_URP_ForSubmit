using UnityEngine;

public class PlayerFactory
{
    public Player BuildPlayer(GameContext gameContext, PlayerData playerData)
    {
        GameObject playerRoot = GameObject.Instantiate(playerData.prefab);
        if(playerData.prefab != null)
        {
            if (playerRoot.TryGetComponent<Player>(out Player player))
            {
                player.Initialize(gameContext, playerData);
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