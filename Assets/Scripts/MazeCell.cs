using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeCell : MonoBehaviour
{
	public Vector3 position;
	public int mazePositionX;
	public int mazePositionZ;
	public bool visited;
	public List<MazeCell> neighbors;

	public bool leftWall;
	public bool topWall;
	public bool rightWall;
	public bool bottomWall;

	public int wallCount;

	public void InitializeNeighbors(MazeCell[,] maze, int sizeX, int sizeZ)
	{
		//if cells have neighbors to the left (cells in 2nd column --> last column)
		if (this.mazePositionX > 0)
		{
			//check if left neighbor has not been visited and add it to list of neighbors
			if (!maze[this.mazePositionX - 1, this.mazePositionZ].visited)
			{
				MazeCell leftNeighbor = maze[this.mazePositionX - 1, this.mazePositionZ];
				neighbors.Add(leftNeighbor);
			}
		}

		//if cells have neighbors below it (cells in 2nd row --> last row)
		if (this.mazePositionZ > 0)
		{
			//check if bottom neighbor has not been visited and add it to list of neighbors
			if (!maze[this.mazePositionX, this.mazePositionZ - 1].visited)
			{
				MazeCell bottomNeighbor = maze[this.mazePositionX, this.mazePositionZ - 1];
				neighbors.Add(bottomNeighbor);
			}
		}

		//if cells have neighbors above it (cells from first row --> second to last row)
		if (this.mazePositionZ < sizeZ - 1)
		{
			//check if top neighbor has not been visited and add it to list of neighbors
			if (!maze[this.mazePositionX, this.mazePositionZ + 1].visited)
			{
				MazeCell topNeighbor = maze[this.mazePositionX, this.mazePositionZ + 1];
				neighbors.Add(topNeighbor);
			}
		}

		//if cells have neighbors to the right (cells in 1st column --> second to last column)
		if (this.mazePositionX < sizeX - 1)
		{
			//check if right neighbor has not been visited and add it to list of neighbors
			if (!maze[this.mazePositionX + 1, this.mazePositionZ].visited)
			{
				MazeCell rightNeighbor = maze[this.mazePositionX + 1, this.mazePositionZ];
				neighbors.Add(rightNeighbor);
			}
		}
	}

}
