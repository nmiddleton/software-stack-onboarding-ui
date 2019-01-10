<%@ Page Title="" Language="C#" MasterPageFile="~/Default.master" AutoEventWireup="true" Inherits="TR.Profile.UI.CustomerObjectsPage" Codebehind="CustomerObjects.aspx.cs" %>

<asp:Content ContentPlaceHolderID="TitleContentPlaceHolder" Runat="Server">
	<asp:Literal runat="server" Text="Customer Objects" />
</asp:Content>

<asp:Content ContentPlaceHolderID="BodyContentPlaceHolder" Runat="Server">
    <table width="100%" >
        <tr>
            <td align="center">
                <table border="0px" cellpadding="0px" cellspacing="10px" style="background-color:#eeeeee;" width="950px">
                    <!-- ITIC : BEGIN -->
                    <tr>
                        <td align="center" style="font-weight:bold; " >ITIC:&nbsp;<asp:Label ID="ITICLabel" runat="server" /></td>
                    </tr>
                    <tr>
                        <td align="center" >
							<table>
								<tr>
									<td align="right" >Infrastructure:</td>
									<td align="left" ><asp:Label ID="InfrastructureLabel" runat="server" /></td>
								</tr>
								<tr>
									<td align="right" >Capability:</td>
									<td align="left" ><asp:Label ID="CapabilityLabel" runat="server" /></td>
								</tr>
								<tr>
									<td align="right" >Logical System Group:</td>
									<td align="left" ><asp:Label ID="LogicalSystemGroupLabel" runat="server" /></td>
								</tr>
							</table>
                        </td>
                    </tr>
                    <tr>
                        <td align="center" style="font-weight:bold; " >Profile Name: <asp:Label ID="FilenameLabel" runat="server" /></td>
                    </tr>
                    <!-- ITIC : END -->

					<tr>
                        <td>
							<table border="0px" cellpadding="0px" cellspacing="0px" width="100%" style="border: thin solid Gray; ">
								<!-- MENU : BEGIN -->
								<tr>
									<td>
										<table width="100%" style="font-weight: bold;" >
											<tr style="height: 50px">
												<td align="center" style="background-color: Green; border-bottom-style: solid; border-bottom-width: 10px; border-bottom-color: Orange;" >
													<img alt="" src="Images/DocumentOK16x16p.png" />&nbsp;Customer Objects<br />
													<span style="font-size:x-small; ">(advanced)</span>
												</td>
												<td align="center" style="background-color: Green; border-bottom-style: solid; border-bottom-width: 10px; border-bottom-color: Gray;">
													<img alt="" src="Images/DocumentOK16x16p.png" height="16px" width="16px" />&nbsp;
													<asp:LinkButton ID="LinkButton1" runat="server" OnClick="PostConfigurationActionsButton_Click" Text="Advanced Configuration Actions" />
													<br />
													<span style="font-size:x-small; ">(advanced)</span>
												</td>
												<td align="center" style="background-color: Green; border-bottom-style: solid; border-bottom-width: 10px; border-bottom-color: Gray;">
													<img alt="" src="Images/DocumentOK16x16p.png" height="16px" width="16px" />&nbsp;
													<asp:LinkButton ID="LinkButton2" runat="server" OnClick="DomainNameResolutionButton_Click" Text="Domain Name Resolution" />
													<br />
													<span style="font-size:x-small; ">(advanced)</span>
												</td>
											</tr>
										</table>
									</td>
								</tr>
								<!-- MENU : END -->

								<!-- WELCOME : BEGIN -->
								<tr>
									<td align="justify">
										<p style="margin:10px; " >Enter the names of your custom project stacks or scans below. You must enter the names of these scans or stacks exactly and they must exist in the TPM environment that this Management Profile is being loaded into.</p>
									</td>
								</tr>
								<!-- WELCOME : END -->
								
								<!-- BODY : BEGIN -->
								<tr>
									<td align="center">
										<table border="0px" cellpadding="10px" width="700px" style="border: thin dashed #66CCFF; margin:10px;">
											<tr>
												<th align="center">Custom Software:</th>
											</tr>
											<tr>
												<td>
													<table border="0px" cellpadding="0px" cellspacing="10px">
														<tr>
															<td align="right"" valign="top"><asp:TextBox ID="StackTextBox" runat="server" Width="300px"/></td>
															<td valign="top" rowspan="3"><asp:ListBox ID="StacksListBox" runat="server" Width="300px" Height="130px" SelectionMode="Multiple"/></td>
														</tr>
														<tr>
															<td align="right"" valign="middle"><asp:Button ID="AddStackButton" runat="server" Text="Add->" Width="80px" onclick="AddStackButton_Click" /></td>
														</tr>
														<tr>
															<td align="right"" valign="bottom"><asp:Button ID="RemoveStackButton" runat="server" Text="Remove" Width="80px" onclick="RemoveStackButton_Click" /></td>
														</tr>
													</table>
												</td>
											</tr>
										</table>

										<table border="0px" cellpadding="10px" width="700px" style="border: thin dashed #66CCFF; margin:10px;">
											<tr>
												<th align="center">Custom Scans</th>
											</tr>
											<tr>
												<td>
													<table border="0px" cellpadding="0px" cellspacing="10px">
														<tr>
															<td align="right"" valign="top"><asp:TextBox ID="ScanTextBox" runat="server" Width="300px"/></td>
															<td valign="top" rowspan="3"><asp:ListBox ID="ScansListBox" runat="server" Width="300px" Height="130px" SelectionMode="Multiple"/></td>
														</tr>
														<tr>
															<td align="right"" valign="middle"><asp:Button ID="AddScanButton" runat="server" Text="Add->" Width="80px" onclick="AddScanButton_Click" /></td>
														</tr>
														<tr>
															<td align="right"" valign="bottom"><asp:Button ID="RemoveScamButton" runat="server" Text="Remove" Width="80px" onclick="RemoveScamButton_Click" /></td>
														</tr>
													</table>
												</td>
											</tr>
										</table>
									</td>
								</tr>
								<!-- BODY : END -->

								<!-- FOOTER : BEGIN -->
								<tr>
									<td>
										<table border="0px" cellpadding="0px" cellspacing="5px" style="background-color:#dddddd;" width="100%" >
											<tr>
												<td><asp:Button ID="SubmitButton" runat="server" Text="Finish" onclick="SubmitButton_Click" /></td>
												<td><asp:Image ID="MessageImage" runat="server" /></td>
												<td align="left" valign="middle" style="width:100%;" ><asp:Label ID="MessageLabel" runat="server" Font-Italic="true"/></td>
											</tr>
										</table>
									</td>
								</tr>
								<!-- FOOTER : END -->
							</table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>

</asp:Content>

<asp:Content ContentPlaceHolderID="DebugContentPlaceHolder" Runat="Server">
	<asp:Label ID="DebugLabel" runat="server" />
</asp:Content>
