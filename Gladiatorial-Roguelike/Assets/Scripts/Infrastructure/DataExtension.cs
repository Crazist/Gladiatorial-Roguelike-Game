using UnityEngine;

namespace Infrastructure
{
    public static class DataExtension
    {
        public static T ToDeserialized<T>(this string json) =>
            JsonUtility.FromJson<T>(json);
    }
}