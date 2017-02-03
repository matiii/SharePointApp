using System;
using Microsoft.SharePoint;

namespace SharePointApp.Training.Events.Registration.SetRegistrationIdAdded
{
    /// <summary>
    /// List Item Events
    /// </summary>
    public class SetRegistrationIdAdded : SPItemEventReceiver
    {
        /// <summary>
        /// An item was added.
        /// </summary>
        public override void ItemAdded(SPItemEventProperties properties)
        {
            if (properties.ListTitle == "Registrations")
            {
                string classId = properties.AfterProperties["Title"].ToString();
                string id = properties.ListItem["ID"].ToString();
                properties.ListItem["RegistrationId"] = classId + "-" + id;
                properties.ListItem.Update();
                
                SPList classesList = properties.Web.Lists["Classes"];
                SPListItem row = classesList.Items.GetItemById(Convert.ToInt32(classId));
                row["Registrations"] = Convert.ToInt32(row["Registrations"]) + 1;
                row.Update();
            }

            base.ItemAdded(properties);
        }


    }
}