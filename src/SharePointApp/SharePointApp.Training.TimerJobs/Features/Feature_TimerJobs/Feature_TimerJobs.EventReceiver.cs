using System;
using System.Runtime.InteropServices;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Administration;

namespace SharePointApp.Training.TimerJobs.Features.Feature_TimerJobs
{
    /// <summary>
    /// This class handles events raised during feature activation, deactivation, installation, uninstallation, and upgrade.
    /// </summary>
    /// <remarks>
    /// The GUID attached to this class may be used during packaging and should not be modified.
    /// </remarks>

    [Guid("1d48292c-69f6-4af8-9de9-9fac6053847e")]
    // ReSharper disable once InconsistentNaming
    public class Feature_TimerJobsEventReceiver : SPFeatureReceiver
    {
        private const string JobName = "Training Registration Portal - Process Instructor Schedules";
        // Uncomment the method below to handle the event raised after a feature has been activated.

        public override void FeatureActivated(SPFeatureReceiverProperties properties)
        {
            var webApp = (SPWebApplication) properties.Feature.Parent;

            RemoveFeatureIfExist(webApp);

            var schedule = new ProcessInstructorSchedules(JobName, webApp, SPServer.Local, SPJobLockType.Job) {Title = JobName};
            var weekly = new SPWeeklySchedule();
            weekly.BeginDayOfWeek = DayOfWeek.Friday;
            weekly.BeginHour = 16;
            weekly.EndDayOfWeek = DayOfWeek.Friday;
            weekly.EndHour = 17;
            schedule.Schedule = weekly;
            schedule.Update();
        }


        // Uncomment the method below to handle the event raised before a feature is deactivated.

        public override void FeatureDeactivating(SPFeatureReceiverProperties properties)
        {
            RemoveFeatureIfExist((SPWebApplication) properties.Feature.Parent);
        }


        private void RemoveFeatureIfExist(SPWebApplication app)
        {
            foreach (var webAppJobDefinition in app.JobDefinitions)
            {
                if (webAppJobDefinition.Name == JobName)
                {
                    webAppJobDefinition.Delete();
                    break;
                }
            }
        }
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
