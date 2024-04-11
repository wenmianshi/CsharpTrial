using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Hogwarts
{
    public enum Type
    {
        [EnumMember(Value = "meat")]
        Meat,
        [EnumMember(Value = "vegetable")]
        Vegetable,
        [EnumMember(Value = "fruit")]
        Fruit
    }

    public struct Food
    {
        public string name { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public Type type { get; set; }
        public float nutritionValue { get; set; }
    }
}
