using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Board : MonoBehaviour
{
    public GameObject tile;
    public GameObject[,] map;
    int mapSize = 19;
    public Dictionary<string, bool> Sides = new Dictionary<string, bool>();
    public Dictionary<string, bool> Corners = new Dictionary<string, bool>();
    bool[,] tempMap;
    bool[,] validityMap;
    //bool circle = false;
    int[,] spawn;

    void Start()
    {
        map = new GameObject[mapSize, mapSize];
        validityMap = new bool[mapSize, mapSize];
        const float outerRadius = 1F;
        const float innerRadius = outerRadius * 0.866025404F;

        for (int i = 0; i < mapSize; i++)
        {
            for (int j = 0; j < mapSize; j++)
            {
                if (j <= (mapSize) / 2)
                {
                    if (i + j >= (mapSize / 2))
                    {
                        map[i, j] = Instantiate(tile, new Vector3(j * outerRadius * 1.5F, (i + j * 0.5F) * innerRadius * 2F, -0.4F), Quaternion.identity);
                        validityMap[i, j] = true;
                    }
                }
                else
                {
                    if (i + j < (mapSize * 1.5F - 1F))
                    {
                        map[i, j] = Instantiate(tile, new Vector3(j * outerRadius * 1.5F, (i + j * 0.5F) * innerRadius * 2F, -0.4F), Quaternion.identity);
                        validityMap[i, j] = true;
                    }
                }
            }
        }
    }

    void Update()
    {
        bool[,] checkmapGreen = new bool[mapSize, mapSize];
        bool[,] checkmapCyan = new bool[mapSize, mapSize];

        for (int i = 0; i < mapSize; i++)
        {
            for (int j = 0; j < mapSize; j++)
            {
                if (validityMap[i, j] != false)
                {
                    if (map[i, j].GetComponent<Tile>().trueColor == Color.green)
                    {
                        checkmapGreen[i, j] = true;
                    }
                    else if (map[i, j].GetComponent<Tile>().trueColor == Color.cyan)
                    {
                        checkmapCyan[i, j] = true;
                    }
                }
            }
        }

        checkVictory(checkmapCyan);
        checkVictory(checkmapGreen);
    }

    void checkVictory(bool[,] map)
    {
        //int[,] startingPoint;
        //Starting from tile i,j seek one of three victory methods
        for (int i = 0; i < mapSize; i++)
        {
            for (int j = 0; j < mapSize; j++)
            {
                if (map[i, j] == true)
                {
                    tempMap = map;
                    resetVictoryTracker();
                    flood(i, j);
                    //Iterate through normal map and remove all checked tiles using tempmap
                }

            }
        }
    }

    void flood(int i, int j)
    {
        //Turn off the current location to prevent infinite recursion
        tempMap[i, j] = false;
        bool canGoDownLeft = true;
        bool canGoDownRight = true;
        bool canGoRight = true;
        bool canGoLeft = true;
        bool canGoUpLeft = true;
        bool canGoUpRight = true;

        //If EAST
        if (i == 0)
        {
           canGoRight = false;
           canGoUpRight = false;
           if(j == Math.Floor((double)mapSize/2))
            {
                Corners["E"] = true;
                //print("TAGGED E");
                canGoDownLeft = false;
            }
           else if(j == mapSize-1)
            {
                Corners["SE"] = true;
                //print("TAGGED SE");
                canGoDownRight = false;
            }
           else
            {
                Sides["1"] = true;
            }
        }
        //If WEST
        else if(i == mapSize-1)
        {
            canGoLeft = false;
            canGoUpLeft = false;
            //Barrier
            if (j == 0)
            {
                canGoDownLeft = false;
            }
            else if (j == mapSize - 1)
            {
                canGoDownRight = false;
            }

            if (j == Math.Floor((double)mapSize / 2))
            {
                Corners["W"] = true;
                //print("TAGGED W");
            }
            else if (j == 0)
            {
                Corners["NE"] = true;
                //print("TAGGED NE");
            }
            else
            {
                Sides["2"] = true;
            }
        }
        //Hexagons are madness
        else if (i == Math.Floor((double)mapSize / 2))
        {
            if (j == 0)
            {
                Corners["NW"] = true;
                //print("TAGGED NW");
            }
            else if(j == mapSize - 1)
            {
                Corners["SW"] = true;
                //print("TAGGED SW");
            }
            else
            {
                Sides["3"] = true;
            }
        }
        //If SOUTH
        if(j == mapSize-1)
        {
            canGoDownRight = false;
            canGoUpRight = false;
            if (i != (Math.Floor((double)mapSize / 2)) && i != 0 && i != mapSize - 1)
            {
                Sides["4"] = true;
            }
        }
        //If NORTH
        if(j == 0)
        {
            canGoDownLeft = false;
            canGoUpLeft = false;
            if(i != (Math.Floor((double)mapSize / 2)) && i != 0 && i != mapSize - 1)
            {
                Sides["5"] = true;
            }
        }

        if(j == Math.Floor((double)mapSize / 2))
        {
            if (i != (Math.Floor((double)mapSize / 2)) && i != 0 && i!= mapSize - 1)
            {
                Sides["6"] = true;
            }
        }

        int sides = 0;
        foreach (KeyValuePair<string, bool> entry in Sides)
        {
            if (entry.Value == true)
            {
                sides++;
            }
        }

        int corners = 0;
        foreach (KeyValuePair<string, bool> entry in Corners)
        {
            if (entry.Value == true)
            {
                corners++;
            }
        }

        if(corners >= 2 || sides >= 3)
        {
            print("VICTORY");
            return;
        }

        if (canGoDownLeft)
        {
            if (tempMap[i,j-1] == true)
            {
                flood(i,j-1);
            }
        }
        if (canGoDownRight)
        {
            if (tempMap[i, j+1] == true)
            {
                flood(i, j+1);
            }
        }
        if (canGoRight)
        {
            if (tempMap[i-1, j] == true)
            {
                flood(i-1, j);
            }
        }
        if (canGoLeft)
        {
            if (tempMap[i+1, j] == true)
            {
                flood(i+1, j);
            }
        }
        if (canGoUpLeft)
        {
            if (tempMap[i+1, j-1] == true)
            {
                flood(i+1, j-1);
            }
        }
        if (canGoUpRight)
        {
            if (tempMap[i-1, j+1] == true)
            {
                flood(i-1, j+1);
            }
        }
        return;
    }
    
    void resetVictoryTracker()
    {
        Sides.Clear();
        Sides.Add("1", false);
        Sides.Add("2", false);
        Sides.Add("3", false);
        Sides.Add("4", false);
        Sides.Add("5", false);
        Sides.Add("6", false);
        Corners.Clear();
        Corners.Add("NE", false);
        Corners.Add("SE", false);
        Corners.Add("NW", false);
        Corners.Add("SW", false);
        Corners.Add("E", false);
        Corners.Add("W", false);
    }
}