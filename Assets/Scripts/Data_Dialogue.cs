using UnityEngine;

[CreateAssetMenu(menuName = "Data/Dialogues", fileName ="DataDialogue")]
public class Data_Dialogue : ScriptableObject
{
    public string nameCharacter;
    [TextArea] public string text;
}
