using UnityEngine;
using System;
using System.Collections.Generic;
using System.Collections; 


public class Example{
    //HARDCODED PUZZLES//
    public TileType[][] puzzle1 = new TileType[][]
    {
        new TileType[] {TileType.Red, TileType.Empty, TileType.Null, TileType.Null},
        new TileType[] {TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty},
        new TileType[] {TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty},
        new TileType[] {TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty},
        new TileType[] {TileType.Null, TileType.Null, TileType.Blue, TileType.Empty}
    };

    public TileType[][] puzzle2 = new TileType[][]
    {
        new TileType[] {TileType.Empty, TileType.Empty, TileType.Empty},
        new TileType[] {TileType.Empty, TileType.Empty, TileType.Empty},
        new TileType[] {TileType.Empty, TileType.Empty, TileType.Empty},
        new TileType[] {TileType.Blue, TileType.Red, TileType.Empty}
    };

    public TileType[][] puzzleMedium = new TileType[][]
    {
        new TileType[] {TileType.Null, TileType.Green, TileType.Null, TileType.Null, TileType.Null, TileType.Null},
        new TileType[] {TileType.Green, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Null},
        new TileType[] {TileType.Empty, TileType.Empty, TileType.Yellow, TileType.Empty, TileType.Empty, TileType.Null},
        new TileType[] {TileType.Blue, TileType.Empty, TileType.Empty, TileType.Empty,TileType.Magenta, TileType.Null},
        new TileType[] {TileType.Empty, TileType.Empty, TileType.Gray, TileType.Empty, TileType.Magenta, TileType.Magenta},
        new TileType[] {TileType.Empty, TileType.Empty, TileType.Empty, TileType.Magenta, TileType.Magenta, TileType.Magenta},
        new TileType[] {TileType.Empty, TileType.Red, TileType.Empty, TileType.Empty, TileType.Null, TileType.Null}

    };
    public TileType[][] puzzle4 = new TileType[][]
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

    public TileType[][] puzzleIntermidiate = new TileType[][]
    {
        new TileType[] {TileType.Null, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Green, TileType.Green},
        new TileType[] {TileType.Null, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Green, TileType.Green},
        new TileType[] {TileType.Null, TileType.Blue, TileType.Blue, TileType.Empty, TileType.Green, TileType.Empty},
        new TileType[] {TileType.Null, TileType.Blue, TileType.Blue, TileType.Empty, TileType.Green, TileType.Empty},
        new TileType[] {TileType.Null, TileType.Blue, TileType.Blue, TileType.Blue, TileType.Empty, TileType.Empty},
        new TileType[] {TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Red, TileType.Yellow},
        new TileType[] {TileType.Null, TileType.Null, TileType.Null, TileType.Null, TileType.Empty, TileType.Null}

    };

    public TileType[][] puzzleDifficult = new TileType[][]
    {
        new TileType[] {TileType.Null,TileType.Null,TileType.Null, TileType.Null, TileType.Null, TileType.Null, TileType.Empty, TileType.Empty, TileType.Empty},
        new TileType[] {TileType.Null,TileType.Null, TileType.Green, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty},
        new TileType[] {TileType.Null,TileType.Null, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Blue, TileType.Empty, TileType.Empty},
        new TileType[] {TileType.Magenta,TileType.Magenta, TileType.Magenta, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Blue, TileType.Empty, TileType.Red},
        new TileType[] {TileType.Magenta,TileType.Null, TileType.Empty, TileType.Empty, TileType.Null, TileType.Empty, TileType.Empty, TileType.Yellow, TileType.Null},
        new TileType[] {TileType.Magenta,TileType.Magenta, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Null},
        new TileType[] {TileType.Empty,TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Null},
        new TileType[] {TileType.Empty,TileType.Null, TileType.Empty, TileType.Gray, TileType.Null, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Null},
        new TileType[] {TileType.Empty,TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Null, TileType.Null, TileType.Null, TileType.Null}

    };
    public TileType[][] puzzleHard = new TileType[][]
    {
        new TileType[] {TileType.Null,TileType.Empty,TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Null},
        new TileType[] {TileType.Null,TileType.Empty,TileType.Empty, TileType.Blue, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Null},
        new TileType[] {TileType.Null,TileType.Null, TileType.Null, TileType.Magenta, TileType.Empty, TileType.Empty, TileType.Gray, TileType.Null},
        new TileType[] {TileType.Null,TileType.Red, TileType.Red, TileType.Magenta, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Null},
        new TileType[] {TileType.Red,TileType.Red, TileType.Null, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Null},
        new TileType[] {TileType.Empty,TileType.Empty, TileType.Null, TileType.Green, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Null},
        new TileType[] {TileType.Null,TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Null},
        new TileType[] {TileType.Null,TileType.Null, TileType.Null, TileType.Null, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Null},
        new TileType[] {TileType.Empty,TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Yellow},
        new TileType[] {TileType.Empty,TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty}

    };
    public TileType[][] puzzleExpert = new TileType[][]
    {
        new TileType[] {TileType.Empty,TileType.Empty,TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty,TileType.Empty,TileType.Empty},
        new TileType[] {TileType.Empty,TileType.Empty,TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty,TileType.Empty,TileType.Empty},
        new TileType[] {TileType.Empty,TileType.Empty,TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty,TileType.Empty,TileType.Empty},
        new TileType[] {TileType.Magenta,TileType.Magenta,TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty,TileType.Empty,TileType.Empty},
        new TileType[] {TileType.Empty,TileType.Empty,TileType.Empty, TileType.Red, TileType.Empty, TileType.Green, TileType.Empty, TileType.Empty,TileType.Empty,TileType.Empty},
        new TileType[] {TileType.Empty,TileType.Empty,TileType.Empty, TileType.Red, TileType.Empty, TileType.Green, TileType.Blue, TileType.Empty,TileType.Gray,TileType.Empty},
        new TileType[] {TileType.Empty,TileType.Empty,TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty,TileType.Empty,TileType.Empty},
        new TileType[] {TileType.Empty,TileType.Empty,TileType.Empty, TileType.Yellow, TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty,TileType.Empty,TileType.Empty},

    };
}