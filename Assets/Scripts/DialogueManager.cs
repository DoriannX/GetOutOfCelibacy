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
    private Dialogues _nextDialogue;

    //Dialogue manager
    private bool _isDialogueStarted = false;
    private int _dialogueIndex = 0;
    int _textIndex = 0;
    [HideInInspector] public UnityEvent DialogueFinished;

    //Positions
    [SerializeField] private Vector3 _position;
    Vector3 _basePosition;

    //Coroutines
    private Coroutine _dialogueCoroutine;


    private void Start()
    {
        _dialogueBoxTransform = _dialogueBox.GetComponent<RectTransform>();
        _basePosition = _dialogueBoxTransform.localPosition;
        _nextDialogue = _firstDialogue;
    }

    public void SwitchDialogue()
    {
        if (_isDialogueStarted)
            StopDialogue();
        else
            StartDialogue();
    }

    private void StartDialogue()
    {
        _isDialogueStarted = true;
        _dialogueDisplayer.text = "";
        _textIndex = 0;
        _dialogueIndex = 0;
        _nextDialogue = _firstDialogue;
        _dialogueCoroutine = StartCoroutine(DisplayText());
        DisplayButtons();
    }

    public void NextDialogue(Dialogues nextDialogue)
    {

        
        _nextDialogue = nextDialogue;
        _dialogueDisplayer.text = "";
        _textIndex = 0;
        _dialogueIndex++;
        DisplayButtons();
    }

    private void StopDialogue()
    {
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
        for (int i = 0; i<_nextDialogue.choix.Count; i++)
        {
            print("button displayed");
            Button button = Instantiate(_buttonChoice, _canvas.transform);
            _buttons.Add(button);
            button.GetComponent<RectTransform>().localPosition = buttonPos[i];
            button.GetComponentInChildren<TextMeshProUGUI>().text = _nextDialogue.choix[i].name;
            button.onClick.AddListener(delegate { NextDialogue(_nextDialogue.choix[i-1]); });
        }
    }

    private void Update()
    {
        if(_isDialogueStarted)
        { 
            _dialogueBoxTransform.localPosition = Vector3.Lerp(_dialogueBoxTransform.localPosition, _position, .1f);
            _textSwitchButton.text = "Stop";
        }else
        {
            _textSwitchButton.text = "Start";
            _dialogueBoxTransform.localPosition = Vector3.Lerp(_dialogueBoxTransform.localPosition, _basePosition, .1f);
        }
    }

    IEnumerator DisplayText()
    {
        if (_textIndex < _nextDialogue.Dialogue.Length)
        {
            _dialogueDisplayer.text += _nextDialogue.Dialogue[_textIndex];
            _textIndex++;
        }
        yield return new WaitForSeconds(.01f);
        StopCoroutines();
        _dialogueCoroutine = StartCoroutine(DisplayText());
    }

    private void StopCoroutines()
    {
        if( _dialogueCoroutine != null )
            StopCoroutine(_dialogueCoroutine);
    }
}
