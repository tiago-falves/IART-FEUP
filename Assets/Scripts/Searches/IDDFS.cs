using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class IDDFS
{

    private int depth;
    private List<TileType> colors;
    private Puzzle puzzle;


    //Deve implmentar se visited? Nao porque senao ele nao ia voltar ver a arovre completa
    public bool search(Puzzle puzzle, int depth)
    {
        this.depth = depth;
        colors = puzzle.puzzleColors();
        this.puzzle = puzzle.copy();

        for (int limit = 0; limit < depth; limit++)
        {
            if (dls(puzzle, limit))
                return true;
        }
        return false;
    }

    public bool dls(Puzzle current, int limit)
    {

        if (current.isComplete())
        {
            current.displayPuzzle();
            return true;
        }

        if (limit <= 0) return false;

        foreach (TileType tile in colors)
        {

            Puzzle puzzleDown = current.copy();
            if (puzzleDown.moveDown(tile))
            {
                if (dls(puzzleDown, limit - 1))
                    return true;
            }

            Puzzle puzzleUp = current.copy();
            if (puzzleUp.moveUp(tile))
            {
                if (dls(puzzleUp, limit - 1))
                    return true;

            }
            Puzzle puzzleLeft = current.copy();
            if (puzzleLeft.moveLeft(tile))
            {
                if (dls(puzzleLeft, limit - 1))
                    return true;
            }
            Puzzle puzzleRight = current.copy();
            if (puzzleRight.moveRight(tile))
            {
                if (dls(puzzleRight, limit - 1))
                    return true;
            }
        }
        return false;
    }
}

