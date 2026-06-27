using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillingSystem.Utils
{
    /// <summary>
    /// Stores information about the currently logged-in user.
    /// Being static means any form can read these values
    /// without passing objects between forms.
    /// </summary>
    public static class AppSession
    {
        public static int CurrentUserID { get; set; }
        public static string CurrentUsername { get; set; }
        public static string CurrentFullName { get; set; }
        public static string CurrentRole { get; set; }

        /// <summary>
        /// Clears all session data. Called on logout.
        /// </summary>
        public static void Clear()
        {
            CurrentUserID = 0;
            CurrentUsername = string.Empty;
            CurrentFullName = string.Empty;
            CurrentRole = string.Empty;
        }
    }
}

