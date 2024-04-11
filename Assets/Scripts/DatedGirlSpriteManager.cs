using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DatedGirlSpriteManager : MonoBehaviour
{
    //Faces
    [Header("Dated girl")]
    [SerializeField] Sprite _datedGirlHappyFace;
    [SerializeField] Sprite _datedGirlDigustFace;
    [SerializeField] Sprite _datedGirlNeutralFace;
    Dictionary<Face, Sprite> _datedGirlFaces;

    [Header("Williams")]
    [SerializeField] Sprite _datedGirlHappyWilliamsFace;
    [SerializeField] Sprite _datedGirlDigustWilliamsFace;
    [SerializeField] Sprite _datedGirlNeutralWilliamsFace;
    Dictionary<Face, Sprite> _datedGirlWilliamsFaces;
    
    public enum Face { Happy, Disgust, Neutral };

    //Instance
    public static DatedGirlSpriteManager Instance;

    //Components
    Image _datedGirlImage;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    private void Start()
    {
        _datedGirlImage = GetComponent<Image>();
        _datedGirlFaces = new Dictionary<Face, Sprite>() { { Face.Happy, _datedGirlHappyFace }, { Face.Disgust, _datedGirlDigustFace }, { Face.Neutral, _datedGirlNeutralFace } };
        _datedGirlWilliamsFaces = new Dictionary<Face, Sprite>() { { Face.Happy, _datedGirlHappyWilliamsFace }, { Face.Disgust, _datedGirlDigustWilliamsFace }, { Face.Neutral, _datedGirlNeutralWilliamsFace } };
        ChoseFace(Face.Neutral, false);

    }

    public void ChoseFace(Face face, bool williams)
    {
        Dictionary<Face, Sprite> datedGirlFaces = new Dictionary<Face, Sprite>();
        if (williams)
            datedGirlFaces = _datedGirlWilliamsFaces;
        else
            datedGirlFaces = _datedGirlFaces;
        _datedGirlImage.sprite = datedGirlFaces[face];
    }
}
