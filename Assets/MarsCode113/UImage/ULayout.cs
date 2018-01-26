namespace MarsCode113.UImage
{
    using System.Collections.Generic;
    using UnityEngine;

    public class ULayout : MonoBehaviour
    {

        [SerializeField] Texture2D atlas;

        [SerializeField] List<ULayoutElement> layoutElements = new List<ULayoutElement>();


#if UNITY_EDITOR
        [SerializeField] bool layoutListFoldout;

        public Texture2D Atlas
        {
            get { return atlas; }
        }


        public List<ULayoutElement> LayoutElements
        {
            get { return layoutElements; }
            set { layoutElements = value; }
        }


        public void ClearGameObjects()
        {
            foreach(var e in layoutElements) {
                if(e.Go != null) {
                    DestroyImmediate(e.Go);
                }
            }
        }


        public void ClearAllLayoutElements()
        {
            foreach(var e in layoutElements) {
                if(e.Go != null) {
                    DestroyImmediate(e.Go);
                }
            }

            layoutElements = new List<ULayoutElement>();
        }


        public void ClearInvalidElements(List<Sprite> sprites)
        {
            foreach(var e in layoutElements.ToArray()) {
                if(IsInvalidElement(e, sprites)) {
                    DestroyImmediate(e.Go);
                    layoutElements.Remove(e);
                }
            }
        }


        bool IsInvalidElement(ULayoutElement element, List<Sprite> atlasSprites)
        {
            if(element.Go == null || element.Sprite == null)
                return true;

            foreach(var s in atlasSprites) {
                if(element.Sprite == s)
                    return false;
            }

            return true;
        }
#endif

    }
}