using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Web;
using newFace.Server.Utility;
using newFace.Shared.Models.Resource;
using newFace.Shared.Repositories.Resource;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting.Internal;
using Xabe.FFmpeg;

namespace newFace.Server.Services.Resource
{
    public class FileRepository : IFileRepository
    {
        HostingEnvironment HostingEnvironment=new HostingEnvironment();
        public ResultFile RemoveFile(string FileAddress)
        {
            ResultFile Result = new ResultFile();
            try
            {

                if (FileAddress != null && FileAddress.Length > 0)
                {
                    string fullPath = Path.Combine(Directory.GetCurrentDirectory(), FileAddress);/*HostingEnvironment.MapPath("~" + FileAddress)*/;
                    if (System.IO.File.Exists(fullPath))
                    {
                        System.IO.File.Delete(fullPath);

                        #region Delete All Size From Image
                        string[] filesizes = { "S50x50", "S150x150", "S250x250", "S350x350", "S450x450" };
                        var fileName = Path.GetFileName(FileAddress);
                        foreach (var itemName in filesizes)
                        {
                            fullPath = Path.Combine(Directory.GetCurrentDirectory(), FileAddress.Replace(fileName, itemName + fileName));
                            if (System.IO.File.Exists(fullPath))
                            {
                                System.IO.File.Delete(fullPath);
                            }
                        }

                        #endregion
                        Result.Statue = Enums.Statue.Success;
                        Result.Message = "با موفقیت حذف شد";
                        return Result;
                    }
                    else
                    {
                        Result.Statue = Enums.Statue.Failure;
                        Result.Message = "فایل وجود ندارد";
                        return Result;
                    }
                }
                else
                {
                    Result.Statue = Enums.Statue.Failure;
                    Result.Message = "آدرس فایل صحیح نیست";
                    return Result;
                }


            }
            catch (System.Exception e)
            {
                Result.Statue = Enums.Statue.Failure;
                Result.Message = e.Message;
                return Result;
            }

        }

