<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="SegCoaSetup.aspx.cs" Inherits="SegCoaSetup" Title="Chart-of-Accounts"  Theme="Themes" MaintainScrollPositionOnPostback="true"%>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">  

<div id="frmMainDiv" style="background-color:White; width:100%; "> 
<table style="width:100%; font-family:Verdana; font-size:8pt;">
<tr>
<td style="width:1%;" align="center"></td>
<td style="width:98%;" align="left">
<asp:UpdatePanel ID="UpdatePanel3" runat="server">
<ContentTemplate>
<table style="width:100%;">
<tr>
<td colspan="3" style="width:100%;" align="left">
<table style="width:100%;" border="0" cellpadding="0" cellspacing="0" >
<tr>
<td style="width:100%; vertical-align:top;"  align="left">
<asp:GridView  RowStyle-Height="25px" CssClass="mGrid" PagerStyle-CssClass="pgr"  
        AlternatingRowStyle-CssClass="alt" ID="dgLevel" runat="server" AutoGenerateColumns="False" 
        Caption="Step:1 - Level Setup" CaptionAlign="Top"
        AllowPaging="True" Width="100%" BackColor="White" BorderWidth="1px" BorderStyle="Solid"
        CellPadding="2" BorderColor="LightGray" Font-Size="8pt" AllowSorting="True" 
        PageSize="5" onrowcancelingedit="dgLevel_RowCancelingEdit" 
        onrowediting="dgLevel_RowEditing" onrowupdating="dgLevel_RowUpdating" 
        onrowcommand="dgLevel_RowCommand" onrowdeleting="dgLevel_RowDeleting" 
        onrowdatabound="dgLevel_RowDataBound">
  <HeaderStyle Font-Size="10pt" ForeColor="Black" Font-Bold="True" BackColor="LightGray" HorizontalAlign="center" /> 
  <Columns>
  
  <asp:TemplateField ItemStyle-Width="130px" ItemStyle-HorizontalAlign="Center">
  <ItemTemplate>  
  <asp:LinkButton ID="lbAddNew" runat="server" CausesValidation="False" CommandName="AddNew" Text="AddNew" Font-Size="8pt"></asp:LinkButton>
  <asp:LinkButton ID="lbEdit" runat="server" Text="Edit" CausesValidation="false" CommandName="Edit" Font-Size="8pt"/>
  <asp:LinkButton ID="lbDelete" runat="server" CausesValidation="False" CommandName="Delete" Text="Delete" Font-Size="8pt"></asp:LinkButton>  
  <ajaxToolkit:ConfirmButtonExtender ID="detdeleteconfirm" runat="server" ConfirmText="Are you sure to delete??" TargetControlID="lbDelete"></ajaxToolkit:ConfirmButtonExtender>
  </ItemTemplate>
  <EditItemTemplate>
  <asp:LinkButton ID="lbUpdate" runat="server" Text="Update" CausesValidation="false" CommandName="Update" Font-Size="8pt"/>
  <asp:LinkButton ID="lbCancel" runat="server" Text="Cancel" CausesValidation="false" CommandName="Cancel" Font-Size="8pt"/>
  </EditItemTemplate>
  <FooterTemplate>
  <asp:LinkButton ID="lbInsert" runat="server" Text="Insert" CausesValidation="false" CommandName="Insert" Font-Size="8pt"/>
  <asp:LinkButton ID="lbCancel" runat="server" Text="Cancel" CausesValidation="false" CommandName="Cancel" Font-Size="8pt"/>
  </FooterTemplate>
  </asp:TemplateField>
  
  <asp:TemplateField HeaderText="Level Code" ItemStyle-Width="100px" ItemStyle-HorizontalAlign="Center">
  <ItemTemplate>  
  <asp:Label ID="lblLevelCode" runat="server" Text='<%#Eval("lvl_code") %>' Font-Size="8pt"></asp:Label>     
  </ItemTemplate>
  <EditItemTemplate>  
  <asp:TextBox SkinID="tbGray" ID="txtLevelCode" runat="server" Text='<%#Eval("lvl_code") %>' Width="100px" CssClass="tbc" Font-Size="8pt" MaxLength="2" TabIndex="1"></asp:TextBox>   
  </EditItemTemplate>
  <FooterTemplate>
  <asp:TextBox SkinID="tbGray" ID="txtLevelCode" runat="server" Text="" Font-Size="8pt" Width="100px" CssClass="tbc" MaxLength="2" TabIndex="1"></asp:TextBox>   
  </FooterTemplate>
  </asp:TemplateField>
  
  <asp:TemplateField HeaderText="Description" ItemStyle-Width="150px" ItemStyle-HorizontalAlign="Left">
  <ItemTemplate>
  <asp:Label ID="lblLevelDesc" runat="server" Text='<%#Eval("lvl_desc") %>' Font-Size="8pt"></asp:Label>  
  </ItemTemplate>
  <EditItemTemplate>  
  <asp:TextBox SkinID="tbGray" ID="txtLevelDesc" runat="server" Text='<%#Eval("lvl_Desc") %>' Width="150px" CssClass="tbl" Font-Size="8pt" MaxLength="50" TabIndex="2"></asp:TextBox>   
  </EditItemTemplate>
  <FooterTemplate>
  <asp:TextBox SkinID="tbGray" ID="txtLevelDesc" runat="server" Text="" Font-Size="8pt" MaxLength="50" TabIndex="2" CssClass="tbc"  Width="150px"></asp:TextBox>   
  </FooterTemplate>
  </asp:TemplateField>  
  
  <asp:TemplateField HeaderText="Length" ItemStyle-Width="80px" ItemStyle-HorizontalAlign="Center">
  <ItemTemplate>  
  <asp:Label ID="lblLevelMaxSize" runat="server" Text='<%#Eval("lvl_max_size") %>' Font-Size="8pt"></asp:Label>     
  </ItemTemplate>
  <EditItemTemplate>  
  <asp:TextBox SkinID="tbGray" ID="txtLevelMaxSize" runat="server" Text='<%#Eval("lvl_max_size") %>' Width="80px" CssClass="tbc" Font-Size="8pt" MaxLength="1" TabIndex="3"></asp:TextBox>   
  </EditItemTemplate>
  <FooterTemplate>
  <asp:TextBox SkinID="tbGray" ID="txtLevelMaxSize" runat="server" Text="" Font-Size="8pt" MaxLength="1" TabIndex="3" Width="80px" CssClass="tbc"></asp:TextBox>   
  </FooterTemplate>
  </asp:TemplateField>
  
  <asp:TemplateField HeaderText="Enabled" ItemStyle-Width="80px" ItemStyle-HorizontalAlign="Center">
  <ItemTemplate>  
  <asp:Label ID="lblLevelEnabled" runat="server" Text='<%#Eval("lvl_enabled") %>' Font-Size="8pt"></asp:Label>     
  </ItemTemplate>
  <EditItemTemplate>  
  <asp:DropDownList SkinID="ddlPlain" ID="ddlLevelEnabled" runat="server" Font-Size="8pt" Width="80px" CssClass="tbc" Height="18px" TabIndex="4" >
    <asp:ListItem></asp:ListItem>
    <asp:ListItem Text="Yes" Value="Y"></asp:ListItem>
    <asp:ListItem Text="No" Value="N"></asp:ListItem>
    </asp:DropDownList>  
  </EditItemTemplate>
  <FooterTemplate>
  <asp:DropDownList SkinID="ddlPlain" ID="ddlLevelEnabled" runat="server" Font-Size="8pt" Width="80px" CssClass="tbc" Height="18px" TabIndex="4" >
    <asp:ListItem></asp:ListItem>
    <asp:ListItem Text="Yes" Value="Y"></asp:ListItem>
    <asp:ListItem Text="No" Value="N"></asp:ListItem>
    </asp:DropDownList>
  </FooterTemplate>
  </asp:TemplateField>
  
  <asp:TemplateField HeaderText="Type" ItemStyle-Width="120px" ItemStyle-HorizontalAlign="Center">
  <ItemTemplate>  
  <asp:Label ID="lblLevelSegType" runat="server" Text='<%#Eval("lvl_seg_type") %>' Font-Size="8pt"></asp:Label>     
  </ItemTemplate>
  <EditItemTemplate>  
  <asp:DropDownList SkinID="ddlPlain" ID="ddlLevelSegType" runat="server" Font-Size="8pt" Width="120px" Height="18px" TabIndex="4" >
    <asp:ListItem></asp:ListItem>
    <asp:ListItem Text="Natural Segments" Value="N"></asp:ListItem>
    <asp:ListItem Text="Cost Center" Value="X"></asp:ListItem>
    <asp:ListItem Text="Balance Segments" Value="B"></asp:ListItem>
    </asp:DropDownList>   
  </EditItemTemplate>
  <FooterTemplate>
  <asp:DropDownList SkinID="ddlPlain" ID="ddlLevelSegType" runat="server" Font-Size="8pt" Width="120px" Height="18px" TabIndex="4" >
    <asp:ListItem></asp:ListItem>
    <asp:ListItem Text="Natural Segments" Value="N"></asp:ListItem>
    <asp:ListItem Text="Cost Center" Value="X"></asp:ListItem>
    <asp:ListItem Text="Balance Segments" Value="B"></asp:ListItem>
    </asp:DropDownList>
  </FooterTemplate>
  </asp:TemplateField>
  
  <asp:TemplateField HeaderText="Order" ItemStyle-Width="70px" ItemStyle-HorizontalAlign="Center">
  <ItemTemplate>  
  <asp:Label ID="lblLevelOrder" runat="server" Text='<%#Eval("lvl_order") %>' Font-Size="8pt"></asp:Label>     
  </ItemTemplate>
  <EditItemTemplate>  
  <asp:TextBox SkinID="tbGray" ID="txtLevelOrder" runat="server" Text='<%#Eval("lvl_order") %>' Width="70px" CssClass="tbc" Font-Size="8pt" MaxLength="2" TabIndex="5"></asp:TextBox>   
  </EditItemTemplate>
  <FooterTemplate>
  <asp:TextBox SkinID="tbGray" ID="txtLevelOrder" runat="server" Text="" Font-Size="8pt" Width="70px" CssClass="tbc" MaxLength="2" TabIndex="5"></asp:TextBox>   
  </FooterTemplate>
  </asp:TemplateField>
  
  </Columns>
                        <RowStyle BackColor="white" />
                        <EditRowStyle BackColor="" />
                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                        <HeaderStyle BackColor="Silver" Font-Bold="True" ForeColor="Black" />
                        <AlternatingRowStyle BackColor="#F5F5F5" />
