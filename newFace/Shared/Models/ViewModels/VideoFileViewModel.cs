using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Microsoft.AspNetCore.Http;

namespace newFace.Shared.Models.ViewModels
{
    public class VideoFileViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }
        public IFormFile File { get; set; }
    }
}