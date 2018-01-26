using System;
using UnityEngine;

namespace MarsCode113.DialogueFramework
{
    [SystemTag("Dialogue")]
    public interface IDialogueEngine
    {

        Vector2 CamZone { get; }

        float DefaultValue { get; }

        float BottomHeight { get; }


        #region [ Basic ]

        void RegisterSystem(IDialogueSystem system);


        void RemoveSystem(IDialogueSystem system);


        T GetSystem<T>() where T : class, IDialogueSystem;

        #endregion


        #region [ Core ]

        void Launch(DialogueScript script);


        void SetQuitAction(Action quit);


        void Ready(float delay = 0);


        void Read();


        void Quit();

        #endregion

    }
}