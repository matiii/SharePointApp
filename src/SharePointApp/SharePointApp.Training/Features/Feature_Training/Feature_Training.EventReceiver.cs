using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using Microsoft.SharePoint;

namespace SharePointApp.Training.Features.Feature_Training
{
    /// <summary>
    /// This class handles events raised during feature activation, deactivation, installation, uninstallation, and upgrade.
    /// </summary>
    /// <remarks>
    /// The GUID attached to this class may be used during packaging and should not be modified.
    /// </remarks>

    [Guid("d67e3f2a-633d-4c7c-8a00-a6036949bcbf")]
    public class Feature_TrainingEventReceiver : SPFeatureReceiver
    {
        // Uncomment the method below to handle the event raised after a feature has been activated.

        public override void FeatureActivated(SPFeatureReceiverProperties properties)
        {
            var web = (SPWeb) properties.Feature.Parent;

            SPList courseList = web.Lists["Courses"];
            SPField field = courseList.Fields["Tytu³"];
            field.Title = "Course Title";
            field.Update();

            SPList trainersList = web.Lists["Trainers"];
            SPField fullNameField = trainersList.Fields["Imiê i nazwisko"];
            fullNameField.Required = true;
            fullNameField.Update();

            SPList classesList = web.Lists["Classes"];

            //Title column updates
            SPField titleField = classesList.Fields["Tytu³"];
            titleField.Required = false;
            titleField.ShowInNewForm = false;
            titleField.ShowInEditForm = false;
            titleField.Title = "Class ID";
            titleField.Update();

            //Registrations column updates
            SPField registrationsField = classesList.Fields["Registrations"];
            registrationsField.DefaultValue = "0";
            registrationsField.ShowInNewForm = false;
            registrationsField.Update();

            //Add the "Start Date" and "End Date" columns to the list, ensure they both display Date and Time, and add them to the default view of the list
            SPFieldDateTime startDate = web.ParentWeb.Fields["Data rozpoczêcia"] as SPFieldDateTime;
            startDate.DisplayFormat = SPDateTimeFieldFormatType.DateTime;
            SPFieldDateTime endDate = web.ParentWeb.Fields["Data zakoñczenia"] as SPFieldDateTime;
            endDate.DisplayFormat = SPDateTimeFieldFormatType.DateTime;
            classesList.Fields.Add(startDate);
            classesList.Fields.Add(endDate);
            SPView defaultView = classesList.DefaultView;
            defaultView.ViewFields.Add(startDate);
            defaultView.ViewFields.Add(endDate);
            defaultView.Update();
            classesList.Update();
        }


        // Uncomment the method below to handle the event raised before a feature is deactivated.

        //public override void FeatureDeactivating(SPFeatureReceiverProperties properties)
        //{
        //}


        // Uncomment the method below to handle the event raised after a feature has been installed.

        //public override void FeatureInstalled(SPFeatureReceiverProperties properties)
        //{
        //}


        // Uncomment the method below to handle the event raised before a feature is uninstalled.

        //public override void FeatureUninstalling(SPFeatureReceiverProperties properties)
        //{
        //}

        // Uncomment the method below to handle the event raised when a feature is upgrading.

        //public override void FeatureUpgrading(SPFeatureReceiverProperties properties, string upgradeActionName, System.Collections.Generic.IDictionary<string, string> parameters)
        //{
        //}
    }
}
