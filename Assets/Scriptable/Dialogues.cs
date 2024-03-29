using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Dialogues", order = 1)]
public class Dialogues : ScriptableObject
{
    [TextArea (17, 22)]
    public string Dialogue;
    public bool greenFlag;
    public bool redFlag;
    public List<Dialogues> choix = null;
    public UnityEvent Event;
}
