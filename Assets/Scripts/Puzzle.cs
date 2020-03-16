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
    //HARDCODED PUZZLES//
    private GameObject tilePrefab;
    private TileType[][] puzzleMatrix;
    public Puzzle(TileType[][] matrix, GameObject tilePrefab) {
        puzzleMatrix = matrix;
        this.tilePrefab = tilePrefab;
    }

    public Puzzle copy(){
        Puzzle puzzle = new Puzzle(puzzleMatrix,tilePrefab);
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
                if( puzzleMatrix[i][j] != TileType.Empty && !puzzleColors.Contains(puzzleMatrix[i][j])){
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
        List<Puzzle> searchList = new List<Puzzle>();
        Puzzle current = copy();
        var visited = new HashSet<Puzzle>();

        searchList.Add(current);

        List<TileType> colors = current.puzzleColors();


        while(searchList.Count != 0){
   
            if(typeOfSearch == "bfs"){
                if(current.breadthFirstSearch(searchList,colors,visited)){
                    return true;
                }
            } else if(typeOfSearch == "dfs"){
                if(current.depthFirstSearch(searchList,colors,visited)){
                    return true;
                }
            }
            
        }

        return false;

    }

    public bool depthFirstSearch(List<Puzzle> searchStack,List<TileType> colors, HashSet<Puzzle> visited){


        Puzzle current = searchStack[0];
        searchStack.RemoveAt(0);

        if(visited.Contains(current)){   
            current.displayPuzzle();
            return false;
        } 


        if(current.isComplete()){   
            current.displayPuzzle();
            return true;
        }

        displayConsole();


        visited.Add(current);

        foreach (TileType tile in colors){
            Puzzle puzzleDown = copy();
            if (puzzleDown.moveDown(tile)){
                searchStack.Insert(0,puzzleDown);                        
            }    
            Puzzle puzzleUp = copy();
            
            if (puzzleUp.moveUp(tile)){
                searchStack.Insert(0,puzzleUp);                        
            }     
            Puzzle puzzleLeft = copy();
            if (puzzleLeft.moveLeft(tile)){
                searchStack.Insert(0,puzzleLeft);                        
            }  
            Puzzle puzzleRight = copy();
            if (puzzleRight.moveRight(tile)){
                searchStack.Insert(0,puzzleRight);                        
            } 
                                        
        }

        return false;

    }


    public bool breadthFirstSearch(List<Puzzle> searchQueue,List<TileType> colors, HashSet<Puzzle> visited){

        displayConsole();

        Puzzle current = searchQueue[searchQueue.Count-1];
        searchQueue.RemoveAt(searchQueue.Count -1);

        if(visited.Contains(current)){   
            current.displayPuzzle();
            return false;
        } 

        if(current.isComplete()){   
            current.displayPuzzle();
            return true;
        } 


        visited.Add(current);


        foreach (TileType tile in colors){
            Puzzle puzzleDown = copy();
            if (puzzleDown.moveDown(tile)){
                searchQueue.Add(puzzleDown);                        
            }    
            Puzzle puzzleUp = copy();
            
            if (puzzleUp.moveUp(tile)){
                searchQueue.Add(puzzleUp);                        
            }     
            Puzzle puzzleLeft = copy();
            if (puzzleLeft.moveLeft(tile)){
                searchQueue.Add(puzzleLeft);                        
            }  
            Puzzle puzzleRight = copy();
            if (puzzleRight.moveRight(tile)){
                searchQueue.Add(puzzleRight);                        
            }                                         
        }

        return false;

    }
}