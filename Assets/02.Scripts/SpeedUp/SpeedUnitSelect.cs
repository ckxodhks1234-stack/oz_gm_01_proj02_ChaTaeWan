using UnityEngine;

public class SpeedUnitSelect : MonoBehaviour
{
    [SerializeField] private SpeedUnitController speedUnitController;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SelectCheck();
        }
    }

    private void SelectCheck()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, 100f))
        {
            if (hit.collider.GetComponentInParent<SpeedUnitController>() != null)
            {
                speedUnitController.SetSelected(true);
            }
            else
            {
                //다른 곳 클릭 - 선택 해제
                speedUnitController.SetSelected(false);
            }
        }
    }
}