</asp:GridView>
 
</td>
</tr>
</table>
</td>
</tr>
<tr><td style="height:10px; line-height:normal; vertical-align:baseline;" colspan="3"></td></tr>
<tr>
<td style="width:38%;" valign="top" align="left">
<asp:Panel ID="pnlTreeView" runat="server" Width="400px" ScrollBars="Auto" Height="450px" HorizontalAlign="Left">
   <asp:TreeView ID="TreeView1" runat="server" AutoGenerateDataBindings="False" Width="100%"
             onselectednodechanged="TreeView1_SelectedNodeChanged" ImageSet="Msdn"
        ForeColor="Blue" ParentNodeStyle-ForeColor="Green">           
            <SelectedNodeStyle Font-Underline="False" HorizontalPadding="3px" 
                VerticalPadding="2px" BackColor="White" BorderColor="#888888" 
                BorderStyle="Solid" BorderWidth="1px" />            
            <NodeStyle Font-Size="8pt" ForeColor="Black" HorizontalPadding="2px" 
                NodeSpacing="2px" VerticalPadding="3px" Font-Names="Verdana" />
   </asp:TreeView>
</asp:Panel>
</td>
<td style="width:2%; border-right:solid 1px gray;">
</td>
<td style="width:60%; vertical-align:top;" align="left">
<table style="vertical-align:top; width:100%;">
    <tr>
        <td align="center" colspan="5" 
            style="width:100%; vertical-align:top; font-size:10pt; color:Maroon;">
            Step:2 - Segment Code Setup</td>
    <tr>
        <td align="left" style="width: 20%;">
            <asp:Label ID="lblSegCode" runat="server" Font-Size="8pt">Segment Code</asp:Label>
        </td>
        <td align="left" style="width: 28%;">
            <asp:TextBox ID="txtSegCode" runat="server" AutoPostBack="False" 
                Font-Size="8pt" MaxLength="7" SkinID="tbGray" TabIndex="5" Width="100%"></asp:TextBox>
        </td>
        <td style="width:4%;">
        </td>
        <td align="left" style="width: 20%;">
            <asp:Label ID="lblLvlCode" runat="server" Font-Size="8pt" Width="100%">Level</asp:Label>
        </td>
        <td align="left" style="width: 28%;">
            <asp:TextBox ID="txtLvlCode" runat="server" AutoPostBack="False" 
                Font-Size="8pt" MaxLength="2" SkinID="tbGray" TabIndex="6" Width="94%"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td align="left" style="width: 20%;">
            <asp:Label ID="lblSegDesc" runat="server" Font-Size="8pt" Width="100%">Description</asp:Label>
        </td>
        <td align="left" colspan="4" style="width: 80%;">
            <asp:TextBox ID="txtSegDesc" runat="server" AutoPostBack="False" 
                Font-Size="8pt" MaxLength="150" SkinID="tbGray" TabIndex="7" Width="98%"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td align="left" style="width: 20%;">
            <asp:Label ID="lblParentCode" runat="server" Font-Size="8pt">Parent Code</asp:Label>
        </td>
        <td align="left" style="width: 28%;">
            <asp:TextBox ID="txtParentCode" runat="server" AutoPostBack="False" 
                Font-Size="8pt" MaxLength="7" SkinID="tbGray" TabIndex="8" Width="100%"></asp:TextBox>
        </td>
        <td style=" width:4%;">
        </td>
        <td align="left" style="width: 20%;">
            <asp:Label ID="lblBudAllowed" runat="server" Font-Size="8pt">Budget Allowed</asp:Label>
        </td>
        <td align="left" style="width: 28%;">
            <asp:DropDownList ID="ddlBudAllowed" runat="server" AutoPostBack="False" 
                Font-Size="8pt" SkinID="ddlPlain" TabIndex="9" Width="100%">
                <asp:ListItem Text="Yes" Value="Y"></asp:ListItem>
                <asp:ListItem Text="No" Value="N"></asp:ListItem>
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td align="left" style="width: 20%;">
            <asp:Label ID="lblPostAllowed" runat="server" Font-Size="8pt">Post Allowed</asp:Label>
        </td>
        <td align="left" style="width: 28%;">
            <asp:DropDownList ID="ddlPostAllowed" runat="server" AutoPostBack="False" 
                Font-Size="8pt" SkinID="ddlPlain" TabIndex="10" Width="101%">
                <asp:ListItem Text="Yes" Value="Y"></asp:ListItem>
                <asp:ListItem Text="No" Value="N"></asp:ListItem>
            </asp:DropDownList>
        </td>
        <td style=" width:4%;">
        </td>
        <td align="left" style="width: 20%;">
            <asp:Label ID="lblAccType" runat="server" Font-Size="8pt">Account Type</asp:Label>
        </td>
        <td align="left" style="width: 28%;">
            <asp:DropDownList ID="ddlAccType" runat="server" AutoPostBack="False" 
                Font-Size="8pt" SkinID="ddlPlain" TabIndex="11" Width="99%">
                <asp:ListItem Text="N/A" Value="N"></asp:ListItem>
                <asp:ListItem Text="Asset" Value="A"></asp:ListItem>
                <asp:ListItem Text="Liability" Value="L"></asp:ListItem>
                <asp:ListItem Text="Income" Value="I"></asp:ListItem>
                <asp:ListItem Text="Expense" Value="E"></asp:ListItem>
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td align="left" style="width: 20%;">
            <asp:Label ID="lblOpenDate" runat="server" Font-Size="8pt">Open Date</asp:Label>
        </td>
        <td align="left" style="width: 28%;">
            <asp:TextBox ID="txtOpenDate" runat="server" AutoPostBack="False" 
                Font-Size="8pt" MaxLength="11" SkinID="tbGray" TabIndex="12" Width="100%"></asp:TextBox>
        <ajaxtoolkit:calendarextender runat="server" ID="Calendarextender2" TargetControlID="txtOpenDate" Format="dd/MM/yyyy"/>
        </td>
        <td style="width:4%;">
        </td>
        <td align="left" style="width: 20%;">
            <asp:Label ID="lblRootLeaf" runat="server" Font-Size="8pt">Root/Leaf?</asp:Label>
        </td>
        <td align="left" style="width: 28%;">
            <asp:DropDownList ID="ddlRootLeaf" runat="server" AutoPostBack="False" 
                Font-Size="8pt" SkinID="ddlPlain" TabIndex="13" Width="99%">
                <asp:ListItem Text="Root" Value="R"></asp:ListItem>
                <asp:ListItem Text="Leaf" Value="L"></asp:ListItem>
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td align="left" style="width: 20%;">
            <asp:Label ID="lblTaxable" runat="server" Font-Size="8pt">Taxable</asp:Label>
        </td>
        <td align="left" style="width: 28%;">
            <asp:DropDownList ID="ddlTaxable" runat="server" AutoPostBack="False" 
                Font-Size="8pt" SkinID="ddlPlain" TabIndex="14" Width="101%">
                <asp:ListItem Text="Yes" Value="Y"></asp:ListItem>
                <asp:ListItem Text="No" Value="N"></asp:ListItem>
            </asp:DropDownList>
        </td>
        <td style=" width:4%;">
        </td>
        <td align="left" style="width: 20%;">
            <asp:Label ID="lblStatus" runat="server" Font-Size="8pt">Status</asp:Label>
        </td>
        <td align="left" style="width: 28%;">
            <asp:DropDownList ID="ddlStatus" runat="server" AutoPostBack="False" 
                Font-Size="8pt" SkinID="ddlPlain" TabIndex="15" Width="99%">
                <asp:ListItem Text="Active" Value="A"></asp:ListItem>
                <asp:ListItem Text="Inactive" Value="U"></asp:ListItem>
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td colspan="5" style="width:100%; height:20px; vertical-align:bottom;">
            <table style="width:100%;">
                <tr>
                    <td style="width:100px;">
                    </td>
                    <td align="center">
                        <asp:Button ID="btnClear" runat="server" Height="25px" onclick="btnClear_Click" 
                            Text="Clear" ToolTip="Clear" Width="100px" />
                    </td>
                    <td style="width:20px;">
                    </td>
                    <td align="center">
                        <asp:Button ID="btnDelete" runat="server" Height="25px" 
                            onclick="btnDelete_Click" 
                            onclientclick="javascript:return window.confirm('are u really want to delete these data')" 
                            Text="Delete" ToolTip="Delete" Width="100px" />
                    </td>
                    <td style="width:20px;">
                    </td>
                    <td align="center">
                        <asp:Button ID="btnSave" runat="server" Height="25px" onclick="btnSave_Click" 
                            Text="Save" ToolTip="Save" Width="100px" />
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td align="center" colspan="5" 
            style="width:100%; height:23px; vertical-align:top;">
            <asp:Label ID="lblTransStatus" runat="server" Font-Size="8pt" Text=""></asp:Label>
            <asp:UpdateProgress ID="updateProgress" runat="server" 
                AssociatedUpdatePanelID="UpdatePanel3">
                <ProgressTemplate>
                    <div ID="progressBackgroundFilter">
                    </div>
                    <div ID="processMessage">
                        <img src="img/loading.gif" alt="" />
                    </div>
                </ProgressTemplate>
            </asp:UpdateProgress>
        </td>
    </tr>
    <tr>
        <td colspan="5" style="width:100%; height:150px; vertical-align:bottom;">
            <table style="width:100%;">
                <tr>
                    <td align="center" colspan="5" 
                        style="width:100%; vertical-align:bottom; font-size:10pt; color:Maroon;">
                        Step:3 - Chart-of-Account Code Generation</td>
                </tr>
                <tr>
                    <td colspan="5" style="width:100%; height:20px; vertical-align:bottom;">
                        <asp:GridView  RowStyle-Height="25px" ID="dgGlCoaGen" runat="server" AllowPaging="True" 
                            AllowSorting="true" AlternatingRowStyle-CssClass="alt" 
                            AutoGenerateColumns="false" BackColor="White" BorderColor="LightGray" 
                            BorderStyle="Solid" BorderWidth="1px" CellPadding="2" CellSpacing="0" 
                            CssClass="mGrid" Font-Size="8pt" PagerStyle-CssClass="pgr" PageSize="6" 
                            ShowHeader="false" Width="100%">
                            <Columns>
                                <asp:BoundField DataField="lvl_desc" ItemStyle-BackColor="LightGray" 
                                    ItemStyle-Font-Bold="true" ItemStyle-Height="18px" 
                                    ItemStyle-HorizontalAlign="Left" ItemStyle-Width="120px" />
                                <asp:BoundField DataField="seg_code" ItemStyle-HorizontalAlign="Left" 
                                    ItemStyle-Width="40px" />
                                <asp:BoundField DataField="seg_desc" ItemStyle-HorizontalAlign="Left" 
                                    ItemStyle-Width="140px" />
                            </Columns>
                        </asp:GridView>
                    </td>
                </tr>
                <tr>
                    <td colspan="5" style="width:100%; height:20px; vertical-align:bottom;">
                        <table style="width:100%; text-align:center;">
                            <tr>
                                <td style="width:100px;">
                                </td>
                                <td style="width:100px;">
                                    <asp:Button ID="btnClearCoa" runat="server" Height="25px" 
                                        onclick="btnClearCoa_Click" Text="Clear" ToolTip="Clear" Width="100px" />
                                </td>
                                <td style="width:20px;">
                                </td>
                                <td style="width:100px;">
                                    <asp:Button ID="btnShowCoa" runat="server" Height="25px" Text="Show COA"
                                        ImageUrl="~/img/show.jpg" onclick="btnShowCoa_Click" ToolTip="Show COA" 
                                        Width="100px" />
                                </td>
                                <td style="width:20px;">
                                </td>
                                <td style="width:100px;">
                                    <asp:Button ID="btnGenCoa" runat="server" Height="25px" Text="Generate COA"
                                        ImageUrl="~/img/generate.jpg" onclick="btnGenCoa_Click" ToolTip="Geneate COA" 
                                        Width="100px" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    </table>
 
