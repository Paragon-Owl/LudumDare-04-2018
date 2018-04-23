using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyMovement : MonoBehaviour
{
    public Transform target;
    private bool dead;
    private double timeBetween2Request = 0.2;
    private double lastRequest;
    public Vector2[] path;
    private int actualTargetIndex = 1;
    private const int Speed = 3;
    
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
        lastRequest = Time.time;
    }

    public bool needARequest()
    {
        Debug.Log(Time.time - lastRequest > timeBetween2Request);
        return Time.time - lastRequest > timeBetween2Request;
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

    public bool IsDead()
    {
        return dead;
    }

    public void Die()
    {
        dead = true;
    }
/*
    public void OnDrawGizmos()
    {
        Debug.Log(Astar.matrice);
        foreach (Node n in Astar.matrice.GetMatrice())
        {
            Gizmos.color = (n.isWalkable) ? Color.blue : Color.red;
            if(path != null && ((IList) path).Contains(n.worldPosition))
                Gizmos.color = Color.cyan;
            if(Astar.matrice.NodeFromWorldPoint(transform.position) == n)
                Gizmos.color = Color.black;
            Gizmos.DrawCube(n.worldPosition, Vector3.one * (float) 0.9);
        }
    }
    */
}