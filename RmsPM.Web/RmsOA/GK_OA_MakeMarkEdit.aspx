﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GK_OA_MakeMarkEdit.aspx.cs"
    Inherits="RmsOA_GK_OA_MakeMarkEdit" %>

<%@ Register Src="../WorkFlowControl/workflowtoolbar.ascx" TagName="workflowtoolbar"
    TagPrefix="uc5" %>
<%@ Register Src="../WorkFlowControl/WorkFlowList.ascx" TagName="WorkFlowList" TagPrefix="uc4" %>
<%@ Register Src="../UserControls/inputcontract.ascx" TagName="inputcontract" TagPrefix="uc3" %>
<%@ Register TagPrefix="cc3" Namespace="AspWebControl" Assembly="AspWebControl" %>
<%@ Register TagPrefix="uc2" TagName="InputUnit" Src="../UserControls/InputUnit.ascx" %>
<%@ Register TagPrefix="uc1" TagName="InputUser" Src="../UserControls/InputUser.ascx" %>
<%@ Register TagPrefix="uc1" TagName="InputSupplier" Src="../UserControls/InputSupplier.ascx" %>
<%@ Register TagPrefix="cc4" Namespace="Rms.ControlLb" Assembly="RmsPM.BLL" %>
<%@ Register TagPrefix="uc1" TagName="AttachMentAdd" Src="../UserControls/AttachMentAdd.ascx" %>
<%@ Register TagPrefix="uc1" TagName="AttachMentList" Src="../UserControls/AttachMentList.ascx" %>
<%@ Register TagPrefix="uc1" TagName="InputSystemGroup" Src="../UserControls/InputSystemGroup.ascx" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>打分记录</title>
    <link href="../Images/index.css" type="text/css" rel="stylesheet" />

    <script type="text/javascript" language="javascript" src="../Rms.js"></script>

    <script type="text/javascript" language="javascript">
        function SelectUnit()
		{
			OpenSmallWindow("../SelectBox/SelectUnit.aspx?UnitCode=000000");
		}
		function SelectUnitReturn(code, name)
		{
			window.document.all.FormView1_txtUnitName.value = name;
			window.document.all.FormView1_txtUnit.value = code;
		}	
		
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <table height="100%" cellspacing="0" cellpadding="0" width="100%" border="0">
            <tr>
                <td height="25">
                    <table cellspacing="0" cellpadding="0" width="100%" border="0">
                        <tr>
                            <td class="topic" align="center" background="../images/topic_bg.gif" height="25">
                                打分记录</td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td valign="top">
                    <asp:FormView ID="FormView1" runat="server" DataSourceID="ObjectDataSource1" Width="100%"
                        OnItemInserted="FormView1_ItemInserted" OnItemDeleted="FormView1_ItemDeleted"
                        OnItemUpdated="FormView1_ItemUpdated" OnItemInserting="FormView1_ItemInserting"
                        DataKeyNames="Code" OnDataBound="FormView1_DataBound" OnItemUpdating="FormView1_ItemUpdating">
                        <EditItemTemplate>
                            <table id="Table2" class="table" width="100%">
                                <tr>
                                    <td class="tools-area" width="16">
                                        <img align="absMiddle" src="../images/btn_li.gif" /></td>
                                    <td class="tools-area">
                                        <asp:Button ID="btnSave" runat="server" CommandName="Update" CssClass="button" Text=" 保存 " />&nbsp;
                                        <input id="btnClose" runat="server" class="button" name="btnClose" onclick="javascript:window.close();"
                                            type="button" value=" 关闭 " />
                                    </td>
                                </tr>
                            </table>
                            <table border="0" cellpadding="0" cellspacing="0" class="form" width="100%">
                                <tr>
                                    <td class="form-item" style="width: 20%">
                                        部门：</td>
                                    <td>
                                        <input id="txtUnit" runat="server" class="input" name="txtUnit" size="8" style="width: 72px;
                                            height: 18px" type="hidden" value='<%# Bind("UnitCode") %>' /><input id="txtUnitName"
                                                runat="server" class="input" name="txtUnit" style="width: 121px; height: 18px"
                                                type="text" /><img onclick="SelectUnit();return false;" src="../images/ToolsItemSearch.gif"
                                                    style="cursor: hand" />
                                    </td>
                                    <td class="form-item" style="width: 20%">
                                        打分日期：</td>
                                    <td>
                                        <cc3:Calendar ID="Calendar6" runat="server" CalendarMode="Date" CalendarResource="../Images/CalendarResource/"
                                            Display="True" ReadOnly="False" Value='<%# Bind("RegisterDate") %>'>
                                        </cc3:Calendar>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="form-item" style="width: 20%">
                                        标题：</td>
                                    <td>
                                        <asp:TextBox ID="TitleTextBox" runat="server" Text='<%# Bind("Title") %>' CssClass="input"
                                            Width="100%"></asp:TextBox></td>
                                    <td class="form-item" style="width: 20%">
                                        打分类型：</td>
                                    <td>
                                        <asp:DropDownList ID="drpMarkType" runat="server">
                                            <asp:ListItem Selected="True">请选择</asp:ListItem>
                                            <asp:ListItem>员工打分</asp:ListItem>
                                            <asp:ListItem>部门打分</asp:ListItem>
                                            <asp:ListItem>部门经理打分</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:TextBox ID="MarkTypeTextBox" runat="server" Visible ="false" Text='<%# Bind("MarkType") %>' CssClass="input"
                                            Width="100%"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="form-item">
                                        附件
                                    </td>
                                    <td colspan="3" class="blackbordertdpaddingcontent">
                                        <uc1:AttachMentAdd ID="Attachmentadd1" runat="server" CtrlPath="../UserControls/"
                                            AttachMentType="MakeMark" MasterCode='<%# Eval("Code") %>'></uc1:AttachMentAdd>
                                    </td>
                                </tr>
                            </table>
                        </EditItemTemplate>
                        <InsertItemTemplate>
                            <table id="Table1" class="table" width="100%">
                                <tr>
                                    <td class="tools-area" width="16">
                                        <img align="absMiddle" src="../images/btn_li.gif" /></td>
                                    <td class="tools-area">
                                        <asp:Button ID="btnSave" runat="server" CommandName="Insert" CssClass="button" Text=" 保存 " />
                                        <input id="btnClose" runat="server" class="button" name="btnClose" onclick="javascript:window.close();"
                                            type="button" value=" 关闭 " />
                                    </td>
                                </tr>
                            </table>
                            <table border="0" cellpadding="0" cellspacing="0" class="form" width="100%">
                                <tr>
                                    <td class="form-item" style="width: 20%">
                                        部门：</td>
                                    <td>
                                        <input id="txtUnit" runat="server" class="input" name="txtUnit" size="8" style="width: 72px;
                                            height: 18px" type="hidden" value='<%# Bind("UnitCode") %>' /><input id="txtUnitName"
                                                runat="server" class="input" name="txtUnit" style="width: 121px; height: 18px"
                                                type="text" /><img onclick="SelectUnit();return false;" src="../images/ToolsItemSearch.gif"
                                                    style="cursor: hand" />
                                    </td>
                                    <td class="form-item" style="width: 20%">
                                        打分日期：</td>
                                    <td>
                                        <cc3:Calendar ID="Calendar6" runat="server" CalendarMode="Date" CalendarResource="../Images/CalendarResource/"
                                            Display="True" ReadOnly="False" Value='<%# Bind("RegisterDate") %>'>
                                        </cc3:Calendar>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="form-item" style="width: 20%">
                                        标题：</td>
                                    <td>
                                        <asp:TextBox ID="TitleTextBox" runat="server" Text='<%# Bind("Title") %>' CssClass="input"
                                            Width="100%"></asp:TextBox></td>
                                    <td class="form-item" style="width: 20%">
                                        打分类型：</td>
                                    <td>
                                        <asp:DropDownList ID="drpMarkType" runat="server">
                                            <asp:ListItem Selected="True">请选择</asp:ListItem>
                                            <asp:ListItem>员工打分</asp:ListItem>
                                            <asp:ListItem>部门打分</asp:ListItem>
                                            <asp:ListItem>部门经理打分</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:TextBox ID="TextBox1" Visible ="false" runat="server" Text='<%# Bind("MarkType") %>' CssClass="input"
                                            Width="100%"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="form-item">
                                        附件
                                    </td>
                                    <td colspan="3" class="blackbordertdpaddingcontent">
                                        <uc1:AttachMentAdd ID="Attachmentadd1" runat="server" CtrlPath="../UserControls/"
                                            AttachMentType="MakeMark" MasterCode='<%# Eval("Code") %>'></uc1:AttachMentAdd>
                                    </td>
                                </tr>
                            </table>
                        </InsertItemTemplate>
                        <ItemTemplate>
                            <table id="Table3" class="table" width="100%">
                                <tr>
                                    <td class="tools-area" width="16">
                                        <img align="absMiddle" src="../images/btn_li.gif" /></td>
                                    <td class="tools-area">
                                        <asp:Button ID="EditButton" runat="server" CommandName="Edit" CssClass="button" Text=" 修改 " />
                                        <asp:Button ID="DeleteButton" runat="server" CommandName="Delete" CssClass="button"
                                            Text=" 删除 " />
                                        <input id="btnClose" runat="server" class="button" name="btnClose" onclick="javascript:window.close();"
                                            type="button" value=" 关闭 " />
                                    </td>
                                </tr>
                            </table>
                            <table border="0" cellpadding="0" cellspacing="0" class="form" width="100%">
                                <tr>
                                    <td class="form-item" style="width: 20%">
                                        部门：</td>
                                    <td>
                                        <asp:Label ID="UnitLabel" runat="server" Text='<%# Bind("UnitCode") %>'></asp:Label></td>
                                    <td class="form-item" style="width: 20%">
                                        打分日期：</td>
                                    <td>
                                        <asp:Label ID="RegisterDateLabel" runat="server" Text='<%# Bind("RegisterDate") %>'></asp:Label></td>
                                </tr>
                                <tr>
                                    <td class="form-item" style="width: 20%">
                                        标题：</td>
                                    <td>
                                        <asp:Label ID="TitleLabel" runat="server" Text='<%# Bind("Title") %>'></asp:Label></td>
                                    <td class="form-item" style="width: 20%">
                                        打分类型：</td>
                                    <td>
                                        <asp:Label ID="MarkTypeLabel" runat="server" Text='<%# Bind("MarkType") %>'></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="form-item">
                                        附件</td>
                                    <td colspan="3" class="blackbordertdpaddingcontent">
                                        <uc1:AttachMentList ID="Attachmentlist1" runat="server" CtrlPath="../UserControls/"
                                            AttachMentType="MakeMark" MasterCode='<%# Eval("Code") %>'></uc1:AttachMentList>
                                    </td>
                                </tr>
                            </table>
                        </ItemTemplate>
                    </asp:FormView>
                    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" DataObjectTypeName="RmsOA.MODEL.GK_OA_MakeMarksModel"
                        DeleteMethod="Delete" InsertMethod="Insert" SelectMethod="GetGK_OA_MakeMarksListOne"
                        TypeName="RmsOA.BFL.GK_OA_MakeMarksBFL" UpdateMethod="Update" OnInserted="ObjectDataSource1_Inserted">
                        <SelectParameters>
                            <asp:QueryStringParameter Name="Code" QueryStringField="Code" Type="Int32" />
                        </SelectParameters>
                    </asp:ObjectDataSource>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
