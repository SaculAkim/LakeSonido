using DG.Tweening;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using Sequence = DG.Tweening.Sequence;

public class QuestLog : MonoBehaviour
{
    [SerializeField] GameObject QuestItemPrefab;
    [SerializeField] AudioClip NewQuestSound;
    [SerializeField] AudioClip QuestCompleteSound;

    AudioSource questLogSource;

    List<Sequence> questSequences;

    private void Start()
    {
        questLogSource = GetComponent<AudioSource>();
        questSequences = new List<Sequence>();
    }

    Dictionary<int, string> Quests = new Dictionary<int, string>() {
        { 0, "Find the cabin near the lake." },
        { 1, "Search for hidden treasure." },
        { 2, "Unlock the crate." },
        { 3, "Retrieve the artifact from the crate." },
        { 4, "Show your work to your teacher!" }};

    List<int> completedQuests = new List<int>();

    public void AddQuest(int QuestID)
    {
        if (completedQuests.Contains(QuestID)) return; 
        foreach (QuestLogItem quest in GetComponentsInChildren<QuestLogItem>())
        {
            if (quest.QuestID == QuestID)
            {
                return;
            }
        }
        GameObject newQuest = Instantiate(QuestItemPrefab, transform);
        newQuest.GetComponent<QuestLogItem>().Initialize(QuestID, Quests[QuestID]);
        RectTransform questTransform = newQuest.GetComponent<RectTransform>();
        questTransform.sizeDelta = new Vector2(0, 100);
        float totalLength = questSequences.Sum(x => x.Duration());
        Sequence questSequence = DOTween.Sequence();
        questSequence.AppendInterval(totalLength * 0.75f)
                    .AppendCallback(() => PlayNewQuestSound())
                    .Append(questTransform.DOSizeDelta(new Vector2(510, 100), 1f))
                    .OnComplete(() => RemoveSequenceFromListWhenDone(questSequence));
        questSequences.Add(questSequence);
    }

    void PlayNewQuestSound()
    {
        if (NewQuestSound != null) questLogSource.PlayOneShot(NewQuestSound);
    }
    void PlayFinishedQuestSound()
    {
        if (QuestCompleteSound != null) questLogSource.PlayOneShot(QuestCompleteSound);
    }

    void RemoveSequenceFromListWhenDone(Sequence sequence)
    {
        questSequences.Remove(sequence);
    }

    public void RemoveQuest(int QuestID)
    {
        completedQuests.Add(QuestID);
        foreach (QuestLogItem quest in GetComponentsInChildren<QuestLogItem>())
        {
            if(quest.QuestID == QuestID)
            {
                float totalLength = questSequences.Where(x => x.active).Sum(x => x.Duration() - x.Elapsed());
                Sequence questSequence = DOTween.Sequence();
                questSequence.AppendInterval(totalLength * 0.75f)
                            .AppendCallback(() => PlayFinishedQuestSound())
                            .Append(quest.gameObject.GetComponent<RectTransform>().DOSizeDelta(new Vector2(0, 100), 1f))
                            .Append(quest.gameObject.GetComponent<RectTransform>().DOSizeDelta(new Vector2(0, 0), 0.5f))
                            .AppendCallback(() => Destroy(quest.gameObject))
                            .OnComplete(() => RemoveSequenceFromListWhenDone(questSequence));
                questSequences.Add(questSequence);
            }
        }
    }

}
