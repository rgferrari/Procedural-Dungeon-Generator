using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class WanderingDrunk
{
    private string[] directions = {"North", "East", "South", "West"};

    private string previousDirection;

    private Vector3Int position = new Vector3Int(0,0,0);

    private int horizontalStepSize, verticalStepSize;

    public WanderingDrunk(int horizontalStepSize, int verticalStepSize)
    {
        this.horizontalStepSize = horizontalStepSize;

        this.verticalStepSize = verticalStepSize;
    }

    public void Step(string direction)
    {
        if (direction == "North")
        {
            position = position + new Vector3Int(0, verticalStepSize, 0);
            previousDirection = "South";
        }
        else if (direction == "East")
        {
            position = position + new Vector3Int(horizontalStepSize, 0, 0);
            previousDirection = "West";
        }
        else if (direction == "South")
        {
            position = position + new Vector3Int(0, -verticalStepSize, 0);
            previousDirection = "North";
        }
        else if (direction == "West")
        {
            position = position + new Vector3Int(-horizontalStepSize, 0, 0);
            previousDirection = "East";
        }
    }
    public void Step()
    {
        string randDirection = directions[Random.Range(0,directions.Length)];

        if (randDirection == "North")
        {
            position = position + new Vector3Int(0,verticalStepSize,0);
            previousDirection = "South";
        }
        else if (randDirection == "East")
        {
            position = position + new Vector3Int(horizontalStepSize,0,0);
            previousDirection = "West";
        }
        else if (randDirection == "South")
        {
            position = position + new Vector3Int(0,-verticalStepSize,0);
            previousDirection = "North";
        }
        else if (randDirection == "West")
        {
            position = position + new Vector3Int(-horizontalStepSize,0,0);
            previousDirection = "East";
        }
    }

    public string GetPreviousDirection() 
    {
        return previousDirection;
    }

    public Vector3Int GetPosition() 
    {
        return position;
    }
}
