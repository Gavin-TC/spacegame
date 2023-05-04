namespace Spacegame.Quests;

public class Quest
{
    public string questName { get; set; }
    public string questDescription { get; set; }
    public bool isCompleted { get; set; }
    public int experienceReward { get; set; }
    public int creditReward { get; set; }
    public Dictionary<string, int> itemReward { get; set; }
}