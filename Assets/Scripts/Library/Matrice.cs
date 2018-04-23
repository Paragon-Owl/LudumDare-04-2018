using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Tilemaps;

public class Matrice
{
    const float nodeRadius = 0.5f;


    Vector2 center;
    Vector2 gridWorldSize;
    Node[,] matrice;
    float nodeDiameter;
    int gridSizeX, gridSizeY;

    public Matrice(Vector2 center, Vector2 gridWorldSize)
    {
        this.nodeDiameter = nodeRadius * 2;
        this.center = center;
        this.gridWorldSize = gridWorldSize;
        gridSizeX = Mathf.RoundToInt(gridWorldSize.x / nodeDiameter);
        gridSizeY = Mathf.RoundToInt(gridWorldSize.y / nodeDiameter);
        CreateMatrice();
    }

    //For now
    public Matrice(Vector2 center,Tilemap t)
    {
        gridWorldSize = new Vector2(100,100);
        this.center = center;
        nodeDiameter = t.cellSize.x;
        gridSizeX = Mathf.RoundToInt(gridWorldSize.x / nodeDiameter);
        gridSizeY = Mathf.RoundToInt(gridWorldSize.y / nodeDiameter);
        CreateMatrice(t);
    }
    
    public Matrice(Tilemap notWalkable, Tilemap map)
    {
        Debug.Log("New Matrice Creator");
        gridWorldSize = new Vector2(map.cellBounds.size.x,map.cellBounds.size.y);
        center = map.cellBounds.center;
        Debug.Log("Nouvelle matrice center : " + center);
        nodeDiameter = map.cellSize.x;
        gridSizeX = Mathf.RoundToInt(gridWorldSize.x / nodeDiameter);
        gridSizeY = Mathf.RoundToInt(gridWorldSize.y / nodeDiameter);
        CreateMatrice(notWalkable);
    }

    void CreateMatrice(Tilemap notWalkable)
    {
        Debug.Log("Here with tilemap");
        matrice = new Node[gridSizeX, gridSizeY];
        Vector2 worldBottomLeft = center - Vector2.right * gridWorldSize.x / 2 - Vector2.up * gridWorldSize.y / 2;
        for (int x = 0; x < gridSizeX; x++)
        {
            for (int y = 0; y < gridSizeY; y++)
            {
                bool isWalkable;
                Vector2 worldPoint = worldBottomLeft + Vector2.right * (x * nodeDiameter + nodeRadius) + Vector2.up * (y * nodeDiameter + nodeRadius);
                isWalkable = ! notWalkable.HasTile(notWalkable.WorldToCell(worldPoint));
                matrice[x, y] = new Node(worldPoint, x, y, isWalkable);
                
            }
        }
    }
    void CreateMatrice()
    {
        Debug.Log("Here");
        matrice = new Node[gridSizeX, gridSizeY];
        Vector2 worldBottomLeft = center - Vector2.right * gridWorldSize.x / 2 - Vector2.up * gridWorldSize.y / 2;
        for (int x = 0; x < gridSizeX; x++)
        {
            for (int y = 0; y < gridSizeY; y++)
            {
                Vector2 worldPoint = worldBottomLeft + Vector2.right * (x * nodeDiameter + nodeRadius) + Vector2.up * (y * nodeDiameter + nodeRadius);
                matrice[x, y] = new Node(worldPoint, x, y);
            }
        }
    }
    public int GetSize()
    {
        return gridSizeX * gridSizeY;
    }
    public Node NodeFromWorldPoint(Vector2 worldPosition)
    {
        float percentX = ((worldPosition.x - center.x + gridWorldSize.x / 2) / gridWorldSize.x);
        float percentY = ((worldPosition.y - center.y + gridWorldSize.y / 2) / gridWorldSize.y);
        percentX = Mathf.Clamp01(percentX);
        percentY = Mathf.Clamp01(percentY);
        int x = Mathf.RoundToInt((gridSizeX - 1) * percentX);
        int y = Mathf.RoundToInt((gridSizeY - 1) * percentY);
        return matrice[x, y];
    }
    
    public List<Node> GetNeighbours(Node node)
    {
        List<Node> neighbours = new List<Node>();
        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                if (x == 0 && y == 0)
                    continue;

                int checkX = node.gridX + x;
                int checkY = node.gridY + y;

                if (checkX >= 0 && checkX < gridSizeX && checkY >= 0 && checkY < gridSizeY)
                {
                    neighbours.Add(matrice[checkX, checkY]);
                }
            }
        }
        return neighbours;
    }
    //DEBUG
    public Node[,] GetMatrice()
    {
        return matrice;
    }
   
}