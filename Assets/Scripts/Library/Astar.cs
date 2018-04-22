using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Tilemaps;

public static class Astar 
{

    public static Matrice matrice;

    static public Vector2[] FindPath(Vector2 startPos, Vector2 targetPos)
    {
        Node startNode = matrice.NodeFromWorldPoint(startPos);
        Node targetNode = matrice.NodeFromWorldPoint(targetPos);
        Heap<Node> openSet = new Heap<Node>(matrice.GetSize());
        HashSet<Node> closedSet = new HashSet<Node>();
        openSet.Add(startNode);

        while (openSet.Count > 0)
        {
            Node currentNode = openSet.RemoveFirst();
            closedSet.Add(currentNode);

            if (currentNode == targetNode)
            {
                return RetracePath(startNode, targetNode);
            }
            foreach (Node neighbour in matrice.GetNeighbours(currentNode))
            {
                if (closedSet.Contains(neighbour) || !neighbour.isWalkable)
                {
                    continue;
                }

                int newCostToNeighbour = currentNode.distanceFromStart + GetDistance(currentNode, neighbour);
                if (newCostToNeighbour < neighbour.distanceFromStart || !openSet.Contains(neighbour))
                {
                    neighbour.distanceFromStart = newCostToNeighbour;
                    neighbour.distanceFromEnd = GetDistance(neighbour, targetNode);
                    neighbour.parent = currentNode;

                    if (!openSet.Contains(neighbour))
                        openSet.Add(neighbour);
                    else
                        openSet.UpdateItem(neighbour);
                }
            }
        }
        return null;
    }

    static Vector2[] RetracePath(Node startNode, Node endNode)
    {
        List<Node> path = new List<Node>();
        Node currentNode = endNode;
        path.Add(endNode);
        while (currentNode != startNode)
        {
            path.Add(currentNode);
            currentNode = currentNode.parent;
        }
        path.Add(startNode);
        path.Reverse();
        return SimplifyPath(path);
    }

    static Vector2[] SimplifyPath(List<Node> path)
    {
        List<Vector2> waypoints = new List<Vector2>();
        Vector2 directionOld = Vector2.zero;

        for (int i = 1; i < path.Count; i++)
        {
            Vector2 directionNew = new Vector2(path[i - 1].gridX - path[i].gridX, path[i - 1].gridY - path[i].gridY);
            if (directionNew != directionOld)
            {
                waypoints.Add(path[i-1].worldPosition);
            }
            directionOld = directionNew;
        }
        return waypoints.ToArray();
    }

    static int GetDistance(Node nodeA, Node nodeB)
    {
        int dstX = Mathf.Abs(nodeA.gridX - nodeB.gridX);
        int dstY = Mathf.Abs(nodeA.gridY - nodeB.gridY);

        if (dstX > dstY)
            return 14 * dstY + 10 * (dstX - dstY);
        return 14 * dstX + 10 * (dstY - dstX);
    }

}
