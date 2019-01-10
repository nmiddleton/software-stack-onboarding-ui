<%@ Page Title="" Language="C#" MasterPageFile="~/Default.master" AutoEventWireup="true" Inherits="TR.Profile.UI.DomainNameResolutionPage" Codebehind="DomainNameResolution.aspx.cs" %>

<asp:Content ContentPlaceHolderID="TitleContentPlaceHolder" Runat="Server">
	<asp:Literal runat="server" Text="Domain Name Resolution" />
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
												<td align="center" style="background-color: Green; border-bottom-style: solid; border-bottom-width: 10px; border-bottom-color: Gray;">
													<img alt="" src="Images/DocumentOK16x16p.png" height="16px" width="16px" />&nbsp;
													<asp:LinkButton ID="LinkButton1" runat="server" OnClick="CustomerObjectsButton_Click" Text="Customer Objects" />
													<br />
													<span style="font-size:x-small; ">(advanced)</span>
												</td>
												<td align="center" style="background-color: Green; border-bottom-style: solid; border-bottom-width: 10px; border-bottom-color: Gray;">
													<img alt="" src="Images/DocumentOK16x16p.png" height="16px" width="16px" />&nbsp;
													<asp:LinkButton ID="LinkButton2" runat="server" OnClick="PostConfigurationActionsButton_Click" Text="Advanced Configuration Actions" />
													<br />
													<span style="font-size:x-small; ">(advanced)</span>
												</td>
												<td align="center" style="background-color: Green; border-bottom-style: solid; border-bottom-width: 10px; border-bottom-color: Orange;" >
													<img alt="" src="Images/DocumentOK16x16p.png" />&nbsp;Domain Name Resolution
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
										<p style="margin:10px; " >Select how the Name Resolution of the GMI devices is being managed for this project.</p></td></tr><!-- WELCOME : END --><!-- BODY : BEGIN --><tr>
									<td align="center">
										<table border="0px" cellpadding="10px" width="700px" style="border: thin dashed #66CCFF; margin:10px; " >
											<tr>
												<th align="center">Name resolution options:</th></tr><tr>
												<td align="left">
													<asp:RadioButtonList ID="OptionsRadioButtonList" runat="server"  AutoPostBack="True" onselectedindexchanged="OptionsRadioButtonList_SelectedIndexChanged" >
														<asp:ListItem>Default</asp:ListItem>
														<asp:ListItem>Host file</asp:ListItem>
														<asp:ListItem>Manual DNS</asp:ListItem>
													</asp:RadioButtonList>
												</td>
											</tr>
											<tr>
												<td>
													<asp:GridView ID="OptionsGridView" runat="server" Width="100%" AutoGenerateColumns="False" 
														DataSourceID="LocalPropertiesObjectDataSource" DataKeyNames="Infrastructure">
														<Columns>
															<asp:BoundField HeaderText="Infrastructure" DataField="Infrastructure" SortExpression="Infrastructure" ReadOnly="True" />
															<asp:BoundField HeaderText="Datacentre Suffix" DataField="DatacentreSuffix" />
															<asp:BoundField HeaderText="Clientsited Suffix" DataField="ClientsitedSuffix" ReadOnly="True" />
															<asp:CommandField ShowEditButton="True" ButtonType="Image" 
																CancelImageUrl="~/Images/Cancel16x16p.png" 
																EditImageUrl="~/Images/Edit16x16p.png" 
																UpdateImageUrl="~/Images/Update16x16p.png" />
														</Columns>
													</asp:GridView>

													<asp:ObjectDataSource ID="LocalPropertiesObjectDataSource" runat="server" 
														TypeName="TR.Profile.UI.LocalPropertiesContext, TR.Profile.UI" 
														SelectMethod="Get" 
														UpdateMethod="UpdateDatacentreSuffix" >

														<SelectParameters>
															<asp:SessionParameter Name="Items" SessionField="Local_Properties_Context" />
														</SelectParameters>
														<UpdateParameters>
															<asp:SessionParameter Name="Items" SessionField="Local_Properties_Context" />
															<asp:Parameter Name="Infrastructure" Type="String" />
															<asp:Parameter Name="DatacentreSuffix" Type="String" />
														</UpdateParameters>
													</asp:ObjectDataSource>
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
