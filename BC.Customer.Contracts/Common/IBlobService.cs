using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BC.Customer.Contracts.Common
{
    public interface IBlobService
    {
        Task<Uri> UploadFileBlobAsync(string blobContainerName, Stream content, string contentType, string fileName);
        Task<Tuple<string, string>> UploadProfilePicture(IFormFile file);
        void DeleteFileBlobAsync(string fileName);

    }
}
