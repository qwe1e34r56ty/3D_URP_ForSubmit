using System.Collections.Generic;
using System.Linq;
using Unity.AI.Navigation;
using UnityEngine;

public class Stage : MonoBehaviour
{
    GameContext gameContext;
    StageData stageData;
    public List<Room> rooms;
    GameObject endObject;
    HashSet<Vector2Int> floorPositions;
    NavMeshSurface surface;
    public Vector2Int start { get; private set; }
    public Vector2Int dest { get; private set; }
    MST mst;

    public void initialize(GameContext gameContext, StageData stageData)
    {
        this.stageData = stageData;
        rooms = Room.GenerateRooms(stageData);
        mst = new MST(rooms);
        floorPositions = new HashSet<Vector2Int>();
        start = rooms[0].Center;
        dest = rooms[rooms.Count - 1].Center;
        endObject = Instantiate(stageData.EndPrefab, new Vector3(dest.x, stageData.EndPrefab.transform.localScale.y / 2, dest.y), Quaternion.identity, transform);

        ApplyRoomsToFloorSet();
        ApplyCorridorsToFloorSet();
        InstantiateTiles();

        surface = gameObject.AddComponent<NavMeshSurface>();
        surface.collectObjects = CollectObjects.Children;
        surface.layerMask = LayerMask.GetMask("Walkable");
        surface.BuildNavMesh();

        if (stageData.enemyData != null && rooms.Count > 0)
        {
            foreach (var room in rooms)
            {
                Vector2Int pos = room.Center;

                Enemy enemy = gameContext.enemyFactory.BuildEnemy(gameContext, stageData.enemyData);
                enemy.Teleport(new Vector3(pos.x, 0, pos.y));
                enemy.SetNavAgent();

                gameContext.enemies.Add(enemy);
            }

        }
    }


    void ApplyRoomsToFloorSet()
    {
        foreach (Room room in rooms)
        {
            for (int x = room.rect.xMin; x < room.rect.xMax; x++)
            {
                for (int y = room.rect.yMin; y < room.rect.yMax; y++)
                {
                    floorPositions.Add(new Vector2Int(x, y));
                }
            }
        }
    }

    void ApplyCorridorsToFloorSet()
    {
        foreach (var edge in mst.edges)
        {
            Vector2Int roomACenter = edge.roomA.Center;
            Vector2Int roomBCenter = edge.roomB.Center;
            int corridorWidth = 1;

            for (int x = Mathf.Min(roomACenter.x, roomBCenter.x); x <= Mathf.Max(roomACenter.x, roomBCenter.x); x++)
            {
                for (int w = -corridorWidth; w <= corridorWidth; w++)
                {
                    floorPositions.Add(new Vector2Int(x, roomACenter.y + w));
                }
            }

            for (int y = Mathf.Min(roomACenter.y, roomBCenter.y); y <= Mathf.Max(roomACenter.y, roomBCenter.y); y++)
            {
                for (int w = -corridorWidth; w <= corridorWidth; w++)
                {
                    floorPositions.Add(new Vector2Int(roomBCenter.x + w, y));
                }
            }
        }
    }

    void InstantiateTiles()
    {
        foreach (Vector2Int pos in floorPositions)
        {
            if (stageData.floorPrefab)
            {
                GameObject.Instantiate(stageData.floorPrefab, new Vector3(pos.x, - stageData.floorPrefab.transform.localScale.y / 2, pos.y), Quaternion.identity, transform);
            }
        }
    }
}
