using System;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Administration;

namespace SharePointApp.Training.TimerJobs
{
    public class ProcessInstructorSchedules: SPJobDefinition
    {
        public ProcessInstructorSchedules()
        {
        }

        public ProcessInstructorSchedules(string jobName, SPService service, SPServer server, SPJobLockType lockType) : base(jobName, service, server, lockType)
        {
        }

        public ProcessInstructorSchedules(string jobName, SPWebApplication webApplication, SPServer server, SPJobLockType lockType) : base(jobName, webApplication, server, lockType)
        {
        }

        public override void Execute(Guid targetInstanceId)
        {
            SPWebApplication webApp = (SPWebApplication)Parent;

            SPSite trainingSite = webApp.Sites[""];
            SPWeb rootWeb = trainingSite.AllWebs["/trainings"];
            SPList trainersList = rootWeb.Lists["Trainers"];
            SPListItemCollection trainers = trainersList.Items;

            foreach (SPListItem trainer in trainers)
            {
                SPField emailField = trainer.Fields.GetFieldByInternalName("Email");
                SPField fullNameField = trainer.Fields.GetFieldByInternalName("FullName");
                //string email = trainer[emailField.Id].ToString();
                string fullName = trainer[fullNameField.Id].ToString();

                SPList classes = rootWeb.Lists["Classes"];
                var query = new SPQuery();
                query.ViewFields = "<FieldRef Name='Trainer'/> <FieldRef Name='Venue'/> <FieldRef Name='StartDate'/> <FieldRef Name='_EndDate'/> <FieldRef Name='Registrations' />";
                query.Query = "<Where><And><Eq><FieldRef Name='Trainer' /><Value Type='Lookup'>" + fullName + "</Value></Eq><Geq><FieldRef Name='StartDate'/><Value Type='DateTime'><Today /></Value></Geq></And></Where>";
                SPListItemCollection classesForTrainer = classes.GetItems(query);
            }

        }
    }
}
