using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ItemManager : MonoBehaviour
{
    enum Flags { RedFlag, GreenFlag, NeutralFlag }
    [SerializeField] List<GameObject> _itemPrefabs = new List<GameObject>();
    List<GameObject> _itemsSpawned = new List<GameObject>();
    [SerializeField] List<Flags> _flags;
    [SerializeField] Canvas _canvas;

    //Positions
    [SerializeField] Vector3 _firstItemBasePosition;
    List<Vector3> _itemsPositions = new List<Vector3>();

    //Buttons
    public Button NextButton;

    public static ItemManager Instance;

    [HideInInspector] public bool IsItemChosen = false;
    [HideInInspector] public bool IsItemBeingChose;

    private void Awake()
    {
        if(Instance == null)
            Instance = this;
    }

    private void Start()
    {
        NextButton.gameObject.SetActive(false);
        SetPositions();
    }

    private void SetPositions()
    {
        _itemsPositions.Add(_firstItemBasePosition);
        for(int i = 1; i < _itemPrefabs.Count; i++)
        {
            _itemsPositions.Add(_itemsPositions[i - 1] + new Vector3(0, 75, 0));
        }
    }

    public void DisplayObjectToSelect()
    {
        if (!IsItemBeingChose)
        {
            for (int i = 0; i < _itemPrefabs.Count; i++)
            {
                _itemsSpawned.Add(Instantiate(_itemPrefabs[i], _canvas.transform));
                _itemsSpawned[i].GetComponent<RectTransform>().localPosition = _itemsPositions[i];
                IsItemBeingChose = true;
            }
            NextButton.gameObject.SetActive(true);
        }
        else
        {
            IsItemChosen = true;
            print(PointsToAdd());
            DestroyObjectToSelect();
        }
    }

    public void DestroyObjectToSelect()
    {
        foreach(GameObject itemSpawned in _itemsSpawned.ToList())
        {
            _itemsSpawned.Remove(itemSpawned);
            Destroy(itemSpawned.gameObject);
        }
    }

    public int PointsToAdd()
    {
        int points = 0;

        for(int i =0; i < _itemsSpawned.Count; i++)
        {
            if (_itemsSpawned[i].GetComponent<Toggler>().IsSelected)
            {
                switch (_flags[i])
                {
                    case Flags.RedFlag:
                        points--;
                        break;
                    case Flags.GreenFlag:
                        points++;
                        break;
                    default:
                        break;
                }
            }
        }

        return points;
    }

    public void ChoseNextDialogue()
    {
        print("nextDialogue"); 
        NextButton.gameObject.SetActive(false);
        if (PointsToAdd() > 0)
            DialogueManager.Instance.NextDialogue(DialogueManager.Instance.DialogueNext.choix[0]);
        else if (PointsToAdd() == 0)
            DialogueManager.Instance.NextDialogue(DialogueManager.Instance.DialogueNext.choix[1]);
        else
            DialogueManager.Instance.NextDialogue(DialogueManager.Instance.DialogueNext.choix[2]);
    }
}
