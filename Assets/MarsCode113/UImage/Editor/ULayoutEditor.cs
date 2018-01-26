namespace MarsCode113.UImage
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using MarsCode113;
    using UnityEditor;
    using UnityEditorInternal;
    using UnityEngine;
    using UnityEngine.UI;

    [CustomEditor(typeof(ULayout)), CanEditMultipleObjects]
    public class ULayoutEditor : Editor
    {

        ULayout script;

        Color32 headerColor = new Color32(92, 208, 255, 255);

        Color32 removeButtonColor = new Color32(255, 147, 251, 255);

        SerializedProperty atlas;

        SerializedProperty layoutElements;

        SerializedProperty layoutListFoldout;

        ReorderableList layoutList;

        Sprite focusSprite;

        Vector4 focusSpriteBorder;

        ULayoutPreview preview = new ULayoutPreview();


        #region Initialize Section

        void OnEnable()
        {
            script = target as ULayout;
            atlas = serializedObject.FindProperty("atlas");

            layoutElements = serializedObject.FindProperty("layoutElements");
            layoutList = new ReorderableList(serializedObject, layoutElements, false, false, false, true) {
                drawElementCallback = DrawElementCallback,
                onSelectCallback = OnSelectCallback
            };

            layoutListFoldout = serializedObject.FindProperty("layoutListFoldout");
        }


        void DrawElementCallback(Rect rect, int arrayID, bool isActive, bool isFocused)
        {
            GUI.enabled = false;

            var element = layoutList.serializedProperty.GetArrayElementAtIndex(arrayID);
            rect.y += 2;

            EditorGUIUtility.labelWidth = 25;

            EditorGUI.PropertyField(
                new Rect(rect.x, rect.y, 60, EditorGUIUtility.singleLineHeight),
                element.FindPropertyRelative("go"),
                new GUIContent((arrayID + 1) + ".")
            );

            EditorGUI.PropertyField(
                new Rect(rect.x + 70, rect.y, rect.width - 70, EditorGUIUtility.singleLineHeight),
                element.FindPropertyRelative("sprite"),
                GUIContent.none
            );

            GUI.enabled = true;
        }


        void OnSelectCallback(ReorderableList l)
        {
            var e = l.serializedProperty.GetArrayElementAtIndex(l.index);
            var spr = e.FindPropertyRelative("sprite").objectReferenceValue as Sprite;
            focusSprite = spr;
            focusSpriteBorder = spr.border;
        }

        #endregion


        #region Basic Section

        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            serializedObject.Update();

            DrawBasicSection();

            GUILayout.Space(5);

            if(HasAssignedAtlas())
                DrawLayoutElementsSection();

            serializedObject.ApplyModifiedProperties();

            DrawRemoveComponentUI();
        }


        void DrawBasicSection()
        {
            GUILayout.Space(5);

            DrawHeaderUI();

            DrawAtlasPropertyUI();

            DrawOprationUI();

            GUILayout.Space(5);
        }


        void DrawHeaderUI()
        {
            EditorTools.DrawHeaderLable("ULayout", headerColor);
        }


        void DrawAtlasPropertyUI()
        {
            EditorGUILayout.BeginHorizontal();
            {
                EditorGUILayout.LabelField("Atlas", GUILayout.Width(65));
                EditorGUILayout.PropertyField(atlas, new GUIContent(""));
            }
            EditorGUILayout.EndHorizontal();
        }


        void DrawOprationUI()
        {
            if(HasAssignedAtlas()) {
                if(GUILayout.Button("Build"))
                    BuildLayout();

                if(HasLayoutElements()) {
                    if(GUILayout.Button("Clear"))
                        ClearLayout();

                    if(GUILayout.Button("Update"))
                        UpdateLayout();
                }
            }
            else EditorGUILayout.HelpBox("Must assign an atlas.", MessageType.Warning);
        }


        void DrawRemoveComponentUI()
        {
            GUI.color = removeButtonColor;

            if(GUILayout.Button("Remove Component")) {
                DestroyImmediate(script);
            }

            GUI.color = Color.white;
        }

        #endregion


        #region Core Section

        void BuildLayout()
        {
            var parentCanvas = script.transform.GetComponentInParent<Canvas>();
            if(parentCanvas == null)
                throw new Exception("Must put this game object under a canvas");

            InitTargetRectTransform();

            script.ClearAllLayoutElements();
            serializedObject.Update();

            var sprites = GetSpritesWithAtlas();
            var metaDataPool = GetMetaDataWithAtlas();

            foreach(var sprite in sprites) {
                var elementAnchorPos = TakeOutAnchorPosFromPool(sprite, ref metaDataPool);
                var go = GenerateElement(sprite, elementAnchorPos);
                Undo.RegisterCreatedObjectUndo(go, "ULayout - Build");
            }
        }


        void ClearLayout()
        {
            foreach(var e in script.LayoutElements) {
                if(e.Go != null)
                    Undo.DestroyObjectImmediate(e.Go);
            }

            Undo.RecordObject(script, "ULayout - Clear");
            script.ClearAllLayoutElements();
            serializedObject.Update();
        }


        void UpdateLayout()
        {
            var sprites = GetSpritesWithAtlas();
            var metaDataPool = GetMetaDataWithAtlas();

            script.ClearInvalidElements(sprites);
            serializedObject.Update();

            foreach(var sprite in sprites) {
                var elementAnchorPos = TakeOutAnchorPosFromPool(sprite, ref metaDataPool);
                var img = GetMatchedElement(sprite);
                if(img == null)
                    GenerateElement(sprite, elementAnchorPos);
                else
                    UpdateElementState(img);
            }
        }


        void InitTargetRectTransform()
        {
            script.name = atlas.objectReferenceValue.name;

            var rect = script.GetComponent<RectTransform>();
            if(rect == null)
                rect = script.gameObject.AddComponent<RectTransform>();

            rect.anchoredPosition3D = Vector3.zero;
            rect.localScale = Vector3.one;

            rect.sizeDelta = Vector2.zero;
            rect.anchorMin = Vector2.zero;
            rect.anchorMax = Vector2.one;
        }


        GameObject GenerateElement(Sprite sprite, Vector2 elementAnchorPos)
        {
            var img = GenerateLayoutElementAndReturn(sprite);
            UpdateElementState(img, elementAnchorPos);
            AddLayoutElement(img);
            return img.gameObject;
        }


        Image GenerateLayoutElementAndReturn(Sprite sprite)
        {
            var go = new GameObject(sprite.name, typeof(Image));
            GameObjectUtility.SetParentAndAlign(go, script.gameObject);

            var img = go.GetComponent<Image>();
            img.sprite = sprite;

            return img;
        }


        void UpdateElementState(Image img, Vector2 anchorPos)
        {
            UpdateElementState(img);
            img.rectTransform.anchoredPosition = anchorPos;
        }


        void UpdateElementState(Image img)
        {
            img.SetNativeSize();
        }


        void AddLayoutElement(Image img)
        {
            var arrayID = layoutElements.arraySize;
            layoutElements.arraySize = arrayID + 1;
            layoutElements.GetArrayElementAtIndex(arrayID).FindPropertyRelative("go").objectReferenceValue = img.gameObject;
            layoutElements.GetArrayElementAtIndex(arrayID).FindPropertyRelative("sprite").objectReferenceValue = img.sprite;
        }


        string GetAtlasAssetPath()
        {
            return AssetDatabase.GetAssetPath(atlas.objectReferenceValue);
        }


        List<Sprite> GetSpritesWithAtlas()
        {
            return AssetDatabase.LoadAllAssetsAtPath(GetAtlasAssetPath()).OfType<Sprite>().ToList<Sprite>();
        }


        List<SpriteMetaData> GetMetaDataWithAtlas()
        {
            var ti = AssetImporter.GetAtPath(GetAtlasAssetPath()) as TextureImporter;
            return ti.spritesheet.ToList<SpriteMetaData>();
        }


        Vector2 TakeOutAnchorPosFromPool(Sprite spr, ref List<SpriteMetaData> metaDataPool)
        {
            foreach(var data in metaDataPool) {
                if(spr.name == data.name) {
                    var pos = GetAnchorPosWithMetaData(data);
                    metaDataPool.Remove(data);
                    return pos;
                }
            }

            throw new Exception("No matched meta data: " + spr.name);
        }


        Vector2 GetAnchorPosWithMetaData(SpriteMetaData metaData)
        {
            var width = metaData.rect.width;
            var height = metaData.rect.height;
            var posX = -((width * metaData.pivot.x) - (width * 0.5f));
            var posY = -((height * metaData.pivot.y) - (height * 0.5f));

            return new Vector2(Mathf.RoundToInt(posX), Mathf.RoundToInt(posY));
        }


        Image GetMatchedElement(Sprite sprite)
        {
            foreach(var e in script.LayoutElements) {
                if(e.Sprite == sprite) {
                    return e.Go.GetComponent<Image>();
                }
            }

            return null;
        }


        bool HasAssignedAtlas()
        {
            return script.Atlas != null;
        }


        bool HasLayoutElements()
        {
            return layoutElements.arraySize > 0;
        }

        #endregion


        #region Layout Elements Section  

        void DrawLayoutElementsSection()
        {
            if(!HasLayoutElements())
                return;

            DrawLayoutFoldoutUI();

            if(!layoutListFoldout.boolValue)
                return;

            EditorGUILayout.BeginVertical("Box");
            {
                layoutList.DoLayoutList();

                DrawSpriteBorderEditorUI();
            }
            EditorGUILayout.EndVertical();

            if(!OnLayoutElementSelected())
                return;

        }


        void DrawLayoutFoldoutUI()
        {
            EditorGUILayout.BeginHorizontal();
            {
                layoutListFoldout.boolValue = EditorGUILayout.Foldout(layoutListFoldout.boolValue, "Layout Elements");

                if(!OnLayoutElementSelected()) {
                    GUI.enabled = false;
                    DrawUnselectLayoutElementUI();
                    GUI.enabled = true;
                }
                else DrawUnselectLayoutElementUI();
            }
            EditorGUILayout.EndHorizontal();
        }


        void DrawUnselectLayoutElementUI()
        {
            if(GUILayout.Button(new GUIContent("Unselect"), EditorStyles.miniButton, GUILayout.Width(85)))
                layoutList.index = -1;
        }


        bool OnLayoutElementSelected()
        {
            return layoutList.index > -1;
        }

        #endregion


        #region Border Editor Section

        void DrawSpriteBorderEditorUI()
        {
            if(!OnLayoutElementSelected())
                return;

            EditorGUILayout.BeginVertical("Box");
            {
                EditorGUILayout.LabelField("Border", GUILayout.Width(60));

                EditorGUILayout.BeginHorizontal();
                {
                    DrawBorderHorizontalParams();

                    DrawBorderVerticalParams();
                }
                EditorGUILayout.EndHorizontal();
            }
            EditorGUILayout.EndVertical();
        }


        void DrawBorderHorizontalParams()
        {
            EditorGUILayout.BeginVertical();
            {
                GUI.SetNextControlName("Border_L");
                focusSpriteBorder.x = EditorGUILayout.IntField("L", (int)focusSpriteBorder.x);

                GUI.SetNextControlName("Border_R");
                focusSpriteBorder.z = EditorGUILayout.IntField("R", (int)focusSpriteBorder.z);
            }
            EditorGUILayout.EndVertical();

            SyncBorderHorizontalParams();
        }


        void SyncBorderHorizontalParams()
        {
            focusSpriteBorder.x = Mathf.Clamp(focusSpriteBorder.x, 0, focusSprite.rect.width);
            focusSpriteBorder.z = Mathf.Clamp(focusSpriteBorder.z, 0, focusSprite.rect.width);

            if(focusSpriteBorder.x + focusSpriteBorder.z > focusSprite.rect.width) {
                if(GUI.GetNameOfFocusedControl() == "Border_L") {
                    focusSpriteBorder.z = focusSprite.rect.width - focusSpriteBorder.x;
                }
                else if(GUI.GetNameOfFocusedControl() == "Border_R") {
                    focusSpriteBorder.x = focusSprite.rect.width - focusSpriteBorder.z;
                }
                else {
                    focusSpriteBorder.x = focusSprite.border.x;
                    focusSpriteBorder.z = focusSprite.border.z;
                }
            }
        }


        void DrawBorderVerticalParams()
        {
            EditorGUILayout.BeginVertical();
            {
                GUI.SetNextControlName("Border_T");
                focusSpriteBorder.w = EditorGUILayout.IntField("T", (int)focusSpriteBorder.w);

                GUI.SetNextControlName("Border_B");
                focusSpriteBorder.y = EditorGUILayout.IntField("B", (int)focusSpriteBorder.y);
            }
            EditorGUILayout.EndVertical();

            SyncBorderVerticalParams();
        }


        void SyncBorderVerticalParams()
        {
            focusSpriteBorder.w = Mathf.Clamp(focusSpriteBorder.w, 0, focusSprite.rect.height);
            focusSpriteBorder.y = Mathf.Clamp(focusSpriteBorder.y, 0, focusSprite.rect.height);

            if(focusSpriteBorder.y + focusSpriteBorder.w > focusSprite.rect.height) {
                if(GUI.GetNameOfFocusedControl() == "Border_B") {
                    focusSpriteBorder.w = focusSprite.rect.height - focusSpriteBorder.y;
                }
                else if(GUI.GetNameOfFocusedControl() == "Border_T") {
                    focusSpriteBorder.y = focusSprite.rect.height - focusSpriteBorder.w;
                }
                else {
                    focusSpriteBorder.y = focusSprite.border.y;
                    focusSpriteBorder.w = focusSprite.border.w;
                }
            }
        }


        void ModifySpriteBorder(Sprite spr, Vector4 newBorder)
        {
            var ti = AssetImporter.GetAtPath(GetAtlasAssetPath()) as TextureImporter;
            var metaData = ti.spritesheet;

            for(int i = 0; i < metaData.Length; i++) {
                if(spr.name == metaData[i].name) {
                    metaData[i].border = newBorder;
                    ti.spritesheet = metaData.ToArray();
                    ti.SaveAndReimport();
                    AssetDatabase.ImportAsset(GetAtlasAssetPath(), ImportAssetOptions.ForceUpdate);
                    break;
                }
            }
        }

        #endregion


        #region Preview Section

        public override void OnPreviewGUI(Rect rect, GUIStyle background)
        {
            if(!OnLayoutElementSelected())
                preview.PreviewAtlas(rect, script.Atlas);
            else
                preview.PreviewSprite(rect, focusSprite, focusSpriteBorder, HasChangedBorderValues());
        }


        public override bool HasPreviewGUI()
        {
            return HasAssignedAtlas();
        }


        bool HasChangedBorderValues()
        {
            return focusSprite.border != focusSpriteBorder;
        }

        #endregion

    }
}