using System;

namespace MarsCode113.DialogueFramework
{
    [Serializable]
    public class DialogueNode
    {

        public string Tag;

        public string Target;

        public string[] Arguments;

        public float Delay;

        public string Text;

        public int NodeNumber;

    }
}