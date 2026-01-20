using UnityEngine;
using UnityEngine.UI;

public class UnitSlot : MonoBehaviour
{
    [SerializeField] private Image frameImage;
    [SerializeField] private Image iconImage;

    public void Set(UnitData data)
    {
        if (data == null)
        {
            Clear();
            return;
        }

        iconImage.sprite = data.icon;
        iconImage.color = Color.white;

        frameImage.sprite = data.frameSprite;
        frameImage.color = data.frameColor;
        frameImage.enabled = true;
    }

    public void Clear()
    {
        iconImage.sprite = null;
        iconImage.color = new Color(1, 1, 1, 0);

        frameImage.sprite = null;
        frameImage.enabled = false;
    }
}
