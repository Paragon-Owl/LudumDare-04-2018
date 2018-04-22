using UnityEngine;


public class Node : IHeapItem<Node>
{
    public Vector2 worldPosition;
    public int gridX;
    public int gridY;

    public int distanceFromStart;
    public int distanceFromEnd;
    public Node parent;
    public bool isWalkable;
    
    public int heapIndex;
    public Node(Vector2 _worldPos, int _gridX, int _gridY)
    {
        worldPosition = _worldPos;
        gridX = _gridX;
        gridY = _gridY;
    }

    public Node(Vector2 _worldPos, int _gridX, int _gridY, bool isWalkable)
    {
        worldPosition = _worldPos;
        gridX = _gridX;
        gridY = _gridY;
        this.isWalkable = isWalkable;
    }
    
    public int Fcost
    {
        get
        {
            return distanceFromStart + distanceFromEnd;
        }
    }

    public int HeapIndex
    {
        get
        {
            return heapIndex;
        }
        set
        {
            heapIndex = value;
        }
    }

    public int CompareTo(Node other)
    {
        int result = Fcost.CompareTo(other.Fcost);
        if (result == 0)
        {
            result = distanceFromEnd.CompareTo(other.distanceFromEnd);
        }
        return -result;
    }
}