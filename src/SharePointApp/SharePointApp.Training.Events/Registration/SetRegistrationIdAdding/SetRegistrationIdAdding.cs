using System.Linq;
using System.Web;
using Microsoft.SharePoint;

namespace SharePointApp.Training.Events.Registration.SetRegistrationIdAdding
{
    /// <summary>
    /// List Item Events
    /// </summary>
    public class SetRegistrationIdAdding : SPItemEventReceiver
    {
        private readonly HttpContext _httpContext;

        public SetRegistrationIdAdding()
        {
            _httpContext = HttpContext.Current;
        }

        /// <summary>
        /// An item is being added.
        /// </summary>
        public override void ItemAdding(SPItemEventProperties properties)
        {
            if (properties.ListTitle == "Registrations")
            {
                string key =
                    _httpContext.Request.QueryString.AllKeys.FirstOrDefault(x => x.ToLower() == "classid");

                if (key == null)
                {
                    properties.Cancel = true;
                    properties.ErrorMessage = "You have to register by special form.";
                    return;
                }

                properties.AfterProperties["Title"] = _httpContext.Request.QueryString[key];
            }

            base.ItemAdding(properties);
        }


    }
}