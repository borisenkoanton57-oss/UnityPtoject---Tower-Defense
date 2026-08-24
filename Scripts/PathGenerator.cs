using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using System.Collections;

public class PathGenerator : MonoBehaviour
{
    [Header("Map Objects")]
    [SerializeField] private GameObject cavePrefab;
    [SerializeField] private GameObject castlePrefab;

    [Header("Cave")]
    [SerializeField] private string spawnPointName = "SpawnPoint";

    [Header("Stats")]
    [SerializeField] private LevelScript level;
    private int lastRow;
    private int lastColumn;

    [Header("Path")]
    public GameObject pathPrefab;
    [SerializeField] private Transform paths;
    [SerializeField] private BuildingPlacer placer;

    [Header ("Grid")]
    public int rows;
    public int columns;
    public float cellSize;
    private bool[,] grid;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        grid = new bool[rows, columns];
        GameObject path = Instantiate(pathPrefab, paths);
        Vector3 position = new Vector3(0f, 0f, Random.Range(0, rows) * cellSize);
        path.transform.position = position;
        level.spawn = new Vector3(-cellSize, 1f, position.z);
        lastColumn = 0;
        lastRow = Mathf.FloorToInt(position.z / cellSize);
        grid [lastRow, lastColumn] = true;
        Generate();
        PlaceMapObjects();
    }

    private void PlaceMapObjects()
    {
        // =========================
        // ПЕЩЕРА
        // =========================

        Vector3 firstPathPosition = paths.GetChild(0).position;

        GameObject cave = Instantiate(
        cavePrefab,
        firstPathPosition + Vector3.left * cellSize,
        Quaternion.Euler(0f, 90f, 0f)
        );

        Transform spawnPoint = cave.transform.Find(spawnPointName);

        if (spawnPoint != null)
        {
            level.spawn = spawnPoint.position;
        }
        else
        {
            Debug.LogWarning("SpawnPoint не найден внутри пещеры!");

            level.spawn =
                firstPathPosition + Vector3.left * cellSize;
        }


        // ---------------------------------
        // ЗАМОК
        // ---------------------------------

        int lastIndex = paths.childCount - 1;

        Vector3 lastPathPosition =
            paths.GetChild(lastIndex).position;

        Vector3 castlePosition =
            lastPathPosition + Vector3.right * cellSize;


        // Определяем направление последнего участка дороги
        Vector3 direction;

        if (lastIndex > 0)
        {
            Vector3 previousPathPosition =
                paths.GetChild(lastIndex - 1).position;

            direction =
                (lastPathPosition - previousPathPosition).normalized;
        }
        else
        {
            direction = Vector3.right;
        }


        // Замок должен смотреть НА ДОРОГУ
        Vector3 castleForward = -direction;

        Quaternion castleRotation =
            Quaternion.LookRotation(castleForward, Vector3.up);


        Instantiate(
            castlePrefab,
            castlePosition,
            castleRotation
        );
    }

    private void Generate()
    {
        int column = lastColumn;
        int row = lastRow;

        int direction = Random.Range(0, 3);
        switch (direction)
        {
            case 0:
                column++;
                break;
            case 1:
                row--;
                break;
            case 2:
                row++;
                break;
            default:
                Debug.Log("Direction out of range");
                break;
        }

        if (!(column >= columns || row < 0 || row >= rows || grid[row, column]))
        {
            GameObject path = Instantiate(pathPrefab, paths);
            Vector3 position = new Vector3(column * cellSize, 0f, row * cellSize);
            path.transform.position = position;
            lastColumn = column;
            lastRow = row;
            grid[lastRow, lastColumn] = true;
            placer.occupiedPositions.Add(Vector3Int.RoundToInt(position));
        }
        if(lastColumn < columns - 1)
        {
            Generate();
        }
    }
}
