using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Essensausgleich.Data
{
    /// <summary>
    /// A Service that provides the capabiliti to share certain data to other apps
    /// </summary>
    public class DataSharingController : Infra.AppObjekt, IShare
    {
        /// <summary>
        /// Starts the Sharing of a text
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task RequestAsync(ShareTextRequest request)
        {
            Microsoft.Maui.ApplicationModel.DataTransfer.Share.RequestAsync(request);
            return Task.CompletedTask;
        }
        /// <summary>
        /// Starts the Sharing of a File
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task RequestAsync(ShareFileRequest request)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Starts the Sharing of multiple Files
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task RequestAsync(ShareMultipleFilesRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
