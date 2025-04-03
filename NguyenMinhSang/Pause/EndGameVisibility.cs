using UnityEngine;

public class EndGameVisibility : MonoBehaviour
{
    public GameObject targetGameObject; // Reference to the GameObject you want to unhide

    // This function will be called by the animation event
    public void UnhideGameObject()
    {
        if (targetGameObject != null)
        {
            targetGameObject.SetActive(true); // Unhide the GameObject
        }
    }
}
