using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WorkFlow.DataAccess;
using Dapper.DAL.Models;

namespace WorkFlow.Controllers
{
    public class VillageProfileController : Controller
    {
     
        [HttpGet]
        public ActionResult GetVillageProfile()
        {
            VillageProfileRepo villRepo = new VillageProfileRepo();
            ViewBag.Constituency = villRepo.getAllConstituency();
            ViewBag.District = villRepo.EmptyList();
            ViewBag.Block = villRepo.EmptyList();
            ViewBag.Village = villRepo.EmptyList();
            return View();
        }

        [HttpPost]
        public ActionResult InsertVillageProfile(tbVillageProfile VillProfile)
        {

            VillageProfileRepo villRepo = new VillageProfileRepo();
            var result = villRepo.InsertVillProfile(VillProfile);

            if (result)
                return Json(new { status = "Success", data = "" }, JsonRequestBehavior.AllowGet);
            else
                return Json(new { status = "error", data = "" }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult UpdateVillageProfile(tbVillageProfile VillProfile)
        {

            VillageProfileRepo villRepo = new VillageProfileRepo();
            var result = villRepo.UpdateVillProfile(VillProfile);

            if (result)
                return Json(new { status = "Success", data = "" }, JsonRequestBehavior.AllowGet);
            else
                return Json(new { status = "error", data = "" }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetProfilebySarpanchID(int SarpanchID)
        {

            VillageProfileRepo villRepo = new VillageProfileRepo();
            var data = villRepo.checkProfileExist(SarpanchID);
            if (data != null)
                return Json(new { status = true, data = data }, JsonRequestBehavior.AllowGet);
            else
                return Json(new { status = false, data = "" }, JsonRequestBehavior.AllowGet);

        }

        [HttpGet]
        public ActionResult GetDistrict(string Constituency)
        {

            VillageProfileRepo villRepo = new VillageProfileRepo();
            var district = villRepo.getAllDistrict(Constituency);
            return Json(new { status="success",data= district },JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetBlock(string District)
        {

            VillageProfileRepo villRepo = new VillageProfileRepo();
            var block = villRepo.getBlock(District);
            return Json(new { status = "success", data = block }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetVillage(string Block)
        {

            VillageProfileRepo villRepo = new VillageProfileRepo();
            var village = villRepo.getVillage(Block);
            return Json(new { status = "success", data = village }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetSarpanchInformation(string _Village, string Constituency, string district, string Block)
        {
            VillageProfileRepo villRepo = new VillageProfileRepo();
            var sarpanch = villRepo.getSarpanchInfo(_Village, Constituency, district, Block);
            if (sarpanch.Count() != 0)
            { return Json(new { status = "success", data = sarpanch }, JsonRequestBehavior.AllowGet); }
            else
            { return Json(new { status = "error", data = sarpanch }, JsonRequestBehavior.AllowGet); }
            
        }

        #region CastWisePopulation

        [HttpGet]
        public ActionResult GetVillagePopulation(int VillID)
        {
            VillageProfileRepo villRepo = new VillageProfileRepo();
            var result = villRepo.getVillCastWisePopulation(VillID);
            if (result != null)
                return Json(new { status = "Success", data = result }, JsonRequestBehavior.AllowGet);
            else
                return Json(new { status = "error", data = "" }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult InsertVillagePopulation(VILL_CASTE VillPopulation)
        {

            VillageProfileRepo villRepo = new VillageProfileRepo();
            var result = villRepo.InsertVillCastWisePopulation(VillPopulation);

            if (result)
                return Json(new { status = "Success", data = "" }, JsonRequestBehavior.AllowGet);
            else
                return Json(new { status = "error", data = "" }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult UpdateVillagePopulation(VILL_CASTE VillPopulation)
        {

            VillageProfileRepo villRepo = new VillageProfileRepo();
            var result = villRepo.UpdateVillCastWisePopulation(VillPopulation);

            if (result)
                return Json(new { status = "Success", data = "" }, JsonRequestBehavior.AllowGet);
            else
                return Json(new { status = "error", data = "" }, JsonRequestBehavior.AllowGet);
        }
        #endregion

    }
}