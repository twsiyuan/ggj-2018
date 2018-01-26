using MarsCode113;
using UnityEditor;
using UnityEngine;

namespace MarsCode113.UImage
{
    public class ULayoutPreview
    {
        Color32 tiledColor1 = new Color32(255, 255, 255, 255);

        Color32 tiledColor2 = new Color32(200, 200, 200, 255);

        Color32 borderLine1 = new Color32(0, 250, 255, 200);

        Color32 borderLine2 = new Color32(255, 75, 255, 255);


        public void PreviewAtlas(Rect view, Texture2D atlas)
        {
            var displayRect = new Rect(view.x, view.y, atlas.width, atlas.height);
            var uv = new Rect(0, 0, 1, 1);

            DrawPreviewImage(ref displayRect, view, atlas, uv);
            DrawPreviewShaderLabel(view, atlas.name, atlas.width, atlas.height);
        }


        public void PreviewSprite(Rect rect, Sprite sprite, Vector4 border, bool hasValueChanged)
        {
            var view = new Rect(rect.x, rect.y, sprite.rect.width, sprite.rect.height);
            var uv = new Rect(
                sprite.rect.x / sprite.texture.width,
                sprite.rect.y / sprite.texture.height,
                sprite.rect.width / sprite.texture.width,
                sprite.rect.height / sprite.texture.height
            );

            var borderText = EditorTools.GetColorTexture(hasValueChanged? borderLine1 : borderLine2);

            DrawPreviewImage(ref view, rect, sprite.texture, uv);
            DrawSpriteBorder(view, sprite, border, borderText);
            DrawPreviewShaderLabel(rect, sprite.name, Mathf.RoundToInt(sprite.rect.width), Mathf.RoundToInt(sprite.rect.height));
        }


        void DrawPreviewImage(ref Rect view, Rect rect, Texture2D displayTexture, Rect uv)
        {
            var scale = rect.width / view.width;
            view.width *= scale;
            view.height *= scale;

            if(rect.height > view.height) {
                view.y += (rect.height - view.height) * 0.5f;
            }
            else if(view.height > rect.height) {
                float fHeight = rect.height / view.height;
                view.width *= fHeight;
                view.height *= fHeight;
            }

            if(rect.width > view.width)
                view.x += (rect.width - view.width) * 0.5f;

            DrawTiledBG(view);

            GUI.DrawTextureWithTexCoords(view, displayTexture, uv, true);
        }


        void DrawTiledBG(Rect displayRect)
        {
            var width = Mathf.RoundToInt(displayRect.width);
            var height = Mathf.RoundToInt(displayRect.height);
            var tiledBG = EditorTools.GetTiledTexture("ULayoutPreviewBG", 16, tiledColor1, tiledColor2);

            GUI.BeginGroup(displayRect);
            {
                for(int y = 0; y < height; y += tiledBG.height) {
                    for(int x = 0; x < width; x += tiledBG.width) {
                        GUI.DrawTexture(new Rect(x, y, tiledBG.width, tiledBG.height), tiledBG);
                    }
                }
            }
            GUI.EndGroup();
        }


        void DrawSpriteBorder(Rect inspectorPreviewRect, Sprite displaySprite, Vector4 spriteBorder, Texture2D borderLineTexture)
        {
            var borderLeft = spriteBorder.x;
            var borderRight = spriteBorder.z;
            var borderBottom = spriteBorder.y;
            var borderTop = spriteBorder.w;

            GUI.BeginGroup(inspectorPreviewRect);
            {
                if(borderLeft > 0) {
                    var xLeft = inspectorPreviewRect.width * (borderLeft / displaySprite.rect.width);
                    GUI.DrawTexture(new Rect(xLeft, 0, 1, inspectorPreviewRect.height), borderLineTexture);
                }

                if(borderRight > 0) {
                    var xRight = inspectorPreviewRect.width * ((displaySprite.rect.width - borderRight) / displaySprite.rect.width);
                    GUI.DrawTexture(new Rect(xRight, 0, 1, inspectorPreviewRect.height), borderLineTexture);
                }

                if(borderBottom > 0) {
                    var yBottom = inspectorPreviewRect.height * ((displaySprite.rect.height - borderBottom) / displaySprite.rect.height);
                    GUI.DrawTexture(new Rect(0, yBottom, inspectorPreviewRect.width, 1), borderLineTexture);
                }

                if(borderTop > 0) {
                    var yTop = inspectorPreviewRect.height * (borderTop / displaySprite.rect.height);
                    GUI.DrawTexture(new Rect(0, yTop, inspectorPreviewRect.width, 1), borderLineTexture);
                }
            }
            GUI.EndGroup();
        }


        void DrawPreviewShaderLabel(Rect previewRect, string name, int width, int height)
        {
            var r = new Rect(previewRect.x, previewRect.y - 5, previewRect.width, previewRect.height);
            var content = string.Format("{0}\r\nImage Size: {1}x{2}", name, width, height);
            EditorGUI.DropShadowLabel(r, content);
        }

    }
}