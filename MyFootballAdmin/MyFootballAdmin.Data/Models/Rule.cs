using Prism.Mvvm;
using System.Collections.Generic;
using System.Linq;

namespace MyFootballAdmin.Data.Models
{
    public class Rule:BindableBase
    {
        private string _ruleName;
        private bool _highLow;
        private int _rulePriority;
        private string _parameterName;

        public string RuleName
        {
            get { return _ruleName; }
            set { SetProperty(ref _ruleName, value); }
        }

        public bool HighLow
        {
            get { return _highLow; }
            set { SetProperty(ref _highLow, value); }
        }
        public int RulePriority
        {
            get { return _rulePriority; }
            set { SetProperty(ref _rulePriority, value); }
        }
        public string ParameterName
        {
            get { return _parameterName; }
            set { SetProperty(ref _parameterName, value); }
        }

    }
}