</td>
</tr>
<tr><td style="height:10px;" colspan="3"></td></tr>
<tr><td style="height:10px;" colspan="3" align="center">
<asp:Button ID="btnSaveCoa" runat="server" Text="Save COA" Visible="false" SkinID="lbPlain" onclick="btnSaveCoa_Click" Height="25px" Width="100px"></asp:Button>
</td></tr>
<tr>
<td  colspan="3" style="width:100%;" align="center">
<asp:GridView  RowStyle-Height="25px" CssClass="mGrid" PagerStyle-CssClass="pgr"  AlternatingRowStyle-CssClass="alt" ID="dgGlCoa" runat="server" AutoGenerateColumns="false" 
        Caption="Chart-of-Account Code" CaptionAlign="Top"
        AllowPaging="True" Width="100%" BackColor="White" BorderWidth="1px" BorderStyle="Solid"
        CellPadding="2" CellSpacing="0" BorderColor="LightGray" Font-Size="8pt" 
        AllowSorting="true" PageSize="20" 
        onpageindexchanging="dgGlCoa_PageIndexChanging" 
        onrowcancelingedit="dgGlCoa_RowCancelingEdit" 
        onrowdeleting="dgGlCoa_RowDeleting" onrowediting="dgGlCoa_RowEditing" 
        onrowupdating="dgGlCoa_RowUpdating" onrowdatabound="dgGlCoa_RowDataBound">
  <HeaderStyle Font-Size="8" Font-Names="Arial" Font-Bold="True" BackColor="Blue" HorizontalAlign="center" ForeColor="White"/>

  <Columns>
  <asp:TemplateField ItemStyle-Width="100px" ItemStyle-HorizontalAlign="Center">
  <ItemTemplate>  
  <asp:CheckBox ID="chkInc" runat="server" Checked="true" Visible="false" AutoPostBack="true" OnCheckedChanged="chkIncCheck_Changed" />
  <asp:LinkButton ID="lbEdit" runat="server" Text="Edit" CausesValidation="false" CommandName="Edit"/>
  <asp:LinkButton ID="lbDelete" runat="server" CausesValidation="False" CommandName="Delete" Text="Delete"></asp:LinkButton>
  <ajaxToolkit:ConfirmButtonExtender ID="detdeleteconfirm" runat="server" ConfirmText="Are you sure to delete??" TargetControlID="lbDelete"></ajaxToolkit:ConfirmButtonExtender>
  </ItemTemplate>
  <EditItemTemplate>
  <asp:LinkButton ID="lbUpdate" runat="server" Text="Update" CausesValidation="false" CommandName="Update" />
  <asp:LinkButton ID="lbCancel" runat="server" Text="Cancel" CausesValidation="false" CommandName="Cancel"/>
  </EditItemTemplate>
  </asp:TemplateField>
   
  <asp:TemplateField HeaderText="COA Code" ItemStyle-Width="100px" ItemStyle-HorizontalAlign="Center" ItemStyle-Height="30px">
  <ItemTemplate>
  <asp:Label ID="lblGlCoaCode" runat="server" Text='<%#Eval("gl_coa_code") %>' Width="100px" Font-Size="8pt"></asp:Label>  
  </ItemTemplate>
  <EditItemTemplate>  
  <asp:TextBox SkinID="tbGray" ID="txtGlCoaCode" runat="server" Text='<%#Eval("gl_coa_code") %>' Width="100px" Font-Size="8" MaxLength="13"></asp:TextBox>   
  </EditItemTemplate>
  </asp:TemplateField>
  
  <asp:TemplateField HeaderText="COA Code" ItemStyle-Width="420px" ItemStyle-HorizontalAlign="Left">
  <ItemTemplate>
  <asp:Label ID="lblGlCoaDesc" runat="server" Text='<%#Eval("coa_desc") %>' Font-Size="8pt"></asp:Label>  
  </ItemTemplate>
  <EditItemTemplate>  
  <asp:TextBox SkinID="tbGray" ID="txtGlCoaDesc" runat="server" Text='<%#Eval("coa_desc") %>' Width="420px" Font-Size="8pt" MaxLength="150"></asp:TextBox>   
  </EditItemTemplate>
  </asp:TemplateField>
  
  <asp:TemplateField HeaderText="Account Type" ItemStyle-Width="100px" ItemStyle-HorizontalAlign="Center">
  <ItemTemplate>
  <asp:Label ID="lblGlAccType" runat="server" Text='<%#Eval("acc_type") %>' Width="100px" Font-Size="8pt"></asp:Label>  
  </ItemTemplate>
  <EditItemTemplate>  
  <asp:DropDownList SkinID="ddlPlain" ID="ddlGlAccType" runat="server" Width="100px" TabIndex="7" AutoPostBack="False" Font-Size="8">
    <asp:ListItem Text="Asset" Value="A"></asp:ListItem>
    <asp:ListItem Text="Liability" Value="L"></asp:ListItem>
    <asp:ListItem Text="Income" Value="I"></asp:ListItem>
    <asp:ListItem Text="Expense" Value="E"></asp:ListItem>
    </asp:DropDownList>   
  </EditItemTemplate>
  </asp:TemplateField>
  
  <asp:TemplateField HeaderText="Status" ItemStyle-Width="80px" ItemStyle-HorizontalAlign="Center">
  <ItemTemplate>
  <asp:Label ID="lblGlStatus" runat="server" Text='<%#Eval("status") %>' Width="80px" Font-Size="8pt"></asp:Label>  
  </ItemTemplate>
  <EditItemTemplate>  
  <asp:DropDownList SkinID="ddlPlain" ID="ddlGlStatus" runat="server" Width="80px" TabIndex="7" AutoPostBack="False" Font-Size="8">
    <asp:ListItem Text="Active" Value="A"></asp:ListItem>
    <asp:ListItem Text="Inactive" Value="U"></asp:ListItem>
    </asp:DropDownList>   
  </EditItemTemplate>
  </asp:TemplateField>  
  <asp:BoundField HeaderText="Natural Code" ItemStyle-Width="70px" DataField="coa_natural_code"  ItemStyle-HorizontalAlign="Center" ItemStyle-Height="15px"/>  
  </Columns>
                        <RowStyle BackColor="white" />
                        <EditRowStyle BackColor="" />
                        <PagerStyle BackColor="" HorizontalAlign="Center" />
                        <HeaderStyle BackColor="" Font-Bold="True" ForeColor="Black" />
                        <AlternatingRowStyle BackColor="#F5F5F5" />
