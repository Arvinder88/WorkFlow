using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dapper.DAL.Models;

namespace WorkFlow.ViewModal
{
    public class villageProfileVM
    {
        public int VillProfileID { get; set; }
        public int  constituency { get; set; }
        public  string District { get; set; }
        public  string Block { get; set; }
        public  string Village { get; set; }
        public int SarpanchID { get; set; }

        public IEnumerable<VILL_CASTE> VILL_CASTE { get; set; }

    }
}