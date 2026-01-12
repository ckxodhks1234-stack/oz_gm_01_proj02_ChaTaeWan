using UnityEngine;

public class SpeedUnitController : MonoBehaviour
{
    [SerializeField] private SpeedUnit speedUnit;
    private bool isSelected;

    public void SetSelected(bool selected)
    {
        isSelected = selected;
    }

    void Update()
    {
        if(!isSelected) return;

        if (Input.GetMouseButtonDown(1))
        {
            MoveSpeedUnit();
        }
    }

    private void MoveSpeedUnit()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, 100f))
        {
            speedUnit.MoveTo(hit.point);
        }
    }
}
