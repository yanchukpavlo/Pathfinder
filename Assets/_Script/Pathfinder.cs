using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class Pathfinder : MonoBehaviour
{
    public static readonly Color PathColor = new Color(0.3f, 1f, 0.6f);
    public static readonly Color StartColor = new Color(0f, 1f, 0f);
    public static readonly Color EndColor = new Color(1f, 0f, 0f);
    public static readonly Color OpenColor = new Color(.4f, .6f, .4f);
    public static readonly Color ClosedColor = new Color(0.35f, 0.4f, 0.5f);

    public static List<TileBase> FindPath(TileBase startNode, TileBase targetNode)
    {
        var toSearch = new List<TileBase>() { startNode };
        var processed = new List<TileBase>();

        while (toSearch.Any())
        {
            var current = toSearch[0];
            foreach (var t in toSearch)
                if (t.F < current.F || t.F == current.F && t.H < current.H) current = t;

            processed.Add(current);
            toSearch.Remove(current);

            //current.SetColor(ClosedColor);

            if (current == targetNode)
            {
                var currentPathTile = targetNode;
                var path = new List<TileBase>();
                var count = 100;
                while (currentPathTile != startNode)
                {
                    path.Add(currentPathTile);
                    currentPathTile = currentPathTile.Connection;
                    count--;
                    if (count < 0) throw new Exception();
                }

                foreach (var tile in path) tile.SetColor(PathColor);
                startNode.SetColor(StartColor);
                targetNode.SetColor(EndColor);
                Debug.Log($"Path lenth = {path.Count}.");
                return path;
            }

            foreach (var neighbor in current.Neighbors.Where(t => t.Walkable && !processed.Contains(t)))
            {
                var inSearch = toSearch.Contains(neighbor);

                var costToNeighbor = current.G + current.GetDistance(neighbor);

                if (!inSearch || costToNeighbor < neighbor.G)
                {
                    neighbor.SetG(costToNeighbor);
                    neighbor.SetConnection(current);

                    if (!inSearch)
                    {
                        neighbor.SetH(neighbor.GetDistance(targetNode));
                        toSearch.Add(neighbor);
                        //neighbor.SetColor(OpenColor);
                    }
                }
            }
        }

        Debug.LogWarning("Can`t find path.");
        return null;
    }
}
