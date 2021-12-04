using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Maze : MonoBehaviour
{
    //create variables for rows and columns of maze
    [SerializeField] public int sizeX;
    [SerializeField] public int sizeZ;

    [SerializeField] private Predator enemy;
    [SerializeField] private int numOfEnemies;

    //create classes for maze walls and grid cells
    [SerializeField] private MazeWall wall;
    [SerializeField] private MazeCell cellPrefab;
    [SerializeField] private HeartPowerUp heartPrefab;
    [SerializeField] private AmmoPowerUp ammoPrefab;
    [SerializeField] private Key keyPrefab;


    //create a counter for use in creating entrance/exit later
    private int count = 0;

    public void GenerateEnemies(MazeCell[,] maze, int numOfEnemies, Predator pred)
    {
        Predator enemy = Instantiate(pred);
        var rnd = new System.Random();
        for (int i = 0; i < numOfEnemies; i++)
        {
            MazeCell cell = maze[rnd.Next(0, sizeX), rnd.Next(0, sizeZ)];
            enemy.transform.position = new Vector3(0, 0.5f, 0);
            //character.transform.position = new Vector3(-mazeInstance.sizeX / 2, 0.5f, -mazeInstance.sizeZ / 2);
            /*enemy.transform.SetParent(this.transform);*/

        }
    }

    private bool CarvePassage(MazeCell currentNode, MazeCell nextNode)
    {
        //if the neighbor is above the current node, take down the top wall of the current node and the bottom wall of the neighbor
        if ((nextNode.mazePositionX == currentNode.mazePositionX) && (nextNode.mazePositionZ == currentNode.mazePositionZ + 1))
        {
            currentNode.topWall = false;
            nextNode.bottomWall = false;
            currentNode.wallCount--;
            nextNode.wallCount--;
            return true;
        }

        //if the neighbor is below the current node, take down the bottom wall of the current node and the top wall of the neighbor
        if ((nextNode.mazePositionX == currentNode.mazePositionX) && (nextNode.mazePositionZ == currentNode.mazePositionZ - 1))
        {
            currentNode.bottomWall = false;
            nextNode.topWall = false;
            currentNode.wallCount--;
            nextNode.wallCount--;
            return true;
        }

        //if the neighbor is to the right of the current node, take down the right wall of the current node and the left wall of the neighbor
        if ((nextNode.mazePositionX == currentNode.mazePositionX + 1) && (nextNode.mazePositionZ == currentNode.mazePositionZ))
        {
            currentNode.rightWall = false;
            nextNode.leftWall = false;
            currentNode.wallCount--;
            nextNode.wallCount--;
            return true;
        }

        //if the neighbor is to the left of the current node, take down the left wall of the current node and the right wall of the neighbor
        if ((nextNode.mazePositionX == currentNode.mazePositionX - 1) && (nextNode.mazePositionZ == currentNode.mazePositionZ))
        {
            currentNode.leftWall = false;
            nextNode.rightWall = false;
            currentNode.wallCount--;
            nextNode.wallCount--;
            return true;
        }

        //if no path carved return false
        return false;
    }

    private MazeCell[,] RecursiveBacktracker(MazeCell[,] maze, int sizeX, int sizeZ)
    {
        //create a Random variable and create stack of current nodes
        var rnd = new System.Random();
        Stack<MazeCell> currentPath = new Stack<MazeCell>();

        //creat random row and position to choose first node from
        int randomXPosition = rnd.Next(0, sizeX);
        int randomZPosition = rnd.Next(0, sizeZ);

        //start from a random node and add it to the current path and set it to visited
        MazeCell startingNode = maze[randomXPosition, randomZPosition];
        startingNode.visited = true;
        currentPath.Push(startingNode);

        while (currentPath.Count > 0)
        {
            //grab the first node in the stack and acquire unvisited neighbors
            MazeCell currentNode = currentPath.Peek();
            List<MazeCell> neighbors = currentNode.neighbors;
            List<MazeCell> unvisitedNeighbors = new List<MazeCell>();

            foreach (MazeCell neighbor in neighbors)
            {
                if (!neighbor.visited)
                {
                    unvisitedNeighbors.Add(neighbor);
                }
            }

            bool pathCarved = false;
            MazeCell nextNode = null;

            //while the path hasn't been carved or there are unvisited neighbors remaining
            while (!pathCarved && unvisitedNeighbors.Count > 0)
            {
                //take a random unvisited neighbor and carve the path
                int randomPos = rnd.Next(0, unvisitedNeighbors.Count);
                nextNode = unvisitedNeighbors[randomPos];
                pathCarved = CarvePassage(currentNode, nextNode);
                nextNode.visited = true;
                unvisitedNeighbors.RemoveAt(randomPos);
            }

            //if the path was carved at the neighbor to the stack otherwise backtrack
            if (pathCarved)
            {
                currentPath.Push(nextNode);
            }
            else
            {
                currentPath.Pop();
            }
        }

        return maze;
    }

    public MazeCell[,] CreateGrid(int sizeX, int sizeZ)
    {
        //create a grid of maze cells
        MazeCell[,] maze = new MazeCell[sizeX, sizeZ];

        //create each cell in the grid and print the coords to the console
        for (int i = 0; i < sizeX; i++)
        {
            for (int j = 0; j < sizeZ; j++)
            {
                maze[i, j] = CreateCell(i, j);
                Debug.Log(GetCoords(maze[i, j]));
            }
        }

        //iterate through the cells again and initialize their neighbors
        for (int i = 0; i < sizeX; i++)
        {
            for (int j = 0; j < sizeZ; j++)
            {
                maze[i, j].InitializeNeighbors(maze, sizeX, sizeZ);
            }
        }

        return maze;
    }

    private MazeCell CreateCell(int i, int j)
    {
        //Instantiate maze cell
        MazeCell newCell = Instantiate(cellPrefab);
        newCell.transform.SetParent(this.transform);

        //set all walls to true originally to create grid
        newCell.topWall = true;
        newCell.bottomWall = true;
        newCell.rightWall = true;
        newCell.leftWall = true;

        newCell.wallCount = 4;

        //set the cell position
        newCell.position = new Vector3(-sizeX / 2 + i, 0, -sizeZ / 2 + j);

        //set maze cell row and column positions
        newCell.mazePositionX = i;
        newCell.mazePositionZ = j;

        //set maze cell to not visited
        newCell.visited = false;

        //Actually draws the cells to the relative positions
        newCell.transform.localPosition = new Vector3(-sizeX / 2 + i, 0, -sizeZ / 2 + j);

        return newCell;

    }

    private Key CreateKey(int i, int j)
    {
        //Instantiate maze cell
        Key newKey = Instantiate(keyPrefab);
        newKey.transform.SetParent(this.transform);

        //Actually draws the cells to the relative positions
        newKey.transform.position = new Vector3(-sizeX / 2 + i, 0.5f, -sizeZ / 2 + j);

        return newKey;
    }

    private HeartPowerUp CreateHeartPowerUp(int i, int j)
    {
        //Instantiate maze cell
        HeartPowerUp newPowerUp = Instantiate(heartPrefab);
        newPowerUp.transform.SetParent(this.transform);

        //Actually draws the cells to the relative positions
        newPowerUp.transform.position = new Vector3(-sizeX / 2 + i, 0.5f, -sizeZ / 2 + j);

        return newPowerUp;
    }

    private AmmoPowerUp CreateAmmoPowerUp(int i, int j)
    {
        //Instantiate maze cell
        AmmoPowerUp newPowerUp = Instantiate(ammoPrefab);
        newPowerUp.transform.SetParent(this.transform);

        //Actually draws the cells to the relative positions
        newPowerUp.transform.position = new Vector3(-sizeX / 2 + i, 0.5f, -sizeZ / 2 + j);

        return newPowerUp;
    }


    public MazeCell[,] Generate()
    {
        //create the maze and grid of cells
        MazeCell[,] maze = CreateGrid(sizeX, sizeZ);

        //apply the recursive backtracker
        maze = RecursiveBacktracker(maze, sizeX, sizeZ);

        //draw resulting walls in maze after paths have been carved out
        DrawResultingMaze(maze);

        //Generates enemies in the maze (SAVE OFF ON GENERATING ENEMIES FOR NOW)
        GenerateEnemies(maze, numOfEnemies, enemy);

        DrawMazeItems(maze);

        return maze;
    }

    private void DrawMazeItems(MazeCell[,] maze)
    {
        int count = 0;
        int numOfPowerUps = 0;
        bool keyCreated = false;
        for (int i = 0; i < sizeX; i++)
        {
            for (int j = 0; j < sizeZ; j++)
            {
                count++;
                //get each cell in the maze
                MazeCell cell = maze[i, j];

                //if the cell has 3 walls (dead end) and cell is not entrance cell or exit cell
                //create token at cell
                if ((cell.wallCount == 3 && count > 1 && count < (sizeX * sizeZ)) && numOfPowerUps < 4)
                {
                    if ((numOfPowerUps % 2) == 0)
                    {
                        CreateHeartPowerUp(cell.mazePositionX, cell.mazePositionZ);
                    }
                    else
                    {
                        CreateAmmoPowerUp(cell.mazePositionX, cell.mazePositionZ);
                    }
                    numOfPowerUps++;
                }
                else if ((cell.wallCount == 3 && count > 1 && count < (sizeX * sizeZ)) && !keyCreated)
                {
                    CreateKey(cell.mazePositionX, cell.mazePositionZ);
                    keyCreated = true;
                }

            }
        }

        //create at most 5 more tokens if they are not also dead end cells at random positions
        //if cell is dead end don't add token, so this will add 0 - 5 token extra random tokens in maze
/*        System.Random rd = new System.Random();
        for (int i = 0; i < 5; i++)
        {
            MazeCell randCell = maze[rd.Next(sizeX), rd.Next(sizeZ)];
            if (randCell.wallCount != 3)
            {
                CreatePowerUp(randCell.mazePositionX, randCell.mazePositionZ);
            }
        }*/


    }

    private void DrawResultingMaze(MazeCell[,] maze)
    {
        //iterate through the cells in the maze to draw the corresponding walls
        for (int i = 0; i < sizeX; i++)
        {
            for (int j = 0; j < sizeZ; j++)
            {
                //increment a counter
                count++;

                //get each cell in the maze
                MazeCell cell = maze[i, j];

                //if the count is less than the last iteration and cells have top walls, draw top walls for every row and column except the top right corner
                if ((count < (sizeX * sizeZ)) && cell.topWall)
                {
                    MazeWall topWall = Instantiate(wall);
                    topWall.transform.SetParent(cell.transform);
                    Vector3 topWallOffset = new Vector3(0, 1f, 1 / 2f);
                    topWall.transform.position = cell.position + topWallOffset;
                }

                //if cells have left walls, draw left walls for all cells
                if (cell.leftWall)
                {
                    MazeWall leftWall = Instantiate(wall);
                    leftWall.transform.SetParent(cell.transform);
                    Vector3 leftWallOffset = new Vector3(-1 / 2f, 1f, 0);
                    leftWall.transform.position = cell.position + leftWallOffset;
                    leftWall.transform.eulerAngles = new Vector3(0, 90, 0);
                }

                //if cells have right walls, draw right walls for last column in maze 
                if (i == sizeX - 1 && cell.rightWall)
                {
                    MazeWall rightWall = Instantiate(wall);
                    rightWall.transform.SetParent(cell.transform);
                    Vector3 rightWallOffset = new Vector3(1 / 2f, 1f, 0);
                    rightWall.transform.position = cell.position + rightWallOffset;
                    rightWall.transform.eulerAngles = new Vector3(0, 90, 0);
                }

                //if the count is greater than the first iteration and cells have bottom walls, draw bottom walls for last row in maze (creating an entrance at the bottom)
                if (j == 0 && cell.bottomWall)
                {
                    MazeWall bottomWall = Instantiate(wall);
                    bottomWall.transform.SetParent(cell.transform);
                    Vector3 bottomWallOffset = new Vector3(0, 1f, -1 / 2f);
                    bottomWall.transform.position = cell.position + bottomWallOffset;
                }
            }
        }
    }

    private Vector2 GetCoords(MazeCell cell)
    {
        //get the coordinates of each cell in the maze
        Vector2 coordinates = new Vector2(cell.position.x, cell.position.z);
        return coordinates;
    }

    public void DestroyMaze()
    {
        //take each object in the maze and destroy it for resetting the level
        foreach (Transform child in transform)
        {
            GameObject.Destroy(child.gameObject);
        }
    }
}
