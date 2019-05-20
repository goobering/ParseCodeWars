using System;

namespace ParseCodeWars
{
    class Kata
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string[] CompletedLanguages { get; set; }
        public DateTime CompletedAt { get; set; }

        public Kata(string id, string name, string[] completedLanguages, DateTime completedAt)
        {
            Id = id;
            Name = name;
            CompletedLanguages = completedLanguages;
            CompletedAt = completedAt;
        }
    }
}