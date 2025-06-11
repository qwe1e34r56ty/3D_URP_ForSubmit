
using UnityEngine;

class Edge
{
    public Room roomA, roomB;
    public float distance;
    public Edge(Room roomA, Room roomB)
    {
        this.roomA = roomA;
        this.roomB = roomB;
        distance = Vector2Int.Distance(roomA.center, roomB.center);
    }
}