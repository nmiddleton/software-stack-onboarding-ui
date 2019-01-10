<%@ Page Title="" Language="C#" MasterPageFile="~/Default.master" AutoEventWireup="true" Inherits="TR.Profile.UI.PlatformStacksPage" Codebehind="PlatformStacks.aspx.cs" %>

<asp:Content ContentPlaceHolderID="TitleContentPlaceHolder" Runat="Server">
	<asp:Literal runat="server" Text="Platform Stack" />
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

                    <!-- CONTENT : BEGIN -->
					<tr>
                        <td>
							<table border="0px" cellpadding="0px" cellspacing="0px" width="100%" style="border: thin solid Gray; ">
								<!-- MENU : BEGIN -->
								<tr>
									<td>
										<table width="100%" style="font-weight: bold;" >
											<tr style="height: 50px">
												<td align="center" style="background-color: Green; border-bottom-style: solid; border-bottom-width: 10px; border-bottom-color: Orange;" >
													<img alt="" src="Images/DocumentOK16x16p.png" width="16px" height="16px" />&nbsp;Platform Stacks
												</td>
												<td align="center" style="border-bottom-style: solid; border-bottom-width: 10px; border-bottom-color: Gray;">
													<img alt="" src="Images/DocumentEmpty16x16p.png" width="16px" height="16px" />&nbsp;<span style="color: #303030">Standard Stacks</span>
												</td>
												<td align="center" style="border-bottom-style: solid; border-bottom-width: 10px; border-bottom-color: Gray;">
													<img alt="" src="Images/DocumentEmpty16x16p.png" width="16px" height="16px" />&nbsp;<span style="color: #303030">Provisioning Plan Extras</span>
												</td>
											</tr>
										</table>
									</td>
								</tr>
								<!-- MENU : END -->

								<!-- WELCOME : BEGIN -->
								<tr>
									<td align="justify">
										<p style="margin:10px; " >The stacks displayed below are specific to the platform you selected for this Management Profile and are named accordingly. They will provide a core set of GMI components to the devices that have been validated against that TR OS Build version. If your project has it’s own base stack or stacks, select NONE and you can specify these later. Content of the stacks including version details can be found by clicking on the help next to the stack.</p>
									</td>
								</tr>
								<!-- WELCOME : END -->
								
								<!-- BODY : BEGIN -->
								<tr>
									<td align="center">
										<table border="0px" cellpadding="10px" cellspacing="0px" width="700px" style="border: thin dashed #66CCFF; margin: 10px;">
											<tr>
												<th>Platform Stack:</th>
											</tr>
											<tr>
												<td style="width:24px;">
													<table border="0px" cellpadding="0px" cellspacing="0px" >
														<tr>
															<td>
																<asp:Repeater ID="ImagesRepeater" runat="server" >
																	<HeaderTemplate>
																		<table border="0px" cellpadding="0px" cellspacing="0px" >
																	</HeaderTemplate>
																	<ItemTemplate>
																			<tr>
																				<td>
																					<img onclick="updatehelp('<%# DataBinder.Eval(Container.DataItem, "StackLookup.Description") %>')" alt="help" src="Images/Help24x24p.png" width="24px" height="24px" />
																				</td>
																			</tr>
																	</ItemTemplate>
																	<FooterTemplate>
																		</table>
																	</FooterTemplate>
																</asp:Repeater>
															</td>
															<td align="left"  valign="middle" >
																<asp:RadioButtonList ID="StacksRadioButtonList" runat="server" onselectedindexchanged="StacksRadioButtonList_SelectedIndexChanged" />
															</td>
														</tr>
													</table>
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
										<table border="0px" style="background-color:#dddddd;" width="100%">
											<tr>
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
                    <!-- CONTENT : END -->
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
