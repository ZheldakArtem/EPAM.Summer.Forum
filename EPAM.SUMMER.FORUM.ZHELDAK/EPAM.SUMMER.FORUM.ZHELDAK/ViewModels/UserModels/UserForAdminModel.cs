﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EPAM.SUMMER.FORUM.ZHELDAK.ViewModels
{
    public class UserForAdminModel
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string[] Roles { get; set; }

    }
}