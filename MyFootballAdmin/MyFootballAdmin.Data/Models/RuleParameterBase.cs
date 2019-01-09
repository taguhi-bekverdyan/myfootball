namespace MyFootballAdmin.Data.Models
{
    public class RuleParameterBase
    {
        public RuleType RuleType { get; set; }

        public string  RuleParameterName { get; set; }

    }

    public enum RuleType
    {
        Points,
        GoalDiff,
        YellowCards
    }
}