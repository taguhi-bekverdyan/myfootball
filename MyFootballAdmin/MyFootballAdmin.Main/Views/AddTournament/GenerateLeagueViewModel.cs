using MyFootballAdmin.Common.Prism;
using MyFootballAdmin.Data.Models;
using MyFootballAdmin.Main.Views.Notifications;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using MyFootballAdmin.Data.Services.LeagueService;

namespace MyFootballAdmin.Main.Views.AddTournament
{
    public class GenerateLeagueViewModel: BindableBase, INavigationAware, IRegionManagerAware
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly INotificationService _notificationService;
        private readonly IShellService _shellService;
        private readonly IRegionManager _regionManager;
        private readonly ILeagueService _leagueService;

        public GenerateLeagueViewModel(IShellService shellService, IRegionManager regionManager, IEventAggregator eventAggregator, INotificationService notificationService, ILeagueService leagueService)
        {
            _shellService = shellService;
            _regionManager = regionManager;
            _eventAggregator = eventAggregator;
            _notificationService = notificationService;
            _leagueService = leagueService;
            SelectedRule = new Rule();
            Rules = new ObservableCollection<Rule>();
            RuleString = new ObservableCollection<string>();
            RuleString.Add("WON");
            RuleString.Add("DRAW");
            RuleString.Add("LOST");
            RuleString.Add("GOALS FOR");
            RuleString.Add("GOALS AGAINST");
            RuleString.Add("GOALS DIFFERENCE");
            RuleString.Add("POINTS");
            RuleString.Add("YELLO CARDS");
            RuleString.Add("RED CARDS");
        }
        #region Types
        private League _leagueToGenerate;
        public League LeagueToGenerate
        {
            get { return _leagueToGenerate; }
            set { SetProperty(ref _leagueToGenerate, value); }
        }
        private string _selectedComboBox;
        public string SelectedComboBox
        {
            get { return _selectedComboBox; }
            set { SetProperty(ref _selectedComboBox, value); }
        }
        private bool _selectedRedioButton;
        public bool SelectedRedioButton
        {
            get { return _selectedRedioButton; }
            set { SetProperty(ref _selectedRedioButton, value); }
        }

        private ObservableCollection<Rule> _rules;
        public ObservableCollection<Rule> Rules
        {
            get { return _rules; }
            set { SetProperty(ref _rules, value); }
        }
        private ObservableCollection<string> _ruleString;
        public ObservableCollection<string> RuleString
        {
            get { return _ruleString; }
            set { SetProperty(ref _ruleString, value); }
        }
        private Rule _selectedRule;
        public Rule SelectedRule
        {
            get { return _selectedRule; }
            set { SetProperty(ref _selectedRule, value); }
        }
        #endregion

        #region Commands
        private DelegateCommand _generateCommand;
        public DelegateCommand GenerateCommand => _generateCommand ?? (_generateCommand = new DelegateCommand(GenerateCommandAction));
        private DelegateCommand<Rule> _editCommand;
        private DelegateCommand _addCommand;
        private DelegateCommand _deleteCommand;
        private DelegateCommand _saveCommand;
        public DelegateCommand DeleteCommand => _deleteCommand ?? (_deleteCommand = new DelegateCommand(DeleteCommandAction));
        public DelegateCommand AddCommand => _addCommand ?? (_addCommand = new DelegateCommand(AddCommandAction));
        public DelegateCommand SaveCommand => _saveCommand ?? (_saveCommand = new DelegateCommand(SaveCommandAction));

        public DelegateCommand<Rule> EditCommand => _editCommand ?? (_editCommand = new DelegateCommand<Rule>(EditCommandAction));


        public void DeleteCommandAction()
        {
            if(!RuleString.Contains(SelectedRule.RuleName))
            RuleString.Add(SelectedRule.RuleName);
            SelectedRedioButton = SelectedRule.HighLow;
            Rules.Remove(SelectedRule);
        }
        public void SaveCommandAction()
        {

            SelectedRule.RuleName = SelectedComboBox;
            RuleString.Remove(SelectedComboBox);
            SelectedRule.HighLow = SelectedRedioButton;
            SelectedRedioButton = false;
        }
        public void AddCommandAction()
        {
            Rule p = new Rule();
            p.RuleName = SelectedComboBox;
            p.HighLow = SelectedRedioButton;
            RuleString.Remove(SelectedComboBox);
            Rules.Add(p);
        }
      
     
        public void EditCommandAction(Rule selectedPause)
        {
            RuleString.Add(SelectedRule.RuleName);
            SelectedRedioButton = SelectedRule.HighLow;
            SelectedComboBox = SelectedRule.RuleName;
        }
        public void GenerateCommandAction()
        {
            List<Team> Clubs = new List<Team>();
            Team club = new Team();
            club.Name = "LIV";
            Clubs.Add(club);
            club = new Team();
            club.Name = "MCI";
            Clubs.Add(club);
            club = new Team();
            club.Name = "TOT";
            Clubs.Add(club);
            club = new Team();
            club.Name = "CHE";
            Clubs.Add(club);
            club = new Team();
            club.Name = "ARS";
            Clubs.Add(club);
            club = new Team();
            club.Name = "MUN";
            Clubs.Add(club);
            club = new Team();
            club.Name = "EVE";
            Clubs.Add(club);
            club = new Team();
            club.Name = "BOU";
            Clubs.Add(club);
            club = new Team();
            club.Name = "LEI";
            Clubs.Add(club);
            club = new Team();
            club.Name = "WOL";
            Clubs.Add(club);
            club = new Team();
            club.Name = "WHU";
            Clubs.Add(club);
            club = new Team();
            club.Name = "WAT";
            Clubs.Add(club);
            club = new Team();
            club.Name = "BHA";
            Clubs.Add(club);
            club = new Team();
            club.Name = "CAR";
            Clubs.Add(club);
            club = new Team();
            club.Name = "NEW";
            Clubs.Add(club);
            club = new Team();
            club.Name = "CRY";
            Clubs.Add(club);
            club = new Team();
            club.Name = "BUR";
            Clubs.Add(club);
            club = new Team();
            club.Name = "HUD";
            Clubs.Add(club);
            club = new Team();
            club.Name = "SOU";
            Clubs.Add(club);
            club = new Team();
            club.Name = "FUL";
            Clubs.Add(club);
            LeagueToGenerate.Teams = Clubs;
            MessageBox.Show(LeagueToGenerate.EndDate.ToShortDateString());
            LeagueToGenerate.Generate();
            LeagueToGenerate.Created=LeagueToGenerate.Updated=DateTime.Now;
            LeagueToGenerate.Tournament.Created = LeagueToGenerate.Tournament.Updated = DateTime.Now;

            _leagueService.Create(LeagueToGenerate);
            DataContractJsonSerializer jsonFormatter = new DataContractJsonSerializer(typeof(League));
            using (FileStream fs = new FileStream("league.json", FileMode.OpenOrCreate))
            {
                jsonFormatter.WriteObject(fs, LeagueToGenerate);
            }
        }



        #endregion
        public IRegionManager RegionManager { get; set; }


        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            throw new NotImplementedException();
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {

        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            var param = navigationContext.Parameters["request"];
            LeagueToGenerate = (League)param;
        }
    }
}
