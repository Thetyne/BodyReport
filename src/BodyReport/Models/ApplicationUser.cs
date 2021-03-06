﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace BodyReport.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        /// <summary>
        /// User supended
        /// </summary>
        public bool Suspended { get; set; }
        /// <summary>
        /// RegistrationDate
        /// </summary>
        public virtual DateTime RegistrationDate { get; set; }
        /// <summary>
        /// Last login date
        /// </summary>
        public virtual DateTime LastLoginDate { get; set; }
    }
}
