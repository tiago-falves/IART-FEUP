﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RLPuzzleMenu : MonoBehaviour
{
    public GameObject inferencePrefab;
    public GameManager gameManagerRL;
    private string algorithm = "";

    public Camera gameCameraRl;
    public Camera menuCameraRl;
    // Start is called before the first frame update

    public void setAlgorithm(string algorithm)
    {
        this.algorithm = algorithm;
    }
    public void chooseRL1()
    {
        Debug.Log("RL Puzzle 1");
        menuCameraRl.gameObject.SetActive(false);
        gameCameraRl.gameObject.SetActive(true);
        Instantiate(inferencePrefab, new Vector3(0,0,0), Quaternion.identity);
        GameObject manager = GameObject.FindWithTag("Manager");
        manager.GetComponent<InferenceManager>().puzzleMatrix = Example.puzzleNNeasy1;
    }
    public void chooseRL2()
    {
        Debug.Log("RL Puzzle 2");
        menuCameraRl.gameObject.SetActive(false);
        gameCameraRl.gameObject.SetActive(true);
        Instantiate(inferencePrefab, new Vector3(0,0,0), Quaternion.identity);
        GameObject manager = GameObject.FindWithTag("Manager");
        manager.GetComponent<InferenceManager>().puzzleMatrix = Example.puzzleNNeasy2;
    }
    public void chooseRL3()
    {
        Debug.Log("RL Puzzle 3");
        menuCameraRl.gameObject.SetActive(false);
        gameCameraRl.gameObject.SetActive(true);
        Instantiate(inferencePrefab, new Vector3(0,0,0), Quaternion.identity);
        GameObject manager = GameObject.FindWithTag("Manager");
        manager.GetComponent<InferenceManager>().puzzleMatrix = Example.puzzleNNeasy3;
    }
    public void chooseRL4()
    {
        Debug.Log("RL Puzzle 4");
        menuCameraRl.gameObject.SetActive(false);
        gameCameraRl.gameObject.SetActive(true);
        Instantiate(inferencePrefab, new Vector3(0,0,0), Quaternion.identity);
        GameObject manager = GameObject.FindWithTag("Manager");
        manager.GetComponent<InferenceManager>().puzzleMatrix = Example.puzzleNNmedium1;
    }
    public void chooseRL5()
    {
        Debug.Log("RL Puzzle 5");
        menuCameraRl.gameObject.SetActive(false);
        Instantiate(inferencePrefab, new Vector3(0,0,0), Quaternion.identity);
        GameObject manager = GameObject.FindWithTag("Manager");
        manager.GetComponent<InferenceManager>().puzzleMatrix = Example.puzzleNNmedium2;
    }
    public void chooseRL6()
    {
        Debug.Log("RL Puzzle 6");
        menuCameraRl.gameObject.SetActive(false);
        gameCameraRl.gameObject.SetActive(true);
        Instantiate(inferencePrefab, new Vector3(0,0,0), Quaternion.identity);
        GameObject manager = GameObject.FindWithTag("Manager");
        manager.GetComponent<InferenceManager>().puzzleMatrix = Example.puzzleNNmedium3;
    }
    public void chooseRL7()
    {
        Debug.Log("RL Puzzle 7");
        menuCameraRl.gameObject.SetActive(false);
        gameCameraRl.gameObject.SetActive(true);
        Instantiate(inferencePrefab, new Vector3(0,0,0), Quaternion.identity);
        GameObject manager = GameObject.FindWithTag("Manager");
        manager.GetComponent<InferenceManager>().puzzleMatrix = Example.puzzleNNhard1;
    }

    public void chooseRL8()
    {
        Debug.Log("RL Puzzle 8");
        menuCameraRl.gameObject.SetActive(false);
        gameCameraRl.gameObject.SetActive(true);
        Instantiate(inferencePrefab, new Vector3(0,0,0), Quaternion.identity);
        GameObject manager = GameObject.FindWithTag("Manager");
        manager.GetComponent<InferenceManager>().puzzleMatrix = Example.puzzleNNhard2;
    }
    public void chooseRL9()
    {
        Debug.Log("RL Puzzle 9");
        menuCameraRl.gameObject.SetActive(false);
        gameCameraRl.gameObject.SetActive(true);
        Instantiate(inferencePrefab, new Vector3(0,0,0), Quaternion.identity);
        GameObject manager = GameObject.FindWithTag("Manager");
        manager.GetComponent<InferenceManager>().puzzleMatrix = Example.puzzleNNhard3;
    }

}
