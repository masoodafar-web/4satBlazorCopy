using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Web;
using newFace.Shared.Models.Resource;
using Microsoft.AspNetCore.Http;


namespace newFace.Shared.Repositories.Resource
{
    public interface IFileRepository
    {
        ResultFile SaveFileApi(IFormFile Apifile, string UploadFolderName, string MaxFileSizeMb, bool Resizable = false, bool IgnoreChangeImageResulation = false, ChunkMetaData chunkMetaData = null);
        ResultFile RemoveFile(string FileAddress);
        ResultFile SaveFile(IFormFile File, string UploadFolderName, string MaxFileSizeMb, bool Resizable = false, bool IgnoreChangeImageResulation = false, ChunkMetaData chunkMetaData = null);
        ResultFile SaveFile(IFormFile file, string fileBase64, string UploadFolderName, string MaxFileSizeMegabyte, bool Resizable = false, bool IgnoreChangeImageResulation = false, ChunkMetaData chunkMetaData = null);

        string GetVideoThumbnail(string videoUrl);
    }
    [DataContract]
    public class ChunkMetaData
    {
        [DataMember(Name = "uploadUid")]
        public string UploadUid { get; set; }
        [DataMember(Name = "fileName")]
        public string FileName { get; set; }
        [DataMember(Name = "relativePath")]
        public string RelativePath { get; set; }
        [DataMember(Name = "contentType")]
        public string ContentType { get; set; }
        [DataMember(Name = "chunkIndex")]
        public long ChunkIndex { get; set; }
        [DataMember(Name = "totalChunks")]
        public long TotalChunks { get; set; }
        [DataMember(Name = "totalFileSize")]
        public long TotalFileSize { get; set; }
    }
}
