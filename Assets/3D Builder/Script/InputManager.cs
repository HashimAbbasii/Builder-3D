using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputManager : MonoBehaviour
{
    public event Action<float> onRotate;
    public event Action<float> onScale;
    [SerializeField]
    private Camera sceneCamera;

    private Vector3 lastPosition;

    [SerializeField]
    private LayerMask placementLayerMask;
    public event Action onClick, onExit;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            onClick?.Invoke();
        }
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            onExit?.Invoke();
        }

        if (Input.GetKey(KeyCode.R))
        {
           // Debug.Log("Rotate");
            float rotationAmount = Input.GetKey(KeyCode.LeftShift) ? -5f : 5f; // Rotate clockwise or counterclockwise
            onRotate?.Invoke(rotationAmount);
        }

        // Scaling Input
        if (Input.GetKey(KeyCode.S))
        {
            Debug.Log("Scale");
            float scaleAmount = Input.GetKey(KeyCode.LeftShift) ? -0.1f : 0.1f; // Scale up or down
            onScale?.Invoke(scaleAmount);
        }

    }
    public bool IsPointerOverUI() => EventSystem.current.IsPointerOverGameObject();
    

    
    public Vector3 GetSelectedMapPosition()
    {
        Vector3 mousePos=Input.mousePosition;
        mousePos.z=sceneCamera.nearClipPlane;
        Ray ray = sceneCamera.ScreenPointToRay(mousePos);
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit, 100, placementLayerMask))
        {
            lastPosition = hit.point;
        }
        return lastPosition;
    }
}
