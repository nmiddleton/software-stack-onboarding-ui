<%@ Page Title="" Language="C#" MasterPageFile="~/Default.master" AutoEventWireup="true" Inherits="TR.Profile.UI.ITICPage" Codebehind="ITIC.aspx.cs" %>

<asp:Content ContentPlaceHolderID="TitleContentPlaceHolder" Runat="Server">
	<asp:Literal runat="server" Text="Management Profile Name" />
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
					<tr>
						<th>Management Profile Name</th>
					</tr>
                    <tr>
                        <td align="justify">
                            <p>GMS uses a combination of ITIC and Standard TR OS Build to create a unique Management Profile name specific to the project and class of device. The information you enter here must be correct and in the <a href="http://sdsbc.ime.reuters.com/content/rgp/gsmg/gpo/config_asset%20mgmt/configuration%20management/infrastructure_catalogue.xls">Infrastructure Catalogue</a> before proceeding. </p>
							<p>Information on TR OS Build versions can be found <a href="http://www.ime.reuters.com/si-platforms/default_platforms_platforms.asp">here</a></p>
                        </td>
                    </tr>

                    <!-- BODY : BEGIN -->
                    <tr>
                        <td align="center">
							<table border="0px" cellpadding="10px" cellspacing="0px" width="800px">
								<!-- ITIC : BEGIN -->
								<tr>
									<td>
										<table border="0px" cellpadding="5px" width="100%" style="border: thin dashed #66CCFF">
											<tr>
												<th colspan="3">ITIC:</th>
											</tr>
											<tr>
												<td>
													<asp:Image runat="server" ImageUrl="~/Images/Help24x24p.png" Width="24" Height="24" 
														onclick="updatehelp('An Infrastructure is a pillar of ThomsonReuters deployed functionality. It is used to describe boundaries for TR management, governance and resposibility at the highest level. An infrastructure, in a Systems Management sense, can be thought of as the collective noun for a set of capabilities.  Each Infrastructure is made up from one of more capabilities that exist below it in the ITIC hierarchy. For further help with the definition of ITIC, refer to this documentation: http://www.ime.reuters.com/doclib/?d=253461')" />
												</td>
												<td align="left">Infrastructure</td>
												<td align="left"><asp:DropDownList ID="InfrastructureDropDownList" Width="400px"
														runat="server" AutoPostBack="True" 
														onselectedindexchanged="InfrastructureDropDownList_SelectedIndexChanged" /></td>
											</tr>
											<tr>
												<td>
													<asp:Image runat="server" ImageUrl="~/Images/Help24x24p.png" Width="24" Height="24" 
														onclick="updatehelp('A Capability describes a subset of an Infrastructure. It could describe a technical realm or a collection of products and services. A Capability, in a Systems Management sense, can be thought of as the collective noun for a set of Logical System Groups.  Each Capability is made up from one of more Logical System Groups that exist below it in the ITIC hierarchy. For further help with the definition of ITIC, refer to this documentation: http://www.ime.reuters.com/doclib/?d=25346')" />
												</td>
												<td align="left">Capability</td>
												<td align="left"><asp:DropDownList ID="CapabilityDropDownList" Width="400px" 
														runat="server" AutoPostBack="True" 
														onselectedindexchanged="CapabilityDropDownList_SelectedIndexChanged" /></td>
											</tr>
											<tr>
												<td>
													<asp:Image runat="server" ImageUrl="~/Images/Help24x24p.png" Width="24" Height="24" 
														onclick="updatehelp('A Logical System Group is the lowest granularity of the ITIC hierarchy. A logical system group describes a collection of systems that all perform the same task. Multiple logical system groups are put together to form a capability. Some examples of logical system groups “apache web servers”, “iis web servers”, “mysql databases” and “oracle databases”, these might be put together (with other LSGs) to form a capability “TR Web site”. A Logical System Group, in a Systems Management sense, can be thought of as the collective noun for a set of systems that can be treated identically for systems management. For further help with the definitionof ITIC, refer to this documentation: http://www.ime.reuters.com/doclib/?d=25346')" />
												</td>
												<td align="left">Logical system group</td>
												<td align="left"><asp:DropDownList ID="LogicalSystemGroupDropDownList" Width="400px" 
														runat="server" AutoPostBack="True" 
														onselectedindexchanged="LogicalSystemGroupDropDownList_SelectedIndexChanged" /></td>
											</tr>
											<tr>
												<td align="left" colspan="3">
													<p id="HelpP" style="font-style:italic; font-size:smaller; color:Blue;" />
												</td>
											</tr>
										</table>
									</td>
								</tr>
								<!-- ITIC : END -->

								<!-- PLATFROM : BEGIN -->
								<tr>
									<td>
										<table border="0px" cellpadding="5px" width="100%" style="border: thin dashed #66CCFF">
											<tr>
												<th><img alt="Platform" src="Images/Filter24x24p.png" width="24px" height="24px" />&nbsp;Platform:</th>
												<th><img alt="Platform" src="Images/Filter24x24p.png" width="24px" height="24px" />&nbsp;Version:</th>
												<th><img alt="Platform" src="Images/Filter24x24p.png" width="24px" height="24px" />&nbsp;Architecture:</th>
												<th><img alt="Platform" src="Images/Filter24x24p.png" width="24px" height="24px" />&nbsp;Build:</th>
												<td align="center">
													<asp:ImageButton ID="DropFilterImageButton" runat="server" ImageUrl="~/Images/DropFilter24x24p.png" ToolTip="Drop Filter" Height="24px" Width="24px" onclick="DropFilterImageButton_Click" />
												</td>
											</tr>
											<tr>
												<td align="center">
													<asp:DropDownList ID="CoarseFilterDropDownList" Width="150px" runat="server" AutoPostBack="True" onselectedindexchanged="PopulateStacks_Handler" />
												</td>
												<td align="center">
													<asp:DropDownList ID="FineFilterDropDownList" Width="150px" runat="server" AutoPostBack="True" onselectedindexchanged="PopulateStacks_Handler" />
												</td>
												<td align="center">
													<asp:DropDownList ID="ArchitectureDropDownList" Width="150px" runat="server" AutoPostBack="True" onselectedindexchanged="PopulateStacks_Handler" />
												</td>
												<td align="center">
													<asp:DropDownList ID="BuildDropDownList" Width="150px" runat="server" AutoPostBack="True" onselectedindexchanged="PopulateStacks_Handler" />
												</td>
											</tr>
											<tr>
												<th colspan="5">Standard OS Build:</th>
											</tr>
											<tr>
												<td align="left" colspan="5">
													<asp:RadioButtonList ID="StacksRadioButtonList" runat="server" 
														AutoPostBack="True" 
														onselectedindexchanged="StacksRadioButtonList_SelectedIndexChanged" />
												</td>
											</tr>
										</table>
									</td>
								</tr>
								<!-- PLATFROM : END -->
							</table>
                        </td>
                    </tr>

					<!-- FOOTER : BEGIN : AccessButton, MessageImage, Message -->
                    <tr>
                        <td>
                            <table border="0px" cellpadding="0px" cellspacing="5px" style="background-color:#dddddd;" width="100%" >
                                <tr>
                                    <td><asp:Button ID="AcceptButton" runat="server" Text="Accept" onclick="AcceptButton_Click" /></td>
									<td><asp:Image ID="MessageImage" runat="server" /></td>
									<td align="left" valign="middle" style="width:100%;" ><asp:Label ID="MessageLabel" runat="server" Font-Italic="true"/></td>
                                </tr>
                            </table>
                        </td>
                    </tr>
					<!-- FOOTER : END -->
					<!-- BODY : END -->
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
