<!--
                           $Id: Test.aspx 13936 2010-11-24 14:57:22Z neil.middleton $
              $LastChangedDate: 2010-11-24 14:57:22 +0000 (Wed, 24 Nov 2010) $
          $LastChangedRevision: 13936 $
                $LastChangedBy: neil.middleton $
                      $HeadURL: https://sami.cdt.int.thomsonreuters.com/svn/gms_gmi/trunk/infrastructure/tools/webfrontend/Solution/TR.Profile.UI/Default.master.cs $
-->

<%@ Page Language="C#" AutoEventWireup="true" Inherits="TR.Profile.UI.TestPage" Codebehind="Test.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">
    <title></title>
</head>

<body>
    <form id="form1" runat="server">
        <div>Test Page !</div>
			<asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataSourceID="ObjectDataSource1" DataKeyNames="Infrastructure">
				<Columns>
					<asp:CommandField ShowEditButton="True" ButtonType="Image" 
						CancelImageUrl="~/Images/Cancel16x16p.png" 
						EditImageUrl="~/Images/Edit16x16p.png" 
						UpdateImageUrl="~/Images/Update16x16p.png" />
					<asp:BoundField HeaderText="Infrastructure" DataField="Infrastructure" SortExpression="Infrastructure" ReadOnly="True" />
					<asp:BoundField HeaderText="DatacentreSuffix" DataField="DatacentreSuffix" />
					<asp:BoundField HeaderText="ClientsitedSuffix" DataField="ClientsitedSuffix" ReadOnly="True" />
				</Columns>
			</asp:GridView>

    		<asp:ObjectDataSource ID="ObjectDataSource1" runat="server" 
				TypeName="TR.Profile.UI.LocalPropertiesContext, TR.Profile.UI"
				SelectMethod="Get"
				UpdateMethod="UpdateDatacentreSuffix">

				<SelectParameters>
					<asp:SessionParameter Name="Items" SessionField="Local_Properties_Context" />
				</SelectParameters>
				<UpdateParameters>
					<asp:SessionParameter Name="Items" SessionField="Local_Properties_Context" />
					<asp:Parameter Name="Infrastructure" Type="String" />
					<asp:Parameter Name="DatacentreSuffix" Type="String" />
				</UpdateParameters>
			</asp:ObjectDataSource>
    </form>
</body>

</html>
