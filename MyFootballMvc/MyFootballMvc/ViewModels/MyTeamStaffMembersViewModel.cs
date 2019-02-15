using MyFootballMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyFootballMvc.ViewModels
{
    public class MyTeamStaffMembersViewModel:MyTeamViewModel
    {

        public List<Staff> MyStaffMembers { get; set; }
        public Staff SelectedStaff { get; set; } = new Staff();

        public MyTeamStaffMembersViewModel():base()
        {

        }

        public MyTeamStaffMembersViewModel(string token,string id):base(token,id)
        {
            MyStaffMembers = Team.StaffMembers;
        }

    }
}
