<%@ Page Title="" Language="C#" MasterPageFile="~/Default.master" AutoEventWireup="true" Inherits="TR.Profile.UI.NewProvisioningPlanPage" Codebehind="NewProvisioningPlan.aspx.cs" %>

<asp:Content ContentPlaceHolderID="TitleContentPlaceHolder" Runat="Server">
	<asp:Literal runat="server" Text="New Provisioning Plan" />
</asp:Content>

<asp:Content ContentPlaceHolderID="BodyContentPlaceHolder" Runat="Server">
	<br />
	<table width="100%">
		<tr>
			<td align="center">
				<table cellpadding="5px">
					<tr>
						<td>Provisioning Plan Name:</td>
						<td><asp:TextBox ID="NameTextBox" runat="server" Width="300px" /></td>
						<td><asp:Button ID="AcceptButton" runat="server" Text="Accept" onclick="AcceptButton_Click" /></td>
						<td><asp:Button ID="CancelButton" runat="server" Text="Cancel" onclick="CancelButton_Click" /></td>
					</tr>
					<tr>
						<td colspan="4">
							<table>
								<tr>
									<td><asp:Image ID="MessageImage" runat="server" /></td>
									<td align="left" valign="middle" style="width:100%;" ><asp:Label ID="MessageLabel" runat="server" Font-Italic="true"/></td>
								</tr>
							</table>
						</td>
					</tr>
				</table>
			</td>
		</tr>
	</table>
	<br />
</asp:Content>