</asp:GridView>
</td>
</tr>
</table>
</ContentTemplate>
</asp:UpdatePanel>  
</td>
<td style="width:1%;" align="center"></td>
</tr>
</table> 
</div> 
<script language="javascript" type="text/javascript">
    function disposeTree(sender, args) {
        var elements = args.get_panelsUpdating();
        for (var i = elements.length - 1; i >= 0; i--) {
            var element = elements[i];
            var allnodes = element.getElementsByTagName('*'),
                length = allnodes.length;
            var nodes = new Array(length)
            for (var k = 0; k < length; k++) {
                nodes[k] = allnodes[k];
            }
            for (var j = 0, l = nodes.length; j < l; j++) {
                var node = nodes[j];
                if (node.nodeType === 1) {
                    if (node.dispose && typeof (node.dispose) === "function") {
                        node.dispose();
                    }
                    else if (node.control && typeof (node.control.dispose) === "function") {
                        node.control.dispose();
                    }

                    var behaviors = node._behaviors;
                    if (behaviors) {
                        behaviors = Array.apply(null, behaviors);
                        for (var k = behaviors.length - 1; k >= 0; k--) {
                            behaviors[k].dispose();
                        }
                    }
                }
            }
            element.innerHTML = "";
        }
    }
    Sys.WebForms.PageRequestManager.getInstance().add_pageLoading(disposeTree);
</script>
</asp:Content>

