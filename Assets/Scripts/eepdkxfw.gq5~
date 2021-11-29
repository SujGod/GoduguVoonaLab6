using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MazeCreation : MonoBehaviour
{
    [SerializeField] private Maze mazeInstance;
    [SerializeField] private GameObject character;
    [SerializeField] private Predator enemy;
    [SerializeField] private int numOfEnemies;
    //[SerializeField] private GameObject text;
    [SerializeField] private MazeCell[,] maze;
    [SerializeField] private OnExit exitTrigger;
    private Maze createdMaze;
    private bool allItemsDeleted;

    void Start()
    {
        //create the first random maze on startup
        CreateMaze();
        allItemsDeleted = false;
        //text.SetActive(false);
    }

    private void Update()
    {
        //if the exit trigger has been reached and not all items are deleted in maze
        if (exitTrigger.exitReached && !allItemsDeleted)
        {
            //destroy all objects in maze and maze instance itself
            createdMaze.DestroyMaze();
            GameObject.Destroy(createdMaze.gameObject);

            //set boolean flag to true for all items being deleted
            allItemsDeleted = true;

            //display game over text
            //text.SetActive(true);
        }

        //if all items are deleted
        if (allItemsDeleted)
        {
            //create new maze
            CreateMaze();

            //set flag back to false for items not being deleted in the maze
            allItemsDeleted = false;

            //stop displaying the game over text
            //StartCoroutine(StopGameOverText());
        }
    }

    private void CreateMaze()
    {
        //create instance of the maze for retrying level
        createdMaze = Instantiate(mazeInstance);

        //generate maze for the created maze instance
        maze = createdMaze.Generate();
        Debug.Log("The maze has been generated");

        //Generates enemies in the maze
        createdMaze.GenerateEnemies(maze, numOfEnemies, enemy);

        //set character to start of maze (at entrance cell)
        character.transform.position = new Vector3(-mazeInstance.sizeX / 2, 0.5f, -mazeInstance.sizeZ / 2);

        //obtain exit cell in order to create exit trigger for displaying game over and retrying level
        MazeCell exitCell = maze[mazeInstance.sizeX - 1, mazeInstance.sizeZ - 1];
        Vector3 triggerOffset = new Vector3(0, 1 / 2f, 1 / 2f);
        exitTrigger.transform.position = exitCell.position + triggerOffset;

        //set exitTrigger boolean to false
        exitTrigger.exitReached = false;
    }

/*    IEnumerator StopGameOverText()
    {
        //display game over text for 2 seconds then turn it off
        yield return new WaitForSeconds(2);
        text.SetActive(false);
    }*/
}
