using UnityEngine.UI;

public class EmptyGraphic : MaskableGraphic
{

    protected EmptyGraphic()
    {
        useLegacyMeshGeneration = false;
    }


    protected override void OnPopulateMesh(VertexHelper toFill)
    {
        toFill.Clear();
    }

}
