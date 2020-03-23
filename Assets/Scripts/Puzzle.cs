﻿using UnityEngine;
using System;
using System.Collections.Generic;
using System.Collections; 

public enum TileType {Empty, Null, Red, Blue, Green, Yellow, Gray, Magenta}

public class Puzzle
{
    //HARDCODED PUZZLES//
    public static TileType[][] puzzle1 = new TileType[][]
    {
        new TileType[] {TileType.Red, TileType.Empty, TileType.Null, TileType.Null},
        new TileType[] {TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty},
        new TileType[] {TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty},
        new TileType[] {TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty},
        new TileType[] {TileType.Null, TileType.Null, TileType.Blue, TileType.Empty}
    };

    public static TileType[][] puzzle2 = new TileType[][]
    {
        new TileType[] {TileType.Empty, TileType.Empty, TileType.Empty},
        new TileType[] {TileType.Empty, TileType.Empty, TileType.Empty},
        new TileType[] {TileType.Empty, TileType.Empty, TileType.Empty},
        new TileType[] {TileType.Blue, TileType.Red, TileType.Empty}
    };

    public static TileType[][] puzzle3 = new TileType[][]
    {
        new TileType[] {TileType.Null, TileType.Green, TileType.Null, TileType.Null, TileType.Null, TileType.Null},
        new TileType[] {TileType.Green, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Null},
        new TileType[] {TileType.Empty, TileType.Empty, TileType.Yellow, TileType.Empty, TileType.Empty, TileType.Null},
        new TileType[] {TileType.Blue, TileType.Empty, TileType.Empty, TileType.Empty,TileType.Magenta, TileType.Null},
        new TileType[] {TileType.Empty, TileType.Empty, TileType.Gray, TileType.Empty, TileType.Magenta, TileType.Magenta},
        new TileType[] {TileType.Empty, TileType.Empty, TileType.Empty, TileType.Magenta, TileType.Magenta, TileType.Magenta},
        new TileType[] {TileType.Empty, TileType.Red, TileType.Empty, TileType.Empty, TileType.Null, TileType.Null}

    };
    public static TileType[][] puzzle4 = new TileType[][]
    {
        new TileType[] {TileType.Null, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Green, TileType.Green, TileType.Green},
        new TileType[] {TileType.Null, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Green, TileType.Green, TileType.Green},
        new TileType[] {TileType.Null, TileType.Blue, TileType.Blue, TileType.Empty, TileType.Green, TileType.Empty, TileType.Empty},
        new TileType[] {TileType.Null, TileType.Blue, TileType.Blue, TileType.Empty, TileType.Green, TileType.Empty, TileType.Empty},
        new TileType[] {TileType.Null, TileType.Blue, TileType.Blue, TileType.Blue, TileType.Empty, TileType.Empty, TileType.Empty},
        new TileType[] {TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Red, TileType.Yellow, TileType.Empty},
        new TileType[] {TileType.Empty, TileType.Empty, TileType.Empty, TileType.Magenta, TileType.Empty, TileType.Null, TileType.Null},
        new TileType[] {TileType.Null, TileType.Null, TileType.Null, TileType.Null, TileType.Empty, TileType.Null, TileType.Null}

    };
    //HARDCODED PUZZLES//
    private GameObject tilePrefab;
    private TileType[][] puzzleMatrix;
    public TileType[][] PuzzleMatrix{get {return puzzleMatrix;} set{puzzleMatrix = value;}}
    public Puzzle(TileType[][] matrix, GameObject tilePrefab) {
        puzzleMatrix = matrix;
        this.tilePrefab = tilePrefab;
    }

    

    public Puzzle copy(){

        TileType[][] copy = new TileType[puzzleMatrix.Length][];
        

        for (int i = 0; i < puzzleMatrix.Length; i++)
        {
            copy[i] = new TileType[puzzleMatrix[i].Length];
            Array.Copy(puzzleMatrix[i], copy[i],puzzleMatrix[i].Length);
            
        }
        Puzzle puzzle = new Puzzle(copy,tilePrefab);
        return puzzle;
    }

