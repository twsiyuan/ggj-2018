namespace MarsCode113.UImage
{
    using System;
    using UnityEngine;

    [Serializable]
    public class ULayoutElement
    {
        [SerializeField] GameObject go;

        [SerializeField] Sprite sprite;

        public GameObject Go
        {
            get { return go; }
        }

        public Sprite Sprite
        {
            get { return sprite; }
        }
    }
}