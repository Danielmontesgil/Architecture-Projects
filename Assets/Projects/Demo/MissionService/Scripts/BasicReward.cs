using MissionsService;

public class BasicItemReward : IRewardable
{
    public string Id { get; set; }
    public string Title { get; set; }
    public string Icon { get; set; }
    public int Amount { get; set; }
}
