﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
enum playerType { Bot, Player };

public class GameManager : MonoBehaviour
{
    private Puzzle currentPuzzle;
    private Puzzle firstPuzzle;
    private playerType currentPlayer;
    public GameObject tilePrefab;

    public GameObject hintButton;


    public void Start()
    {
        // Test test = new Test(tilePrefab);
        // test.runTests(5, "Results/All.txt");

        /*
        Puzzle puzzle = new Puzzle(Example.puzzleHard, tilePrefab);
        puzzle.displayPuzzle();
        Node solution = puzzle.search("AStar");
        List<Node> steps = solution.getPath();        
        StartCoroutine(DisplayPuzzleStates(steps, 0.5f));
        */
    }

    private IEnumerator testAnimation()
    {
        Puzzle puzzle = new Puzzle(Example.puzzleHard, tilePrefab);
        puzzle.displayPuzzle();
        GameObject tileGroup = puzzle.displayTilesOfType(TileType.Red);
        Vector3 target = new Vector3(4.5f, 0.375f, 1.0f); //(TargetJoint2D.y = 0.375 always), the x an z will be equal to the tile's x and z, and depending on the type of move  +0.5 or -0.5 will be added to the x or z        
        yield return new WaitForSeconds(1f);
        for(int i = 0; i < 180/5; i++) {            
            tileGroup.transform.RotateAround(target,Vector3.forward, -5); //Forwards for down or up, Right for left or right            
            yield return new WaitForSeconds(0.0001f);
        }
    }

    public void ManagerStarter(string searchOption, TileType[][] puzzleLevel)
    {
        currentPuzzle = new Puzzle(copyMatrix(puzzleLevel), tilePrefab);
        firstPuzzle = new Puzzle(puzzleLevel, tilePrefab);
        currentPuzzle.displayPuzzle();

        var watch = System.Diagnostics.Stopwatch.StartNew();

        Node solution = currentPuzzle.search(searchOption);

        watch.Stop();
        Debug.Log(searchOption);
        Debug.Log("Time taken: " + watch.ElapsedMilliseconds / 1000.0);

        if (solution == null)
        {
            Debug.LogError("Algorithm failed to solve puzzle!!");
            return;
        }

        List<Node> steps = solution.getPath();

        Debug.Log("Steps taken: " + steps.Count);

        StartCoroutine(DisplayPuzzleStates(steps, 0.5f));
    }

    public void hidePuzzle()
    {
        currentPuzzle.hidePuzzle();
    }
    public void backToOriginal()
    {
        currentPuzzle = firstPuzzle;
    }

    public void HumanMode(TileType[][] puzzleLevel)
    {
        currentPuzzle = new Puzzle(copyMatrix(puzzleLevel), tilePrefab);
        firstPuzzle = currentPuzzle.copy();
        currentPuzzle.displayPuzzle();
        hintButton.GetComponent<GetHint>().puzzle = currentPuzzle;
    }

    private List<Puzzle> puzzleStates = new List<Puzzle>();
    public void DisplayState(Puzzle puzzle)
    {
        puzzleStates.Add(puzzle);
    }

    private IEnumerator DisplayPuzzleStates(List<Node> steps, float time)
    {        
        //Debug.Log(puzzleStates.Count);
        for (int i = 0; i < steps.Count; i++)
        {
            if (i != 0) {
                GameObject tileGroup = steps[i-1].puzzle.displayTilesOfType(steps[i].movedTile); //Display the tiles that moved from the last puzzle to the current                
                Vector3 target = getTarget(tileGroup, steps[i].moveType);                
                Debug.Log("Tile: " + steps[i].movedTile + "   Move: " + steps[i].moveType);              
                Debug.Log("--> Target: " + target);
                Vector3 axis = Vector3.forward;  //Forwards for down or up, Right for left or right   
                int direction = 1; //positive for right and up  
                if(steps[i].moveType == MoveType.Right || steps[i].moveType == MoveType.Left) axis = Vector3.right;
                if(steps[i].moveType == MoveType.Down || steps[i].moveType == MoveType.Left) direction = -1;
                for(int j = 0; j < 180/5; j++) {            
                    tileGroup.transform.RotateAround(target,axis, 5*direction);       
                    yield return new WaitForSeconds(0.0001f);
                }
                
                steps[i - 1].puzzle.hidePuzzle();                
            }                        
            yield return new WaitForSeconds(time);
        }
    }

    private Vector3 getTarget(GameObject tileGroup, MoveType move) {
        //For down we need highest X / For up lowest X
        //For Left we need lowest Z / For right highest Z
        Vector3 target = new Vector3(0.0f, -1.1f, 0.0f); //hopefully this is ok
        foreach(Transform tile in tileGroup.transform) {
            switch(move) {
                case MoveType.Down:
                    if(tile.position.x > target.x || target.y == -1.1f) {
                        target = tile.position;
                        target.x += 0.5f;
                    }
                    break;
                case MoveType.Up:
                    if(tile.position.x < target.x || target.y == -1.1f) {
                        target = tile.position;
                        target.x -= 0.5f;
                    }
                    break;
                case MoveType.Left:
                    if(tile.position.z < target.z || target.y == -1.1f) {
                        target = tile.position;
                        target.z -= 0.5f;
                    }
                    break;
                case MoveType.Right:
                    if(tile.position.z > target.z || target.y == -1.1f) {
                        target = tile.position;
                        target.z += 0.5f;
                    }
                    break;
            }
        }        
        return target;
    }

    public TileType[][] copyMatrix(TileType[][] matrix)
    {
        TileType[][] copy = new TileType[matrix.Length][];
        for (int i = 0; i < matrix.Length; i++)
        {
            copy[i] = new TileType[matrix[i].Length];
            Array.Copy(matrix[i], copy[i], matrix[i].Length);
        }
        return copy;
    }
}
