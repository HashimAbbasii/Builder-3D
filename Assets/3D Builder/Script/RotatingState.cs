using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingState : IPlacementState
{
    private int gameObjectIndex = -1;
    int ID;
    Grid grid;
    PreviewSystem previewSystem;

    GridData floorData;
    GridData furnitureData;
    ObjectPlacer objectPlacer;

    public RotatingState(Grid grid, PreviewSystem previewSystem, GridData floorData, GridData furnitureData, ObjectPlacer objectPlacer)
    {
        this.grid = grid;
        this.previewSystem = previewSystem;
        this.floorData = floorData;
        this.furnitureData = furnitureData;
        this.objectPlacer = objectPlacer;

        previewSystem.StartShowingRemovePreview();

    }
    public void EndState()
    {
        previewSystem.StopShowingPreview();
    }


    public void onAction(Vector3Int gridPosition)
    {
        Debug.Log("Rotating Scale");
        GridData selectedData = null;
        if (furnitureData.CanPlaceObjectAt(gridPosition, Vector2Int.one) == false)
        {
            selectedData = furnitureData;
        }
        else if (floorData.CanPlaceObjectAt(gridPosition, Vector2Int.one) == false)
        {
            selectedData = floorData;
        }
        if (selectedData == null)
        {

        }
        else
        {
            gameObjectIndex = selectedData.GetRepresentationIndex(gridPosition);
            if (gameObjectIndex == -1)
            {
                return;
            }
            selectedData.SelectEditableObject(gridPosition);
            objectPlacer.RotatingObject(gameObjectIndex);

        }
        Vector3 cellPosition = grid.CellToWorld(gridPosition);
        previewSystem.UpdatePosition(cellPosition, CheckifSelectionIsValid(gridPosition));

    }

    public void UpdateState(Vector3Int gridPosition)
    {
        bool validity = CheckifSelectionIsValid(gridPosition);
        previewSystem.UpdatePosition(grid.CellToWorld(gridPosition), validity);
    }
    public bool CheckifSelectionIsValid(Vector3Int gridPosition)
    {
        return !(furnitureData.CanPlaceObjectAt(gridPosition, Vector2Int.one) && floorData.CanPlaceObjectAt(gridPosition, Vector2Int.one));
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
