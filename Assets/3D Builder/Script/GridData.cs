using AYellowpaper.SerializedCollections;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GridData 
{

    public SerializedDictionary<Vector3Int, PlacementData> placeObjects = new();

    public void AddObjectAt(Vector3Int gridPosition,Vector2Int objectSize,int ID,int placedObjectIndex)
    {
        List<Vector3Int> positionToOccupy = CalculatePositions(gridPosition, objectSize);
        PlacementData data= new PlacementData(positionToOccupy,ID,placedObjectIndex);
        foreach(var pos in positionToOccupy)
        {
            if (placeObjects.ContainsKey(pos))
                throw new Exception($"Dictionary already contain this cell position {pos}");
            placeObjects[pos] = data;
        }
    }

    private List<Vector3Int> CalculatePositions(Vector3Int gridPosition, Vector2Int objectSize)
    {
        List<Vector3Int> returnVal = new();
        for(int x =0; x< objectSize.x; x++)
        {
            for(int y =0; y< objectSize.y; y++)
            {
                returnVal.Add(gridPosition + new Vector3Int(x,y));
            }
        }
        return returnVal;
    }
    public bool CanPlaceObjectAt(Vector3Int gridPosition, Vector2Int objectSize)
    {
        List<Vector3Int> positionToOccupy= CalculatePositions(gridPosition, objectSize);
        foreach(var pos in positionToOccupy)
        {
            if (placeObjects.ContainsKey(pos))
            
                return false;
            
        }
        return true;
    }
    internal int GetRepresentationIndex(Vector3Int gridPosition)
    {
        if (placeObjects.ContainsKey(gridPosition) == false)
        {
            return -1;
        }
        return placeObjects[gridPosition].PlacedObjectIndex;
    }
    internal void RemoveObjectAt(Vector3Int gridPosition)
    {
        foreach(var pos in placeObjects[gridPosition].occupiedPosition)
        {
            placeObjects.Remove(pos);
        }
    }


    internal void SelectEditableObject(Vector3Int gridPosition)
    {
        foreach (var pos in placeObjects[gridPosition].occupiedPosition)
        {
            Debug.Log("location require");
            //placeObjects.Remove(pos);
        }
    }
}

[Serializable]
public class PlacementData
{
    public List<Vector3Int> occupiedPosition;
    public int ID { get; private set; }
    public int PlacedObjectIndex { get; private set; }

    public PlacementData(List<Vector3Int> occupiedPosition, int iD, int placedObjectIndex)
    {
        this.occupiedPosition = occupiedPosition;
        ID = iD;
        PlacedObjectIndex = placedObjectIndex;
    }
}
