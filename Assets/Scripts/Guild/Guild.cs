using UnityEngine;

public class Guild : MonoBehaviour
{
    [SerializeField] private WorldModul _wordmodul;
    [SerializeField] private QuestModul _questModul;
    [SerializeField] private FractionModul _fractionModul;
    [SerializeField] private FightModul _fightModul;
    [SerializeField] private AdventurerModul _adventurerModul;
    [SerializeField] private GuildModul _guildModul;

    private void OnEnable()
    {
        _fractionModul.NewQuestAvaible += GenerateQuest;
        _wordmodul.QuestAppear += GenerateQuest;
    }

    private void OnDisable()
    {
        _fractionModul.NewQuestAvaible -= GenerateQuest;
        _wordmodul.QuestAppear -= GenerateQuest;
    }

    private void GenerateQuest(IFractionQuestGiver questGiver)
    {
        _questModul.GenerateQuest(questGiver);
    }

    private void GenerateQuest(ISettelmentQuestGiver questGiver)
    {
        _questModul.GenerateQuest(questGiver);
    }
}
