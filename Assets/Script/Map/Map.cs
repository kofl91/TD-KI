using UnityEngine;
using System.Collections;
using System.Collections.Generic;


[System.Serializable]
public class SpawnPoint 
{
    public Transform self;
    public Transform destination;
}

public abstract class Map : MonoBehaviour
{

    public enum eTileType
    {
        Free=0,
        Turret,
        Way
    };

    public List<SpawnPoint> spawnPoints = new List<SpawnPoint>();

    public List<Waypoint> wayPoints = new List<Waypoint>();

    protected eTileType[,] grid;

    protected int mapSizeX;
    protected int mapSizeY;

    private int creationCursorX;
    private int creationCursorY;

    private GameObject lastWaypoint;

    public bool isLoaded = true;



    void Start()
    {
        createMap();
    }

    #region MapCreation
    public abstract void createMap();

    public void clearMap()
    {
        for (int x = 0; x < mapSizeX; x++)
        {
            for (int y = 0; y < mapSizeY; y++)
            {
                grid[x, y] = eTileType.Free;
            }
        }
    }

    protected void createSpawnPoint(int x, int y, Transform firstDestination)
    {
        GameObject go = (GameObject)Instantiate(PrefabContainer.Instance.spawnPointPrefab, new Vector3(x, 1.0f, y), Quaternion.identity);
        SpawnPoint sp = new SpawnPoint();
        sp.self = go.transform;
        sp.destination = firstDestination;
        spawnPoints.Add(sp);
    }

    protected void createWayPoint(int x, int y)
    {
        
        lastWaypoint = (GameObject)Instantiate(PrefabContainer.Instance.wayPointPrefab, new Vector3(x, 1.5f, y), Quaternion.identity);
        Waypoint wp = lastWaypoint.GetComponent<Waypoint>();
        wayPoints.Add(wp);
    }

    protected GameObject createEndZone(int x, int y)
    {
        return (GameObject) Instantiate(PrefabContainer.Instance.endZonePrefab, new Vector3(x, 1.0f, y), Quaternion.identity);
    }

    protected void startWayFromTo (int x, int y, int xt, int yt)
    {
        setWayFromTo(x, y, xt, yt);
        createWayPoint(xt, yt);
        createSpawnPoint(x, y, lastWaypoint.transform);
        creationCursorX = xt;
        creationCursorY = yt;
    }

    protected void continueWayTo(int x, int y)
    {
        GameObject buffer = lastWaypoint;
        createWayPoint(x, y);
        buffer.GetComponent<Waypoint>().Destination = lastWaypoint.transform;
        setWayFromTo(x, y, creationCursorX, creationCursorY);
        creationCursorX = x;
        creationCursorY = y;
    }

    protected void endWayAt(int x, int y)
    {
        setWayFromTo(x, y, creationCursorX, creationCursorY);
        lastWaypoint.GetComponent<Waypoint>().Destination = createEndZone(x, y).transform;
    }

    protected void setWayFromTo(int x1, int y1, int x2, int y2)
    {
        if (x1 > x2)
        {
            int buf = x1;
            x1 = x2;
            x2 = buf;
        }
        if (y1 > y2)
        {
            int buf = y1;
            y1 = y2;
            y2 = buf;
        }

        if ((x1 <= x2) && (y1 <= y2))
        {
            for (int x = x1; x <= x2; x++)
            {
                for (int y = y1; y <= y2; y++)
                {
                    grid[x, y] = eTileType.Way;
                }
            }
        }
    }

    protected void GenerateMapVisual()
    {
        for (int x = 0; x < mapSizeX; x++)
        {
            for (int y = 0; y < mapSizeY; y++)
            {
                TileType tt = PrefabContainer.Instance.tileTypes[(int)grid[x, y]];

                GameObject go = (GameObject)Instantiate(tt.tileVisualPrefab, new Vector3(x, 1.0f, y), Quaternion.identity);
                if (grid[x, y] != eTileType.Way)
                {
                    ClickableTile ct = go.GetComponent<ClickableTile>();
                    ct.tileX = x;
                    ct.tileY = y;
                }

            }
        }
    }
    #endregion



    #region EnemySpawning
    private List<GameObject> activeEnemys = new List<GameObject>();

    private eTileType[,] Grid
    {
        get
        {
            return grid;
        }

        set
        {
            grid = value;
        }
    }

    public void Spawn(int spawnPrefabIndex)
    {
        for( int i=0; i < spawnPoints.Count; i++)
        {
            Spawn(spawnPrefabIndex, i);
        }
    }

    public void Spawn(int spawnPrefabIndex, int spawnPointIndex)
    {
        GameObject go = Instantiate(PrefabContainer.Instance.enemys[spawnPrefabIndex]
            , spawnPoints[spawnPointIndex].self.position
            , spawnPoints[spawnPointIndex].self.rotation) as GameObject;
        go.SendMessage("SetDestination", spawnPoints[spawnPointIndex].destination);
        activeEnemys.Add(go);
    }

    public void Despawn(GameObject go)
    {
        activeEnemys.Remove(go);
        Destroy(go);
    }


    public void ClearEnemys()
    {
        foreach (GameObject go in activeEnemys)
        {
            Destroy(go);
        }
        activeEnemys.Clear();
    }


    public int getEnemysLeft()
    {
        return activeEnemys.Count;
    }


    #endregion


    public float lastTime = 0.0f;
    public float interval = 1.0f;

    public void Update()
    {
        if ((Time.time - lastTime) >= interval)
        {
            GameManager.Instance.currentMap.Spawn(0 , 0);
            lastTime = Time.time;
        }
    }
    
}
