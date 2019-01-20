using System;
using System.Collections.Generic;

namespace MyFootballAdmin.Data.Models
{
    public class League:EntityBase<League>
    {
        public List<Team> Teams { get; set; }
        public int CountOfMatches { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public List<DayOfWeek> MatchDays { get; set; }
        public List<Rule> Rules { get; set; }
        public List<Pause> Pauses { get; set; }
        public Tournament Tournament { get; set; }
        public List<Tour> Tour { get; set; }
        public LeagueResultTable ResultTable { get; set; }
        public int OneHalfTime { get; set; }
        public int BreakTime { get; set; }
        public int PlayersCount { get; set; }
        public int YelloCardsToDisqualification { get; set; }
        private int[,] Play;
        private int l;

        public League()
        {
            Tour = new List<Tour>();
            Pauses = new List<Pause>();
            MatchDays = new List<DayOfWeek>();
            Rules = new List<Rule>();
        }
        public League(int count_of_clubs)
        {
            Tour = new List<Tour>((count_of_clubs - 1) * CountOfMatches);
        }
        private void Add1Tour(int tour_num, List<Tour> q)
        {
            Tour t = new Tour();
            Match m;
            if (tour_num % 2 == 0)
            {
                for (int i = 0; i < Teams.Count / 2; ++i)
                {
                    m = new Match();

                    m.Team1 = Teams[Play[0, i]];
                    m.Team2 = Teams[Play[1, i]];
                    m.GoalClub1 = 0;
                    m.GoalClub2 = 0;
                    t.Matches.Add(m);
                }
            }
            else
            {
                for (int i = (Teams.Count / 2) - 1; i >= 0; --i)
                {
                    m = new Match();

                    m.Team2 = Teams[Play[0, i]];
                    m.Team1 = Teams[Play[1, i]];
                    m.GoalClub1 = 0;
                    m.GoalClub2 = 0;
                    t.Matches.Add(m);
                }
            }
            q.Add(t);
            int temp = Play[0, (Teams.Count / 2) - 1];
            for (int i = (Teams.Count / 2) - 2; i >= 1; --i)
            {
                Play[0, i + 1] = Play[0, i];
            }
            Play[0, 1] = Play[1, 0];
            for (int i = 0; i <= (Teams.Count / 2) - 2; i++)
            {
                Play[1, i] = Play[1, i + 1];
            }
            Play[1, (Teams.Count / 2) - 1] = temp;
        }

        public bool Generate()
        {
            MatchDays.Sort();
            if (MatchDays[0] == DayOfWeek.Sunday)
            {
                MatchDays.Add(DayOfWeek.Sunday);
                MatchDays.RemoveAt(0);
            }
            List<DayOfWeekForTour> dayOfWeekForTours = new List<DayOfWeekForTour>();
            DateTime temp = StartDate;
            DayOfWeekForTour day = new DayOfWeekForTour();
            int index = 0;
            int indexDayOfWeek = 0;
            while (temp < EndDate)
            {
                if (temp.DayOfWeek == MatchDays[indexDayOfWeek])
                {
                    day.TourWeekDays.Add(temp);
                    indexDayOfWeek = (indexDayOfWeek + 1) % MatchDays.Count;
                    if (indexDayOfWeek == 0)
                    {
                        dayOfWeekForTours.Add(day);
                        day = new DayOfWeekForTour();
                    }
                }
                temp = temp.AddDays(1);
                if (temp.Date == Pauses[index].PauseStart.Date)
                {
                    temp = Pauses[index].PauseEnd;
                    if (index < Pauses.Count - 1)
                        ++index;
                    indexDayOfWeek = 0;
                    day = new DayOfWeekForTour();
                }
            }

            if (dayOfWeekForTours.Count < (Teams.Count - 1) * CountOfMatches)
            {
                return false;
            }

            l = 0;
            if (Teams.Count % 2 == 1)
            {
                Team fict = new Team();
                fict.Name = "FFF";
                Teams.Add(fict);
                l = 1;
            }
            int count = dayOfWeekForTours.Count - (Teams.Count - 1) * CountOfMatches;
            for (int i = 1; i <= count; ++i)
            {
                dayOfWeekForTours.RemoveAt((i * ((Teams.Count - 1) * CountOfMatches)) / (count + 1));
            }
            Play = new int[2, Teams.Count / 2];
            for (int i = 0; i < Teams.Count / 2; ++i)
            {
                Play[0, i] = i;
                Play[1, i] = i + Teams.Count / 2;
            }
            List<Tour> tours = new List<Tour>();
            for (int i = 0; i < Teams.Count - 1; ++i)
            {
                Add1Tour(i, tours);
            }
            index = 0;
            Tour t;
            Match m;
            for (int i = 0; i < CountOfMatches; ++i)
            {
                for (int j = 0; j < Teams.Count - 1; ++j)
                {
                    t = new Tour();
                    for (int k = 0; k < Teams.Count / 2; ++k)
                    {
                        m = new Match();
                        if (i % 2 == 0)
                        {
                            m.Team1 = tours[j].Matches[k].Team1;
                            m.Team2 = tours[j].Matches[k].Team2;
                        }
                        else
                        {
                            m.Team1 = tours[j].Matches[k].Team2;
                            m.Team2 = tours[j].Matches[k].Team1;
                        }
                        m.GoalClub1 = 0;
                        m.GoalClub2 = 0;
                        if (m.Team1.Name == "FFF" || m.Team2.Name == "FFF")
                            continue;
                        t.Matches.Add(m);
                    }
                    for (int y = 0; y < (Teams.Count / 2) - l; ++y)
                    {
                        t.Matches[y].MatchDateTime = dayOfWeekForTours[index].TourWeekDays[y % MatchDays.Count];
                    }
                    Tour.Add(t);
                    ++index;
                }
            }
            return true;
        }
    }
}
