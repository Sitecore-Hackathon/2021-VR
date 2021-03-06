using VRBYOD.Foundation.CustomVision.Rendering.Helpers;
using VRBYOD.Foundation.CustomVision.Rendering.Service;
using VRBYOD.Project.MLHost.Rendering.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;

namespace VRBYOD.Project.MLHost.Rendering.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageDetectionController : ControllerBase
    {
        private readonly ILogger<ImageDetectionController> _logger;
        private readonly ICustomVisionService _customVisionService;
        private string base64String = string.Empty;

        public ImageDetectionController(ICustomVisionService customVisionService, ILogger<ImageDetectionController> logger)
        {
            _logger = logger;
            _customVisionService = customVisionService;
        }

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [Route("IdentifyObjects")]
        public async Task<IActionResult> IdentifyObjects([FromForm] ImageDetectioModel formData)
        {
            if (formData.imageFile.Length == 0)
                return BadRequest();
            try
            {
                MemoryStream imageMemoryStream = new MemoryStream();
                await formData.imageFile.CopyToAsync(imageMemoryStream);

                //Check that the image is valid
                byte[] imageData = imageMemoryStream.ToArray();
                if (!imageData.IsValidImage())
                    return StatusCode(StatusCodes.Status415UnsupportedMediaType);

                //Convert to Image
                Image image = Image.FromStream(imageMemoryStream);
                _logger.LogInformation($"Start processing image...");

                //Measure execution time
                var watch = System.Diagnostics.Stopwatch.StartNew();
                //Detect the objects in the image                
                var resultImage = await Task.Run(() => _customVisionService.PredictImage(image, imageMemoryStream, formData.modelName));
                var result = ConvertToResult(resultImage);
                //Stop measuring time
                watch.Stop();
                var elapsedMs = watch.ElapsedMilliseconds;
                _logger.LogInformation($"Image processed in {elapsedMs} miliseconds");
                return Ok(result);
            }
            catch (Exception e)
            {
                _logger.LogInformation("Error is: " + e.Message);
                return BadRequest();
            }
        }

        [HttpPost, HttpPut]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [Route("TrainProject")]
        public async Task<IActionResult> TrainProject(string publishModelName)
        {
            await Task.Run(() => _customVisionService.TrainProject(publishModelName));
            return Ok(new { Result = "Training Done!" });
        }

        [HttpDelete]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [Route("DeleteImages")]
        public async Task<IActionResult> DeleteImages()
        {
            await Task.Run(() => _customVisionService.PurgeImageFromProject());
            return Ok(new { Result = "Image Removal Done!" });
        }
        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [Route("GetPublishedModels")]
        public async Task<IActionResult> GetPublishedModels()
        {
            var models = await Task.Run(() => _customVisionService.GetPublishedModels());
            //No Models found
            if (models == null)
                return StatusCode(StatusCodes.Status404NotFound);
            return Ok(new { Result = models });

        }

        #region handlingresult
        private Result ConvertToResult(Image image)
        {
            using (MemoryStream m = new MemoryStream())
            {
                image.Save(m, image.RawFormat);
                byte[] imageBytes = m.ToArray();

                // Convert byte[] to Base64 String
                base64String = Convert.ToBase64String(imageBytes);
                var result = new Result { imageString = base64String };
                return result;
            }
        }

        private class Result
        {
            public string imageString { get; set; }
        }
        #endregion
    }
}
