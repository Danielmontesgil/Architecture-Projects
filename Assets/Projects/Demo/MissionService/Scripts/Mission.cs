using System;
using System.Collections.Generic;
using MissionsService;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

[Serializable]
public class Mission: IMission
{
    public string Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public MissionState State { get; set; }
    [JsonConverter(typeof(RewardsConverter))]
    public List<IRewardable> Reward { get; set; }
    public int QuantityToComplete { get; set; }
    public int Progress { get; set; }
    [JsonConverter(typeof(MissionRequirementConverter))]
    public List<IMissionRequirement> Requirements { get; set; }
}

[Serializable]
public class MissionsData
{
    public List<Mission> Missions { get; set; }
}

public class MissionRequirementConverter : JsonConverter<List<IMissionRequirement>>
{
    public override List<IMissionRequirement> ReadJson(JsonReader reader, Type objectType, List<IMissionRequirement> existingValue, bool hasExistingValue, JsonSerializer serializer)
    {
        var jsonArray = JArray.Load(reader);
        var requirements = new List<IMissionRequirement>();

        foreach (var item in jsonArray)
        {
            string type = item["Type"]?.ToString();
            switch (type)
            {
                case "LevelRequirement":
                    var newRequirement = item.ToObject<LevelRequirement>();
                    requirements.Add(newRequirement);
                    break;
                default:
                    throw new JsonSerializationException($"Tipo de requisito desconocido: {type}");
            }
        }

        return requirements;
    }

    public override void WriteJson(JsonWriter writer, List<IMissionRequirement> value, JsonSerializer serializer)
    {
        serializer.Serialize(writer, value);
    }
}

public class RewardsConverter : JsonConverter<List<IRewardable>>
{
    public override List<IRewardable> ReadJson(JsonReader reader, Type objectType, List<IRewardable> existingValue, bool hasExistingValue,
        JsonSerializer serializer)
    {
        var jsonArray = JArray.Load(reader);
        var rewards = new List<IRewardable>();

        foreach (var item in jsonArray)
        {
            string type = item["Type"]?.ToString();
            switch (type)
            {
                case "BasicReward":
                    var newReward = item.ToObject<BasicItemReward>();
                    rewards.Add(newReward);
                    break;
                default:
                    throw new JsonSerializationException($"Tipo de requisito desconocido: {type}");
            }
        }

        return rewards;
    }

    public override void WriteJson(JsonWriter writer, List<IRewardable> value, JsonSerializer serializer)
    {
        serializer.Serialize(writer, value);
    }
}
