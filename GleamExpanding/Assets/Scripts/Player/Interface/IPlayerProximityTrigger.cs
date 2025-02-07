using UnityEngine;

public interface IPlayerProximityTrigger
{
    void OnPlayerEnter(GameObject interactableObj);
    void OnPlayerExit();
}
