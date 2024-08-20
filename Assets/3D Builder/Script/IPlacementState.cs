using UnityEngine;

public interface IPlacementState
{
    void EndState();
    void onAction(Vector3Int gridPosition);
    void UpdateState(Vector3Int gridPosition);
}