
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class Room
{
    public RectInt rect;
    public Room(RectInt rect)
    {
        this.rect = rect;
    }

    public Vector2Int Center { get
        {
            return new(rect.x + rect.width / 2, rect.y + rect.height / 2);
        }
    }
    public static List<Room> GenerateRooms(StageData stageData)
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
}