<%@ Page Title="" Language="C#" MasterPageFile="~/Default.master" AutoEventWireup="true" Inherits="TR.Profile.UI.DocumentPage" Codebehind="Document.aspx.cs" %>

<asp:Content ContentPlaceHolderID="TitleContentPlaceHolder" Runat="Server">
	<asp:Literal runat="server" Text="Management Profile" />
</asp:Content>

<asp:Content ContentPlaceHolderID="BodyContentPlaceHolder" Runat="Server">
	<table border="0px" cellpadding="0px" cellspacing="10px" width="100%">
		<tr>
			<td><asp:ImageButton runat="server" ToolTip="Back" onclick="Back_Click" ImageUrl="~/Images/Back24x24p.png" Height="24px" Width="24px" /></td>
			<td><asp:Image ID="MessageImage" runat="server" /></td>
			<td align="left" valign="middle" style="width:100%;" ><asp:Label ID="MessageLabel" runat="server" Font-Italic="true"/></td>
		</tr>
		<tr>
			<td colspan="3" align="center"><asp:Label ID="DocumentLabel" runat="server" /></td>
		</tr>
		<tr>
			<td colspan="3"><asp:ImageButton runat="server" ToolTip="Back" onclick="Back_Click" ImageUrl="~/Images/Back24x24p.png" Height="24px" Width="24px" /></td>
		</tr>
	</table>
</asp:Content>

