﻿using UnityEngine;
using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;

public enum TileType { Empty, Null, Red, Blue, Green, Yellow, Gray, Magenta }

public class Puzzle
{
    private List<GameObject> gameObjects = new List<GameObject>();
    public GameObject tilePrefab;
    private TileType[][] puzzleMatrix;
    public TileType[][] PuzzleMatrix { get { return puzzleMatrix; } set { puzzleMatrix = value; } }
    public Puzzle(TileType[][] matrix, GameObject tilePrefab)
    {
        puzzleMatrix = matrix;
        this.tilePrefab = tilePrefab;
    }



    public Puzzle copy()
    {

        TileType[][] copy = new TileType[puzzleMatrix.Length][];


        for (int i = 0; i < puzzleMatrix.Length; i++)
        {
            copy[i] = new TileType[puzzleMatrix[i].Length];
            Array.Copy(puzzleMatrix[i], copy[i], puzzleMatrix[i].Length);

        }
        Puzzle puzzle = new Puzzle(copy, tilePrefab);
        return puzzle;
    }

    public void displayPuzzle()
    {
        for (int i = 0; i < puzzleMatrix.Length; i++)
        {
            for (int j = 0; j < puzzleMatrix[i].Length; j++)
            {
                if (puzzleMatrix[i][j] != TileType.Null)
                {
                    GameObject baseTile = UnityEngine.Object.Instantiate(tilePrefab, new Vector3(i, 0.125f, j), Quaternion.identity); //Base Block Below
                    baseTile.GetComponent<MeshRenderer>().material.color = Color.white;
                    gameObjects.Add(baseTile);

                    if ((int)puzzleMatrix[i][j] > 1)
                    {
                        GameObject instantiatedTile = UnityEngine.Object.Instantiate(tilePrefab, new Vector3(i, 0.375f, j), Quaternion.identity); //Actual Tiles on top                    
                        gameObjects.Add(instantiatedTile);
                        instantiatedTile.GetComponent<Human>().puzzle = this;
                        instantiatedTile.GetComponent<Human>().tile = puzzleMatrix[i][j];

                        switch (puzzleMatrix[i][j])
                        {
                            case TileType.Blue:
                                instantiatedTile.GetComponent<Renderer>().material.color = Color.blue;
                                break;
                            case TileType.Red:
                                instantiatedTile.GetComponent<Renderer>().material.color = Color.red;
                                break;
                            case TileType.Yellow:
                                instantiatedTile.GetComponent<Renderer>().material.color = Color.yellow;
                                break;
                            case TileType.Green:
                                instantiatedTile.GetComponent<Renderer>().material.color = Color.green;
                                break;
                            case TileType.Magenta:
                                instantiatedTile.GetComponent<Renderer>().material.color = Color.magenta;
                                break;
                            case TileType.Gray:
                                instantiatedTile.GetComponent<Renderer>().material.color = Color.gray;
                                break;
                            default:
                                Debug.LogError("Unknown Material for tile: " + puzzleMatrix[i][j]);
                                break;
                        }
                    }
                }
            }
        }
    }

    public void hidePuzzle()
    {
        for (int i = 0; i < gameObjects.Count; i++)
        {
            UnityEngine.Object.Destroy(gameObjects[i]);
        }
    }
    public int[] findRotationAxis(TileType tile)
    {

        int[] rotationAxis = new int[4];
        rotationAxis[0] = puzzleMatrix.Length;
        rotationAxis[2] = puzzleMatrix[0].Length;
        rotationAxis[3] = -1;

        //Discover the rotation axis
        for (int i = 0; i < puzzleMatrix.Length; i++)
        {
            for (int j = 0; j < puzzleMatrix[0].Length; j++)
            {
                if (puzzleMatrix[i][j] == tile)
                {

                    if (i < rotationAxis[0])
                    {
                        rotationAxis[0] = i;
                    }
                    rotationAxis[1] = i; //Calculates the DownRotationAxis

                    if (j < rotationAxis[2])
                    {
                        rotationAxis[2] = j;
                    }

                    if (j > rotationAxis[3])
                    {
                        rotationAxis[3] = j;
                    }
                }
            }
        }
        return rotationAxis;

    }

