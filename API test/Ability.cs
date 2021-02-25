using System;
using Newtonsoft.Json;
namespace API_test
{
    public class Ability
    {

        public string Name { get; set; }

        [JsonProperty("ability")]
        public AbilityName TheAbility
        {
            set
            {
                Name = value.Name;
            }
        }
        [JsonProperty("is_hidden")]
        public bool IsHidden { get; set; }
    }
}
