using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

class MST
{
    public List<Room> rooms;
    public List<Edge> edges;
    public MST(List<Room> rooms)
    {
        this.rooms = rooms;
        this.edges = GenerateMST(rooms, GenerateOrderedEdges(rooms));
    }

    private List<Edge> GenerateOrderedEdges(List<Room> rooms)
    {
        List<Edge> edges = new();
        for (int i = 0; i < rooms.Count; i++)
        {
            for (int j = i + 1; j < rooms.Count; j++)
            {
                edges.Add(new Edge(rooms[i], rooms[j]));
            }
        }
        return edges.OrderBy(edge => edge.distance).ToList();
    }

    private List<Edge> GenerateMST(List<Room> rooms, List<Edge> edges)
    {
        Dictionary<Room, Room> parent = new Dictionary<Room, Room>();
        foreach (Room room in rooms)
        {
            parent[room] = room;
        }

        List<Edge> mst = new();
        foreach (Edge edge in edges)
        {
            if (Find(parent, edge.roomA) != Find(parent,edge.roomB))
            {
                mst.Add(edge);
                Union(parent, edge.roomA, edge.roomB);
            }
            if (mst.Count == rooms.Count - 1) break;
        }

        return mst;
    }

    private Room Find(Dictionary<Room, Room> parent, Room room)
    {
        if (parent[room] != room)
        {
            parent[room] = Find(parent, parent[room]);
        }
        return parent[room];
    }

    private void Union(Dictionary<Room, Room> parent, Room roomA, Room roomB)
    {
        var rootA = Find(parent, roomA);
        var rootB = Find(parent,roomB);
        if (rootA != rootB) parent[rootB] = rootA;
    }
}