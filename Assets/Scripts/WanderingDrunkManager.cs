using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class WanderingDrunkManager : MonoBehaviour
{
    public Tilemap backgroundMap;
    public Tilemap wallMap;
    public Tilemap objectsMap;

    public Tile background;

    public Tile wall;

    public Tile player;

    public Tile boss;

    public int maxSteps = 4;

    private int roomWidth = 7, roomHeight = 7;

    WanderingDrunk[] wanderingDrunk = new WanderingDrunk[4];


    // Start is called before the first frame update
    void Start()
    {
        GenerateLevel();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
            GenerateLevel();
    }

    private IEnumerator releaseWanderingDrunks(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        wanderingDrunk[0].Step("North");
        Build(wanderingDrunk[0].GetPosition(), wanderingDrunk[0].GetPreviousDirection());
        wanderingDrunk[1].Step("East");
        Build(wanderingDrunk[1].GetPosition(), wanderingDrunk[1].GetPreviousDirection());
        wanderingDrunk[2].Step("South");
        Build(wanderingDrunk[2].GetPosition(), wanderingDrunk[2].GetPreviousDirection());
        wanderingDrunk[3].Step("West");
        Build(wanderingDrunk[3].GetPosition(), wanderingDrunk[3].GetPreviousDirection());

        for (int i = 0; i < maxSteps; i++) {
            yield return new WaitForSeconds(delayTime);
            wanderingDrunk[0].Step();
            Build(wanderingDrunk[0].GetPosition(), wanderingDrunk[0].GetPreviousDirection());
            wanderingDrunk[1].Step();
            Build(wanderingDrunk[1].GetPosition(), wanderingDrunk[1].GetPreviousDirection());
            wanderingDrunk[2].Step();
            Build(wanderingDrunk[2].GetPosition(), wanderingDrunk[2].GetPreviousDirection());
            wanderingDrunk[3].Step();
            Build(wanderingDrunk[3].GetPosition(), wanderingDrunk[3].GetPreviousDirection());

            if(i == maxSteps - 1)
            {
                SpawnBoss();
            }
        }
    }

    private void Build(Vector3Int position, string doorDirection)
    {
        if (wallMap.GetTile(new Vector3Int(position.x, position.y, 0)) == null)
        {
            for (int x = 0; x < roomWidth; x++)
            {
                for (int y = 0; y < roomHeight; y++)
                {
                    //draw the left and right walls
                    if (x == 0 || x == roomWidth - 1)
                        wallMap.SetTile(position + new Vector3Int(x, y, 0), wall);

                    //draw the top and bottom walls
                    else if (y == 0 || y == roomHeight - 1)
                        wallMap.SetTile(position + new Vector3Int(x, y, 0), wall);

                    backgroundMap.SetTile(position + new Vector3Int(x, y, 0), background);
                }
            }
        }
        if(doorDirection == "North") 
        {
            wallMap.SetTile(position + new Vector3Int(roomWidth/2, roomHeight - 1, 0), null);
            wallMap.SetTile(position + new Vector3Int(roomWidth/2, roomHeight, 0), null);
        }
        else if(doorDirection == "East") 
        {
            wallMap.SetTile(position + new Vector3Int(roomWidth - 1, roomHeight/2, 0), null);
            wallMap.SetTile(position + new Vector3Int(roomWidth, roomHeight/2, 0), null);
        }
        else if(doorDirection == "South") 
        {
            wallMap.SetTile(position + new Vector3Int(roomWidth/2, 0, 0), null);
            wallMap.SetTile(position + new Vector3Int(roomWidth/2, -1, 0), null);
        }
        else if(doorDirection == "West") 
        {
            wallMap.SetTile(position + new Vector3Int(0, roomHeight/2, 0), null);
            wallMap.SetTile(position + new Vector3Int(-1, roomHeight/2, 0), null);
        }
    }
    private void BuildSpawnRoom(Vector3Int position)
    {
        if (wallMap.GetTile(new Vector3Int(position.x, position.y, 0)) == null)
        {
            for (int x = 0; x < roomWidth; x++)
            {
                for (int y = 0; y < roomHeight; y++)
                {
                    //draw the left and right walls
                    if (x == 0 || x == roomWidth - 1)
                        wallMap.SetTile(position + new Vector3Int(x, y, 0), wall);

                    //draw the top and bottom walls
                    else if (y == 0 || y == roomHeight - 1)
                        wallMap.SetTile(position + new Vector3Int(x, y, 0), wall);

                    backgroundMap.SetTile(position + new Vector3Int(x, y, 0), background);

                    if(y == roomWidth/2 && x == roomHeight/2)
                        objectsMap.SetTile(position + new Vector3Int(x, y, 0), player);
                }
            }
        }
    }

    private void GenerateLevel()
    {
        wanderingDrunk[0] = new WanderingDrunk(roomWidth, roomHeight);
        wanderingDrunk[1] = new WanderingDrunk(roomWidth, roomHeight);
        wanderingDrunk[2] = new WanderingDrunk(roomWidth, roomHeight);
        wanderingDrunk[3] = new WanderingDrunk(roomWidth, roomHeight);

        ClearMap();

        BuildSpawnRoom(new Vector3Int(0, 0, 0));

        StartCoroutine(releaseWanderingDrunks(.5f));
    }

    private void SpawnBoss()
    {
        Vector3Int bossRoomPosition = wanderingDrunk[Random.Range(0, wanderingDrunk.Length)].GetPosition();
        objectsMap.SetTile(bossRoomPosition + new Vector3Int(roomWidth / 2, roomHeight / 2, 0), boss);
    }

    private void ClearMap()
    {
        backgroundMap.ClearAllTiles();
        wallMap.ClearAllTiles();
        objectsMap.ClearAllTiles();
    }
}
