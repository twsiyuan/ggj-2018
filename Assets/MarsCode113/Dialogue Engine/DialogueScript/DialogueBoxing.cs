using System;
using System.Collections.Generic;
using UnityEngine;

namespace MarsCode113.DialogueFramework
{
    [Serializable]
    public class DialogueBoxing
    {

        [SerializeField]
        private string index;
        public string Index
        {
            get { return index; }
        }

        [SerializeField]
        private List<DialogueNode> nodes;
        public List<DialogueNode> Nodes
        {
            get { return nodes; }
        }


        public DialogueBoxing(string index)
        {
            this.index = index;
            nodes = new List<DialogueNode>();
        }


        public void AddNode(DialogueNode node)
        {
            nodes.Add(node);
        }

    }
}