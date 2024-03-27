using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Dialogues", order = 1)]
public class Dialogues : ScriptableObject
{
    public string Dialogue;
    public bool greenFlag;
    public bool redFlag;
    public List<Dialogues> choix = null;
}
