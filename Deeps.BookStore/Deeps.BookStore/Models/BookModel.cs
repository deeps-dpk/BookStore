using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Deeps.BookStore.Enums;
using Deeps.BookStore.Data;
using Microsoft.AspNetCore.Http;

namespace Deeps.BookStore.Models
{
    public class BookModel
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Author { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        [Required(ErrorMessage = "Please Choose Langeage")]
        [Display(Name = "Language Name")]
        public int LanguageId { get; set; }
        // [Required(ErrorMessage = "Please Choose Langeage")]
        public string LanguageName { get; set; }

        // [Required(ErrorMessage = "Please choose Multiple Langeages")]
        // public List<string> MultiLanguages { get; set; }
        //  public LanguaeEnum LanguageEnum { get; set; }

        [Required(ErrorMessage = "Please Enter Total Pages")]
        [Display(Name = "TOTAL PAGES")]
        public int? TotalPages { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }

        [Display(Name = "Cover Photo")]
        [Required(ErrorMessage = "Please Choose the Cover Photo")]
        public IFormFile CoverPhoto { get; set; }
        public string CoverImageUrl { get; set; }
        [Display(Name = "Gallery Images")]
        [Required(ErrorMessage = "Please Choose the gallery  Images of your Book")]
        public IFormFileCollection GalleryFiles { get; set; }
        public List<GalleryModel> Gallery { get; set; }
        [Display(Name = "Pdf Book")]
        [Required(ErrorMessage = "Please Choose the Pdf file")]
        public IFormFile BookPdf { get; set; }
        public string BookPdfUrl { get; set; }        

    }
}
