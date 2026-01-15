using System.Collections.Generic;
using UnityEngine;

public class UnitResultPanel : MonoBehaviour
{
    [SerializeField] private Transform resultTransform;
    [SerializeField] private UnitResultUI resultPrefab;

    //뽑기용
    public void AddResult(UnitData data, string description, float unitChance)
    {
        UnitResultUI ui = Instantiate(resultPrefab, resultTransform);
        ui.Show(data, description, unitChance);
    }

    //합성용
    public void AddSynthesisResult(string description)
    {
        UnitResultUI ui = Instantiate(resultPrefab, resultTransform);
        ui.ShowSynthesis(description);
    }
}
