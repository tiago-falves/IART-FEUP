﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum playerType { Bot, Player };

public class GameManager : MonoBehaviour
{
    private Puzzle currentPuzzle;
    private playerType currentPlayer;
    public GameObject tilePrefab;
    public void Start() {

        currentPuzzle = new Puzzle(Puzzle.puzzle4, tilePrefab);
        //currentPuzzle.displayPuzzle();
        currentPuzzle.search("dfs");
        currentPuzzle.displayPuzzle();
        Debug.Log("Ended DFS");
        StartCoroutine(DisplayPuzzleStates(2));
    }

    private List<Puzzle> puzzleStates = new List<Puzzle>();
    public void DisplayState(Puzzle puzzle) {
        puzzleStates.Add(puzzle);
    }

    private IEnumerator DisplayPuzzleStates(int time) {
        Debug.Log(puzzleStates.Count);
        for(int i = 0; i < puzzleStates.Count; i++) {
            Puzzle puzzle = puzzleStates[i];
            Debug.Log("Started Displaying");
            puzzle.displayPuzzle();
            yield return new WaitForSeconds(time);
        }
    }
}
