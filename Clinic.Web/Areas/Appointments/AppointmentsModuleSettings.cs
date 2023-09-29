using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Blazorise;
using Clinic.Web.Shared;

namespace Clinic.Web.Areas.Appointments
{
    public class AppointmentsModuleSettings
    {
        ClaimsPrincipal _user; 
        public AppointmentsModuleSettings(ClaimsPrincipal user)
        {
            _user = user;
        }

        public NavigationMenuItem[] GetMenu()
        {
            var mainMenu = new NavigationMenuItem() { Link = "#", IconId = IconName.Circle, Title = "Appointments" };

            mainMenu.SubMenus.Add(new NavigationMenuItem() { Link = "Appointments/Appointments/index", IconId = IconName.Circle, Title = "Appointments" });
            mainMenu.SubMenus.Add(new NavigationMenuItem() { Link = "Appointments/Patients/index", IconId = IconName.Circle, Title = "Patients" });
            mainMenu.SubMenus.Add(new NavigationMenuItem() { Link = "Appointments/Attendances/index", IconId = IconName.Circle, Title = "Attendances" });
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
