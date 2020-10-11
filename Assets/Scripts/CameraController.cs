using UnityEngine;

public class CameraController : MonoBehaviour
{
    /// <summary>
    /// Set the camera height based on the max between the width and the height
    /// </summary>
    /// <param name="gridSize">Current grid size</param>
    public void UpdateCameraPosition(Vector2Int gridSize)
    {
        int newHeight = Mathf.Max(gridSize.x, gridSize.y);
        transform.position = new Vector3(0f, newHeight);
    }
}
