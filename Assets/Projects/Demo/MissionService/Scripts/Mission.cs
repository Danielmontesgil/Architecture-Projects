using System;
using System.Collections.Generic;
using MissionsService;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;

[Serializable]
public class Mission: IMission
{
    public string Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public MissionState State { get; set; }
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
                    if (!(newRequirement is IMissionRequirement))
                    {
                        Debug.LogError("Cerdo");
                    }
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