    public bool moveUp(TileType tile)
    {
        int rotationAxis = puzzleMatrix.Length;
        bool foundAxis = false;

        //Discover the rotation axis
        for (int i = 0; (i < puzzleMatrix.Length) && (!foundAxis); i++)
        {
            for (int j = 0; j < puzzleMatrix[i].Length; j++)
            {
                if (puzzleMatrix[i][j] == tile)
                {
                    rotationAxis = i;
                    foundAxis = true;
                    break;
                }
            }
        }

        List<Tuple<int, int>> positions = new List<Tuple<int, int>>();

        //Discovers if all the elements can rotate
        //Adds positions of the tiles to a List
        for (int i = rotationAxis; i < puzzleMatrix.Length; i++)
        {
            for (int j = 0; j < puzzleMatrix[i].Length; j++)
            {
                if (tile == puzzleMatrix[i][j])
                {
                    int symetricX = i - 2 * (i - rotationAxis) - 1;
                    if (canMove(symetricX, j))
                    {
                        positions.Add(Tuple.Create(symetricX, j));
                    }
                    else return false;
                }
            }
        }

        //If they can all move, change the colors
        for (int i = 0; i < positions.Count; i++)
        {
            puzzleMatrix[positions[i].Item1][positions[i].Item2] = tile;
        }

        return true;
    }

    public void executeMove(List<Tuple<int, int>> positions, TileType tile)
    {
        for (int i = 0; i < positions.Count; i++)
        {
            puzzleMatrix[positions[i].Item1][positions[i].Item2] = tile;
        }
    }

    public List<Tuple<int, int>> getMoveUpList(TileType tile)
    {
        int rotationAxis = puzzleMatrix.Length;
        bool foundAxis = false;

        //Discover the rotation axis
        for (int i = 0; (i < puzzleMatrix.Length) && (!foundAxis); i++)
        {
            for (int j = 0; j < puzzleMatrix[i].Length; j++)
            {
                if (puzzleMatrix[i][j] == tile)
                {
                    rotationAxis = i;
                    foundAxis = true;
                    break;
                }
            }
        }

        List<Tuple<int, int>> positions = new List<Tuple<int, int>>();
        //Discovers if all the elements can rotate
        //Adds positions of the tiles to a List
        for (int i = rotationAxis; i < puzzleMatrix.Length; i++)
        {
            for (int j = 0; j < puzzleMatrix[i].Length; j++)
            {
                if (tile == puzzleMatrix[i][j])
                {
                    int symetricX = i - 2 * (i - rotationAxis) - 1;
                    if (canMove(symetricX, j))
                    {
                        positions.Add(Tuple.Create(symetricX, j));
                    }
                    else return null;
                }
            }
        }
        return positions;
    }

    public void undoMoveUp(TileType tile)
    {
        int lowerBound = 0;
        int upperBound = puzzleMatrix[0].Length - 1;

        //Discover the bounds
        for (int i = 0; i < puzzleMatrix.Length; i++)
        {
            for (int j = 0; j < puzzleMatrix[i].Length; j++)
            {
                if (puzzleMatrix[i][j] == tile)
                {
                    if (i < upperBound)
                        upperBound = i;
                    if (i > lowerBound)
                        lowerBound = i;
                }
            }
        }

        int symetryAxis = (upperBound + lowerBound) / 2;

        for (int i = 0; i <= symetryAxis; i++)
        {
            for (int j = 0; j < puzzleMatrix[i].Length; j++)
            {
                if (puzzleMatrix[i][j] == tile)
                {
                    puzzleMatrix[i][j] = TileType.Empty;
                }
            }
        }
    }

    public bool moveDown(TileType tile)
    {
        int rotationAxis = -1;
        bool foundAxis = false;

        //Discover the rotation axis
        for (int i = puzzleMatrix.Length - 1; (i >= 0) && (!foundAxis); i--)
        {
            for (int j = 0; j < puzzleMatrix[i].Length; j++)
            {
                if (puzzleMatrix[i][j] == tile)
                {
                    rotationAxis = i;
                    foundAxis = true;
                    break;
                }
            }
        }

        List<Tuple<int, int>> positions = new List<Tuple<int, int>>();

        //Discovers if all the elements can rotate
        //Adds positions of the tiles to a List
        for (int i = 0; i <= rotationAxis; i++)
        {
            for (int j = 0; j < puzzleMatrix[i].Length; j++)
            {
                if (tile == puzzleMatrix[i][j])
                {
                    int symetricX = i + 2 * (rotationAxis - i) + 1;
                    if (canMove(symetricX, j))
                    {
                        positions.Add(Tuple.Create(symetricX, j));
                    }
                    else return false;
                }
            }
        }

        //If they can all move, change the colors
        for (int i = 0; i < positions.Count; i++)
        {
            puzzleMatrix[positions[i].Item1][positions[i].Item2] = tile;
        }

        return true;
    }

