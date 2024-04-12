using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Dialogues", order = 1)]
public class Dialogues : ScriptableObject
{
    public string Name;
    [TextArea (17, 22)]
    public string Dialogue;
    public bool greenFlag;
    public bool redFlag;
    public bool Williams;
    public List<Dialogues> choix = null;
    public UnityEvent Event;
    public bool HasToTransform;
}
