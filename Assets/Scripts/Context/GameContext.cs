using System.Collections.Generic;
using System.Reflection;
using System.Threading;
using UnityEngine;

public class GameContext
{
    public Stage stage = null;
    public Player player = null;
    public HashSet<Enemy> enemies = new HashSet<Enemy>();

    public List<StageData> stageDataList = null;
    public PlayerData playerData = null;

    public StageFactory stageFactory = new StageFactory();
    public PlayerFactory playerFactory = new PlayerFactory();
    public EnemyFactory enemyFactory = new EnemyFactory();

    public GameContext(string stageDatasDir, string playerDataPath)
    {
        stageDataList = new List<StageData>( Resources.LoadAll<StageData>(stageDatasDir));
        playerData = Resources.Load<PlayerData>(playerDataPath);
    }
}