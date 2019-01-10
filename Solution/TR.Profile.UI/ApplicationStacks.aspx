<%@ Page Title="" Language="C#" MasterPageFile="~/Default.master" AutoEventWireup="true" Inherits="TR.Profile.UI.ApplicationStacksPage" Codebehind="ApplicationStacks.aspx.cs" %>

<asp:Content ContentPlaceHolderID="TitleContentPlaceHolder" Runat="Server">
	<asp:Literal runat="server" Text="Application Stacks" />
</asp:Content>

<asp:Content ContentPlaceHolderID="BodyContentPlaceHolder" Runat="Server">
	<script type="text/javascript" >
	//<![CDATA[
		function updatehelp(text) {
			var control;
			control = document.getElementById("HelpP");
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
                        <td colspan="2" align="center" style="font-weight:bold; " ><asp:Label ID="ITICLabel" runat="server" /></td>
                    </tr>
                    <tr>
                        <td colspan="2" align="center" >
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
                        <td colspan="2" align="center" style="font-weight:bold; " >Profile Name: <asp:Label ID="FilenameLabel" runat="server" /></td>
                    </tr>
                    <!-- ITIC : END -->

					<tr>
                        <td>
							<table border="0px" cellpadding="0px" cellspacing="0px" width="100%" style="border: thin solid Gray; ">
								<!-- MENU : BEGIN -->
								<tr>
									<td>
										<table width="100%" style="font-weight: bold;" >
											<tr style="height: 50px" >
												<td align="center" style="background-color: Green; border-bottom-style: solid; border-bottom-width: 10px; border-bottom-color: Gray;">
													<img alt="" src="Images/DocumentOK16x16p.png" width="16px" height="16px" />&nbsp;Platform Stacks
												</td>
												<td align="center" style="background-color: Green; border-bottom-style: solid; border-bottom-width: 10px; border-bottom-color: Orange;" >
													<img alt="" src="Images/DocumentOK16x16p.png" width="16px" height="16px" />&nbsp;Standard Stacks
												</td>
												<td align="center" style="border-bottom-style: solid; border-bottom-width: 10px; border-bottom-color: Gray;">
													<img alt="" src="Images/DocumentEmpty16x16p.png" width="16px" height="16px" />&nbsp;Provisioning Plan Extras
												</td>
											</tr>
										</table>
									</td>
								</tr>
								<!-- MENU : END -->

								<!-- WELCOME : BEGIN -->
								<tr>
									<td align="justify">
										<p style="margin:10px; " >Below are the Middleware or standard application stacks. Select the appropriate stack according to the Logical System Group function that the devices will be performing. Multiple selections are allowed. Details on the contents of the stacks including version information can be found by clicking on the help next to the stack. Details on configuration of the components and “How To’s” are found <a href="http://portal.emea.ime.reuters.com/sites/gmi/Development/Forms/AllItems.aspx?RootFolder=%2fsites%2fgmi%2fDevelopment%2fReleased%2fHow%20To%27s&FolderCTID=&View=%7bADB9AEAA%2d1A86%2d41D7%2d8C9B%2dAE5C76C8FC52%7d">here</a></p>
									</td>
								</tr>
								<!-- WELCOME : END -->
								
								<!-- BODY : BEGIN -->
								<tr>
									<td align="center">
										<table border="0px" cellpadding="0px" cellspacing="10px" width="700px" style="border: thin dashed #66CCFF; margin:10px; ">
											<tr>
												<th>Coarse Filter:</th>
												<th>Fine Filter:</th>
											</tr>
											<tr>
												<td align="center"><asp:DropDownList ID="CoarseFilterDropDownList" Width="200px" runat="server" 
														AutoPostBack="True" onselectedindexchanged="CoarseFilterDropDownList_SelectedIndexChanged" /></td>
												<td align="center"><asp:DropDownList ID="FineFilterDropDownList" Width="200px" runat="server" 
														AutoPostBack="True" onselectedindexchanged="FineFilterDropDownList_SelectedIndexChanged" /></td>
											</tr>
											<tr>
												<th colspan="2">Standard Stacks:</th>
											</tr>
											<tr>
												<td align="left" colspan="2">
													
												</td>
											</tr>
									
											<tr>
												<td align="left">
													<asp:Repeater ID="StacksRepeater" runat="server" >
														<HeaderTemplate>
															<table border="0px" cellpadding="0px" cellspacing="0px">
														</HeaderTemplate>
														<ItemTemplate>
															<tr>
																<td>
																	<img onclick="updatehelp('<%# DataBinder.Eval(Container.DataItem, "StackLookup.Description") %>')" alt="help" src="Images/Help24x24p.png" width="24px" height="24px" />
																</td>
																<td valign="middle" >
																	<asp:CheckBox runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Name") %>' />
																</td>
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
									<td  colspan="6">
										<table border="0px" cellpadding="0px" cellspacing="5px" style="background-color:#dddddd;" width="100%" >
											<tr>
												<td><asp:Button ID="AcceptButton" runat="server" Text="Accept" onclick="AcceptButton_Click" /></td>
												<td><asp:Image ID="MessageImage" runat="server" Width="24px" Height="24px" /></td>
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

<asp:Content ID="Content2" ContentPlaceHolderID="DebugContentPlaceHolder" Runat="Server">
    <asp:Label ID="DebugLabel" runat="server" />
</asp:Content>
