<%@ Page Title="" Language="C#" MasterPageFile="~/Default.master" AutoEventWireup="true" Inherits="TR.Profile.UI.SummaryPage" Codebehind="Summary.aspx.cs" %>

<asp:Content ContentPlaceHolderID="TitleContentPlaceHolder" Runat="Server">
	<asp:Literal runat="server" Text="Management Profile Summary" />
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
								<tr style="height: 70px; font-weight: bold; ">
									<td align="center" style="background-color: Green; border-bottom-style: solid; border-bottom-width: 10px; border-bottom-color: Orange;" >
										<img alt="" src="Images/DocumentOK16x16p.png" />&nbsp;Summary
										<br />
										<span style="font-size:x-small; ">(advanced)</span>
									</td>
								</tr>
								<!-- MENU : END -->

								<tr>
									<td align="left"><p style="margin:10px; " >This is the summary of the Management Profile that will be created. The Plan Actions area below lists the tasks that will be performed during enrolment as well as other details. The Run Order, Number of Retries and Continue on Error settings can also be changed here. It is not recommended that these be changed from the defaults shown.</p></td></tr><!-- FILENAME : BEGIN --><tr>
									<td align="left">
										<table border="0px" cellpadding="0px" cellspacing="5px">
											<tr>
												<td><img onclick="updatehelp('FilenameP', 'This is the name of this Management Profile based on ITIC and TR OS Build.')" alt="help" src="Images/Help24x24p.png" width="24px" height="24px" /></td>
												<td>Filename:</td>
												<td align="left" style="width:100%;" ><asp:Label ID="FilenameLabel" runat="server" /></td>
											</tr>
											<tr>
												<td>&nbsp;</td>
												<td colspan="2" align="left">
													<p id="FilenameP" style="font-style:italic; font-size:smaller; color:Blue;" />
												</td>
											</tr>
										</table>
									</td>
								</tr>
								<!-- FILENAME : END -->

								<!-- VESION : BEGIN -->
								<tr>
									<td align="left">
										<table border="0px" cellpadding="0px" cellspacing="5px">
											<tr>
												<td valign="middle">
													<img onclick="updatehelp('VersionP', 'Provide your own Version details. This will be appended to the Filename.')" 
														alt="help" src="Images/Help24x24p.png" width="24px" height="24px" />
												</td>
												<td valign="middle">Version:</td>
												<td valign="middle">
													<asp:TextBox ID="VersionTextBox" runat="server" Width="100px" />
												</td>
												<td valign="middle" align="left" style="width:100%;" >
													<asp:Button runat="server" Text="Update" onclick="SaveVersionImageButton_Click" />
												</td>
											</tr>
											<tr>
												<td>&nbsp;</td>
												<td colspan="3" align="left" >
													<p id="VersionP" style="font-style:italic; font-size:smaller; color:Blue;" />
												</td>
											</tr>
										</table>
									</td>
								</tr>
								<!-- VESION : END -->

								<!-- ACTIONS : BEGIN -->
								<tr>
									<td>
										<table border="0px" cellpadding="10px" width="900px" style="border: thin dashed #66CCFF; margin:10px; ">
											<tr>
												<td align="center">
													<table>
														<tr>
															<th><span>Provisioning&nbsp;Plan&nbsp;</span></th>
															<td>
																<asp:DropDownList runat="server" 
																	ID="ProvisioningPlansDropDownList" AutoPostBack="True" 
																	onselectedindexchanged="PlanActionsDropDownList_SelectedIndexChanged" />
																&nbsp;<asp:ImageButton ID="DuplicateProvisioningPlanImageButton" runat="server" 
																	ImageUrl="~/Images/AddProvisioningPlan24x24p.png" 
																	onclick="DuplicateProvisioningPlanImageButton_Click" 
																	ToolTip="Duplicate Provisioning Plan" Height="24px" Width="24px" Visible="false"/>
																&nbsp;<asp:ImageButton ID="DeleteProvisioningPlanImageButton" runat="server" 
																	ImageUrl="~/Images/DeleteProvisioningPlan24x24p.png" 
																	onclick="DeleteProvisioningPlanImageButton_Click" 
																	ToolTip="Delete Provisioning Plan" Height="24px" Width="24px" />
																&nbsp;<asp:Button ID="AdvancedPlanPropertiesButton" runat="server" 
																	Text="Advanced Plan Properties" onclick="AdvancedPlanPropertiesButton_Click1" />
																&nbsp;<span style="font-size: smaller; font-style:italic; ">- please do not change advanced users only</span>
															</td>
														</tr>
													</table>
												</td>
											</tr>
											<tr>
												<td align="center">
													<asp:GridView ID="ActionsGridView" runat="server" AutoGenerateColumns="False" 
														DataSourceID="PlanActionObjectDataSource" DataKeyNames="ParametersString" >
														<Columns>
															<asp:CommandField ButtonType="Image" ShowEditButton="True"
																EditImageUrl="~/Images/Edit16x16p.png" 
																CancelImageUrl="~/Images/Cancel16x16p.png" 
																UpdateImageUrl="~/Images/Update16x16p.png" DeleteImageUrl="~/Images/Delete16x16p.png" ShowDeleteButton="True" />
															<asp:TemplateField HeaderText="Run Order" >
																<ItemTemplate>
																	<asp:Label ID="RunOrderLabel" runat="server" Text='<%# Bind("Order") %>' />
																</ItemTemplate>
																<EditItemTemplate>
																	<asp:TextBox ID="RunOrderTextBox" runat="server" Text='<%# Bind("Order") %>' Width="30px" />
																	<asp:RangeValidator ID="RunOrderRangeValidator" runat="server" 
																		ControlToValidate="RunOrderTextBox" ErrorMessage="must be from 0"
																		MinimumValue="0" MaximumValue="1000" Type="Integer" ForeColor="Red" Font-Size="Small" />
																</EditItemTemplate>
															</asp:TemplateField>
															<asp:BoundField DataField="Name" HeaderText="Actions" ReadOnly="True" />
															<asp:BoundField DataField="ParametersString" HeaderText="Parameters" ReadOnly="True" />
															<asp:TemplateField HeaderText="Max Retries" >
																<ItemTemplate>
																	<asp:Label ID="MaxRetriesLabel" runat="server" Text='<%# Bind("MaxRetries") %>' />
																</ItemTemplate>
																<EditItemTemplate>
																	<asp:TextBox ID="MaxRetriesTextBox" runat="server" Text='<%# Bind("MaxRetries") %>' Width="30px" />
																	<asp:RangeValidator ID="MaxRetriesRangeValidator" runat="server" 
																		ControlToValidate="MaxRetriesTextBox" ErrorMessage="must be from 0 to 10" 
																		MinimumValue="0" MaximumValue="10" Type="Integer" ForeColor="Red" Font-Size="Small" />
																</EditItemTemplate>
															</asp:TemplateField>
															<asp:TemplateField HeaderText="Continue on Error" >
																<ItemTemplate>
																	<asp:Label ID="ContinueOnErrorLabel" runat="server" Text='<%# Bind("ContinueOnError") %>' />
																</ItemTemplate>
																<EditItemTemplate>
																	<asp:RadioButtonList ID="EditContinueOnErrorRadioButtonList" runat="server" RepeatDirection="Horizontal" 
																		DataValueField='<%# Bind("ContinueOnError") %>' 
																		SelectedValue='<%# Bind("ContinueOnError") %>'>
																		<asp:ListItem Value="Y">Yes</asp:ListItem><asp:ListItem Value="N">No</asp:ListItem></asp:RadioButtonList></EditItemTemplate></asp:TemplateField>
														</Columns>
													</asp:GridView>

													<asp:ObjectDataSource runat="server" 
														ID="PlanActionObjectDataSource" TypeName="TR.Profile.UI.PlanActionsContext, TR.Profile.UI" 
														SelectMethod="Get" 
														UpdateMethod="UpdateDatacentreSuffix" 
														DeleteMethod="Delete" >

														<SelectParameters>
															<asp:SessionParameter Name="Profile" SessionField="Profile" />
														</SelectParameters>
														<UpdateParameters>
															<asp:SessionParameter Name="Profile" SessionField="Profile" />
															<asp:Parameter Name="ParametersString" Type="String" />
															<asp:Parameter Name="MaxRetries" Type="String" />
															<asp:Parameter Name="ContinueOnError" Type="String" />
															<asp:Parameter Name="Order" Type="String" />
														</UpdateParameters>
														<DeleteParameters>
															<asp:SessionParameter Name="Profile" SessionField="Profile" />
															<asp:Parameter Name="ParametersString" Type="String" />
														</DeleteParameters>
													</asp:ObjectDataSource>
												</td>
											</tr>
										</table>
									</td>
								</tr>
								<!-- ACTIONS : END -->

								<!-- FOOTER : BEGIN -->
								<tr>
									<td>
										<table border="0px" cellpadding="0px" cellspacing="10px" style="background-color:#dddddd;" width="100%" >
											<tr>
												<td><asp:Button runat="server" Text="Show&nbsp;Profile" onclick="ShowHTMLButton_Click" /></td>
												<td><asp:Button runat="server" Text="Save&nbsp;Profile&nbsp;as&nbsp;HTML" onclick="SaveHTMLButton_Click" /></td>
												<td><asp:Button runat="server" Text="Save&nbsp;Profile&nbsp;as&nbsp;XML" onclick="SaveXMLButton_Click" /></td>
												<td><asp:Button runat="server" Text="Home" onclick="GoHomeButton_Click" /></td>
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