        public ResultFile SaveFileApi(IFormFile Apifile, string UploadFolderName, string MaxFileSizeMegabyte, bool Resizable = false, bool IgnoreChangeImageResulation = false, ChunkMetaData chunkMetaData = null)
        {
            ResultFile Result = new ResultFile();
            try
            {

                if (Apifile != null)
                {
                    int FileSizebyte = int.Parse(MaxFileSizeMegabyte) * 1024 * 1024;
                    if (Apifile.Length > FileSizebyte)
                    {
                        Result.Statue = Enums.Statue.Failure;
                        Result.Message = "حجم فایل بسیار زیاد است حجم مجاز ( " + MaxFileSizeMegabyte + " مگابایت )!!!";
                        return Result;
                    }
                    string FileFolder = "";
                    if (Apifile.ContentType.Contains("image/"))
                    {
                        FileFolder = "Image";
                    }
                    else if (Apifile.ContentType.Contains("video/"))
                    {
                        FileFolder = "Video";
                    }
                    else if (Apifile.ContentType.Contains("audio/"))
                    {
                        FileFolder = "Audio";
                    }
                    else if (Apifile.ContentType.Contains("application/msword") || Apifile.ContentType.Contains("application/pdf") || Apifile.ContentType.Contains("application/vnd.openxmlformats-officedocument.wordprocessingml.document"))
                    {
                        FileFolder = "Document";
                    }
                    else
                    {
                        FileFolder = "File";
                    }

                    Random rand = new Random();
                    string extension = System.IO.Path.GetExtension(Apifile.FileName).ToLower();

                    string fileName = Guid.NewGuid().ToString().Substring(0, 12) + $"{DateTime.Now:yyyy-MM-dd_HH-mm-ssfff}" + rand.Next(1, 1000) + extension;

                    string path = AppDomain.CurrentDomain.BaseDirectory + "Content/Upload/" + UploadFolderName + "/" + FileFolder;
                    string physicalPath = Path.Combine(path, fileName);
                    List<string> FilePathList = new List<string>();
                    string FilePath = "";

                    if (FileFolder == "Image")
                    {
                        Stream stream = Apifile.OpenReadStream();
                        Image image = Image.FromStream(stream);

                        image.ImageExifRotate();

                        var width = image.Width;
                        var height = image.Height;

                        int sourceWidth = image.Width;
                        int sourceHeight = image.Height;

                        float nPercent = 0;
                        float nPercentW = 0;
                        float nPercentH = 0;

                        int destWidth = 0;
                        int destHeight = 0;

                        HelperClass helperClass = new HelperClass();

                        for (int i = 50; i < 500; i += 100)
                        {
                            Image newImage;
                            if (IgnoreChangeImageResulation)
                            {
                                physicalPath = Path.Combine(path, "S" + i + "x" + i + fileName);
                                //var newImage = helperClass.resizeImage(Image.FromStream(Apifile.InputStream, true, true), new Size(destWidth, destHeight));

                                newImage = (Image)(new Bitmap(image, new Size(i, i)));
                            }
                            else
                            {
                                nPercentW = i / (float)sourceWidth;
                                nPercentH = i / (float)sourceHeight;

                                if (nPercentH < nPercentW)
                                    nPercent = nPercentH;
                                else
                                    nPercent = nPercentW;

                                destWidth = (int)(sourceWidth * nPercent);
                                destHeight = (int)(sourceHeight * nPercent);

                                physicalPath = Path.Combine(path, "S" + i + "x" + i + fileName);
                                //var newImage = helperClass.resizeImage(Image.FromStream(Apifile.InputStream, true, true), new Size(destWidth, destHeight));

                                newImage = (Image)(new Bitmap(image, new Size(destWidth, destHeight)));
                            }
                            newImage.Save(physicalPath);
                            FilePathList.Add("/Content/Upload/" + UploadFolderName + "/" + FileFolder + "/" + "S" + i + "x" + i + fileName);
                        }

                        physicalPath = Path.Combine(path, fileName);
                        image.Save(physicalPath);
                        FilePath = "/Content/Upload/" + UploadFolderName + "/" + FileFolder + "/" + fileName;
                    }

                    if (FileFolder != "Image")
                    {
                        physicalPath = Path.Combine(path, fileName);

                        using (var fileStream = new FileStream(physicalPath, FileMode.Create))
                        {
                            Apifile.CopyTo(fileStream);
                        }
                        FilePath = "/Content/Upload/" + UploadFolderName + "/" + FileFolder + "/" + fileName;
                    }

                    Result.Statue = Enums.Statue.Success;
                    Result.Message = "فایل با موفقیت ذخیره شد";
                    Result.FilePath = FilePath;
                    Result.FileType = FileFolder;
                    Result.ResizeFilePaths = FilePathList;
                    Result.FileName = Apifile.FileName;
                    Result.FileSize = Convert.ToInt32(Apifile.Length / 1024);

                    if (FileFolder == "Video")
                    {
                        Result.VideoThumbnail = GetVideoThumbnail(FilePath);
                    }

                    return Result;


                }
                else
                {
                    Result.Statue = Enums.Statue.Failure;
                    Result.Message = "فایل صحیح نیست";
                    return Result;
                }



            }
            catch (System.Exception e)
            {
                Result.Statue = Enums.Statue.Failure;
                Result.Message = e.Message;
                return Result;

            }

        }

