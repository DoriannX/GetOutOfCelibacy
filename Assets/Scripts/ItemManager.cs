using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ItemManager : MonoBehaviour
{
    enum Flags { RedFlag, GreenFlag, NeutralFlag }
    [SerializeField] List<string> _items = new List<string>();
    [SerializeField] List<Flags> _flags;

    //Positions
    [SerializeField] Vector3 _position;
    List<Vector3> _positions = new List<Vector3>();

    //Buttons
    [SerializeField] Toggle _toggle;
    public Button NextButton;
    List<Toggle> _toggles = new List<Toggle>();

    //Components
    [SerializeField] Canvas _canvas; 

    public static ItemManager Instance;

    public bool IsItemChosen = false;
    public bool IsItemBeingChose;

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
        _positions.Add(_position);
        for(int i = 1; i < _items.Count; i++)
        {
            _positions.Add(_positions[i - 1] + new Vector3(0, 75, 0));
        }
    }

    public void DisplayObjectToSelect()
    {
        if (!IsItemBeingChose)
        {
            for (int i = 0; i < _items.Count; i++)
            {
                _toggles.Add(Instantiate(_toggle, _canvas.transform));
                _toggles[i].GetComponentInChildren<Text>().text = _items[i];
                _toggles[i].GetComponent<RectTransform>().localPosition = _positions[i];
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
        foreach(Toggle toggle in _toggles.ToList())
        {
            _toggles.Remove(toggle);
            Destroy(toggle.gameObject);
        }
    }

    public int PointsToAdd()
    {
        int points = 0;

        for(int i =0; i < _toggles.Count; i++)
        {
            if (_toggles[i].isOn)
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
        NextButton.gameObject.SetActive(false);
        if (PointsToAdd() > 0)
            DialogueManager.Instance.NextDialogue(DialogueManager.Instance.DialogueNext.choix[0]);
        else if (PointsToAdd() == 0)
            DialogueManager.Instance.NextDialogue(DialogueManager.Instance.DialogueNext.choix[1]);
        else
            DialogueManager.Instance.NextDialogue(DialogueManager.Instance.DialogueNext.choix[2]);
    }
}
