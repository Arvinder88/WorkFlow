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
            var Constituency = db.Query<constituency_master>("sp_VILLAGE_PROFILE", new { status = "select_cons" }, commandType: CommandType.StoredProcedure).ToList();
            db.Close();
            return Constituency.Select(x=> new DropDownVM { value=x.ID.ToString() ,text=x.CONSTITUENCY});
        }

        public IEnumerable<DropDownVM> getAllDistrict(string Constituency)
        {
            db.Open();
            var District = db.Query<constituency_master>("sp_VILLAGE_PROFILE", new { status = "select_DISTRICT", Consti = Constituency }, commandType: CommandType.StoredProcedure).ToList();
            db.Close();
            return District.Select(x => new DropDownVM { value = x.DISTRICT, text = x.DISTRICT });
        }
        public IEnumerable<DropDownVM> getBlock(string district )
        {
            db.Open();
            var District = db.Query<Sarpanch>("sp_VILLAGE_PROFILE", new { status = "select_BLOCK", District = district }, commandType: CommandType.StoredProcedure).ToList();
            db.Close();
            return District.Select(x => new DropDownVM { value = x.Block, text = x.Block });
        }
        public IEnumerable<DropDownVM> getVillage(string Block)
        {
            db.Open();
             var District = db.Query<Sarpanch>("sp_VILLAGE_PROFILE", new { status = "select_PANCHAYAT", BLOCK = Block }, commandType: CommandType.StoredProcedure).ToList();
            db.Close();
            return District.Select(x => new DropDownVM { value = x.Panchayat, text = x.Panchayat });
        }

        public IEnumerable<Sarpanch> getSarpanchInfo(string _Village, string Constituency,string district,string Block)
        {
            db.Open();
          var Sarpanch=  db.Query<Sarpanch>("sp_VILLAGE_PROFILE", new { status = "fill_sarpanch", District= district, Consti= Constituency, BLOCK = Block, Village = _Village }, commandType: CommandType.StoredProcedure).ToList();
            db.Close();
            return Sarpanch;
        }
        public bool InsertVillProfile(tbVillageProfile VillProfile)
        {
            db.Open();
           
       
            string sql = "INSERT INTO [dbo].[tbVillageProfile]";
            sql = sql +  "([contituency],[district],[block],[village] ,[totalVotes],[casteMajor],[problems] ,[remarks],[comments],[sarpanchID]) ";
            sql= sql+" VALUES(@contituency, @district, @block, @village, @totalVotes, @casteMajor, @problems, @remarks, @comments, @sarpanchID) ";
            var data = new
            {
                village = VillProfile.village,
                district = VillProfile.district,
                block = VillProfile.block,
                contituency = VillProfile.contituency,
                totalVotes = VillProfile.totalVotes,
                casteMajor = VillProfile.casteMajor,
                problems = VillProfile.problems,
                remarks = VillProfile.remarks,
                sarpanchID = VillProfile.sarpanchID,
                comments = VillProfile.comments
            };

        var Constituency = db.Execute(sql, data);
            if (Constituency > 0)
                return true;
            else
                return false;
           
        }

        public bool UpdateVillProfile(tbVillageProfile VillProfile)
        {
            db.Open();


            string sql = "Update [dbo].[tbVillageProfile] ";
            sql = sql + " set [contituency]=@contituency,[district]=@district,[block]=@block,[village] =@village,[totalVotes]=@totalVotes,[casteMajor]=@casteMajor,[problems]=@problems ,[remarks]=@remarks,[comments]=@comments,[sarpanchID]=@sarpanchID  ";
            sql = sql + " where profileID=@profileID";
            var data = new
            {
                village = VillProfile.village,
                district = VillProfile.district,
                block = VillProfile.block,
                contituency = VillProfile.contituency,
                totalVotes = VillProfile.totalVotes,
                casteMajor = VillProfile.casteMajor,
                problems = VillProfile.problems,
                remarks = VillProfile.remarks,
                sarpanchID = VillProfile.sarpanchID,
                comments = VillProfile.comments,
                profileID = VillProfile.profileID
            };

            var Constituency = db.Execute(sql, data);
            if (Constituency > 0)
                return true;
            else
                return false;

        }

        public tbVillageProfile getVillProfile(int VillProfileID)
        {
            db.Open();
          return  db.Get<tbVillageProfile>(VillProfileID);
        }

        public tbVillageProfile checkProfileExist(int SarpanchID)
        {
            db.Open();
            string sql = "select * from tbVillageProfile where sarpanchID=@sarpanchId";

            var vill=db.QueryFirstOrDefault<tbVillageProfile>(sql,new { sarpanchId= SarpanchID });
            return vill;
        }


        //==========================Cast wise Village Population===========================
        public bool InsertVillCastWisePopulation(VILL_CASTE VillCast)
        {
            db.Open();


            string sql = "INSERT INTO [dbo].[VILL_CASTE]";
            sql = sql + "([vill_id],[caste_name],[caste_population]) ";
            sql = sql + " VALUES(@vill_id, @caste_name, @caste_population) ";
            var data = new
            {
                vill_id = VillCast.vill_id,
                caste_name = VillCast.caste_name,
                caste_population= VillCast.caste_population,
               
            };

            var Constituency = db.Execute(sql, data);
            if (Constituency > 0)
                return true;
            else
                return false;

        }

        public bool UpdateVillCastWisePopulation(VILL_CASTE VillCast)
        {
            db.Open();


            string sql = "Update [dbo].[VILL_CASTE] ";
            sql = sql + " set [vill_id]=@vill_id,[caste_name]=@caste_name,[caste_population]=@caste_population ";
            sql = sql + " where caste_id=@caste_id";
            var data = new
            {
                vill_id = VillCast.vill_id,
                caste_name = VillCast.caste_name,
                caste_population = VillCast.caste_population,
                caste_id = VillCast.caste_id,
            };
            var Constituency = db.Execute(sql, data);
            if (Constituency > 0)
                return true;
            else
                return false;

        }

        public tbVillageProfile getVillCastWisePopulation(int villID)
        {
            db.Open();
            string sql = "select * from VILL_CASTE where vill_id=@vill_id";

            var vill = db.QueryFirstOrDefault<tbVillageProfile>(sql, new { vill_id = villID });
            return vill;
        }
        //=================================================================================

        public IEnumerable<DropDownVM> EmptyList()
        {
            return new List<DropDownVM>();
        }

    }
}