        public ResultFile SaveFile(IFormFile file, string UploadFolderName, string MaxFileSizeMegabyte, bool Resizable = false, bool IgnoreChangeImageResulation = false, ChunkMetaData chunkMetaData = null)
        {
            ResultFile Result = new ResultFile();
            try
            {
                if (file != null)
                {
                    int FileSizebyte = int.Parse(MaxFileSizeMegabyte) * 1024 * 1024;
                    if (file.Length > FileSizebyte)
                    {
                        Result.Statue = Enums.Statue.Failure;
                        Result.Message = "حجم فایل بسیار زیاد است حجم مجاز ( " + MaxFileSizeMegabyte + " مگابایت )!!!";
                        return Result;
                    }
                    string FileFolder = "";
                    if (file.ContentType.Contains("image/"))
                    {
                        FileFolder = "Image";
                    }
                    else if (file.ContentType.Contains("video/"))
                    {
                        FileFolder = "Video";
                    }
                    else if (file.ContentType.Contains("audio/"))
                    {
                        FileFolder = "Audio";
                    }
                    else if (file.ContentType.Contains("application/msword") || file.ContentType.Contains("application/pdf") || file.ContentType.Contains("application/vnd.openxmlformats-officedocument.wordprocessingml.document"))
                    {
                        FileFolder = "Document";
                    }
                    else
                    {
                        FileFolder = "File";
                    }

                    Random rand = new Random();
                    string extension = System.IO.Path.GetExtension(file.FileName).ToLower();

                    string fileName = Guid.NewGuid().ToString().Substring(0, 12) + $"{DateTime.Now:yyyy-MM-dd_HH-mm-ssfff}" + rand.Next(1, 1000) + extension;

                    string path = AppDomain.CurrentDomain.BaseDirectory + "Content/Upload/" + UploadFolderName + "/" + FileFolder;
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }


                    string physicalPath = Path.Combine(path, fileName);
                    List<string> FilePathList = new List<string>();
                    string FilePath = "";

                    if (FileFolder == "Image" && Resizable)
                    {
                        Stream stream = file.OpenReadStream();
                        Image image = Image.FromStream(stream);

                        image.ImageExifRotate();

                        var width = image.Width;
                        var height = image.Height;

                        int sourceWidth = image.Width;
                        int sourceHeight = image.Height;

                        float nPercent = 0;
                        float nPercentW = 0;
                        float nPercentH = 0;

                        int destWidth = 0;
                        int destHeight = 0;

                        HelperClass helperClass = new HelperClass();

                        //-------------------------------------------------------------
                        //physicalPath = Path.Combine(path, "S50x50" + fileName);
                        //var image50x50 = helperClass.resizeImage(Image.FromStream(file.InputStream, true, true), new Size((int)(50 * ratio), (int)(50 * ratio)));
                        //image50x50.Save(physicalPath);
                        //FilePathList.Add("/Content/Upload/" + UploadFolderName + "/" + FileFolder + "/" + "S50x50" + fileName);
                        ////-------------------------------------------------------------
                        //physicalPath = Path.Combine(path, "S128x128" + fileName);
                        //var image128x128 = helperClass.resizeImage(Image.FromStream(file.InputStream, true, true), new Size((int)(128 * ratio), (int)(128 * ratio)));
                        //image128x128.Save(physicalPath);
                        //FilePathList.Add("/Content/Upload/" + UploadFolderName + "/" + FileFolder + "/" + "S128x128" + fileName);
                        ////-------------------------------------------------------------
                        //physicalPath = Path.Combine(path, "S200x200" + fileName);
                        //var image200x200 = helperClass.resizeImage(Image.FromStream(file.InputStream, true, true), new Size((int)(200 * ratio), (int)(200 * ratio)));
                        //image200x200.Save(physicalPath);
                        //FilePathList.Add("/Content/Upload/" + UploadFolderName + "/" + FileFolder + "/" + "S200x200" + fileName);
                        //-------------------------------------------------------------

                        for (int i = 50; i < 500; i += 100)
                        {
                            Image newImage;
                            if (IgnoreChangeImageResulation)
                            {
                                physicalPath = Path.Combine(path, "S" + i + "x" + i + fileName);
                                //var newImage = helperClass.resizeImage(Image.FromStream(Apifile.InputStream, true, true), new Size(destWidth, destHeight));

                                newImage = (Image)(new Bitmap(image, new Size(i, i)));
                            }
                            else
                            {
                                nPercentW = i / (float)sourceWidth;
                                nPercentH = i / (float)sourceHeight;

                                if (nPercentH < nPercentW)
                                    nPercent = nPercentH;
                                else
                                    nPercent = nPercentW;

                                destWidth = (int)(sourceWidth * nPercent);
                                destHeight = (int)(sourceHeight * nPercent);

                                physicalPath = Path.Combine(path, "S" + i + "x" + i + fileName);
                                //var newImage = helperClass.resizeImage(Image.FromStream(Apifile.InputStream, true, true), new Size(destWidth, destHeight));

                                newImage = (Image)(new Bitmap(image, new Size(destWidth, destHeight)));
                            }

                            newImage.Save(physicalPath);
                            FilePathList.Add("/Content/Upload/" + UploadFolderName + "/" + FileFolder + "/" + "S" + i + "x" + i + fileName);
                        }

                        //physicalPath = Path.Combine(path, fileName);
                        //image.Save(physicalPath);
                        //FilePath = "/Content/Upload/" + UploadFolderName + "/" + FileFolder + "/" + fileName;
                    }

                    //if (FileFolder != "Image")
                    //{
                    physicalPath = Path.Combine(path, fileName);
                    using (var fileStream = new FileStream(physicalPath, FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }
                    FilePath = "/Content/Upload/" + UploadFolderName + "/" + FileFolder + "/" + fileName;
                    //}

                    Result.Statue = Enums.Statue.Success;
                    Result.Message = "فایل با موفقیت ذخیره شد";
                    Result.FilePath = FilePath;
                    Result.FileType = FileFolder;
                    Result.ResizeFilePaths = FilePathList;
                    Result.FileName = file.FileName;
                    Result.FileSize = Convert.ToInt32(file.Length / 1024);

                    if (FileFolder == "Video")
                    {
                        Result.VideoThumbnail = GetVideoThumbnail(FilePath);
                    }

                    return Result;


                }
                else
                {
                    Result.Statue = Enums.Statue.Failure;
                    Result.Message = "فایل صحیح نیست";
                    return Result;
                }



            }
            catch (System.Exception e)
            {
                Result.Statue = Enums.Statue.Failure;
                Result.Message = e.Message;
                return Result;

            }

        }

