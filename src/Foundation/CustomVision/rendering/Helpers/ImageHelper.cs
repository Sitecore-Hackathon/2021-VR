using Microsoft.Azure.CognitiveServices.Vision.CustomVision.Prediction.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;

namespace VRBYOD.Foundation.CustomVision.Rendering.Helpers
{
    public static class ImageHelper
    {
        /// <summary>
        /// Helper to draw bounding box in predicted image
        /// </summary>
        /// <param name="image"></param>
        /// <param name="predictions"></param>
        /// <returns></returns>
        public static Image DrawBoundingBox(Image image, List<PredictionModel> predictions)
        {
            var originalHeight = image.Height;
            var originalWidth = image.Width;
            bool checkTop = predictions.ToList().Any(x => x.Probability > 0.7);
            foreach (var prediction in predictions)
            {
                //Let's map only high probability boxes
                if (prediction.Probability < 0.5)
                    continue;

                //// process output boxes
                var x = (uint)(prediction.BoundingBox.Left * originalWidth);
                var y = (uint)(prediction.BoundingBox.Top * originalHeight);
                var width = (uint)(prediction.BoundingBox.Width * originalWidth);
                var height = (uint)(prediction.BoundingBox.Height * originalHeight);

                // fit to current image size
                //x = (uint)originalWidth * x;
                //y = (uint)originalHeight * y;
                //width = (uint)originalWidth * width;
                //height = (uint)originalHeight * height;

                using (Graphics thumbnailGraphic = Graphics.FromImage(image))
                {
                    thumbnailGraphic.CompositingQuality = CompositingQuality.HighQuality;
                    thumbnailGraphic.SmoothingMode = SmoothingMode.HighQuality;
                    thumbnailGraphic.InterpolationMode = InterpolationMode.HighQualityBicubic;


                    // Define Text Options
                    Font drawFont = new Font("Arial", GetFonSize(originalWidth), FontStyle.Bold);
                    SizeF size = thumbnailGraphic.MeasureString($"{prediction.TagName} - Probablity {prediction.Probability}", drawFont);
                    SolidBrush fontBrush = new SolidBrush(Color.Black);
                    Point atPoint = new Point((int)x, (int)y - (int)size.Height - 1);

                    // Define BoundingBox options
                    Pen pen = new Pen(Color.OrangeRed, 3.2f);
                    SolidBrush colorBrush = new SolidBrush(Color.OrangeRed);

                    // Draw text on image 
                    thumbnailGraphic.FillRectangle(colorBrush, (int)x, (int)(y - size.Height - 1), (int)size.Width, (int)size.Height);
                    thumbnailGraphic.DrawString(prediction.TagName, drawFont, fontBrush, atPoint);

                    // Draw bounding box on image
                    thumbnailGraphic.DrawRectangle(pen, x, y, width, height);
                }
            }
            return image;
        }

        private static int GetFonSize(int width)
        {
            if (width < 400) return 10;
            if (width < 600) return 12;
            if (width < 800) return 20;
            return 30;
        }
    }
}
