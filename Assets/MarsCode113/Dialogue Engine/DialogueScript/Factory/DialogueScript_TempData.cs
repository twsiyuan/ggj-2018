using System;
using System.Collections.Generic;
using UnityEngine;

namespace MarsCode113.DialogueFramework
{
    [Serializable]
    public class DialogueScript_TempData
    {

        [SerializeField]
        private List<DialogueNode> nodes = new List<DialogueNode>();

        [SerializeField]
        private List<DialogueBoxing> boxings = new List<DialogueBoxing>();


        public List<DialogueNode> Nodes
        {
            get { return nodes; }
        }


        public List<DialogueBoxing> Boxings
        {
            get { return boxings; }
            set { boxings = value; }
        }

    }
}