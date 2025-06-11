
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class Room
{
    public RectInt rect;
    public Vector2Int center
    {
        get
        {
            return new(rect.x + rect.width / 2, rect.y + rect.height / 2);
        }
    }
    public Room(RectInt rect)
    {
        this.rect = rect;
    }
}