    public List<Tuple<int, int>> getMoveDownList(TileType tile)
    {
        int rotationAxis = -1;
        bool foundAxis = false;

        //Discover the rotation axis
        for (int i = puzzleMatrix.Length - 1; (i >= 0) && (!foundAxis); i--)
        {
            for (int j = 0; j < puzzleMatrix[i].Length; j++)
            {
                if (puzzleMatrix[i][j] == tile)
                {
                    rotationAxis = i;
                    foundAxis = true;
                    break;
                }
            }
        }

        List<Tuple<int, int>> positions = new List<Tuple<int, int>>();

        //Discovers if all the elements can rotate
        //Adds positions of the tiles to a List
        for (int i = 0; i < puzzleMatrix.Length; i++)
        {
            for (int j = 0; j < puzzleMatrix[i].Length; j++)
            {
                if (tile == puzzleMatrix[i][j])
                {
                    int symetricX = i + 2 * (rotationAxis - i) + 1;
                    if (canMove(symetricX, j))
                    {

                        positions.Add(Tuple.Create(symetricX, j));
                    }
                    else return null;
                }
            }
        }
        return positions;
    }

    public void undoMoveDown(TileType tile)
    {
        int lowerBound = 0;
        int upperBound = puzzleMatrix[0].Length - 1;

        //Discover the bounds
        for (int i = 0; i < puzzleMatrix.Length; i++)
        {
            for (int j = 0; j < puzzleMatrix[i].Length; j++)
            {
                if (puzzleMatrix[i][j] == tile)
                {
                    if (i < upperBound)
                        upperBound = i;
                    if (i > lowerBound)
                        lowerBound = i;
                }
            }
        }

        int symetryAxis = (upperBound + lowerBound) / 2;

        for (int i = puzzleMatrix.Length - 1; i > symetryAxis; i--)
        {
            for (int j = 0; j < puzzleMatrix[i].Length; j++)
            {
                if (puzzleMatrix[i][j] == tile)
                {
                    puzzleMatrix[i][j] = TileType.Empty;
                }
            }
        }
    }

    public bool moveRight(TileType tile)
    {



        int rotationAxis = -1;
        bool foundAxis = false;

        //Discover the rotation axis
        for (int j = puzzleMatrix[0].Length - 1; (j >= 0) && (!foundAxis); j--)
        {
            for (int i = 0; i < puzzleMatrix.Length; i++)
            {
                if (puzzleMatrix[i][j] == tile)
                {
                    rotationAxis = j;
                    foundAxis = true;
                    break;
                }
            }
        }

        List<Tuple<int, int>> positions = new List<Tuple<int, int>>();

        //Discovers if all the elements can rotate
        //Adds positions of the tiles to a List
        for (int i = 0; i < puzzleMatrix.Length; i++)
        {
            for (int j = 0; j <= rotationAxis; j++)
            {
                if (tile == puzzleMatrix[i][j])
                {
                    int symetricY = j + 2 * (rotationAxis - j) + 1;
                    if (canMove(i, symetricY))
                    {
                        positions.Add(Tuple.Create(i, symetricY));
                    }
                    else return false;
                }
            }
        }

        //If they can all move, change the colors
        for (int i = 0; i < positions.Count; i++)
        {
            puzzleMatrix[positions[i].Item1][positions[i].Item2] = tile;
        }

        return true;
    }

