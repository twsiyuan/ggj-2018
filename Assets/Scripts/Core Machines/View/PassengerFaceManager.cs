using UnityEngine;

public class PassengerFaceManager : MonoBehaviour
{
    [SerializeField]
    private Sprite _face_1;
    [SerializeField]
    private Sprite _face_2;
    [SerializeField]
    private Sprite _face_3;

    public Sprite GetFace1Texture() {
        return _face_1;
    }
    public Sprite GetFace2Texture() {
        return _face_2;
    }
    public Sprite GetFace3Texture() {
        return _face_3;
    }
}