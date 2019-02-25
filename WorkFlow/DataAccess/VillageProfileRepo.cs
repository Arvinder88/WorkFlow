using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data;
using Dapper.DAL.Models;
using System.Data.SqlClient;
using Dapper;
using Dapper.Contrib.Extensions;
using WorkFlow.ViewModal;

namespace WorkFlow.DataAccess
{
    public abstract class _connection
    {
        private SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString);
        public SqlConnection Dapper()
        {
            return con;
        }

    }

    public class VillageProfileRepo:_connection
    {
        SqlConnection db = new SqlConnection();
        public VillageProfileRepo()
        {
            db = Dapper();
        }

        public IEnumerable<DropDownVM> getAllConstituency()
        {

            db.Open();
            var Constituency = db.Query<DropDownVM>("select CON_ID as value,CONSTITUENCY as text from CONSTITUENCY ").ToList();
           
            return Constituency;
        }

        public IEnumerable<DropDownVM> getAllDistrict(string Constituency)
        {
            db.Open();
            var District = db.Query<DropDownVM>("select DISTRICT as value,DISTRICT as text from constituency_master where CONSTITUENCY=@0 ", Constituency).ToList();

            return District;
        }


    }
}