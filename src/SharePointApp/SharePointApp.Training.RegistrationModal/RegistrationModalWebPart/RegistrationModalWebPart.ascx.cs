using System;
using System.ComponentModel;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Microsoft.Office.Server.UserProfiles;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;

namespace SharePointApp.Training.RegistrationModal.RegistrationModalWebPart
{
    [ToolboxItemAttribute(false)]
    public partial class RegistrationModalWebPart : WebPart
    {
        // Uncomment the following SecurityPermission attribute only when doing Performance Profiling on a farm solution
        // using the Instrumentation method, and then remove the SecurityPermission attribute when the code is ready
        // for production. Because the SecurityPermission attribute bypasses the security check for callers of
        // your constructor, it's not recommended for production purposes.
        // [System.Security.Permissions.SecurityPermission(System.Security.Permissions.SecurityAction.Assert, UnmanagedCode = true)]
        public RegistrationModalWebPart()
        {
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            InitializeControl();
        }

        SPWeb currentWeb;
        private DataTable data;
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (Page.IsPostBack)
            //    return;

            currentWeb = SPContext.Current.Web;
            SPUser currentUser = currentWeb.CurrentUser;

            //SPServiceContext serviceContext = SPServiceContext.GetContext(SPContext.Current.Site);
            //UserProfileManager upm = new UserProfileManager(serviceContext);
            //UserProfile currentProfile = upm.GetUserProfile(true);
            string eMail = currentUser.Email;
            SPList registrationsList = currentWeb.Lists["Registrations"];
            SPQuery getRegistrationsForUser = new SPQuery();
            getRegistrationsForUser.ViewFields = "<FieldRef Name='Title'/><FieldRef Name='ID'/>";
            getRegistrationsForUser.Query = "<Where><Eq><FieldRef Name='Email' /><Value Type='Text'>" + eMail + "</Value></Eq></Where>";
            SPListItemCollection currentUserRegistrations = registrationsList.GetItems(getRegistrationsForUser);

            data = new DataTable();
            data.Columns.Add("CourseTitle");
            data.Columns.Add("Venue");
            data.Columns.Add("StartDate");
            data.Columns.Add("EndDate");
            data.Columns.Add("RegId");

            if (currentUserRegistrations.Count > 0)
            {
                foreach (SPListItem registration in currentUserRegistrations)
                {
                    //Get the class ID from the "Title" column of the Registration record:  classid-regid 
                    string title = registration["RegistrationId"].ToString();
                    string classId = title.Substring(0, title.IndexOf('-'));

                    //Get the class record from the Classes list
                    SPListItem theClass = GetClass(classId);

                    string courseTitle = theClass["Course Title"].ToString().Remove(0, 3);
                    string venue = theClass["Venue"].ToString();
                    string startDate = theClass["Data rozpoczęcia"].ToString();
                    string endDate = theClass["Data zakończenia"].ToString();
                    string regId = registration["ID"].ToString();

                    var row = data.Rows.Add();
                    row["CourseTitle"] = courseTitle;
                    row["Venue"] = venue;
                    row["StartDate"] = startDate;
                    row["EndDate"] = endDate;
                    row["RegId"] = regId;

                    //lbClasses.Items.Add(newItem);
                }
            }
            else
            {
                //lbClasses.Items.Add("You are not registered for any classes.");
            }

            if (!Page.IsPostBack)
            {
                ClassesGrid.Columns.Add(new SPBoundField
                {
                    HeaderText = "Registration Id",
                    DataField = "RegId",
                    SortExpression = "RegId"
                });
                ClassesGrid.Columns.Add(new SPBoundField
                {
                    HeaderText = "Course Title",
                    DataField = "CourseTitle",
                    SortExpression = "CourseTitle"
                });
                ClassesGrid.Columns.Add(new SPBoundField
                {
                    HeaderText = "Venue",
                    DataField = "Venue",
                    SortExpression = "Venue"
                });
                ClassesGrid.Columns.Add(new SPBoundField
                {
                    HeaderText = "Start Date",
                    DataField = "StartDate",
                    SortExpression = "StartDate"
                });
                ClassesGrid.Columns.Add(new SPBoundField
                {
                    HeaderText = "End Date",
                    DataField = "EndDate",
                    SortExpression = "EndDate"
                });

                ClassesGrid.Columns[0].ItemStyle.HorizontalAlign = HorizontalAlign.Center;
                ClassesGrid.Columns[1].ItemStyle.HorizontalAlign = HorizontalAlign.Center;
                ClassesGrid.Columns[2].ItemStyle.HorizontalAlign = HorizontalAlign.Center;
                ClassesGrid.Columns[3].ItemStyle.HorizontalAlign = HorizontalAlign.Center;
                ClassesGrid.Columns[4].ItemStyle.HorizontalAlign = HorizontalAlign.Center;
            }

            ClassesGrid.DataSource = data.DefaultView;
            ClassesGrid.DataBind();
            ClassesGrid.Sorting += ClassesGridOnSorting;
        }

        private void ClassesGridOnSorting(object sender, GridViewSortEventArgs args)
        {
            string lastExpression = "";

            if (ViewState["SortExpression"] != null)
                lastExpression = ViewState["SortExpression"].ToString();

            string lastDirection = "asc";

            if (ViewState["SortDirection"] != null)
                lastDirection = ViewState["SortDirection"].ToString();


            string newDirection = "asc";

            if (args.SortExpression == lastExpression)
                newDirection = (lastDirection == "asc") ? "desc" : "asc";

            ViewState["SortExpression"] = args.SortExpression;

            ViewState["SortDirection"] = newDirection;

            data.DefaultView.Sort = args.SortExpression + " " + newDirection;

            ClassesGrid.DataBind();
        }

        protected SPListItem GetClass(string id)
        {
            SPList classesList = currentWeb.Lists["Classes"];
            SPListItem theClass = classesList.GetItemById(Convert.ToInt32(id));
            return theClass;
        }
    }
}
