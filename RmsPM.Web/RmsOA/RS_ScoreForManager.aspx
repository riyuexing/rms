<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RS_ScoreForManager.aspx.cs" Inherits="RmsOA_RS_ScoreForManager" %>


<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <link href="../Images/index.css" rel="stylesheet" type="text/css" />

    <script language="javascript" src="../Rms.js" type="text/javascript"></script>

    <title>打分管理/给部门经理打分</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                <tr>
                    <td background="../images/topic_bg.gif" class="topic" style="height: 25px; text-align: center;">
                        打分管理/给部门经理打分</td>
                </tr>
            </table>
        </div>
        <table border="0" cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td width="40">
                    <img alt="" height="35" src="../Images/12-7-1.gif" width="40" /></td>
                <td background="../Images/12-7-2.gif">
                    <span><span style="color: #FAA210; font-weight: bolder;">
                        <% GetYear(); %>
                    </span>年<span style="color: #FAA210; font-weight: bolder;"><% GetMonth(); %></span>月
                        高科集团给部门经理打分</span></td>
                <td width="15px;">
                    <img height="35" src="../Images/12-7-3.gif" width="15" /></td>
            </tr>
        </table>
        <table id="tableToolBar" class="table" width="100%">
            <tr>
                <td class="tools-area" width="16">
                    <img align="absMiddle" src="../images/btn_li.gif"></td>
                <td class="tools-area">
                    <asp:Button ID="SaveButton" runat="server" CssClass="button" OnClick="SaveButton_Click"
                        Text="保存" />
                    <input class="button" onclick="self.close();" type="button" value="关闭" />
                </td>
            </tr>
        </table>
        &nbsp;
        <asp:GridView ID="GridView1" runat="server" Width="100%" OnRowDataBound="GridView1_RowDataBound" AutoGenerateColumns="False" DataSourceID="ObjectDataSource1">
        <HeaderStyle CssClass="list-title" HorizontalAlign="Center" />
        <FooterStyle CssClass="list-title" />
        <RowStyle HorizontalAlign="Center" />
        <Columns>
            <asp:BoundField  HeaderText="序号" />
            <asp:BoundField DataField="UserName" HeaderText="用户姓名" SortExpression="UserName" />
            <asp:TemplateField HeaderText="考核分数">
            <ItemTemplate>
            <asp:TextBox runat="server" ID="ScoreTextBox" CssClass="input"></asp:TextBox>
            </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        </asp:GridView>
        <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" DataObjectTypeName="RmsOA.MODEL.EmployViewModel"
            OldValuesParameterFormatString="original_{0}" SelectMethod="GetLeadedUsersByUserID"
            TypeName="RmsOA.BFL.RS_ScoreManageBFL" UpdateMethod="UpDateScores">
            <SelectParameters>
                <asp:Parameter Name="userID" Type="String" />
            </SelectParameters>
        </asp:ObjectDataSource>
    </form>
</body>
</html>
