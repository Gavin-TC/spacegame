namespace Spacegame.Quests;

public class QuestManager
{
    private List<Quest> _activeQuests = new List<Quest>();
    private List<Quest> _completedQuests = new List<Quest>();

    public void AddQuest(Quest quest)
    {
        _activeQuests.Add(quest);
    }

    public void CompleteQuest(Quest quest)
    {
        _activeQuests.Remove(quest);
        _completedQuests.Add(quest);
        
        // Add rewards below
        
    }

    public IEnumerable<Quest> GetActiveQuests()
    {
        return _activeQuests;
    }

    public IEnumerable<Quest> GetCompletedQuests()
    {
        return _completedQuests;
    }
}