    public List<Tuple<int, int>> getMoveRightList(TileType tile)
    {
        int rotationAxis = -1;

        //Discover the rotation axis
        for (int i = 0; i < puzzleMatrix.Length; i++)
        {
            for (int j = puzzleMatrix[i].Length - 1; (j >= 0); j--)
            {
                if (puzzleMatrix[i][j] == tile && j > rotationAxis)
                {
                    rotationAxis = j;
                }
            }
        }

        List<Tuple<int, int>> positions = new List<Tuple<int, int>>();

        //Discovers if all the elements can rotate
        //Adds positions of the tiles to a List
        for (int i = 0; i < puzzleMatrix.Length; i++)
        {
            for (int j = 0; j < puzzleMatrix[i].Length; j++)
            {
                if (tile == puzzleMatrix[i][j])
                {
                    int symetricY = j + 2 * (rotationAxis - j) + 1;
                    if (canMove(i, symetricY))
                    {
                        positions.Add(Tuple.Create(i, symetricY));
                    }
                    else return null;
                }
            }
        }
        return positions;
    }

    public void undoMoveRight(TileType tile)
    {
        int leftBound = puzzleMatrix.Length;
        int rightBound = 0;

        //Discover the bounds
        for (int i = 0; i < puzzleMatrix.Length; i++)
        {
            for (int j = 0; j < puzzleMatrix[i].Length; j++)
            {
                if (puzzleMatrix[i][j] == tile)
                {
                    if (j < leftBound)
                        leftBound = j;
                    if (j > rightBound)
                        rightBound = j;
                }
            }
        }

        int symetryAxis = (rightBound + leftBound) / 2;

        for (int i = 0; i < puzzleMatrix.Length; i++)
        {
            for (int j = puzzleMatrix[i].Length - 1; j > symetryAxis; j--)
            {
                if (puzzleMatrix[i][j] == tile)
                {
                    puzzleMatrix[i][j] = TileType.Empty;
                }
            }
        }
    }

    public bool moveLeft(TileType tile)
    {

        int rotationAxis = puzzleMatrix.Length;

        bool foundAxis = false;

        //Discover the rotation axis
        for (int j = 0; (j < puzzleMatrix[0].Length) && (!foundAxis); j++)
        {
            for (int i = 0; i < puzzleMatrix.Length; i++)
            {
                if (puzzleMatrix[i][j] == tile)
                {
                    rotationAxis = j;
                    foundAxis = true;
                    break;
                }
            }
        }

        List<Tuple<int, int>> positions = new List<Tuple<int, int>>();

        //Discovers if all the elements can rotate
        //Adds positions of the tiles to a List
        for (int i = 0; i < puzzleMatrix.Length; i++)
        {
            for (int j = rotationAxis; j < puzzleMatrix[i].Length; j++)
            {
                if (tile == puzzleMatrix[i][j])
                {
                    int symetricY = j - 2 * (j - rotationAxis) - 1;
                    if (canMove(i, symetricY))
                    {
                        positions.Add(Tuple.Create(i, symetricY));
                    }
                    else return false;
                }
            }
        }

        //If they can all move, change the colors
        for (int i = 0; i < positions.Count; i++)
        {
            puzzleMatrix[positions[i].Item1][positions[i].Item2] = tile;
        }

        return true;
    }

    public List<Tuple<int, int>> getMoveLeftList(TileType tile)
    {
        int rotationAxis = puzzleMatrix.Length;

        bool foundAxis = false;

        //Discover the rotation axis
        for (int j = 0; (j < puzzleMatrix[0].Length) && (!foundAxis); j++)
        {
            for (int i = 0; i < puzzleMatrix.Length; i++)
            {
                if (puzzleMatrix[i][j] == tile)
                {
                    rotationAxis = j;
                    foundAxis = true;
                    break;
                }
            }
        }

        List<Tuple<int, int>> positions = new List<Tuple<int, int>>();

        //Discovers if all the elements can rotate
        //Adds positions of the tiles to a List
        for (int i = 0; i < puzzleMatrix.Length; i++)
        {
            for (int j = rotationAxis; j < puzzleMatrix[i].Length; j++)
            {
                if (tile == puzzleMatrix[i][j])
                {
                    int symetricY = j - 2 * (j - rotationAxis) - 1;
                    if (canMove(i, symetricY))
                    {
                        positions.Add(Tuple.Create(i, symetricY));
                    }
                    else return null;
                }
            }
        }
        return positions;
    }

