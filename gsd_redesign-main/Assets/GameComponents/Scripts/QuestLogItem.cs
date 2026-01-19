using UnityEngine;

public class QuestLogItem : MonoBehaviour
{
    [SerializeField] TMPro.TextMeshProUGUI questTextUI;

    public int QuestID;
    public string QuestText;

    public void Initialize(int questID, string questText)
    {
        QuestID = questID;
        QuestText = questText;
        questTextUI.text = questText;
    }
}
