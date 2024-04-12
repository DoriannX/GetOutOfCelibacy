using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    //Components
    [SerializeField] private GameObject _dialogueBox;
    [SerializeField] private TextMeshProUGUI _dialogueDisplayer;
    [SerializeField] TextMeshProUGUI _textSwitchButton;
    [SerializeField] private List<Vector3> buttonPos;
    [SerializeField] private GameObject _canvas;
    [SerializeField] private Dialogues _firstDialogue;
    RectTransform _dialogueBoxTransform;

    //Prefab
    [SerializeField] Button _buttonChoice;
    List<Button> _buttons = new List<Button>();

    //Dialogues
    public Dialogues DialogueNext;

    //Dialogue manager
    private bool _isDialogueStarted = false;
    int _textIndex = 0;
    [HideInInspector] public UnityEvent DialogueFinished;

    //Positions
    [SerializeField] private Vector3 _position;
    Vector3 _basePosition;

    //Coroutines
    private Coroutine _dialogueCoroutine;

    //Instance
    public static DialogueManager Instance;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    private void Start()
    {
        _dialogueBoxTransform = _dialogueBox.GetComponent<RectTransform>();
        _basePosition = _dialogueBoxTransform.localPosition;
        DialogueNext = _firstDialogue;
        _textSwitchButton.text = ">Start<";
    }

    public void SwitchDialogue()
    {
        StopDialogue();
        StartDialogue();
    }

    private void StartDialogue()
    {
        ScoreManager.Instance.ResetScore();
        _isDialogueStarted = true;
        _dialogueDisplayer.text = "";
        _textIndex = 0;
        DialogueNext = _firstDialogue;
        _dialogueCoroutine = StartCoroutine(DisplayText());
        DisplayButtons();
        DatedGirlSpriteManager.Instance.ChoseFace(DatedGirlSpriteManager.Face.Neutral, false);
        _textSwitchButton.gameObject.SetActive(false);
    }

    public void NextDialogue(Dialogues nextDialogue)
    {
        if(nextDialogue.redFlag)
            ScoreManager.Instance.AddScore(-1);
        else if(nextDialogue.greenFlag)
            ScoreManager.Instance.AddScore(1);
        ItemManager.Instance.DisplayObjectToSelect();
        bool isWilliams = (nextDialogue.Williams);
        if (nextDialogue.greenFlag)
            DatedGirlSpriteManager.Instance.ChoseFace(DatedGirlSpriteManager.Face.Happy, isWilliams);
        else if (nextDialogue.redFlag)
            DatedGirlSpriteManager.Instance.ChoseFace(DatedGirlSpriteManager.Face.Disgust, isWilliams);
        else
            DatedGirlSpriteManager.Instance.ChoseFace(DatedGirlSpriteManager.Face.Neutral, isWilliams);

        DialogueNext = nextDialogue;
        if (DialogueNext.choix.Count <= 0)
        {
            print("dialogues finished");
            _textSwitchButton.text = ">Restart<";
            _textSwitchButton.gameObject.SetActive(true);
        }
        _dialogueDisplayer.text = "";
        _textIndex = 0;

        if(_dialogueCoroutine == null)
            _dialogueCoroutine = StartCoroutine(DisplayText());
        if(nextDialogue.Event != null)
            nextDialogue.Event.Invoke();
        DisplayButtons();
    }

    public void SkipDialogue()
    {
        _dialogueDisplayer.text = DialogueNext.Dialogue;
        if(_dialogueCoroutine != null)
            StopCoroutines();
    }

    private void StopDialogue()
    {
        ItemManager.Instance.DestroyObjectToSelect();
        ItemManager.Instance.NextButton.gameObject.SetActive(false);
        ItemManager.Instance.IsItemChosen = false;
        ItemManager.Instance.IsItemBeingChose = false;
        foreach (Button bt in _buttons.ToList())
        {
            _buttons.Remove(bt);
            Destroy(bt.gameObject);
        }
        _isDialogueStarted = false;
        print("dialogue finish");
        StopCoroutines();
    }

    private void DisplayButtons()
    {
        foreach (Button bt in _buttons.ToList())
        {
            _buttons.Remove(bt);
            Destroy(bt.gameObject);
        }
        if(!ItemManager.Instance.IsItemBeingChose || ItemManager.Instance.IsItemChosen)
        {
            for (int i = 0; i < DialogueNext.choix.Count; i++)
            {
                Button button = Instantiate(_buttonChoice, _canvas.transform);
                _buttons.Add(button);
                button.GetComponent<RectTransform>().localPosition = buttonPos[i];
                button.GetComponentInChildren<TextMeshProUGUI>().text = ">" + DialogueNext.choix[i].Name + "<";
                Dialogues curDial = DialogueNext.choix[i];
                button.onClick.AddListener(delegate { NextDialogue(curDial); });
            }
        }
    }

    private void Update()
    {
        if(_isDialogueStarted)
        { 
            _dialogueBoxTransform.localPosition = Vector3.Lerp(_dialogueBoxTransform.localPosition, _position, .1f);
        }
        else
        {
            _dialogueBoxTransform.localPosition = Vector3.Lerp(_dialogueBoxTransform.localPosition, _basePosition, .1f);
        }
    }

    IEnumerator DisplayText()
    {
        if (_textIndex < DialogueNext.Dialogue.Length)
        {
            _dialogueDisplayer.text += DialogueNext.Dialogue[_textIndex];
            _textIndex++;
        }
        yield return new WaitForSeconds(.01f);
        StopCoroutines();
        _dialogueCoroutine = StartCoroutine(DisplayText());
    }

    private void StopCoroutines()
    {
        if( _dialogueCoroutine != null )
        {
            StopCoroutine(_dialogueCoroutine);
            _dialogueCoroutine = null;
        }
    }

}