    public void undoMoveLeft(TileType tile)
    {
        int leftBound = puzzleMatrix.Length;
        int rightBound = 0;

        //Discover the bounds
        for (int i = 0; i < puzzleMatrix.Length; i++)
        {
            for (int j = 0; j < puzzleMatrix[i].Length; j++)
            {
                if (puzzleMatrix[i][j] == tile)
                {
                    if (j < leftBound)
                        leftBound = j;
                    if (j > rightBound)
                        rightBound = j;
                }
            }
        }

        int symetryAxis = (rightBound + leftBound) / 2;

        for (int i = 0; i < puzzleMatrix.Length; i++)
        {
            for (int j = 0; j <= symetryAxis; j++)
            {
                if (puzzleMatrix[i][j] == tile)
                {
                    puzzleMatrix[i][j] = TileType.Empty;
                }
            }
        }
    }

    // Returns True if it is a valid Tile
    public bool canMove(int i, int j)
    {
        if (i < puzzleMatrix.Length && i >= 0)
        {
            if (j >= 0 && j < puzzleMatrix[i].Length)
            {
                if (puzzleMatrix[i][j] == TileType.Empty) return true;
            }
        }
        return false;
    }

    public bool isComplete()
    {

        for (int i = 0; i < puzzleMatrix.Length; i++)
        {
            for (int j = 0; j < puzzleMatrix[i].Length; j++)
            {
                if (puzzleMatrix[i][j] == TileType.Empty)
                {
                    return false;
                }
            }
        }
        return true;
    }

    public List<TileType> puzzleColors()
    {
        Dictionary<TileType, int> colorCount = new Dictionary<TileType, int>();

        for (int i = 0; i < puzzleMatrix.Length; i++)
        {
            for (int j = 0; j < puzzleMatrix[i].Length; j++)
            {
                if (puzzleMatrix[i][j] != TileType.Empty && puzzleMatrix[i][j] != TileType.Null)
                {
                    if (colorCount.ContainsKey(puzzleMatrix[i][j]))
                        colorCount[puzzleMatrix[i][j]] += 1;
                    else
                        colorCount[puzzleMatrix[i][j]] = 1;
                }
            }
        }

        List<TileType> puzzleColors = new List<TileType>(colorCount.Keys);
        puzzleColors = puzzleColors.OrderBy(color => colorCount[color]).ToList();
        puzzleColors.Reverse();

        return puzzleColors;

    }

    public void displayConsole()
    {

        string puzzleString = "";
        for (int i = 0; i < puzzleMatrix.Length; i++)
        {
            for (int j = 0; j < puzzleMatrix[i].Length; j++)
            {
                puzzleString += puzzleMatrix[i][j] + " ";
            }
            puzzleString += "\n";

        }
        Debug.Log(puzzleString);
    }

    public Node search(String typeOfSearch)
    {
        Puzzle current = copy();
        if (typeOfSearch == "BFS")
        {
            BFS bfs = new BFS();
            return bfs.search(current);

        }
        else if (typeOfSearch == "DFS")
        {
            DFS dfs = new DFS();
            return dfs.search(current);

        }
        else if (typeOfSearch == "DFSUndo")
        {
            DFSUndo dfsU = new DFSUndo();
            return dfsU.search(current);
        }
        else if (typeOfSearch == "IDDFS")
        {
            IDDFS iDDFS = new IDDFS();
            iDDFS.search(current, 20);
        }
        else if (typeOfSearch == "IDDFSUndo")
        {
            IDDFSUndo iDDFSUndo = new IDDFSUndo();
            iDDFSUndo.search(current, 20);
        }
        else if (typeOfSearch == "SimpleGreedy")
        {
            SimpleGreedy simpleGreedy = new SimpleGreedy();
            return simpleGreedy.search(current);
        }
        else if (typeOfSearch == "UniqueFirstGreedy")
        {
            UniqueFirstGreedy greedy = new UniqueFirstGreedy();
            return greedy.solve(current, true);
        }

        else if (typeOfSearch == "AStar")
        {
            AStar astar = new AStar();
            return astar.search(current);
        }
        else if (typeOfSearch == "UniformCost")
        {
            UniformCost uCost = new UniformCost();
            return uCost.search(current);
        }
        else Debug.Log("Algortithm does not exist");

        return null;
    }
}