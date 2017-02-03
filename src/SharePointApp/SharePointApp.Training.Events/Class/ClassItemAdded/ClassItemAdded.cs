using Microsoft.SharePoint;

namespace SharePointApp.Training.Events.ClassAdded.ClassItemAdded
{
    /// <summary>
    /// List Item Events
    /// </summary>
    public class ClassItemAdded : SPItemEventReceiver
    {
        /// <summary>
        /// An item was added.
        /// </summary>
        public override void ItemAdded(SPItemEventProperties properties)
        {
            var id = properties.ListItem["ID"].ToString();
            string course = properties.ListItem["Course Title"].ToString();
            properties.ListItem["Class ID"] = course + "-" + id;
            properties.ListItem.Update();
            base.ItemAdded(properties);
        }


    }
}