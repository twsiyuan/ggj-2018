using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace MarsCode113
{
    [InitializeOnLoad]
    public class EditorTools : Editor
    {

        private static Dictionary<string, GUIStyle> styles = new Dictionary<string, GUIStyle>();

        private static Dictionary<Color, Texture2D> colorTextures = new Dictionary<Color, Texture2D>();

        private static Dictionary<string, Texture2D> tiledTextures = new Dictionary<string, Texture2D>();


        [MenuItem("MarsCode113/EditorTools/Log Style Count", priority = 1000)]
        private static void ShowStyleSize()
        {
            Debug.Log("Styles Size: " + styles.Count);
        }


        [MenuItem("MarsCode113/EditorTools/Log ColorTexture Count", priority = 1001)]
        private static void ShowCurrentColorTextureSize()
        {
            Debug.Log("Color Textures Size: " + colorTextures.Count);
        }


        /// <summary>
        /// Draw a header in vertical group with box style.
        /// </summary>
        public static void DrawHeaderLable(string text, Color bgColor, TextAnchor anchor = TextAnchor.MiddleCenter)
        {
            if(!styles.ContainsKey("Header")) {
                styles.Add("Header", new GUIStyle(EditorStyles.whiteBoldLabel));
            }

            if(!styles.ContainsKey("HeaderBox")) {
                styles.Add("HeaderBox", new GUIStyle());
            }

            var header = styles["Header"];
            header.fontSize = 12;
            header.alignment = anchor;

            var box = styles["HeaderBox"];
            box.normal.background = GetColorTexture(bgColor);

            EditorGUILayout.BeginHorizontal(box);
            {
                GUILayout.Label(text, header, GUILayout.Height(16));
            }
            EditorGUILayout.EndHorizontal();
        }


        /// <summary>
        /// 
        /// </summary>
        public static void DrawSubTitleLable(string text, Color color, TextAnchor anchor = TextAnchor.MiddleLeft, int fontSize = 12, int height = 12)
        {
            if(!styles.ContainsKey("SubTitle")) {
                var newStyle = new GUIStyle(EditorStyles.whiteBoldLabel);
                newStyle.normal.textColor = new Color32(200, 200, 200, 255);
                styles.Add("SubTitle", newStyle);
            }

            var style = styles["SubTitle"];
            style.normal.background = GetColorTexture(color);
            style.fontSize = fontSize;
            style.alignment = anchor;

            GUILayout.Label(text, style, GUILayout.Height(height));
        }


        /// <summary>
        /// Return 1x1 pixel texture.
        /// </summary>
        public static Texture2D GetColorTexture(Color color)
        {
            if(!colorTextures.ContainsKey(color))
                GenerateColorTexture(color);

            return colorTextures[color];
        }


        private static void GenerateColorTexture(Color color)
        {
            var output = new Texture2D(1, 1);
            var pixel = new Color[] { color };
            output.SetPixels(pixel);
            output.Apply();

            colorTextures.Add(color, output);
        }


        public static Texture2D GetTiledTexture(string key, int size, Color col1, Color col2)
        {
            if(!tiledTextures.ContainsKey(key))
                GenerateTiledTexture(key, size, col1, col2);

            return tiledTextures[key];
        }


        private static void GenerateTiledTexture(string key, int size, Color col1, Color col2)
        {
            var t = new Texture2D(size * 2, size * 2);
            t.hideFlags = HideFlags.DontSave;

            for(int x = 0; x < size; x++) {
                for(int y = 0; y < size; y++)
                    t.SetPixel(x, y, col1);

                for(int y = size; y < size * 2; y++)
                    t.SetPixel(x, y, col2);
            }

            for(int x = size; x < size * 2; x++) {
                for(int y = 0; y < size; y++)
                    t.SetPixel(x, y, col2);

                for(int y = size; y < size * 2; y++)
                    t.SetPixel(x, y, col1);
            }

            t.Apply();
            t.filterMode = FilterMode.Point;

            tiledTextures.Add(key, t);
        }


        private static GUIStyle GetGUIStyle(string key)
        {
            if(!styles.ContainsKey(key)) {
                styles.Add(key, new GUIStyle());
            }

            return styles[key];
        }

    }
}