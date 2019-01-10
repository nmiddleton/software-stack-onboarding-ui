<%@ Page Title="" Language="C#" MasterPageFile="~/Default.master" AutoEventWireup="true" Inherits="TR.Profile.UI.DefaultPage" Codebehind="Default.aspx.cs" %>

<asp:Content ContentPlaceHolderID="TitleContentPlaceHolder" Runat="Server">
	<asp:Literal runat="server" Text="GMI Management Profile Creation" />
</asp:Content>

<asp:Content ContentPlaceHolderID="BodyContentPlaceHolder" Runat="Server">
	<table width="100%" >
		<tr>
			<td align="center">
				<table border="0px" cellpadding="15px" cellspacing="0px" width="750px" bgcolor="#FFF8F0">
					<!-- HEADER : BEGIN -->
					<tr bgcolor="Orange">
						<td align="center"><span style="color: #FFFFFF; font-weight: bold;">GMI Management Profile Creation</span></td>
					</tr>
					<!-- HEADER : END -->

					<!-- BODY : BEGIN -->
					<tr>
						<td align="left">
							<p>Welcome to the GMI Management Profile application. Use these pages to create a Management profile for your projects systems. The Management Profile is an XML document understood by GMI management systems. If you have previously created, or been given a profile, you can also use this application to look at this XML file in a more human-readable format and even perform some basic changes to it.</p>
							<p>Please select fom the appropriate links below:</p>
							<ul>
								<li><asp:LinkButton ID="CreateProfileLinkButton" runat="server" onclick="CreateProfileLinkButton_Click" Text="Create new Management Profile" /></li>
								<li>
									<asp:LinkButton ID="ModifyLinkButton" runat="server" Text="View an existing Management Profile" onclick="UploadButton_Click" />&nbsp;
									<asp:FileUpload ID="XmlFileUpload" runat="server" Width="400px" BorderColor="Gray" BorderStyle="Dashed" BackColor="White" BorderWidth="1px" ToolTip="Select XML file to load" />
								</li>
								<li><asp:LinkButton ID="HomeLinkButton" runat="server" Text="Go to Home" onclick="HomeLinkButton_Click" /></li>
							</ul>
							<p>GMI publishes the current version of the <a href="Profile.xsd">XML schema</a> for your reference and use.</p>
							<p>GMI provides an example of a working <a href="Profile.xml">XML file</a> for you to view.</p>
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