    public void displayPuzzle() {
        for(int i = 0; i < puzzleMatrix.Length; i++) {
            for(int j = 0; j < puzzleMatrix[i].Length; j++) {
                if(puzzleMatrix[i][j] != TileType.Null) {
                    GameObject baseTile = UnityEngine.Object.Instantiate(tilePrefab, new Vector3(i, 0.125f, j), Quaternion.identity); //Base Block Below
                    baseTile.GetComponent<MeshRenderer>().material.color = Color.white;
                          
                    if((int)puzzleMatrix[i][j] > 1) {
                        GameObject instantiatedTile = UnityEngine.Object.Instantiate(tilePrefab, new Vector3(i, 0.375f, j), Quaternion.identity); //Actual Tiles on top                    

                        switch(puzzleMatrix[i][j]) {
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

    public bool moveUp(TileType tile){

        int rotationAxis = puzzleMatrix.Length;
        bool foundAxis = false;

        //Discover the rotation axis
        for (int i = 0; (i < puzzleMatrix.Length) && (!foundAxis); i++){
            for (int j = 0; j < puzzleMatrix[i].Length; j++){

                if(puzzleMatrix[i][j] == tile){
                    rotationAxis = i;
                    foundAxis = true;
                    break;
                }                
            }
        }

        List<Tuple<int,int>> positions = new List<Tuple<int,int>>() ;

        //Discovers if all the elements can rotate
        //Adds positions of the tiles to a List
        for (int i = rotationAxis; i < puzzleMatrix.Length; i++)
        {
            for (int j = 0; j < puzzleMatrix[i].Length; j++){
                if (tile == puzzleMatrix[i][j]){
                    int symetricX =i-2*(i-rotationAxis)-1;  
                    if(canMove(symetricX,j)){
                        positions.Add(Tuple.Create(symetricX,j));
                    } else return false;
                }
            }
        }

        //If they can all move, change the colors
        for (int i = 0; i < positions.Count; i++){
            puzzleMatrix[positions[i].Item1][positions[i].Item2] = tile;
        }

        return true;
    }

    public void undoMoveUp(TileType tile) {
        int lowerBound = 0;
        int upperBound = puzzleMatrix[0].Length-1;

        //Discover the bounds
        for (int i = 0; i < puzzleMatrix.Length; i++) {
            for (int j = 0; j < puzzleMatrix[i].Length; j++) {
                if(puzzleMatrix[i][j] == tile) {
                    if(i < upperBound)
                        upperBound = i;
                    if(i > lowerBound)
                        lowerBound = i;
                }
            }
        }
    
        int symetryAxis = (upperBound+lowerBound)/2;

        for (int i = 0; i <= symetryAxis; i++) {
            for (int j = 0; j < puzzleMatrix[i].Length; j++) {
                if(puzzleMatrix[i][j] == tile){
                    puzzleMatrix[i][j] = TileType.Empty;
                }
            }
        }
    }

    public bool moveDown(TileType tile){

        int rotationAxis = -1;
        bool foundAxis = false;

        //Discover the rotation axis
        for (int i = puzzleMatrix.Length-1; (i >= 0) && (!foundAxis); i--){
            for (int j = 0; j < puzzleMatrix[i].Length; j++){
                if(puzzleMatrix[i][j] == tile){
                    rotationAxis = i;
                    foundAxis = true;
                    break;
                }                
            }
        }

        List<Tuple<int,int>> positions = new List<Tuple<int,int>>() ;

        //Discovers if all the elements can rotate
        //Adds positions of the tiles to a List
        for (int i = 0; i < puzzleMatrix.Length; i++){
            for (int j = 0; j < puzzleMatrix[i].Length; j++){
                if (tile == puzzleMatrix[i][j]){
                    int symetricX =i+2*(rotationAxis-i)+1;  
                    if(canMove(symetricX,j)){

                        positions.Add(Tuple.Create(symetricX,j));
                    } else return false;
                }
            }
        }

        //If they can all move, change the colors
        for (int i = 0; i < positions.Count; i++){
            puzzleMatrix[positions[i].Item1][positions[i].Item2] = tile;
        }

        return true;
    }

    public void undoMoveDown(TileType tile) {
        int lowerBound = 0;
        int upperBound = puzzleMatrix[0].Length-1;

        //Discover the bounds
        for (int i = 0; i < puzzleMatrix.Length; i++) {
            for (int j = 0; j < puzzleMatrix[i].Length; j++) {
                if(puzzleMatrix[i][j] == tile) {
                    if(i < upperBound)
                        upperBound = i;
                    if(i > lowerBound)
                        lowerBound = i;
                }
            }
        }
    
        int symetryAxis = (upperBound+lowerBound)/2;

        for (int i = puzzleMatrix.Length-1; i > symetryAxis; i--) {
            for (int j = 0; j < puzzleMatrix[i].Length; j++) {
                if(puzzleMatrix[i][j] == tile){
                    puzzleMatrix[i][j] = TileType.Empty;
                }
            }
        }
    }

     public bool moveRight(TileType tile){

        int rotationAxis = -1;

        //Discover the rotation axis
        for (int i = 0; i < puzzleMatrix.Length; i++){
            for (int j = puzzleMatrix[i].Length-1; (j >= 0); j--){
                if(puzzleMatrix[i][j] == tile && j > rotationAxis){
                    rotationAxis = j;
                }                
            }
        }

        List<Tuple<int,int>> positions = new List<Tuple<int,int>>() ;

        //Discovers if all the elements can rotate
        //Adds positions of the tiles to a List
        for (int i = 0; i < puzzleMatrix.Length; i++){
            for (int j = 0; j < puzzleMatrix[i].Length; j++){
                if (tile == puzzleMatrix[i][j]){
                    int symetricY =j+2*(rotationAxis-j)+1;  
                    if(canMove(i,symetricY)){
                        positions.Add(Tuple.Create(i,symetricY));
                    } else return false;
                }
            }
        }

        //If they can all move, change the colors
        for (int i = 0; i < positions.Count; i++){
            puzzleMatrix[positions[i].Item1][positions[i].Item2] = tile;
        }

        return true;
    }

    public void undoMoveRight(TileType tile) {
        int leftBound = puzzleMatrix.Length;
        int rightBound = 0;

        //Discover the bounds
        for (int i = 0; i < puzzleMatrix.Length; i++) {
            for (int j = 0; j < puzzleMatrix[i].Length; j++) {
                if(puzzleMatrix[i][j] == tile) {
                    if(j < leftBound)
                        leftBound = j;
                    if(j > rightBound)
                        rightBound = j;
                }
            }
        }
    
        int symetryAxis = (rightBound+leftBound)/2;

        for (int i = 0; i < puzzleMatrix.Length; i++) {
            for (int j = puzzleMatrix[i].Length-1; j > symetryAxis; j--) {
                if(puzzleMatrix[i][j] == tile){
                    puzzleMatrix[i][j] = TileType.Empty;
                }
            }
        }
    }

    public bool moveLeft(TileType tile){

        int rotationAxis = puzzleMatrix.Length;

        //Discover the rotation axis
        for (int i = 0; i < puzzleMatrix.Length; i++){
            for (int j = 0; j < puzzleMatrix[i].Length; j++){
                if(puzzleMatrix[i][j] == tile && j < rotationAxis){
                    rotationAxis = j;
                }                
            }
        }

        List<Tuple<int,int>> positions = new List<Tuple<int,int>>() ;

        //Discovers if all the elements can rotate
        //Adds positions of the tiles to a List
        for (int i = 0; i < puzzleMatrix.Length; i++){
            for (int j = 0; j < puzzleMatrix[i].Length; j++){
                if (tile == puzzleMatrix[i][j]){
                    int symetricY =j-2*(j-rotationAxis)-1;  
                    if(canMove(i,symetricY)){
                        positions.Add(Tuple.Create(i,symetricY));
                    } else return false;
                }
            }
        }

        //If they can all move, change the colors
        for (int i = 0; i < positions.Count; i++){
            puzzleMatrix[positions[i].Item1][positions[i].Item2] = tile;
        }

        return true;
    }

    public void undoMoveLeft(TileType tile) {
        int leftBound = puzzleMatrix.Length;
        int rightBound = 0;

        //Discover the bounds
        for (int i = 0; i < puzzleMatrix.Length; i++) {
            for (int j = 0; j < puzzleMatrix[i].Length; j++) {
                if(puzzleMatrix[i][j] == tile) {
                    if(j < leftBound)
                        leftBound = j;
                    if(j > rightBound)
                        rightBound = j;
                }
            }
        }
    
        int symetryAxis = (rightBound+leftBound)/2;

        for (int i = 0; i < puzzleMatrix.Length; i++) {
            for (int j = 0; j <= symetryAxis; j++) {
                if(puzzleMatrix[i][j] == tile){
                    puzzleMatrix[i][j] = TileType.Empty;
                }
            }
        }
    }

    // Returns True if it is a valid Tile
    public bool canMove(int i , int j){
        if(i < puzzleMatrix.Length && i >= 0){
            if(j >= 0 && j < puzzleMatrix[i].Length){
                if(puzzleMatrix[i][j] == TileType.Empty) return true;
            }
        }
        return false;
    }

    public bool isComplete(){

        for (int i = 0; i < puzzleMatrix.Length; i++){
            for (int j = 0; j < puzzleMatrix[i].Length; j++){
                if (canMove(i,j)){
                    return false;                    
                }                
            }
        }
        return true; 
    }

    public List<TileType> puzzleColors(){

        List<TileType> puzzleColors = new List<TileType>();

        for (int i = 0; i < puzzleMatrix.Length; i++){
            for (int j = 0; j < puzzleMatrix[i].Length; j++){
                if( puzzleMatrix[i][j] != TileType.Empty && puzzleMatrix[i][j] != TileType.Null && !puzzleColors.Contains(puzzleMatrix[i][j])){
                    puzzleColors.Add(puzzleMatrix[i][j]);
                }          
            }
        }
            
        return puzzleColors;

    } 

    

    
    public void displayConsole(){
        
        string puzzleString="";
        for (int i = 0; i < puzzleMatrix.Length; i++)
        {
            for (int j = 0; j < puzzleMatrix[i].Length; j++)
            {
                puzzleString+=puzzleMatrix[i][j] + " ";
            }            
        }
        Debug.Log(puzzleString);
    }

    public bool search(String typeOfSearch){

        Puzzle current = copy();


        if(typeOfSearch == "DFS"){

            DFS dfs = new DFS(current);
            if(dfs.search(current))
                return true;

        }else if(typeOfSearch == "DFSUndo"){

            DFSUndo dfsU = new DFSUndo(current);
            if(dfsU.search(current))
                return true;

        } 
        else if(typeOfSearch == "BFS"){

            BFS bfs = new BFS(current);
            bfs.search();

        } else if(typeOfSearch == "IDDFS"){

            IDDFS iDDFS = new IDDFS(current,6);
            iDDFS.search();
        }

  
    
        return false;

    }




    
}