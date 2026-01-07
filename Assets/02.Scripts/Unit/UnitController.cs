using System;
using System.Collections.Generic;
using UnityEngine;

public class UnitController : MonoBehaviour
{
    public List<UnitMove> selectedUnits = new List<UnitMove>();
    private Vector3 startMousePos;
    public LayerMask unitLayer;
    public LayerMask groundLayer;

    private bool isDragging;

    //드래그 사각형을 그릴 때 사용할 Texture
    private static Texture2D whiteTex;

    private void Awake()
    {
        whiteTex = new Texture2D(1, 1);
        whiteTex.SetPixel(0, 0, Color.white);
        whiteTex.Apply();
    }

    void Update()
    {
        //좌클릭 시작
        if (Input.GetMouseButtonDown(0))
        {
            startMousePos = Input.mousePosition;
            isDragging = true;
        }
        //좌클릭 종료
        if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
            SelectUnits();
        }
        //우클릭 이동
        if (Input.GetMouseButtonDown(1) && selectedUnits.Count > 0)
        {
            MoveUnits();
        }
    }

    private void SelectUnits()
    {
        //드래그 거리 짧으면 단일 선택
        Vector3 endMousePos = Input.mousePosition;
        Rect selectionRect = GetScreenRect(startMousePos, endMousePos);

        //기존 선택 해제
        foreach (var unit in selectedUnits)
        {
            unit.Selected(false);
        }
        selectedUnits.Clear();

        //유닛 선택
        UnitMove[] allUnits = FindObjectsOfType<UnitMove>();
        foreach (var unit in allUnits)
        {
            Vector3 unitScreenPos = Camera.main.WorldToScreenPoint(unit.transform.position);

            //유닛 좌표도 GUI 기준으로 변환
            unitScreenPos.y = Screen.height - unitScreenPos.y;
            if (selectionRect.Contains(unitScreenPos))
            {
                selectedUnits.Add(unit);
                unit.Selected(true);
            }
        }
    }

    private void MoveUnits()
    {
        //목적지 설정
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hitInfo, 100f, groundLayer))
        {
            //유닛 이동
            Vector3 targetPos = hitInfo.point;
            foreach (var unit in selectedUnits)
            {
                unit.MoveTo(targetPos);
            }
        }
    }

    //스크린 좌표로 사각형 생성
    private Rect GetScreenRect(Vector3 screenPos1, Vector3 screenPos2)
    {
        //Y좌표 반전
        screenPos1.y = Screen.height - screenPos1.y;
        screenPos2.y = Screen.height - screenPos2.y;

        //좌표 정리
        float xMin = Mathf.Min(screenPos1.x, screenPos2.x);
        float yMin = Mathf.Min(screenPos1.y, screenPos2.y);
        float width = Mathf.Abs(screenPos1.x - screenPos2.x);
        float height = Mathf.Abs(screenPos1.y - screenPos2.y);

        return new Rect(xMin, yMin, width, height);
    }

    private void OnGUI()
    {
        if (!isDragging) return;

        if (isDragging)
        {
            //드래그 사각형 그리기
            Rect rect = GetScreenRect(startMousePos, Input.mousePosition);
            DrawScreenRect(rect, new Color(0, 1, 0, 0.25f));
            DrawScreenRectBorder(rect, 2, Color.green);
        }
    }

    private void DrawScreenRect(Rect rect, Color color)
    {
        GUI.color = color;
        GUI.DrawTexture(rect, whiteTex);
        GUI.color = Color.white;
    }

    private void DrawScreenRectBorder(Rect rect, float thickness, Color color)
    {
        //Top
        DrawScreenRect(new Rect(rect.xMin, rect.yMin, rect.width, thickness), color);
        //Left
        DrawScreenRect(new Rect(rect.xMin, rect.yMin, thickness, rect.height), color);
        //Right
        DrawScreenRect(new Rect(rect.xMax - thickness, rect.yMin, thickness, rect.height), color);
        //Down
        DrawScreenRect(new Rect(rect.xMin, rect.yMax - thickness, rect.width, thickness), color);
    }
}
