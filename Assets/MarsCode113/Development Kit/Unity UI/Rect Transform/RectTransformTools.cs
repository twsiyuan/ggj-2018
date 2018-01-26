using UnityEngine;
using UnityEngine.UI;

public static class RectTransformTools
{

    /// <summary>
    /// 
    /// </summary>
    public static void AnchorAlignment(AlignPoint state, RectTransform input)
    {
        SetAnchor(state, input);
    }


    /// <summary>
    /// 維持AnchorPosition的數值進行對齊
    /// </summary>
    /// <para>pivot狀態將會被改變</para>
    /// <para>輸入的對象必須以raw edit mode執行</para>
    static void SetAnchor(AlignPoint state, RectTransform input)
    {
        Rect rect = input.rect;
        var pivot = input.pivot;

        switch(state)
        {
            case AlignPoint.Center:
                input.anchorMin = input.anchorMax = input.pivot = new Vector2(0.5f, 0.5f);
                break;

            case AlignPoint.Top:
                input.anchorMin = input.anchorMax = input.pivot = new Vector2(0.5f, 1);
                break;

            case AlignPoint.Bottom:
                input.anchorMin = input.anchorMax = input.pivot = new Vector2(0.5f, 0);
                break;

            case AlignPoint.Left:
                input.anchorMin = input.anchorMax = input.pivot = new Vector2(0, 0.5f);
                break;

            case AlignPoint.Right:
                input.anchorMin = input.anchorMax = input.pivot = new Vector2(1, 0.5f);
                break;

            case AlignPoint.TL:
                input.anchorMin = input.anchorMax = input.pivot = new Vector2(0, 1);
                break;

            case AlignPoint.TR:
                input.anchorMin = input.anchorMax = input.pivot = new Vector2(1, 1);
                break;

            case AlignPoint.BL:
                input.anchorMin = input.anchorMax = input.pivot = new Vector2(0, 0);
                break;

            case AlignPoint.BR:
                input.anchorMin = input.anchorMax = input.pivot = new Vector2(1, 0);
                break;
        }

        input.anchoredPosition3D = Vector3.zero;
    }


    /// <summary>
    /// 依據Sprite的pivot變更Image
    /// </summary>
    /// <param name="input"></param>
    /// <param name="setNetiveSize"></param>
    public static void SetImagePivotOffset(Image input, bool setNetiveSize = false)
    {
        Sprite spr = input.sprite;

        if(spr == null)
        {
            return;
        }

        if(setNetiveSize)
        {
            input.SetNativeSize();
        }

        float x = spr.pivot.x / spr.textureRect.width;
        float y = spr.pivot.y / spr.textureRect.height;

        input.rectTransform.pivot = new Vector2(x, y);
    }


    /// <summary>
    /// 拉伸 RectTransfrom
    /// </summary>
    public static void Stretch(StretchType type, RectTransform input)
    {
        switch(type)
        {
            case StretchType.StretchHorizontal:
                {
                    input.anchorMin = new Vector2(0, 0.5f);
                    input.anchorMax = new Vector2(1, 0.5f);
                    input.pivot = new Vector2(0.5f, 0.5f);
                    input.offsetMin = new Vector2(0, input.offsetMin.y);
                    input.offsetMax = new Vector2(0, input.offsetMax.y);
                }
                return;

            case StretchType.StretchVertical:
                {
                    input.anchorMin = new Vector2(0.5f, 0);
                    input.anchorMax = new Vector2(0.5f, 1);
                    input.pivot = new Vector2(0.5f, 0.5f);
                    input.offsetMin = new Vector2(input.offsetMin.x, 0);
                    input.offsetMax = new Vector2(input.offsetMax.x, 0);
                }
                return;

            case StretchType.StretchAll:
                {
                    input.anchorMin = new Vector2(0, 0);
                    input.anchorMax = new Vector2(1, 1);
                    input.pivot = new Vector2(0.5f, 0.5f);
                    input.offsetMin = Vector2.zero;
                    input.offsetMax = Vector2.zero;
                }
                return;
        }
    }

}


