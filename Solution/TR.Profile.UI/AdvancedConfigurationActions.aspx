<%@ Page Title="" Language="C#" MasterPageFile="~/Default.master" AutoEventWireup="true" Inherits="TR.Profile.UI.AdvancedConfigurationActionsPage" Codebehind="AdvancedConfigurationActions.aspx.cs" %>

<asp:Content ContentPlaceHolderID="TitleContentPlaceHolder" Runat="Server">
	<asp:Literal runat="server" Text="Post Enrolment Actions" />
</asp:Content>

<asp:Content ContentPlaceHolderID="BodyContentPlaceHolder" Runat="Server">
	<script type="text/javascript" >
	//<![CDATA[
		function updatehelp(id, text) {
			var control;
			control = document.getElementById(id);
			control.innerHTML = text;
		}
	//]]>
	</script>

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
												<td align="center" style="background-color: Green; border-bottom-style: solid; border-bottom-width: 10px; border-bottom-color: Gray;">
													<img alt="" src="Images/DocumentOK16x16p.png" height="16px" width="16px" />&nbsp;
													<asp:LinkButton ID="LinkButton1" runat="server" OnClick="CustomerObjectsButton_Click" Text="Customer Objects" />
													<br />
													<span style="font-size:x-small; ">(advanced)</span>
												</td>
												<td align="center" style="background-color: Green; border-bottom-style: solid; border-bottom-width: 10px; border-bottom-color: Orange;" >
													<img alt="" src="Images/DocumentOK16x16p.png" />&nbsp;Advanced Configuration Actions
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
										<p style="margin:10px; " >Enter below any actions that are required to be performed at the end of the enrolment process on the devices.</p>
									</td>
								</tr>
								<!-- WELCOME : END -->
								
								<!-- BODY : BEGIN -->
								<tr>
									<td align="center">
										<table border="0px" cellpadding="10px" width="700px" style="border: thin dashed #66CCFF; margin:10px; " >
											<tr>
												<th align="center">Post Configuration Workflows:</th>
											</tr>
											<tr>
												<td align="left">
													<asp:Repeater ID="WorkflowsRepeater" runat="server">
														<HeaderTemplate>
															<table border="0px" cellpadding="0px" cellspacing="5px">
														</HeaderTemplate>
														<ItemTemplate>
															<tr>
																<td><img onclick="updatehelp('HelpP', '<%# DataBinder.Eval(Container.DataItem, "Description") %>')" alt="help" src="Images/Help24x24p.png" width="24px" height="24px" /></td>
																<td valign="middle">
																	<asp:HiddenField runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "Code") %>' />
																	<asp:CheckBox runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Name") %>' />
																</td>
																<td valign="middle"><asp:TextBox runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "DefaultValue") %>' Visible='<%# DataBinder.Eval(Container.DataItem, "IsTextBox") %>' Width="300px"/></td>
															</tr>
														</ItemTemplate>
														<FooterTemplate>
															</table>
														</FooterTemplate>
													</asp:Repeater>
												</td>
											</tr>
											<tr>
												<td align="left">
													<p id="HelpP" style="font-style:italic; font-size:smaller; color:Blue;" />
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
