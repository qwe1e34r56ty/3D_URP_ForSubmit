using System.Collections.Generic;
using System.Reflection;
using System.Threading;
using UnityEngine;

public class GameContext
{
    public Stage stage;
    public Player player;
    public HashSet<Enemy> enemies;

    public List<StageData> stageDataList;
    public PlayerData playerData;

    public StageFactory stageFactory;
    public PlayerFactory playerFactory;
    public EnemyFactory enemyFactory;

    public GameContext(string stageDatasDir, string playerDataPath)
    {
        stageDataList = new List<StageData>( Resources.LoadAll<StageData>(stageDatasDir));
        playerData = Resources.Load<PlayerData>(playerDataPath);
        enemies = new HashSet<Enemy>();
        this.stageFactory = new StageFactory();
        this.playerFactory = new PlayerFactory();
        this.enemyFactory = new EnemyFactory();
    }
}