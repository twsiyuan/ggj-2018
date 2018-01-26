using UnityEngine;

namespace MarsCode113.DialogueFramework
{
    public abstract class DialogueSystem_Base : MonoBehaviour, IDialogueSystem
    {

        protected IDialogueEngine engine;


        protected virtual void OnEnable()
        {
            if(engine == null)
                engine = DialogueEngine.Instance;

            engine.RegisterSystem(this);
        }


        protected virtual void OnDisable()
        {
            engine.RemoveSystem(this);
        }


        public abstract void Launch(DialogueNode node);

    }
}