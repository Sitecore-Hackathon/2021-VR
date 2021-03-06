using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VRBYOD.Project.MLHost.Rendering.Models
{
    public class ImageDetectioModel
    {
        public IFormFile imageFile { get; set; }
        public string modelName { get; set; }
    }
}
