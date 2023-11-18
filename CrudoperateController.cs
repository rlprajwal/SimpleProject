using CRUDoperationWebApplication.DataAccessLayer;
using CRUDoperationWebApplication.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace CRUDoperationWebApplication.Controllers
{
    public class CrudoperateController : Controller
    {
        // GET: Crudoperate
        static string statusMsg = string.Empty;
        readonly private Crudoperationdatalayer CODL = new Crudoperationdatalayer();


        [HttpGet]
        public ActionResult CrudPage()
        {
            crudModel model = new crudModel();
            if (!string.IsNullOrEmpty(statusMsg))
            {
                ViewBag.message = statusMsg;
                statusMsg = string.Empty;
            }
            else if (string.IsNullOrEmpty(statusMsg))
            {
                ViewBag.message = null;
                statusMsg = string.Empty;
            }
            model.getcrudDetailslist = CODL.SelectAllCrudData();
            model.getcrudDetailslist2 = null;
            return View(model);
        }

        [HttpPost]
        public ActionResult CrudPage1(crudModel Model)
        {
            crudModel model = new crudModel();
            try
            {
                string Name;
                string filename;
                string email;
                HttpPostedFileBase file = Model.FileAttach;
                model.filesize = 5000;
                int maxcontentlength = 1024 * 5000;
                DateTime currentDate = DateTime.Now;
                Name = Model.Name;
                email = Model.EmailID;
                Model.Birthdate = Convert.ToDateTime(Model.DOB);

                var supportedTypes = new[] { "jpg", "jpeg", "png" };
                var fileExt = System.IO.Path.GetExtension(file.FileName).Substring(1);
                if (!supportedTypes.Contains(fileExt))
                {
                    statusMsg = "File Extension Is InValid - Only Upload WORD/PDF/EXCEL/TXT File";
                    return RedirectToAction("CrudPage", "Crudoperate");
                }
                else if (file.ContentLength > (model.filesize * 1024))
                {
                    statusMsg = "File size Should Be UpTo " + model.filesize + "KB";
                    return RedirectToAction("CrudPage", "Crudoperate");
                }

                if (file.ContentLength <= maxcontentlength)
                {
                    if (Request.Browser.Browser.ToUpper().Equals("IE") || Request.Browser.Browser.ToUpper().Equals("INTERNETEXPLORER"))
                    {
                        string[] testfiles = file.FileName.Split(new char[] { '\\' });
                        filename = testfiles[testfiles.Length - 1];
                    }
                    else
                    {
                        filename = file.FileName;
                    }
                    FileInfo fi = new FileInfo(filename);
                    string ext = fi.Extension;
                    Model.Filename = filename;

                    if (ext.Equals(".jpeg") || ext.Equals(".jpg"))
                    {
                        string fileDesc = "~/App_Data/CrudPage/" + Model.CustomerID + "/" + filename + "";
                        string filePath = Server.MapPath("~/App_Data/CrudPage/" + Model.CustomerID + "/");
                        if (!System.IO.Directory.Exists(filePath))
                        {
                            System.IO.Directory.CreateDirectory(filePath);
                        }
                        filename = Path.Combine(filePath, filename);
                        file.SaveAs(filename);
                        Model.FilePath = fileDesc;
                    }
                    else
                    {
                        statusMsg = "Please upload only .pdf and .jpg";
                        return RedirectToAction("CrudPage", "Crudoperate");
                    }
                }

                string result = CODL.InsertCrudOperation(Model);
                if(result == "Insert")
                {
                    statusMsg = "Data Inserted Successfully";
                }
                else if(result == "")
                {
                    statusMsg = "Data not Inserted Successfully";
                }
                ViewBag.message = statusMsg;
                return RedirectToAction("ShowAllCrudDetails", "Crudoperate");
            }
            catch (Exception ex)
            {
                if (ViewBag.message == null)
                {
                    ViewBag.message = ex.Message;
                }
                string msg = ex.Message;                
                return View("ExceptionView");
            }
        }

        [HttpGet]
        public ActionResult ShowAllCrudDetails()
        {
            crudModel model = new crudModel();
            try
            {
                model.getcrudDetailslist = CODL.SelectAllCrudData();
                if (!string.IsNullOrEmpty(statusMsg))
                {
                    ViewBag.message = statusMsg;
                    statusMsg = string.Empty;
                }
                return View("CrudPage", model);
            }
            catch (Exception ex)
            {
                if (ViewBag.message == null)
                {
                    ViewBag.message = ex.Message;
                }
                string msg = ex.Message;
                return View("ExceptionView");
            }            
        }

        public ActionResult GetImage(int Id, string Path)
        {

            string strPath = string.Empty;
            try
            {
                if(Path != "")
                {
                    strPath = Server.MapPath(Path);
                    FileStream fs = new FileStream(strPath, FileMode.Open, FileAccess.Read);
                    byte[] rawByte = new byte[fs.Length];
                    fs.Read(rawByte, 0, Convert.ToInt32(fs.Length));
                    return File(rawByte, "image");
                }                
                else
                {
                    statusMsg = "jpg is not uploaded.";
                    return RedirectToAction("CrudPage", "Crudoperate");
                }
            }
            catch (Exception ex)
            {
                if (ViewBag.message == null)
                {
                    ViewBag.message = ex.Message;
                }
                string msg = ex.Message;                
                return View("ExceptionView");

            }

        }

        [HttpGet]
        public ActionResult CrudColumnDetails(int Id)
        {
            crudModel model = new crudModel();
            try
            {
                model.getcrudDetailslist = CODL.SelectAllCrudData();
                model.getcrudDetailslist1 = CODL.SelectCrudData1(Id);
                ViewBag.count = "1";
                return View("CrudPage", model);
            }
            catch (Exception ex)
            {
                if (ViewBag.message == null)
                {
                    ViewBag.message = ex.Message;
                }
                string msg = ex.Message;
                return View("ExceptionView");
            }
        }

        [HttpGet]
        public ActionResult ModifyCrudDetails(int Id)
        {
            crudModel model = new crudModel();
            try
            {
                model.getcrudDetailslist2 = CODL.SelectCrudData1(Id);                
                return View("CrudPage", model);
            }
            catch (Exception ex)
            {
                if (ViewBag.message == null)
                {
                    ViewBag.message = ex.Message;
                }
                string msg = ex.Message;
                return View("ExceptionView");
            }
        }

        [HttpPost]
        public ActionResult SaveChanges(crudModel Model)
        {
            crudModel model = new crudModel();
            string result = string.Empty;
            try
            {
                return View("CrudPage", model);
            }
            catch (Exception ex)
            {
                if (ViewBag.message == null)
                {
                    ViewBag.message = ex.Message;
                }
                string msg = ex.Message;
                return View("ExceptionView");
            }
        }

        [HttpPost]
        public JsonResult SaveEditChanges(crudModel Model)
        {
            crudModel model = new crudModel();
            string result = string.Empty;
            try
            {
                string Name;
                string filename;
                string email;
              
                HttpPostedFileBase file = Model.FileAttach;
                
                model.filesize = 5000;
                int maxcontentlength = 1024 * 5000;
                DateTime currentDate = DateTime.Now;
                Name = Model.Name;
                email = Model.EmailID;
                Model.Birthdate = Convert.ToDateTime(Model.Birthdate);

                if(Model.FilePath != null)
                {
                    if(file != null && file.ContentLength > 0)
                    {
                        Model.FilePath = null;
                        Model.Filename = null;
                        var supportedTypes = new[] { "jpg", "jpeg", "png" };
                        var fileExt = System.IO.Path.GetExtension(file.FileName).Substring(1);
                        if (!supportedTypes.Contains(fileExt))
                        {
                            statusMsg = "File Extension Is InValid - Only Upload WORD/PDF/EXCEL/TXT File";
                            return Json(statusMsg, JsonRequestBehavior.AllowGet);
                        }
                        else if (file.ContentLength > (model.filesize * 1024))
                        {
                            statusMsg = "File size Should Be UpTo " + model.filesize + "KB";
                            return Json(statusMsg, JsonRequestBehavior.AllowGet);
                        }
                        if (file.ContentLength <= maxcontentlength)
                        {
                            if (Request.Browser.Browser.ToUpper().Equals("IE") || Request.Browser.Browser.ToUpper().Equals("INTERNETEXPLORER"))
                            {
                                string[] testfiles = file.FileName.Split(new char[] { '\\' });
                                filename = testfiles[testfiles.Length - 1];
                            }
                            else
                            {
                                filename = file.FileName;
                            }
                            FileInfo fi = new FileInfo(filename);
                            string ext = fi.Extension;
                            Model.Filename = filename;

                            if (ext.Equals(".jpeg") || ext.Equals(".jpg") || ext.Equals(".png"))
                            {
                                string fileDesc = "~/App_Data/CrudPage/" + Model.CustomerID + "/" + filename + "";
                                string filePath = Server.MapPath("~/App_Data/CrudPage/" + Model.CustomerID + "/");
                                if (!System.IO.Directory.Exists(filePath))
                                {
                                    System.IO.Directory.CreateDirectory(filePath);
                                }
                                filename = Path.Combine(filePath, filename);
                                file.SaveAs(filename);
                                Model.FilePath = fileDesc;
                            }
                            else
                            {
                                statusMsg = "Please upload only .jpeg, .png and .jpg";
                                return Json(statusMsg, JsonRequestBehavior.AllowGet);
                            }
                        }

                        result = CODL.UpdateCrudOperationDetails(Model);

                    }
                    else if (file == null || file.ContentLength == 0 && Model.FilePath != null && Model.Filename != null)
                    {
                        result = CODL.UpdateCrudOperationDetails(Model);
                    }                       
                }
                
                if (result == "Updated")
                {
                    statusMsg = "Data Updated Successfully";
                }
                else if (result == "")
                {
                    statusMsg = "Data not Updated Successfully";
                }
                return Json(statusMsg, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                if (ViewBag.message == null)
                {
                    ViewBag.message = ex.Message;
                }
                string msg = ex.Message;
                return Json("ExceptionView");
            }
        }

        public ActionResult DeleteUserDetails(int UserId)
        {
            crudModel model = new crudModel();
            int result1 = 0;
            try
            {
                string result = CODL.DeleteCrudOperationDetails(UserId);
                if(result != "")
                {
                    result1 = 1;
                }

                if(result1 == 1)
                {
                    statusMsg = "Data Deleted Successfully";
                }
                else
                {
                    statusMsg = "Data not Deleted Successfully";
                } 
                ViewBag.message = statusMsg;
                return View("CrudPage", model);
            }
            catch (Exception ex)
            {
                if (ViewBag.message == null)
                {
                    ViewBag.message = ex.Message;
                }
                string msg = ex.Message;
                return Json("ExceptionView");
            }
        }
    }
}