using System.Web.Services;

namespace SharePointApp.AddAttachmentToListItem
{
    public class FileService: WebService
    {
        [WebMethod]
        public string Ping()
        {
            return "Pong";
        }

        [WebMethod]
        public bool UploadFile()
        {
            //Context.Request.Files
            return true;
        }
    }
}
