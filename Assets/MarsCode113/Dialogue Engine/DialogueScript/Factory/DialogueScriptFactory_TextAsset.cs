using System;
using UnityEngine;

namespace MarsCode113.DialogueFramework
{
    [Serializable]
    public class DialogueScriptFactory_TextAsset : IDialogueScriptFactory
    {

        [SerializeField]
        private DialogueScript_TempData temp = new DialogueScript_TempData();

        private int nodeCount;

        private int pointer;


        public DialogueScript BuildScript(object source = null)
        {
            var script = ConvertTextAsset((string)source);

            Clean();

            return script;
        }


        private DialogueScript ConvertTextAsset(string json)
        {
            temp = JsonUtility.FromJson<DialogueScript_TempData>(json);
            nodeCount = temp.Nodes.Count;

            for(pointer = 0; pointer < nodeCount; pointer++) {
                if(temp.Nodes[pointer].Tag == "Boxing")
                    FormateBoxings(pointer);
                else
                    temp.Nodes[pointer].NodeNumber = pointer;
            }

            return new DialogueScript(temp.Nodes, temp.Boxings);
        }


        private void FormateBoxings(int boxStart)
        {
            var boxPointer = -1;
            while(pointer < nodeCount) {
                if(temp.Nodes[pointer].Tag == "Boxing") {
                    var index = temp.Nodes[pointer].Target;
                    temp.Boxings.Add(new DialogueBoxing(index));
                    boxPointer++;
                }
                else {
                    var node = temp.Nodes[pointer];
                    node.NodeNumber = pointer;
                    temp.Boxings[boxPointer].AddNode(node);
                }

                pointer++;
            }

            temp.Nodes.RemoveRange(boxStart, nodeCount - boxStart);
        }


        private void Clean()
        {
            temp = null;
            nodeCount = 0;
            pointer = 0;
        }

    }
}