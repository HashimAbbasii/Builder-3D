using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPlacer : MonoBehaviour
{
    private float rotationSpeed = 100f;
    private GameObject lastPlacedObject;
    [SerializeField]
    private List<GameObject> placedGameObject = new();
    public bool rotatingStart=false;
    private GameObject selectedRotaionGameObject;
    public int PlaceObject(GameObject prefab, Vector3 position)
    {
        GameObject newObject = Instantiate(prefab);
        newObject.transform.position = position;
        //throw new NotImplementedException();
        placedGameObject.Add(newObject);
        return placedGameObject.Count-1;
    }
    internal void RemoveObjectAt(int GameObjectIndex)
    {
        if(placedGameObject.Count <= GameObjectIndex || placedGameObject[GameObjectIndex] == null ) return;
        Destroy(placedGameObject[GameObjectIndex]);
        placedGameObject[GameObjectIndex] = null;

    }

    internal void RotatingObject(int GameObjectIndex)
    {
        if (placedGameObject.Count <= GameObjectIndex || placedGameObject[GameObjectIndex] == null)
        { 
          
            return; 
        }
     
        Quaternion SelectedGameRotation = placedGameObject[GameObjectIndex].transform.rotation;
        Vector3 selectedGameRotationEuler = placedGameObject[GameObjectIndex].transform.rotation.eulerAngles;
        
        selectedRotaionGameObject = placedGameObject[GameObjectIndex];
        rotatingStart = true;
        //Destroy(placedGameObject[GameObjectIndex]);
        //placedGameObject[GameObjectIndex] = null;

    }

    public GameObject GetLastPlacedObject()
    {
        return lastPlacedObject;
    }
    private void Update()
    {
        if (rotatingStart)
        {
            Quaternion SelectedGameRotation = selectedRotaionGameObject.transform.rotation;
            //Vector3 selectedGameRotationEuler = selectedRotaionGameObject.transform.rotation.eulerAngles;
            if (Input.GetKey(KeyCode.R))
            {              
                selectedRotaionGameObject.transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
            }
        }
    }

    // Start is called before the first frame update

}
