using UnityEngine;

namespace MarsCode113.DialogueFramework
{
    public interface IStickerFX
    {

        string Index { get; set; }


        void Play(Transform target);


        void Stop(Transform target);

    }
}