using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class Cell : MonoBehaviour
{
    public Material[] skins;

    public bool isAlive;
    public bool tempAlive;
    public Vector2 position;

    void Start()
    {

    }

    private void OnMouseDown()
    {
        SetAlive(!isAlive);
    }

    public void UpdateAlive()
    {
        isAlive = tempAlive;
        UpdateCellDisplay();
    }

    public void UpdateCellDisplay()
    {
        GetComponent<Renderer>().material = (isAlive ? skins[1] : skins[0]);
    }

    public void SetTempAlive(bool alive)
    {
        tempAlive = alive;
    }

    public void SetAlive(bool alive)
    {
        isAlive = alive;
        UpdateCellDisplay();
    }

}
