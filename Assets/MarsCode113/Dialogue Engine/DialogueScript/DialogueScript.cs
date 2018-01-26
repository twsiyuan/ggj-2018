using System;
using System.Collections.Generic;
using ListExtension.ElementControl;
using UnityEngine;

namespace MarsCode113.DialogueFramework
{
    [Serializable]
    public class DialogueScript
    {

        [SerializeField]
        private List<DialogueNode> nodes;

        [SerializeField]
        private List<DialogueBoxing> boxings;


        public DialogueScript(List<DialogueNode> nodes, List<DialogueBoxing> boxings)
        {
            this.nodes = nodes;
            this.boxings = boxings;
        }


        public DialogueNode GetFirstNodeAndDequeue()
        {
            if(nodes.Count == 0)
                return null;
            else
                return nodes.Dequeue();
        }


        public void InsertBoxingContents(string index)
        {
            foreach(var boxing in boxings) {
                if(boxing.Index == index) {
                    nodes.InsertRange(0, boxing.Nodes);
                    return;
                }
            }

            throw new NullReferenceException(string.Format("Cannot find target boxing : ", index));
        }

    }
}