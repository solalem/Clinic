using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Blazorise;
using Clinic.Web.Shared;

namespace Clinic.Web.Areas.Appointments
{
    public class AppointmentsSettings
    {
        ClaimsPrincipal _user; 
        public AppointmentsSettings(ClaimsPrincipal user)
        {
            _user = user;
        }

        public NavigationMenuItem[] GetMenu()
        {
            var mainMenu = new NavigationMenuItem() { Link = "#", IconId = IconName.Circle, Title = "Appointments" };

            mainMenu.SubMenus.Add(new NavigationMenuItem() { Link = "Appointments/Patients/index", IconId = IconName.Users, Title = "Patients" });
            mainMenu.SubMenus.Add(new NavigationMenuItem() { Link = "Appointments/Appointments/index", IconId = IconName.CalendarWeek, Title = "Appointments" });
            mainMenu.SubMenus.Add(new NavigationMenuItem() { Link = "Appointments/Visits/index", IconId = IconName.CalendarCheck, Title = "Visits" });
            var roles = new List<Claim>();
            if (_user != null)
            {
                // TODO: add more menus for admins 
                roles = _user.Claims.Where(x => x.Type =="admin").ToList();
                if (roles != null)
                {
                }
            }

            return new [] { mainMenu };
        }

    }
}
