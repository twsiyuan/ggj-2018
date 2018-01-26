using UnityEngine;

namespace MarsCode113.DialogueFramework
{
    public abstract class StickerFX_Base : MonoBehaviour, IStickerFX
    {

        [SerializeField]
        protected Vector3 offset;

        public string Index {
            get; set;
        }


        public virtual void Play(Transform target)
        {
            transform.SetParent(target.parent);
            transform.localPosition = target.localPosition + offset;
            gameObject.SetActive(true);
        }


        public virtual void Stop(Transform pool)
        {
            gameObject.SetActive(false);
            transform.SetParent(pool);
            transform.localPosition = Vector3.zero;
        }

    }
}