using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class playerScript : MonoBehaviour
{
    public GameObject[] nodes;
    public GameObject monster;
    public float moveSpeed = 1f;
    // int[,] graph = new int[,] { { 1, 2, 6, -1 }, { 0, 3, 4, -1 }, { 0, 3, 11, -1 }, { 1, 2, 4, 10 }, { 1, 3, 5, 9 }, { 4, 6, 7, -1 }, { 0, 5, 8, -1 }, { 5, 8, 9, -1 }, { 6, 7, 12, -1 }, { 4, 7, 10, 12 }, { 3, 9, 11, 12 }, { 2, 10, 12, -1 }, { 8, 9, 10, 11 }, {8,14,-1,-1 },{ 13,6,-1,-1} };
    int[,] graph = new int[,] {
        { 1, 2, 6, -1 },
        { 0, 3, 4, -1 },
        { 0, 3, 11, -1 },
        { 1, 2, 4, 10 },
        { 1, 3, 5, 9 },
        { 4, 6, 7, -1 },
        { 0, 5, -1, -1 },
        { 5, 8, 9, -1 },
        { 7, 12, -1, -1 },
        { 4, 7, 10, 12 },
        { 3, 9, 11, 12 },
        { 2, 10, 12, -1 },
        { 8, 9, 10, 11 },
        { 8, 14, -1, -1 },
        { 13, 6, -1, -1 } };
    public int curPosition = 10, nextPosition = 10;
    
    
    public bool isDrop = false;
 //   Animator anim;
    
    // Start is called before the first frame update

    void Start()
    {
        transform.position = nodes[curPosition].transform.position;
    }

    public float dijktra(int src, int target)
    {
        // print("src = " + src + " target = " + target);
        SortedList<float, int> q = new SortedList<float, int>();
        bool[] vis = new bool[30];
         float[] dis = new float[30];
    print("len " + dis.Length+" "+ vis.Length);
        for (int i = 0; i < 15; i++)
        {
            print(i);
            print("dis " + dis[i]);
            print("vis " + vis[i]);
            dis[i] = float.MaxValue;
            vis[i] = false;
            
        }

        q.Add(0f, src);
        dis[src] = 0;
        vis[src] = true;

        while (q.Count > 0)
        {
            int u = q.Values[0];
            q.Remove(q.Keys[0]);

            // print("removed = " + u);

            for (int i = 0; i < 4; i++)
            {
                int v = graph[u, i];

                if (v == -1) break;

                float newDis = dis[u] + Vector3.Distance(nodes[u].transform.position, nodes[v].transform.position);

                // print("u = " + u + " v = " + v + " newDis = " + newDis);

                if (dis[v] > newDis)
                {
                    dis[v] = newDis;
                    // print("added = " + v);
                    q.Add(dis[v], v);
                }
            }
        }

        return dis[target];
    }
    // Update is called once per frame
    void Update()
    {
        print("playerscript Drop " + isDrop);
        if(!isDrop)
        {
            return;
        }
        // dijktra(nextPosition, monster.GetComponent<monsterWalk>().nextPosition);
        if (monster.GetComponent<monsterWalk>().reachedDist)
        {
            return;
            Debug.Log("Game Over, Enemy Wins");
        }

        if (Vector3.Distance(transform.position,monster.transform.position) <= 8f)
        {
            Debug.Log("Player Wins");
            return;

        }

        // print("updating cur = " + curPosition + " nextPosition = " + nextPosition);
        if (Vector3.Distance(transform.position, nodes[nextPosition].transform.position) < 0.00001f)
        {
            curPosition = nextPosition;
            float mini = float.MaxValue;

            for (int i = 0; i < 4; i++)
            {
                int v = graph[curPosition, i];


                if (v == -1) break;
                float d = dijktra(v, monster.GetComponent<monsterWalk>().nextPosition);

                // print("curPosition= " + curPosition + " v = " + graph[curPosition, i] + " d = " + d);

                if (d < mini)
                {
                    mini = d;
                    nextPosition = v;
                }
            }

            // print("nextPosition = " + nextPosition + " maxi");
            //anim.SetInteger("idle", 1);
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, nodes[nextPosition].transform.position, moveSpeed * Time.deltaTime);
            transform.LookAt(nodes[nextPosition].transform.position);
           // anim.SetInteger("run", 1);
        }
        // print("player cur = " + curPosition + " next = " + nextPosition);
    }
}