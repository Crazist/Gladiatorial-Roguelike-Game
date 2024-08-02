using System;
using Data.Cards;
using Logic.Cards;
using Logic.Enteties;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;

public class CardConverter : JsonConverter
{
    public override bool CanConvert(Type objectType)
    {
        return typeof(Card).IsAssignableFrom(objectType);
    }

    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    {
        JObject jo = new JObject
        {
            { "CardType", value.GetType().Name }
        };

        foreach (var prop in value.GetType().GetProperties())
        {
            if (prop.CanRead && prop.GetIndexParameters().Length == 0)
            {
                if (typeof(CardData).IsAssignableFrom(prop.PropertyType))
                {
                    var cardData = (CardData)prop.GetValue(value, null);
                    jo.Add(prop.Name, cardData != null ? cardData.GetInstanceID() : 0);
                }
                else if (typeof(LevelMultiplierConfig).IsAssignableFrom(prop.PropertyType))
                {
                    var levelMultiplierConfig = (LevelMultiplierConfig)prop.GetValue(value, null);
                    jo.Add(prop.Name, levelMultiplierConfig != null ? levelMultiplierConfig.GetInstanceID() : 0);
                }
                else
                {
                    jo.Add(prop.Name, JToken.FromObject(prop.GetValue(value, null), serializer));
                }
            }
        }

        jo.WriteTo(writer);
    }

    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    {
        JObject jo = JObject.Load(reader);
        var cardType = jo["CardType"].ToString();

        Card target = cardType switch
        {
            "UnitCard" => new UnitCard(),
            "SpecialCard" => new SpecialCard(),
            _ => throw new InvalidOperationException("Unknown card type")
        };

        foreach (var prop in target.GetType().GetProperties())
        {
            if (jo[prop.Name] != null)
            {
                if (typeof(CardData).IsAssignableFrom(prop.PropertyType))
                {
                    int instanceId = jo[prop.Name].ToObject<int>();
                    CardData cardData = FindCardDataByInstanceId(instanceId);
                    prop.SetValue(target, cardData);
                }
                else if (typeof(LevelMultiplierConfig).IsAssignableFrom(prop.PropertyType))
                {
                    int instanceId = jo[prop.Name].ToObject<int>();
                    LevelMultiplierConfig levelMultiplierConfig = FindLevelMultiplierConfigByInstanceId(instanceId);
                    prop.SetValue(target, levelMultiplierConfig);
                }
                else
                {
                    prop.SetValue(target, jo[prop.Name].ToObject(prop.PropertyType, serializer));
                }
            }
        }

        return target;
    }

    private CardData FindCardDataByInstanceId(int instanceId)
    {
        var allCardData = Resources.FindObjectsOfTypeAll<CardData>();
        foreach (var cardData in allCardData)
        {
            if (cardData.GetInstanceID() == instanceId)
            {
                return cardData;
            }
        }
        return null;
    }

    private LevelMultiplierConfig FindLevelMultiplierConfigByInstanceId(int instanceId)
    {
        var allConfigs = Resources.FindObjectsOfTypeAll<LevelMultiplierConfig>();
        foreach (var config in allConfigs)
        {
            if (config.GetInstanceID() == instanceId)
            {
                return config;
            }
        }
        return null;
    }
}
