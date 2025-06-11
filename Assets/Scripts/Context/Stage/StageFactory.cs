using UnityEngine;

public class StageFactory
{
    public Stage BuildStage(GameContext gameContext, StageData stageData)
    {
        GameObject stageRoot = new GameObject("StageRoot");
        if (stageData.floorPrefab != null)
        {
            Stage stage = stageRoot.AddComponent<Stage>();
            stage.initialize(gameContext, stageData);
            return stage;
        }
        return null;
    }

    public void DestoryStage(GameObject stageRoot)
    {
        GameObject.Destroy(stageRoot);
    }
}