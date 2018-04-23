using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyMovement : MonoBehaviour
{
    public Transform target;
    private Vector2[] path;
    private int actualTargetIndex = 1;
    private const int Speed = 10;
    
    private Animator spriteAnimator;
    private List<string> animationNames = new List<string>{
        "ZombieEast",
        "ZombieNorthEast",
        "ZombieNorth",
        "ZombieNorthWest",
        "ZombieWest",
        "ZombieSouthWest",
        "ZombieSouth",
        "ZombieSouthEast"
    };

    public void Start() {
        spriteAnimator = GetComponent<Animator>();
    }


    void FixedUpdate()
    {
        moveObject();
    }

    public void RequestPath()
    {
        path = Astar.FindPath(transform.position, target.position);
    }

    private void moveObject()
    {
        if (path != null && actualTargetIndex < path.Length)
        {
            transform.position = Vector2.MoveTowards(transform.position, path[actualTargetIndex], Speed * Time.deltaTime);
            if (path[actualTargetIndex].x == transform.position.x && path[actualTargetIndex].y == transform.position.y)
            {
                actualTargetIndex++;
            }
        }
        Animate();
    }

    private void Animate() {
        var dir = target.position - transform.position;
        float angle = Vector3.Angle(Vector3.right, dir);
        if(dir.y < 0)
            angle = 360-angle;

        angle+=Mathf.Rad2Deg*(Mathf.PI/8f);
        spriteAnimator.Play(animationNames[Mathf.FloorToInt(angle/45f)%animationNames.Count]);
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("J'encule ta mere");
    }
/*
    public void OnDrawGizmos()
    {
        foreach (Node n in Astar.matrice.GetMatrice())
        {
            Gizmos.color = (n.isWalkable) ? Color.blue : Color.red;
            if(path != null && path.Contains(n.worldPosition))
                Gizmos.color = Color.cyan;
            if(Astar.matrice.NodeFromWorldPoint(transform.position) == n)
                Gizmos.color = Color.black;
            Gizmos.DrawCube(n.worldPosition, Vector3.one * (float) 0.9);
        }
    }
    */
}