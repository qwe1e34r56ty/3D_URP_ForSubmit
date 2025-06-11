using System.Collections.Generic;
using System.Linq;
using Unity.AI.Navigation;
using UnityEngine;

public class Stage : MonoBehaviour
{
    private StageData stageData;
    private GameObject endObject;
    private HashSet<Vector2Int> floorPositions;
    private NavMeshSurface surface;
    private MST mst;

    public List<Room> rooms;
    public Vector2Int start { get; private set; }
    public Vector2Int dest { get; private set; }

    public void initialize(GameContext gameContext, StageData stageData)
    {
        this.stageData = stageData;
        rooms = GenerateRooms(stageData);
        mst = new MST(rooms);
        floorPositions = new HashSet<Vector2Int>();
        start = rooms[0].center;
        dest = rooms[rooms.Count - 1].center;
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
                Vector2Int pos = room.center;

                Enemy enemy = gameContext.enemyFactory.BuildEnemy(gameContext, stageData.enemyData);
                enemy.Teleport(new Vector3(pos.x, 0, pos.y));
                enemy.SetNavAgent();

                gameContext.enemies.Add(enemy);
            }
        }
    }

    private List<Room> GenerateRooms(StageData stageData)
    {
        List<Room> rooms = new List<Room>();
        int attempts = stageData.rooms * 10;
        while (rooms.Count < stageData.rooms && attempts-- > 0)
        {
            int w = stageData.roomWidth;
            int h = stageData.roomHeight;
            int x = Random.Range(0, stageData.rooms * stageData.roomWidth * 3 / 4);
            int y = Random.Range(0, stageData.rooms * stageData.roomHeight * 3 / 4);
            RectInt rect = new(x, y, w, h);

            if (!rooms.Any(r => r.rect.Overlaps(rect)))
            {
                rooms.Add(new Room(rect));
            }
        }
        return rooms;
    }

    private void ApplyRoomsToFloorSet()
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

    private void ApplyCorridorsToFloorSet()
    {
        foreach (var edge in mst.edges)
        {
            Vector2Int roomAcenter = edge.roomA.center;
            Vector2Int roomBcenter = edge.roomB.center;
            int corridorWidth = 1;

            for (int x = Mathf.Min(roomAcenter.x, roomBcenter.x); x <= Mathf.Max(roomAcenter.x, roomBcenter.x); x++)
            {
                for (int w = -corridorWidth; w <= corridorWidth; w++)
                {
                    floorPositions.Add(new Vector2Int(x, roomAcenter.y + w));
                }
            }

            for (int y = Mathf.Min(roomAcenter.y, roomBcenter.y); y <= Mathf.Max(roomAcenter.y, roomBcenter.y); y++)
            {
                for (int w = -corridorWidth; w <= corridorWidth; w++)
                {
                    floorPositions.Add(new Vector2Int(roomBcenter.x + w, y));
                }
            }
        }
    }

    private void InstantiateTiles()
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
