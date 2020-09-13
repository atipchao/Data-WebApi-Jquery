using Data_WebApi_Jquery.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Data_WebApi_Jquery.Controllers
{
    public class EmpDBEntities : DbContext
    {
        public EmpDBEntities()
            : base("name=EmpDBEntities")
        {
        }



        public DbSet<Employee> Employees { get; set; }
        public DbSet<AccessToken> AccessTokens { get; set; }
        public DbSet<FileUpload> FileUploads { get; set; }

    }
}