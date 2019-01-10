<%@ Page Title="" Language="C#" MasterPageFile="~/Default.master" AutoEventWireup="true" CodeBehind="ITICs.aspx.cs" Inherits="TR.Profile.UI.ITICs" %>

<asp:Content ContentPlaceHolderID="TitleContentPlaceHolder" runat="server">
	<asp:Literal runat="server" Text="ITIC Codes" />
</asp:Content>

<asp:Content ContentPlaceHolderID="BodyContentPlaceHolder" runat="server">
	<table border="0" cellpadding="0" cellspacing="0" width="100%" style="background-color: #F7F7F7; " >
		<!-- HEADER BACKGROUND : BEGIN -->
		<tr style="height: 100px; background-color: #0082B5; ">
			<td>&nbsp;</td>
			<td style="width: 800px; ">&nbsp;</td>
			<td>&nbsp;</td>
		</tr>
		<tr style="height: 50px; background-color: #0082B5; ">
			<td>
				<table width="100%" border="0" cellpadding="0" cellspacing="0" style="height:50px; " >
					<tr style="height:30px; " >
						<td>&nbsp;</td>
					</tr>
					<tr style="height:20px; background-color: #006991; " >
						<td>&nbsp;</td>
					</tr>
				</table>
			</td>
			<td style="border-style: solid solid none solid; border-width: thin; border-color: #CCFFCC; height:50px; background-color: #77D43C; " >
				<table width="100%" border="0" cellpadding="0" cellspacing="0" >
					<tr style="height:30px; background-color: #77D43C; " >
						<td >&nbsp;</td>
					</tr>
					<tr style="height:20px; background-color: #488531; " >
						<td >&nbsp;</td>
					</tr>
				</table>
			</td>
			<td>
				<table width="100%" border="0" cellpadding="0" cellspacing="0" style="height:50px; " >
					<tr style="height:30px; " >
						<td>&nbsp;</td>
					</tr>
					<tr style="height:20px; background-color: #006991; " >
						<td>&nbsp;</td>
					</tr>
				</table>
			</td>
		</tr>
		<!-- HEADER BACKGROUND : END -->

		<!-- HEADER : BEGIN -->
		<tr>
			<td>&nbsp;</td>
			<td style="background-color: White; border-style: none solid none solid; border-width: thin; border-color: Gray; " >
				<p style="font-size: large; font-weight: bold; color: #CCFFCC; position: relative; top: -35px; left: 600px;" >&lt;&nbsp;ITICs&nbsp;&gt;</p>
			</td>
			<td>&nbsp;</td>
		</tr>
		<!-- HEADER : END -->

		<!-- BODY : BEGIN -->
		<tr>
			<td>&nbsp;</td>
			<td align="center" style="background-color: White; border-style: none solid none solid; border-width: thin; border-color: Gray; " >
				<!-- BODY CONTENT : BEGIN -->
				<table border="0" cellpadding="0" cellspacing="10px" >
					<tr>
						<td align="left" >Infrastructure:</td>
						<td>
							<asp:DropDownList ID="InfrastructureDropDownList" runat="server" Width="400px" AutoPostBack="True" 
								onselectedindexchanged="InfrastructureDropDownList_SelectedIndexChanged" />
						</td>
					</tr>
					<tr>
						<td align="left" >Capability:</td>
						<td>
							<asp:DropDownList ID="CapabilityDropDownList" runat="server" Width="400px" AutoPostBack="True" 
								onselectedindexchanged="CapabilityDropDownList_SelectedIndexChanged" />
						</td>
					</tr>
					<tr>
						<td align="left" >Logical System Group:</td>
						<td>
							<asp:DropDownList ID="LogicalSystemGroupDropDownList" runat="server" Width="400px" />
						</td>
					</tr>
				</table>
				<table border="0" cellpadding="0" cellspacing="10px" >
					<tr>
						<td><asp:Image ID="MessageImage" runat="server" /></td>
						<td><asp:Label ID="MessageLabel" runat="server" Font-Italic="true"/></td>
					</tr>
				</table>
				<br />
				<!-- BODY CONTENT : END -->
			</td>
			<td>&nbsp;</td>
		</tr>
		<!-- BODY : ENDS -->

		<!-- FOOTER : BEGIN -->
		<tr>
			<td>&nbsp;</td>
			<td style="background-color: #B0B0B0; border-style: none solid solid solid; border-width: thin; border-color: Gray; " align="center">
				<span style="font-weight: bold; color: #FFFFFF">&copy;&nbsp;2011</span>
			</td>
			<td>&nbsp;</td>
		</tr>
		<tr>
			<td>&nbsp;</td>
			<td>&nbsp;</td>
			<td>&nbsp;</td>
		</tr>
		<!-- FOOTER : END -->
	</table>
</asp:Content>
