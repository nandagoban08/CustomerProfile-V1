using Azure.Storage.Blobs.Models;
using Azure.Storage.Blobs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using BC.Customer.Contracts.Common;
using Microsoft.AspNetCore.Http;

namespace BC.Customer.Common
{
    public class BlobService : IBlobService
    {
        private readonly BlobServiceClient _blobServiceClient;
        public BlobService(BlobServiceClient blobServiceClient)
        {
            _blobServiceClient = blobServiceClient;
        }
        /// <summary>
        /// delete customer picture
        /// </summary>
        /// <param name="fileName"></param>
        public  void  DeleteFileBlobAsync(string fileName)
        {
            var containerClient = GetContainerClient("uploads");
            var blobClient = containerClient.GetBlobClient(fileName);
            blobClient.Delete(DeleteSnapshotsOption.IncludeSnapshots);
         
        }

        /// <summary>
        /// update customer picture
        /// </summary>
        /// <param name="blobContainerName"></param>
        /// <param name="content"></param>
        /// <param name="contentType"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public async Task<Uri> UploadFileBlobAsync(string blobContainerName, Stream content, string contentType, string fileName)
        {
            var containerClient = GetContainerClient(blobContainerName);
            var blobClient = containerClient.GetBlobClient(fileName);
            await blobClient.UploadAsync(content, new BlobHttpHeaders { ContentType = contentType });
            return blobClient.Uri;
        }

        /// <summary>
        /// Get the Container  
        /// </summary>
        /// <param name="blobContainerName"></param>
        /// <returns></returns>
        private BlobContainerClient GetContainerClient(string blobContainerName)
        {
            var containerClient = _blobServiceClient.GetBlobContainerClient(blobContainerName);
            containerClient.CreateIfNotExists(PublicAccessType.Blob);
            return containerClient;
        }

        /// <summary>
        /// Get the Image URL and the File Name
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public async Task<Tuple<string, string>> UploadProfilePicture(IFormFile file)
        {
   
            if (file == null)
            {
                return null; ;
            }
            string fileName = GenerateFileName(file.FileName);
            var result = await UploadFileBlobAsync(
                    "uploads",
                    file.OpenReadStream(),
                    file.ContentType,
                    fileName);

           return  Tuple.Create(result.AbsoluteUri, fileName);
        }

        /// <summary>
        /// Generate file name for upload picture 
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        private string GenerateFileName(string fileName)
        {
            string strFileName = string.Empty;
            string[] strName = fileName.Split('.');
            strFileName = DateTime.Now.ToUniversalTime().ToString("yyyyMMdd\\THHmmssfff") + "." + strName[strName.Length - 1];
            return strFileName;
        }
    }
}
