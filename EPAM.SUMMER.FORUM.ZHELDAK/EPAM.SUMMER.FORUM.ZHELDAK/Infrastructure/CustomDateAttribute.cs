using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EPAM.SUMMER.FORUM.ZHELDAK.Infrastructure
{
    public class CustomDateAttribute:RangeAttribute
    {
        public CustomDateAttribute():base(typeof(DateTime), DateTime.Now.AddYears(-70).ToLongDateString(), DateTime.Now.ToLongDateString())
        {
            
        }
    }
}