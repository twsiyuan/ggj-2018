using System;
using UnityEngine;

[Serializable]
public class TableSlot
{

    private TableElement element = null;
    public TableElement Element {
        get { return element; }
    }

    [SerializeField]
    private int[] neighborIndices;


    public void SetElement(TableElement element)
    {
        this.element = element;
    }


    public bool IsExist()
    {
        return element != null;
    }


    public bool IsNeighbor(int index)
    {
        foreach(var n in neighborIndices) {
            if(index == n)
                return true;
        }

        return false;
    }


#if UNITY_EDITOR
    public void SetNeighborIndices(int[] neighborIndices)
    {
        this.neighborIndices = neighborIndices;
    }
#endif

}