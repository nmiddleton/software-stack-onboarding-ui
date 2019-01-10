<%@ Page Title="" Language="C#" MasterPageFile="~/Default.master" AutoEventWireup="true" Inherits="TR.Profile.UI.ModifyProfilePage" Codebehind="ModifyProfile.aspx.cs" %>

<asp:Content ContentPlaceHolderID="TitleContentPlaceHolder" Runat="Server">
	<asp:Literal runat="server" Text="GMI Management Profile Modification" />
</asp:Content>

<asp:Content ContentPlaceHolderID="BodyContentPlaceHolder" Runat="Server">
	<table width="100%" >
		<tr>
			<td align="center">
				<table border="0px" cellpadding="15px" cellspacing="0px" width="750px" bgcolor="#FFF8F0">
					<!-- HEADER : BEGIN -->
					<tr bgcolor="Orange">
						<td align="center"><span style="color: #FFFFFF; font-weight: bold;">GMI Management Profile Modification</span></td>
					</tr>
					<!-- HEADER : END -->

					<!-- BODY : BEGIN -->
					<tr>
						<td align="left">
							<p>...</p>
							<p>
								<asp:LinkButton ID="ModifyLinkButton" runat="server" Text="I would like to modify GMI Management Profile." onclick="UploadButton_Click" ToolTip="Edit existing XML Document" />&nbsp;
								<asp:FileUpload ID="XmlFileUpload" runat="server" Width="350px" BorderColor="Gray" BorderStyle="Dashed" BackColor="White" BorderWidth="1px" ToolTip="Select XML file to load" /><br />
								<br />
								<asp:LinkButton ID="HomeLinkButton" runat="server" Text="Go to Home" onclick="HomeLinkButton_Click" />
							</p>
							<p>For help on how to use this interface, in the first instance refer to the Green Channel Reference Guide.</p>
							<p>For other issues or problems the raise a Service Manager request to <a href="mailto:TO_DCMS_SUPP_GMS_EMEA">TO_DCMS_SUPP_GMS_EMEA</a></p>
						</td>
					</tr>
					<!-- BODY : END -->

					<!-- FOOTER(MESSAGE) : BEGIN -->
					<tr>
						<td align="left">
							<asp:Image ID="MessageImage" runat="server" Width="24px" Height="24px" />&nbsp;
							<asp:Label ID="MessageLabel" runat="server" Font-Italic="true"/>
						</td>
					</tr>
					<!-- FOOTER(MESSAGE) : END -->
				</table>
			</td>
		</tr>
	</table>
</asp:Content>
