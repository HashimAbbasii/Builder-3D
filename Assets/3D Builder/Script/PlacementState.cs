using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementState : IPlacementState
{

    private int selectedObjectIndex = -1;
    int ID;
    Grid grid;
    PreviewSystem preview;
    ObjectDatabaseSO database;
    GridData floorData;
    GridData furnitureData;
    ObjectPlacer objectPlacer;


    public PlacementState(int iD, Grid grid, PreviewSystem preview, ObjectDatabaseSO database, GridData floorData, GridData furnitureData, ObjectPlacer objectPlacer)
    {
        ID = iD;
        this.grid = grid;
        this.preview = preview;
        this.database = database;
        this.floorData = floorData;
        this.furnitureData = furnitureData;
        this.objectPlacer = objectPlacer;

        selectedObjectIndex = database.objectsData.FindIndex(data => data.ID == ID);
        if (selectedObjectIndex > -1)
        {

            preview.StartShowingPlacementPreview(database.objectsData[selectedObjectIndex].Prefabs, database.objectsData[selectedObjectIndex].size);
        }
        else
        {
            throw new System.Exception($"No Object with ID {iD}");
        }



    }

    public void EndState()
    {
        preview.StopShowingPreview();
    }

    public void onAction(Vector3Int gridPosition)
    {
       // Debug.Log("Action 2");
        bool placementValidity = CheckPlacementValidity(gridPosition, selectedObjectIndex);
        if (placementValidity == false)
        {
            return;

        }

        int index = objectPlacer.PlaceObject(database.objectsData[selectedObjectIndex].Prefabs, grid.CellToWorld(gridPosition));
     //   Debug.Log("Index Capture" + index);

        GridData selectedData = database.objectsData[selectedObjectIndex].ID == 0 ? floorData : furnitureData;
        selectedData.AddObjectAt(gridPosition, database.objectsData[selectedObjectIndex].size,
            database.objectsData[selectedObjectIndex].ID, index);
        preview.UpdatePosition(grid.CellToWorld(gridPosition), false);
    }

    private bool CheckPlacementValidity(Vector3Int gridPosition, int selectedObjectIndex)
    {
        GridData selectedData = database.objectsData[selectedObjectIndex].ID == 0 ? floorData : furnitureData;
        return selectedData.CanPlaceObjectAt(gridPosition, database.objectsData[selectedObjectIndex].size);
    }

    public void UpdateState(Vector3Int gridPosition)
    {
        bool placementValidity = CheckPlacementValidity(gridPosition, selectedObjectIndex);
        //mouseIndicator.transform.position = mousePosition;
        preview.UpdatePosition(grid.CellToWorld(gridPosition), placementValidity);
    }
}
