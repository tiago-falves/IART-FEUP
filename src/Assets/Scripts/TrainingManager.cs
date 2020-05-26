﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TrainingManager : MonoBehaviour
{
    public GameObject tilePrefab;
    public List<TileType[][]> puzzleMatrixList = new List<TileType[][]>();

    //public Transform testTransform;
    public void Start() {
        //puzzleMatrixList.Add(Example.puzzleRl1);
        //puzzleMatrixList.Add(Example.puzzleRl2);
        //puzzleMatrixList.Add(Example.puzzleRl3);
        
        puzzleMatrixList.Add(Example.puzzleRlTestEasy);
        //displayPuzzle(generatePuzzle(), testTransform);
    }

    public Puzzle generatePuzzle() {
        if(puzzleMatrixList.Count == 0) return null;

        int randPos = UnityEngine.Random.Range(0,puzzleMatrixList.Count);
        TileType[][] puzzleMatrix = puzzleMatrixList[randPos];

        return new Puzzle(puzzleMatrix, tilePrefab);
    }

    public void displayPuzzle(Puzzle puzzle, Transform puzzleTransform) {
        //Delete all children of the puzzleTransform
        foreach(Transform child in puzzleTransform) {
            Destroy(child.gameObject);
        }

        puzzle.displayPuzzle(puzzleTransform);
        //puzzle.startPuzzle();
    }
}