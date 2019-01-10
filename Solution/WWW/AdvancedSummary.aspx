<%@ Page Title="" Language="C#" MasterPageFile="~/Default.master" AutoEventWireup="true" Inherits="TR.Profile.UI.AdvancedSummaryPage" Codebehind="AdvancedSummary.aspx.cs" %>

<asp:Content ContentPlaceHolderID="TitleContentPlaceHolder" Runat="Server">
	<asp:Literal runat="server" Text="Management Profile Advanced Summary" />
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
                        <td align="center" style="font-weight:bold; " >Profile Name: <asp:Label ID="ProfileNameLabel" runat="server" /></td>
                    </tr>
                    <!-- ITIC : END -->

					<tr>
                        <td>
							<table border="0px" cellpadding="0px" cellspacing="0px" width="100%" style="border: thin solid Gray; ">
								<!-- MENU : BEGIN -->
									<tr style="height: 70px; font-weight: bold;">
										<td align="center" style="background-color: Green; border-bottom-style: solid; border-bottom-width: 10px; border-bottom-color: Orange;" >
											<img alt="" src="Images/DocumentOK16x16p.png" />&nbsp;<span style="color: White;">Advanced Plan Properties</span><br />
											<div style="font-size:small;"><img src="Images/Warning16x16p.png" alt="please do not change advanced users only" width="16px" height="16px" />&nbsp;<span style="color: White;">please do not change advanced users only</span></div>
										</td>
									</tr>
								<!-- MENU : END -->
								
								<!-- PROPERTIES : BEGIN -->
								<tr>
									<th align="center"><div style="margin:10px; " >Advanced Plan Properties:</div></th></tr><tr>
									<td>
										<div style="margin:10px; " >
											<asp:Repeater ID="AdvancedPlanPropertiesRepeater" runat="server" >
												<HeaderTemplate>
													<table border="0px" cellpadding="0px" cellspacing="0px">
												</HeaderTemplate>
												<ItemTemplate>
													<tr>
														<td>
															<table border="0px" cellpadding="2px" cellspacing="0px">
																<tr>
																	<td><asp:HiddenField runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "Code") %>'/></td>
																	<td><img onclick="updatehelp('HelpP', '<%# DataBinder.Eval(Container.DataItem, "Description") %>')" alt="help" src="Images/Help24x24p.png" width="24px" height="24px" /></td>
																	<td><asp:CheckBox runat="server" Text="" Visible='<%# DataBinder.Eval(Container.DataItem, "IsCheckBox") %>' Checked='<%# DataBinder.Eval(Container.DataItem, "IsSelected") %>'/></td>
																	<td><asp:Label runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Name") %>' /></td>
																	<td><asp:TextBox runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "DefaultValue") %>' Visible='<%# DataBinder.Eval(Container.DataItem, "IsTextBox") %>' Width="350px"/></td>
																</tr>
															</table>
														</td>
													</tr>
												</ItemTemplate>
												<FooterTemplate>
													</table>
												</FooterTemplate>
											</asp:Repeater>
										</div>
										<p id="HelpP" style="font-style:italic; font-size:smaller; color:Blue;" />
									</td>
								</tr>
								<!-- PROPERTIES : END -->

								<!-- FOOTER : BEGIN -->
								<tr>
									<td>
										<table border="0px" cellpadding="0px" cellspacing="5px" style="background-color:#dddddd;" width="100%" >
											<tr>
												<td><asp:Button ID="GoBackButton" runat="server" Text="Back" onclick="GoBackButton_Click" /></td>
												<td><asp:Button ID="AcceptButton" runat="server" Text="Accept" onclick="AcceptButton_Click" /></td>
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

