using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Model;
namespace MisLabs.Models
{
    public class ReportViewModel
    {
        public string Title { get; set; }
        public List<Account> Bills { get; set; }
    }
}