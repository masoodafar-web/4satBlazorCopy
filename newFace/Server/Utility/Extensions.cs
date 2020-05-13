using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Net.Mime;
using newFace.Shared.Repositories.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;

namespace newFace.Server.Utility
{
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static class QueryableExtensions
    {
        public static PaginatedList<T> ToPaginatedList<T>(this IQueryable<T> query, int pageIndex, int pageSize, int total)
        {
            var list = query.ToList();
            return new PaginatedList<T>(list, pageIndex, pageSize, total);
        }

        public static IQueryable<T> Paginate<T>(this IQueryable<T> query, int pageIndex, int pageSize)
        {
            var entities = query.Skip((pageIndex - 1) * pageSize).Take(pageSize);
            return entities;
        }
    }
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static class ListExtensions
    {
        public static PaginatedList<T> ToPaginatedList<T>(this IList<T> list, int pageIndex, int pageSize, int total)
        {
            return new PaginatedList<T>(list, pageIndex, pageSize, total);
        }
        public static IEnumerable<T> DistinctBy<T>(this IEnumerable<T> list, Func<T, object> propertySelector)
        {
            return list.GroupBy(propertySelector).Select(x => x.First());
        }
      
    }

    public static class AspkarExtensions
    {
        //صفحه بندی
        public static IQueryable<T> Pagination<T>(this IQueryable<T> query, int pageNumber, int pageSize)
        {
            int skip = Math.Max(pageSize * (pageNumber - 1), 0);
            return query.Skip(skip).Take(pageSize);
        }

        //محاسبه سن
        public static int CalculateAge(this DateTime birthday)
        {
            DateTime now = DateTime.Today;
            int age = now.Year - birthday.Year;
            if (birthday > now.AddYears(-age)) age--;

            return age;
        }

        private const int ExifOrientationTagId = 0x112; //274

        public static void ImageExifRotate(this Image image)
        {

            if (Array.IndexOf(image.PropertyIdList, ExifOrientationTagId) > -1)
            {
                int orientation;

                orientation = image.GetPropertyItem(ExifOrientationTagId).Value[0];

                if (orientation >= 1 && orientation <= 8)
                {
                    switch (orientation)
                    {
                        case 2:
                            image.RotateFlip(RotateFlipType.RotateNoneFlipX);
                            break;
                        case 3:
                            image.RotateFlip(RotateFlipType.Rotate180FlipNone);
                            break;
                        case 4:
                            image.RotateFlip(RotateFlipType.Rotate180FlipX);
                            break;
                        case 5:
                            image.RotateFlip(RotateFlipType.Rotate90FlipX);
                            break;
                        case 6:
                            image.RotateFlip(RotateFlipType.Rotate90FlipNone);
                            break;
                        case 7:
                            image.RotateFlip(RotateFlipType.Rotate270FlipX);
                            break;
                        case 8:
                            image.RotateFlip(RotateFlipType.Rotate270FlipNone);
                            break;
                    }

                    image.RemovePropertyItem(ExifOrientationTagId);
                }
            }
        }

        //public static MediaTypeNames.Image RotateImage(this Image img, float rotationAngle)
        //{
        //    //create an empty Bitmap image
        //    Bitmap bmp = new Bitmap(img.Width, img.Height);

        //    //turn the Bitmap into a Graphics object
        //    Graphics gfx = Graphics.FromImage(bmp);

        //    //now we set the rotation point to the center of our image
        //    gfx.TranslateTransform((float)bmp.Width / 2, (float)bmp.Height / 2);

        //    //now rotate the image
        //    gfx.RotateTransform(rotationAngle);

        //    gfx.TranslateTransform(-(float)bmp.Width / 2, -(float)bmp.Height / 2);

        //    //set the InterpolationMode to HighQualityBicubic so to ensure a high
        //    //quality image once it is transformed to the specified size
        //    gfx.InterpolationMode = InterpolationMode.HighQualityBicubic;

        //    //now draw our new image onto the graphics object
        //    gfx.DrawImage(img, new Point(0, 0));

        //    //dispose of our Graphics object
        //    gfx.Dispose();

        //    //return the image
        //    return bmp;
        //}
    }

    //public static class PartialHelper
    //{
    //    public static string RenderPartialViewToString(this Controller controller, string viewName, object model)
    //    {
    //        if (string.IsNullOrEmpty(viewName))
    //        {
    //            viewName = controller.ControllerContext.RouteData.GetRequiredString("action");

    //        }
    //        controller.ViewData.Model = model;

    //        using (var sw = new StringWriter())
    //        {
    //            var viewResult = ViewEngines.Engines.FindPartialView(controller.ControllerContext, viewName);
    //            var viewContext = new ViewContext(controller.ControllerContext, viewResult.View, controller.ViewData, controller.TempData, sw);
    //            viewResult.View.Render(viewContext, sw);
    //            return sw.GetStringBuilder().ToString();
    //        }
    //    }

    //}
  
}