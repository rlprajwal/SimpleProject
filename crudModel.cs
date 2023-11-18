using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Xunit;

namespace CRUDoperationWebApplication.Models
{
    public class crudModel
    {
        public int CustomerID { get; set; }

        [Required(ErrorMessage = "Enter Your Name")]
        [StringLength(8, ErrorMessage = "Name should not be less than or equal to 4 characters.")]
        public string Name { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Enter Your DOB.")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        [CRUDoperationWebApplication.Models.CustomValidationAttributeDemo.ValidBirthDate(ErrorMessage = "Birth Date can not be greater than current date")]
        public DateTime Birthdate { get; set; }

        public string DOB { get; set; }

        [Required(ErrorMessage = "Enter Your EmailID")]
        [RegularExpression(@"^[\w-\._\+%]+@(?:[\w-]+\.)+[\w]{2,6}$", ErrorMessage = "Please enter a valid email address")]
        public string EmailID { get; set; }

        [Required]
        [Display(Name = "Upload File")]
        public HttpPostedFileBase FileAttach { get; set; }

        public decimal filesize { get; set; }
        public string FilePath { get; set; }

        public string Filename { get; set; }

        public List<ImgObj> ImgLst { get; set; }

        public List<crudModel> getcrudDetailslist { get; set; }

        public List<crudModel> getcrudDetailslist1 { get; set; }

        public List<crudModel> getcrudDetailslist2 { get; set; }

    }

    public class ImgObj
    {
        #region Properties  
        
        public int FileId { get; set; }  
        public string FileName { get; set; }        
        public string FileContentType { get; set; }

        #endregion
    }

    public class CustomValidationAttributeDemo
    {
        [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
        public sealed class ValidBirthDate : ValidationAttribute
        {
            protected override ValidationResult IsValid(object value, ValidationContext validationContext)
            {
                if (value != null)
                {
                    DateTime _birthJoin = Convert.ToDateTime(value);
                    if (_birthJoin > DateTime.Now)
                    {
                        return new ValidationResult("Birth date can not be greater than current date.");
                    }
                }
                return ValidationResult.Success;
            }
        }
    }
}