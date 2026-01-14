using UnityEngine;

public class SpeedUnitSelect : MonoBehaviour
{
    private SpeedUnitController currentSelected;

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
            SpeedUnitController hitUnit = hit.collider.GetComponentInParent<SpeedUnitController>();

            if (hitUnit != null)
            {
                //기존 선택 해제
                if (currentSelected != null)
                {
                    currentSelected.SetSelected(false);
                }
                currentSelected = hitUnit;
                currentSelected.SetSelected(true);
            }
            else
            {
                //빈 공간 클릭
                if (currentSelected != null)
                {
                    currentSelected.SetSelected(false);
                }
                currentSelected = null;
            }
        }
    }
}
