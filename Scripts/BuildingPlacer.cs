using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using Unity.VisualScripting;

public class BuildingPlacer : MonoBehaviour
{
    public bool needToBuild;
    public bool needToSell;
    public GameObject buildingPrefab;
    public LevelScript level;
    public int price;
    [SerializeField] private int gridSize;
    public GameObject placerPrefab;
    private Transform placer;
    public List<Vector3Int> occupiedPositions = new List<Vector3Int>();
     
    // Update is called once per frame
    void Update()
    {
        if (needToSell)
        {
            SellTurret();
            return;
        }

        if (!needToBuild)
        {
            return;
        }

        if (Input.GetMouseButtonDown(1))
        { 
            needToBuild = false;

            if (placer != null)
            {
                Destroy(placer.gameObject);
                placer = null;
            }

            CustomCursor cursor = FindFirstObjectByType<CustomCursor>();

            if (cursor != null)
            {
                cursor.SetBuildCursor(false);
            }
        }

        if (!placer)
        {
            placer = Instantiate(placerPrefab).transform;
        }
        else
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            Vector3Int position = Vector3Int.zero;
            if(Physics.Raycast(ray, out hit))
            {
                position = Vector3Int.RoundToInt(hit.point);
                position.x = Mathf.RoundToInt(position.x / gridSize) * gridSize;
                position.y = 0;
                position.z = Mathf.RoundToInt(position.z / gridSize) * gridSize;
                placer.position = position;
            }

            if (Input.GetKeyDown(KeyCode.R))
            {
                placer.Rotate(0, 90, 0);
            }

            MeshRenderer renderer = placer.GetComponentInChildren<MeshRenderer>();

            if (!occupiedPositions.Contains(position))
            {
                 if(renderer != null)
                {
                    renderer.material.color = new Color(130, 130, 130, 125);
                }

                if (Input.GetMouseButtonDown(0))
                {
                    PlaceBuilding(position);
                }
            }
            else
            {
                if(renderer != null)
                {
                    renderer.material.color = new Color(255, 0, 0, 125);
                }
            }
        }
    }

    private void PlaceBuilding(Vector3Int position)
    {
        Instantiate(buildingPrefab, position, placer.rotation);
        Destroy(placer.gameObject);
        occupiedPositions.Add(position);
        level.FillScoreText(price);
        needToBuild = false;

        CustomCursor cursor = FindFirstObjectByType<CustomCursor>();

        if (cursor != null)
        {
            cursor.SetBuildCursor(false);
        }
    }

    public void StartSellMode()
    {
        needToSell = true;
        needToBuild = false;

        CustomCursor cursor = FindFirstObjectByType<CustomCursor>();

        if (cursor != null)
        {
            cursor.SetSellCursor(true);
        }
    }

    private void SellTurret()
    {
        if (Input.GetMouseButtonDown(1))
        {
            needToSell = false;

            CustomCursor cursor = FindFirstObjectByType<CustomCursor>();

            if (cursor != null)
            {
                cursor.SetSellCursor(false);
            }

            return;
        }

        if (!Input.GetMouseButtonDown(0))
            return;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            TurretScript turret = hit.collider.GetComponentInParent<TurretScript>();

            if (turret != null)
            {
                int refund = Mathf.RoundToInt(turret.SellPrice * 0.25f);

                Vector3Int position = Vector3Int.RoundToInt(turret.transform.position);

                Destroy(turret.gameObject);

                occupiedPositions.Remove(position);

                level.FillScoreText(refund);

                needToSell = false;

                CustomCursor cursor = FindFirstObjectByType<CustomCursor>();

                if (cursor != null)
                {
                    cursor.SetSellCursor(false);
                }
            }
        }
    }
}
