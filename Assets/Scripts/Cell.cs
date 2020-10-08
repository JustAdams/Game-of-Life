using UnityEngine;

public class Cell : MonoBehaviour
{
    public Material[] skins;
    MeshRenderer renderer;

    public bool isAlive;

    public Vector2Int position;

    private void Start()
    {
        renderer = GetComponent<MeshRenderer>();
    }

    public void SetAlive(bool alive)
    {
        isAlive = alive;
        renderer.material = (isAlive ? skins[0] : skins[1]);
    }

}
