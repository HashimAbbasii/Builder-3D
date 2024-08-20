using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;


public class PlacementSystem : MonoBehaviour
{

    [SerializeField]
    private InputManager inputManager;
    [SerializeField]
    private GameObject cellIndicator;
    [SerializeField]
    private Grid grid;

    [SerializeField]
    private ObjectDatabaseSO database;
   

    [SerializeField]
    private GameObject gridVisualization;

    public GridData floorData, furnitureData;
    private Renderer previewRenderer;
    [SerializeField]
    private PreviewSystem previewSystem;
    private Vector3Int lastDetectedPosition=Vector3Int.zero;
    [SerializeField]
    private ObjectPlacer objectPlacer;
    IPlacementState buildingState;
  //  public GameObject currentPlacedObject;



    // Start is called before the first frame update
    void Start()
    {

        StopPlacement();
        floorData = new ();
        furnitureData = new ();
        //previewRenderer = cellIndicator.GetComponentInChildren<Renderer>();


    }

    public void StartPlacement(int ID)
    {

        StopPlacement();
        gridVisualization.SetActive(true);
        buildingState = new PlacementState(ID, grid, previewSystem, database, floorData, furnitureData, objectPlacer);
        inputManager.onClick += PlaceStructure;
        inputManager.onExit += StopPlacement;
        inputManager.onRotate += RotatePlacedObject;
        inputManager.onScale += ScalePlacedObject;
    }
    public void StartRemoving()
    {
        StopPlacement();
        gridVisualization.SetActive(true);
        buildingState = new RemovingState(grid, previewSystem, floorData, furnitureData, objectPlacer);
        inputManager.onClick += PlaceStructure;
        inputManager.onExit += StopPlacement;
       
    }

    public void StartRotatingObject()
    {
        StopPlacement();
        gridVisualization.SetActive(true);
        buildingState = new RotatingState(grid, previewSystem, floorData, furnitureData, objectPlacer);
        inputManager.onClick += PlaceStructure;
        inputManager.onExit += StopPlacement;
    }

    private void PlaceStructure()
    {
        if(inputManager.IsPointerOverUI())
        {
          
            return;
        }
        Debug.Log("Building State" + buildingState);
        Vector3 mousePosition = inputManager.GetSelectedMapPosition();
        Vector3Int gridPosition = grid.WorldToCell(mousePosition);
        //Debug.Log(gridPosition.ToString());
        buildingState.onAction(gridPosition);
       // currentPlacedObject = objectPlacer.GetLastPlacedObject();

    }

    private void RotatePlacedObject(float rotationAmount)
    {
        Debug.Log("aya");
       
        {
            Debug.Log("Rotate");
           // currentPlacedObject.transform.Rotate(Vector3.up, rotationAmount);
        }
    }

    private void ScalePlacedObject(float scaleAmount) { 
    //{
    //    if (currentPlacedObject != null)
    //    {
    //        Vector3 newScale = currentPlacedObject.transform.localScale + new Vector3(scaleAmount, scaleAmount, scaleAmount);
    //        currentPlacedObject.transform.localScale = newScale;
    //    }
    }

    //private bool CheckPlacementValidity(Vector3Int gridPosition, int selectedObjectIndex)
    //{
    //    GridData selectedData = database.objectsData[selectedObjectIndex].ID==0?floorData:furnitureData;
    //    return selectedData.CanPlaceObjectAt(gridPosition, database.objectsData[selectedObjectIndex].size);
    //}


    private void StopPlacement()
    {
        if (buildingState == null)
        {
            return ;
        }
       
        gridVisualization.SetActive(false);
        buildingState.EndState();
        //previewSystem.StopShowingPreview();
        inputManager.onClick -= PlaceStructure;
        inputManager.onExit -= StopPlacement;
        /*throw new NotImplementedException()*/;
        lastDetectedPosition=Vector3Int.zero;
        buildingState=null;
        inputManager.onRotate -= RotatePlacedObject;
        inputManager.onScale -= ScalePlacedObject;
    }

    public bool see;

    // Update is called once per frame
    void FixedUpdate()
    {
        if(buildingState== null)
            return;
        Vector3 mousePosition= inputManager.GetSelectedMapPosition();
        Vector3Int gridPosition = grid.WorldToCell(mousePosition);


       // see = furnitureData.CanPlaceObjectAt(gridPosition, new Vector2Int(1, 0));


        if(lastDetectedPosition != gridPosition)
        {
            buildingState.UpdateState(gridPosition);
            lastDetectedPosition = gridPosition;
        }
        
    }
}
