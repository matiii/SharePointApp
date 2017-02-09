<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RegistrationModalWebPart.ascx.cs" Inherits="SharePointApp.Training.RegistrationModal.RegistrationModalWebPart.RegistrationModalWebPart" %>


<%--<script type="text/javascript">
    var App = function() {
        this.dialogConfirmDelete = null;
        this.openDialog = null;
        this.selectedId = 0;

        function doIt() {
            
        }
    }

    app.prototype.showDialog = function () {
        this.selectedId = document.getElementById('<%= Classes.ClientID %>').value;
        var bodyModal = document.getElementById('confirmDelete');
        bodyModal.style.display = "block";
        this.dialogConfirmDelete = { html: bodyModal, title: 'Confirm Registration Deletion', allowMaximize: true, showClose: true, width: 400, height: 150 };
        this.openDialog = SP.UI.ModalDialog.showModalDialog(this.dialogConfirmDelete);
    }

    app.prototype.init = function() {
        var bDelete = document.getElementById('bDelete');
        bDelete.addEventListener('click', function() {
            this.showDialog();
        });

        var hideDialog = function () {
            this.openDialog.close();
            window.location.reload(true);
        }

        var confirmDeleteBtn = document.getElementById('confirmDeleteBtn');
        confirmDeleteBtn.addEventListener('click', function () {

            var context = new SP.ClientContext();
            var regList = context.get_web().getByTitle('Registrations');
            var reg = regList.getItemById(this.selectedId);
            reg.deleteObject();

            context.executeQueryAsync(
                Function.createDelegate(this, function () {
                    alert('Success');
                    hideDialog();
                }),
                Function.createDelegate(this, function() {
                    alert('Failure');
                    hideDialog();
                }));
        });

        var cancelDeleteBtn = document.getElementById('cancelDeleteBtn');
        cancelDeleteBtn.addEventListener('click', hideDialog);
    }

    var a = new App();
    a.init();

</script>--%>

<SharePoint:SPGridView runat="server" AutoGenerateColumns="False" AllowSorting="True" HorizontalAlign="Center" ID="ClassesGrid"/>

<%--<asp:Repeater ID="Classes" runat="server">
    <HeaderTemplate>
        <table>
    </HeaderTemplate>

    <ItemTemplate>
        <tr>
            <td></td>
        </tr>
    </ItemTemplate>
    
    <FooterTemplate>
        </table>
    </FooterTemplate>
</asp:Repeater>

<input id="bDelete" type="button" />--%>

<asp:Content runat="server" ContentPlaceHolderID="PlaceHolderAdditionalPageHead">
    <script>
        
        if (typeof window.angular == 'undefined') {
            document.write(unescape("%3Cscript src='/_catalogs/theme/angular.min.js' type='text/javascript' %3E%3C/script%3E"));
        }

    </script>
</asp:Content>