        public ResultFile SaveFile(IFormFile file, string fileBase64, string UploadFolderName, string MaxFileSizeMegabyte, bool Resizable = false, bool IgnoreChangeImageResulation = false, ChunkMetaData chunkMetaData = null)
        {
            ResultFile Result = new ResultFile();
            try
            {
                bool createhttppost = false;
                if (file == null)
                {
                    string base64 = fileBase64.Substring(fileBase64.IndexOf(',') + 1);
                    base64 = base64.Trim('\0');
                    byte[] fileBytes = Convert.FromBase64String(base64);

                    file = (IFormFile)new MemoryPostedFile(fileBytes);
                    createhttppost = true;
                }
                if (file != null)
                {
                    int FileSizebyte = int.Parse(MaxFileSizeMegabyte) * 1024 * 1024;
                    if (file.Length > FileSizebyte)
                    {
                        Result.Statue = Enums.Statue.Failure;
                        Result.Message = "حجم فایل بسیار زیاد است حجم مجاز ( " + MaxFileSizeMegabyte + " مگابایت )!!!";
                        return Result;
                    }
                    string FileFolder = "";
                    if (createhttppost)
                    {
                        FileFolder = "Image";
                    }
                    else
                    {
                        if (file.ContentType.Contains("image/"))
                        {
                            FileFolder = "Image";
                        }
                        else if (file.ContentType.Contains("video/"))
                        {
                            FileFolder = "Video";
                        }
                        else if (file.ContentType.Contains("audio/"))
                        {
                            FileFolder = "Audio";
                        }
                        else if (file.ContentType.Contains("application/msword") || file.ContentType.Contains("application/pdf") || file.ContentType.Contains("application/vnd.openxmlformats-officedocument.wordprocessingml.document"))
                        {
                            FileFolder = "Document";
                        }
                        else
                        {
                            FileFolder = "File";
                        }
                    }


                    Random rand = new Random();
                    string extension = "";
                    string fileName = "";
                    if (!createhttppost)
                    {
                        extension = System.IO.Path.GetExtension(file.FileName).ToLower();
                    }
                    else
                    {
                        extension = ".png";

                    }

                    string base64 = fileBase64.Substring(fileBase64.IndexOf(',') + 1);
                    base64 = base64.Trim('\0');
                    byte[] fileBytes = Convert.FromBase64String(base64);

                    Stream stream = new MemoryStream(fileBytes);
                    Image image2 = Image.FromStream(stream);
                    fileName = Guid.NewGuid().ToString().Substring(0, 12) + $"{DateTime.Now:yyyy-MM-dd_HH-mm-ssfff}" + rand.Next(1, 1000) + extension;

                    string path = AppDomain.CurrentDomain.BaseDirectory + "Content/Upload/" + UploadFolderName + "/" + FileFolder;
                    string physicalPath = Path.Combine(path, fileName);
                    List<string> FilePathList = new List<string>();
                    string FilePath = "";

                    if (FileFolder == "Image" && Resizable)
                    {

                        Image image = Image.FromStream(stream);

                        image.ImageExifRotate();

                        var width = image.Width;
                        var height = image.Height;

                        int sourceWidth = image.Width;
                        int sourceHeight = image.Height;

                        float nPercent = 0;
                        float nPercentW = 0;
                        float nPercentH = 0;

                        int destWidth = 0;
                        int destHeight = 0;

                        HelperClass helperClass = new HelperClass();

                        //-------------------------------------------------------------
                        //physicalPath = Path.Combine(path, "S50x50" + fileName);
                        //var image50x50 = helperClass.resizeImage(Image.FromStream(file.InputStream, true, true), new Size((int)(50 * ratio), (int)(50 * ratio)));
                        //image50x50.Save(physicalPath);
                        //FilePathList.Add("/Content/Upload/" + UploadFolderName + "/" + FileFolder + "/" + "S50x50" + fileName);
                        ////-------------------------------------------------------------
                        //physicalPath = Path.Combine(path, "S128x128" + fileName);
                        //var image128x128 = helperClass.resizeImage(Image.FromStream(file.InputStream, true, true), new Size((int)(128 * ratio), (int)(128 * ratio)));
                        //image128x128.Save(physicalPath);
                        //FilePathList.Add("/Content/Upload/" + UploadFolderName + "/" + FileFolder + "/" + "S128x128" + fileName);
                        ////-------------------------------------------------------------
                        //physicalPath = Path.Combine(path, "S200x200" + fileName);
                        //var image200x200 = helperClass.resizeImage(Image.FromStream(file.InputStream, true, true), new Size((int)(200 * ratio), (int)(200 * ratio)));
                        //image200x200.Save(physicalPath);
                        //FilePathList.Add("/Content/Upload/" + UploadFolderName + "/" + FileFolder + "/" + "S200x200" + fileName);
                        //-------------------------------------------------------------

                        for (int i = 50; i < 500; i += 100)
                        {
                            Image newImage;
                            if (IgnoreChangeImageResulation)
                            {
                                physicalPath = Path.Combine(path, "S" + i + "x" + i + fileName);
                                //var newImage = helperClass.resizeImage(Image.FromStream(Apifile.InputStream, true, true), new Size(destWidth, destHeight));

                                newImage = (Image)(new Bitmap(image, new Size(i, i)));
                            }
                            else
                            {
                                nPercentW = i / (float)sourceWidth;
                                nPercentH = i / (float)sourceHeight;

                                if (nPercentH < nPercentW)
                                    nPercent = nPercentH;
                                else
                                    nPercent = nPercentW;

                                destWidth = (int)(sourceWidth * nPercent);
                                destHeight = (int)(sourceHeight * nPercent);

                                physicalPath = Path.Combine(path, "S" + i + "x" + i + fileName);
                                //var newImage = helperClass.resizeImage(Image.FromStream(Apifile.InputStream, true, true), new Size(destWidth, destHeight));

                                newImage = (Image)(new Bitmap(image, new Size(destWidth, destHeight)));
                            }

                            newImage.Save(physicalPath);
                            FilePathList.Add("/Content/Upload/" + UploadFolderName + "/" + FileFolder + "/" + "S" + i + "x" + i + fileName);
                        }

                        //physicalPath = Path.Combine(path, fileName);
                        //image.Save(physicalPath);
                        //FilePath = "/Content/Upload/" + UploadFolderName + "/" + FileFolder + "/" + fileName;
                    }

                    //if (FileFolder != "Image")
                    //{
                    physicalPath = Path.Combine(path, fileName);


                    image2.Save(physicalPath);

                    FilePath = "/Content/Upload/" + UploadFolderName + "/" + FileFolder + "/" + fileName;
                    //}

                    Result.Statue = Enums.Statue.Success;
                    Result.Message = "فایل با موفقیت ذخیره شد";
                    Result.FilePath = FilePath;
                    Result.FileType = FileFolder;
                    Result.ResizeFilePaths = FilePathList;
                    Result.FileName = file.FileName;
                    Result.FileSize = Convert.ToInt32(file.Length / 1024);

                    if (FileFolder == "Video")
                    {
                        Result.VideoThumbnail = GetVideoThumbnail(FilePath);
                    }

                    return Result;


                }
                else
                {
                    Result.Statue = Enums.Statue.Failure;
                    Result.Message = "فایل صحیح نیست";
                    return Result;
                }



            }
            catch (System.Exception e)
            {
                Result.Statue = Enums.Statue.Failure;
                Result.Message = e.Message;
                return Result;

            }

        }

        public string GetVideoThumbnail(string videoUrl)
        {

            var fileName = Guid.NewGuid().ToString().Substring(0, 12) + String.Format("{0:yyyy-MM-dd_HH-mm-ssfff}", DateTime.Now) + ".jpg";
            var fileLocation = "/Content/Upload/Thumbnail/";

            string path = AppDomain.CurrentDomain.BaseDirectory;

            string videoLocation = Path.Combine(path, videoUrl);
            string tumbnailLocation = Path.Combine(path + fileLocation, fileName);
            IMediaInfo mediaInfo = MediaInfo.Get(videoLocation).Result;
            Conversion.Snapshot(videoLocation, tumbnailLocation, TimeSpan.FromMinutes(0.2));
            //var ffMpeg = new NReco.VideoConverter.FFMpegConverter();
            //ffMpeg.GetVideoThumbnail(path + videoUrl, tumbnailLocation, 2);

            return fileLocation + fileName;

        }


    }
}