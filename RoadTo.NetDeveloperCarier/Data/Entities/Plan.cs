using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;

namespace RoadTo.NetDeveloperCarier.Data.Entities
{
    public enum DifficultyLevel
    {
        Easy,
        Medium,
        Hard
    }
    public class Plan
    {
        public int? Id { get; set; }
        public string? Name { get; set; }
        public string? ShortDescription { get; set; }
        public string? FullDescription { get; set; }
        public DifficultyLevel? DifficultyLevel { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? DeadLine { get; set; }
        public DateTime? CompletedAt { get; set; }
        public bool IsCompleted { get; set; }
        public string? UserId { get; set; } 
    }
}
