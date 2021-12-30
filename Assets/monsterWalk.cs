using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class monsterWalk : MonoBehaviour
{
    public GameObject[] nodes;
    public GameObject[] players;
    public float moveSpeed = 1f;
    //int[,] graph = new int[,] { { 1, 2, 6, -1 }, { 0, 3, 4, -1 }, { 0, 3, 11, -1 }, { 1, 2, 4, 10 }, { 1, 3, 5, 9 }, { 4, 6, 7, -1 }, { 0, 5, 8, -1 }, { 5, 8, 9, -1 }, { 6, 7, 12, -1 }, { 4, 7, 10, 12 }, { 3, 9, 11, 12 }, { 2, 10, 12, -1 }, { 8, 9, 10, 11 } };
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
    int[] visited = new int[15];
    List<int> path = new List<int>();
    public int curPosition = 0, nextPosition = 0;
    // Start is called before the first frame update
    public bool reachedDist = false;


    //gameover ui
    public GameObject gameoverPanel;
    public GameObject deckCardPanel;
    public GameObject dropZoneHint;
    public GameObject winPanel;
    public GameObject lossPanel;
    public GameObject hintCam;
    public GameObject bgSound;

    void Start()
    {
        transform.position = nodes[0].transform.position;

        // Array.Clear(visited, 0, totalNode);

        // dfs(0, -2);
    }

    float dijktra(int src, int target)
    {
        // print("src = " + src + " target = " + target);
        SortedList<float, int> q = new SortedList<float, int>();
        bool[] vis = new bool[15];
        float[] dis = new float[15];

        for (int i = 0; i < 15; i++)
        {
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

    // Update is called once per frame
    void Update()
    {
        if(!players[0].GetComponent<playerScript>().isDrop)
        {
            return;
        }

        if (curPosition == 12)
        {
            reachedDist = true;
            print("monster reached destination, Game over");
            bgSound.SetActive(false);
            gameoverPanel.SetActive(true);
            lossPanel.SetActive(true);
            deckCardPanel.SetActive(false);
            dropZoneHint.SetActive(false);

            return;
        }

        for (int i = 0; i < players.Length; i++)
        {
            // print("dis = " + Vector3.Distance(transform.position, players[i].transform.position));
            if (Vector3.Distance(transform.position, players[i].transform.position) < 8f)
            {
                 print("got caught");
                gameoverPanel.SetActive(true);
                winPanel.SetActive(true);
                deckCardPanel.SetActive(false);
                dropZoneHint.SetActive(false);
                hintCam.SetActive(false);
                bgSound.SetActive(false);
                return;
            }
        }

        // print("cur = " + curPosition + " next = " + nextPosition);
        // print("dis = " + Vector3.Distance(transform.position, nodes[nextPosition].transform.position));


        if (Vector3.Distance(transform.position, nodes[nextPosition].transform.position) < 0.01f)
        {
            print("monster finding next move");
            SortedList<float, int> slGood = new SortedList<float, int>();
            SortedList<float, int> slBad = new SortedList<float, int>();

            for (int i = 0; i < 4; i++)
            {
                // print("curPosition = " + nextPosition + " i = " + i);
                // print("graph : " + graph[curPosition,i]);
                var j = graph[nextPosition, i];
                // print("j = " + j);

                if (j == -1) break;

                if (j != curPosition)
                {
                    // print("working on " + j);
                    float resDis = float.MinValue;

                    foreach (GameObject player in players)
                    {
                        int playerNextPos = player.GetComponent<playerScript>().nextPosition;
                        int playerCurPos = player.GetComponent<playerScript>().curPosition;
                        float d = 0f;

                        if (j == playerCurPos)
                        {
                            d = Vector3.Distance(player.transform.position, nodes[playerCurPos].transform.position);
                        }
                        else
                        {
                            d = player.GetComponent<playerScript>().dijktra(playerNextPos, j) + Vector3.Distance(player.transform.position, nodes[playerNextPos].transform.position);
                        }

                        print("playerPos = " + playerNextPos + " d = " + d + " j = " + j);
                        resDis = Math.Max(resDis, Vector3.Distance(transform.position, nodes[j].transform.position) - d);
                        print(Vector3.Distance(transform.position, nodes[j].transform.position) + " - " + d + " = " + (Vector3.Distance(transform.position, nodes[j].transform.position) - d));
                    }

                    print("resDis = " + resDis + " j = " + j);
                    // print("final des = " + d);
                    if (resDis < 0)
                    {
                        float d = dijktra(j, 12);
                        if (!slGood.ContainsKey(d))
                        {
                            slGood.Add(d, j);
                            // print("if added " + (resDis + d - 00.0f) + ", " + j);
                        }
                    }
                    else
                    {
                        if (!slBad.ContainsKey(resDis))
                        {
                            slBad.Add(resDis, j);
                            // print("else added " + (resDis + d) + ", " + j);
                        }
                    }
                }
            }

            // for (int i = 0; i < sl.Values.Count; i++) {
            // 	print("sl values[" + i + "] = " + sl.Values[i] + " key = " + sl.Keys[i]);
            // }

            curPosition = nextPosition;

            if (slGood.Count > 0)
            {
                nextPosition = slGood.Values[0];
            }
            else
            {
                nextPosition = slBad.Values[0];
            }
        }
        else
        {
            // print("moster moving");
            transform.position = Vector3.MoveTowards(transform.position, nodes[nextPosition].transform.position, moveSpeed * Time.deltaTime);
        }

        // print("monster cur = " + curPosition + " next = " + nextPosition);
    }


}