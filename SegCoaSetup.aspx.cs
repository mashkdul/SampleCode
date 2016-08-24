using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.OracleClient;

using icsl;

public partial class SegCoaSetup : System.Web.UI.Page
{
    public static Permis per;
    private static DataTable dtSegParent = new DataTable();
    private static DataTable dtSegChild = new DataTable();
    
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["user"]==null)
            {
                Response.Redirect("Default.aspx");
            }
            else
            {
                string pageName = DataManager.GetCurrentPageName();
                string modid = PermisManager.getModuleId(pageName);
                per = PermisManager.getUsrPermis(Session["user"].ToString().Trim().ToUpper(), modid);
                if (per != null && per.AllowView == "Y")
                {
                    ((Label)Page.Master.FindControl("lblLogin")).Text = Session["wnote"].ToString();
                    ((LinkButton)Page.Master.FindControl("lbLogout")).Visible = true; 
                }
                else
                {
                    Response.Redirect("Default.aspx");
                }
            }
        }
        catch
        {
            Response.Redirect("Default.aspx");
        }       
        txtOpenDate.Attributes.Add("onBlur", "formatdate('" + txtOpenDate.ClientID + "')");
        if (!IsPostBack)
        {
            DataTable dtLvl = LvlManager.GetLevels();
            dgLevel.DataSource = dtLvl;
            dgLevel.DataBind();

            dgGlCoaGen.DataSource = LvlManager.GetLevelsGrid();
            dgGlCoaGen.DataBind();
            Populate();
            TreeView1.CollapseAll();
            
        }
    }
    protected void PopulateNode(Object sender, TreeNodeEventArgs e)
    {
        
    }
    
    public void Populate()
    {
        dtSegParent = SegCoaManager.GetSegCoaAll();
        TreeNode newNode;        
        foreach (DataRow row in dtSegParent.Rows)
        {
            newNode = new TreeNode();
            newNode.Text =  row["seg_coa_code"].ToString() + " - " + row["seg_coa_desc"].ToString() ;
            newNode.Value = row["seg_coa_code"].ToString();
            //newNode.SelectAction = TreeNodeSelectAction.Expand;
            //node.ChildNodes.Add(newNode);
            TreeView1.Nodes.Add(newNode);
            
            if (row["rootleaf"].ToString() == "R")
            {
                PopChild(row["seg_coa_code"].ToString(), newNode);                
            }
        }
    }
    public void PopChild(string segcode, TreeNode node)
    {
        DataTable dt = SegCoaManager.GetSegCoaChild("parent_code='" + segcode + "' order by to_number(seg_coa_code)");
        TreeNode newNode;
        
        foreach (DataRow dr in dt.Rows)
        {
            newNode = new TreeNode();
            newNode.Text = dr["seg_coa_code"].ToString() + " - " + dr["seg_coa_desc"].ToString();
            newNode.Value = dr["seg_coa_code"].ToString();
            //newNode.SelectAction = TreeNodeSelectAction.Expand;
            node.ChildNodes.Add(newNode);            
            if (dr["rootleaf"].ToString() == "R")
            {
                PopChild(dr["seg_coa_code"].ToString(), newNode);
            }            
        }        
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(3000);
        if (txtSegCode.Text != "")
        {
            SegCoa sgcoa = SegCoaManager.getSegCoa(txtSegCode.Text);
            if (sgcoa != null)
            {
                sgcoa.SegCoaDesc = txtSegDesc.Text;
                sgcoa.RootLeaf = ddlRootLeaf.SelectedValue;
                sgcoa.Taxable = ddlTaxable.SelectedValue;
                sgcoa.Status = ddlStatus.SelectedValue;
                sgcoa.PostAllowed = ddlPostAllowed.SelectedValue;
                sgcoa.ParentCode = txtParentCode.Text;
                sgcoa.OpenDate = txtOpenDate.Text;
                sgcoa.LvlCode = txtLvlCode.Text;
                sgcoa.BudAllowed = ddlBudAllowed.SelectedValue;
                sgcoa.AccType = ddlAccType.SelectedValue;
                SegCoaManager.UpdateSegCoa(sgcoa);
            }
            else
            {
                sgcoa = new SegCoa();
                sgcoa.GlSegCode = txtSegCode.Text;
                sgcoa.BookName = Session["book"].ToString();
                sgcoa.SegCoaDesc = txtSegDesc.Text;
                sgcoa.RootLeaf = ddlRootLeaf.SelectedValue;
                sgcoa.Taxable = ddlTaxable.SelectedValue;
                sgcoa.Status = ddlStatus.SelectedValue;
                sgcoa.PostAllowed = ddlPostAllowed.SelectedValue;
                sgcoa.ParentCode = txtParentCode.Text;
                sgcoa.OpenDate = txtOpenDate.Text;
                sgcoa.LvlCode = txtLvlCode.Text;
                sgcoa.BudAllowed = ddlBudAllowed.SelectedValue;
                sgcoa.AccType = ddlAccType.SelectedValue;
                SegCoaManager.CreateSegCoa(sgcoa);
            }
            TreeView1.Nodes.Clear();
            Populate();
            TreeNode node= TreeView1.FindNode(Server.HtmlEncode(txtParentCode.Text));
            if (node != null)
            {
                node.Expand();
            }
            ScriptManager.RegisterClientScriptBlock( this, this.GetType(), "ale", "alert('***Segment Codes Saved in Database Successfully!!');", true);
            //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ale", "alert('***Segment Codes Saved in Database Successfully!!');", true);
        }   
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(3000);
        if (txtSegCode.Text != "")
        {
            int coaF = SegCoaManager.getChild(txtSegCode.Text.ToString());
            if (coaF < 1)
            {
                SegCoa sgcoa = SegCoaManager.getSegCoa(txtSegCode.Text.ToString());
                if (sgcoa != null)
                {
                    SegCoaManager.DeleteSegCoa(sgcoa);
                    TreeView1.Nodes.Clear();
                    Populate();
                    ScriptManager.RegisterClientScriptBlock( this, this.GetType(), "ale", "alert('***Segment Codes deleted from Database Successfully!!');", true);
                    btnClear_Click(sender, e);
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock( this, this.GetType(), "ale", "alert('*** You cannnot delete this segment code while child segment is exist!!');", true);
            }
        }        
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        txtSegCode.Text = "";
        txtSegDesc.Text = "";
        ddlAccType.SelectedValue = "N";
        ddlBudAllowed.SelectedValue = "N";
        txtLvlCode.Text = "";
        txtOpenDate.Text = "";
        txtParentCode.Text = "";
        ddlPostAllowed.SelectedValue = "N";
        ddlRootLeaf.SelectedValue = "L";
        ddlTaxable.SelectedValue = "N";
        ddlStatus.SelectedValue = "U";
    }
    
    protected void TreeView1_SelectedNodeChanged(object sender, EventArgs e)
    {        
        SegCoa segcoa = SegCoaManager.getSegCoa(TreeView1.SelectedNode.Value.ToString());
        if (segcoa != null)
        {
            txtSegCode.Text = segcoa.GlSegCode;
            txtSegDesc.Text = segcoa.SegCoaDesc;
            ddlAccType.SelectedValue = segcoa.AccType;
            ddlBudAllowed.SelectedValue = segcoa.BudAllowed;
            txtLvlCode.Text = segcoa.LvlCode;
            txtOpenDate.Text = segcoa.OpenDate;
            txtParentCode.Text = segcoa.ParentCode;
            ddlPostAllowed.SelectedValue = segcoa.PostAllowed;
            ddlRootLeaf.SelectedValue = segcoa.RootLeaf;
            ddlTaxable.SelectedValue = segcoa.Taxable;
            ddlStatus.SelectedValue = segcoa.Status;

            foreach (GridViewRow gvr in dgGlCoaGen.Rows)
            {
                string lvl = "";
                string connectionString = DataManager.OraConnString();
                OracleDataReader dReader;
                OracleConnection conn = new OracleConnection();
                conn.ConnectionString = connectionString;
                OracleCommand cmd = new OracleCommand();
                cmd = new OracleCommand();
                cmd.Connection = conn;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select lvl_code from gl_level_type where lvl_desc='" + gvr.Cells[0].Text.ToString().Trim() + "'";
                conn.Open();
                dReader = cmd.ExecuteReader();
                if (dReader.HasRows == true)
                {
                    while (dReader.Read())
                    {
                        lvl = dReader["lvl_code"].ToString();
                    }
                }
                if (lvl == segcoa.LvlCode)
                {
                    gvr.Cells[1].Text = segcoa.GlSegCode;
                    gvr.Cells[2].Text = segcoa.SegCoaDesc;
                }
            }
            lblTransStatus.Text = "";
        } 
    }
    
    protected void dgLevel_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        DataTable dtLvl = LvlManager.GetLevels();
        dgLevel.EditIndex = -1;
        dgLevel.DataSource = dtLvl;
        dgLevel.DataBind();
    }
    protected void dgLevel_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {        
        GlLevel lvl = LvlManager.getLevel(((TextBox)dgLevel.Rows[e.RowIndex].FindControl("txtLevelCode")).Text);
        lvl.LvlDesc = ((TextBox)dgLevel.Rows[e.RowIndex].FindControl("txtLevelDesc")).Text;
        lvl.LvlMaxSize = ((TextBox)dgLevel.Rows[e.RowIndex].FindControl("txtLevelMaxSize")).Text;
        lvl.LvlEnabled = ((DropDownList)dgLevel.Rows[e.RowIndex].FindControl("ddlLevelEnabled")).SelectedValue.ToString();
        lvl.LvlSegType = ((DropDownList)dgLevel.Rows[e.RowIndex].FindControl("ddlLevelSegType")).SelectedValue.ToString();
        lvl.LvlOrder = ((TextBox)dgLevel.Rows[e.RowIndex].FindControl("txtLevelOrder")).Text;
        LvlManager.UpdateLevel(lvl);
        DataTable dtLvl = LvlManager.GetLevels();
        dgLevel.EditIndex = -1;
        dgLevel.DataSource = dtLvl;
        dgLevel.DataBind();
        dgGlCoaGen.DataSource = LvlManager.GetLevelsGrid();
        dgGlCoaGen.DataBind();
        //ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script language=JavaScript>alert('Updated Successfully! ');</script>");
    }
    protected void dgLevel_RowEditing(object sender, GridViewEditEventArgs e)
    {
        string le = ((Label)dgLevel.Rows[e.NewEditIndex].FindControl("lblLevelEnabled")).Text.ToString();
        string ls = ((Label)dgLevel.Rows[e.NewEditIndex].FindControl("lblLevelSegType")).Text.ToString();
        DataTable dtLvl = LvlManager.GetLevels();
        dgLevel.EditIndex = e.NewEditIndex;
        dgLevel.DataSource = dtLvl;
        dgLevel.DataBind();
        ((DropDownList)dgLevel.Rows[e.NewEditIndex].FindControl("ddlLevelEnabled")).SelectedIndex = ((DropDownList)dgLevel.Rows[e.NewEditIndex].FindControl("ddlLevelEnabled")).Items.IndexOf(((DropDownList)dgLevel.Rows[e.NewEditIndex].FindControl("ddlLevelEnabled")).Items.FindByValue(le));
        ((DropDownList)dgLevel.Rows[e.NewEditIndex].FindControl("ddlLevelSegType")).SelectedIndex = ((DropDownList)dgLevel.Rows[e.NewEditIndex].FindControl("ddlLevelSegType")).Items.IndexOf(((DropDownList)dgLevel.Rows[e.NewEditIndex].FindControl("ddlLevelSegType")).Items.FindByValue(ls));
    }
    protected void btnGenCoa_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(3000);
        foreach (GridViewRow gvr in dgGlCoaGen.Rows)
        {
            if (gvr.Cells[1].Text.ToString().Replace("&nbsp;", "").ToString().Trim() == "")
            {
                //lblTransStatus.Visible = true;
                //lblTransStatus.ForeColor = System.Drawing.Color.Red;
                //lblTransStatus.Text = "Please input all segment code in Chart-of-Account Generation table";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ale", "alert('Please input all segment code in Chart-of-Account Generation table!!');", true);
                return;
            }
        }
        DataTable[] dtm = new DataTable[dgGlCoaGen.Rows.Count];
        DataTable dtCoa = new DataTable();
        string sepTyp = Session["septyp"].ToString();
        int[] lvlSize = new int[dgGlCoaGen.Rows.Count];
        string[] lvlcode = new string[dgGlCoaGen.Rows.Count];
        
        string criteria = "";
        string[] criteriaN = new string[dgGlCoaGen.Rows.Count];
        int x = 0;
        int y = 0;
        
        foreach (GridViewRow gvr in dgGlCoaGen.Rows)
        {
            string connectionString = DataManager.OraConnString();
            OracleDataReader dReader;
            OracleConnection conn = new OracleConnection();
            conn.ConnectionString = connectionString;
            OracleCommand cmd = new OracleCommand();
            cmd = new OracleCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select lvl_code,lvl_max_size from gl_level_type where lvl_desc='" + gvr.Cells[0].Text.ToString().Trim() + "'";
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }
            dReader = cmd.ExecuteReader();
            if (dReader.HasRows == true)
            {
                while (dReader.Read())
                {
                    lvlSize[x] = int.Parse(dReader["lvl_max_size"].ToString());
                    lvlcode[x] = dReader["lvl_code"].ToString();
                }
            }
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
            if (x == 0 & gvr.Cells[1].Text.ToString().Replace("&nbsp;", "").ToString().Trim() != String.Empty)
            {
                criteria += "substr(gl_coa_code,instr(gl_coa_code,'" + sepTyp + "',0)+1," + lvlSize[x].ToString() + ") in (SELECT SEG_COA_CODE  FROM GL_SEG_COA S where rootleaf='L' and lvl_code='" + lvlcode[x] + "' CONNECT BY PRIOR SEG_COA_CODE=PARENT_CODE START WITH seg_coa_code = " +
                               " '" + gvr.Cells[1].Text.ToString().Trim() + "')  ";
            }
            else if (x > 0 & gvr.Cells[1].Text.ToString().Replace("&nbsp;", "").ToString().Trim() != String.Empty)
            {
                criteria += "and substr(gl_coa_code,instr(gl_coa_code,'" + sepTyp + "'," + y + ")+1," + lvlSize[x].ToString() + ") in (SELECT SEG_COA_CODE  FROM GL_SEG_COA S where rootleaf='L' and lvl_code='" + lvlcode[x] + "' CONNECT BY PRIOR SEG_COA_CODE=PARENT_CODE START WITH seg_coa_code = " +
                               " '" + gvr.Cells[1].Text.ToString().Trim() + "' )  ";
            }
            criteriaN[x] = "seg_coa_code in (SELECT SEG_COA_CODE  FROM GL_SEG_COA S where rootleaf='L' and lvl_code='" + lvlcode[x] + "' CONNECT BY PRIOR SEG_COA_CODE=PARENT_CODE START WITH seg_coa_code = '" + gvr.Cells[1].Text.ToString().Trim() + "') ";
            dtm[x] = SegCoaManager.GetSegCoas(criteriaN[x]);

            y = y + lvlSize[x];
            x = x + 1;
        }
        DataTable dtAlready = GlCoaManager.GetGlCoaCode(criteria);
        dtCoa = dtAlready.Clone();
        
        DataTable dt = new DataTable();
        dt.Columns.Add("seg_coa_code", typeof(string));
        dt.Columns.Add("seg_coa_desc", typeof(string));
        dt.Columns.Add("acc_type", typeof(string));
        DataRow dr;
        for (int i = 0; i < dgGlCoaGen.Rows.Count - 1; i++)
        {
            dt.Clear();
            for (int m = 0; m <= dtm[i].Rows.Count - 1; m++)
            {
                for (int n = 0; n <= dtm[i + 1].Rows.Count - 1; n++)
                {
                    dr = dt.NewRow();
                    dr["seg_coa_code"] = ((DataRow)dtm[i].Rows[m])["seg_coa_code"].ToString().Trim() + sepTyp + ((DataRow)dtm[i + 1].Rows[n])["seg_coa_code"].ToString().Trim();
                    dr["seg_coa_desc"] = ((DataRow)dtm[i].Rows[m])["seg_coa_desc"].ToString().Trim() + ", " + ((DataRow)dtm[i + 1].Rows[n])["seg_coa_desc"].ToString().Trim();
                    if (((DataRow)dtm[i].Rows[m])["acc_type"].ToString().Trim() != "N")
                    {
                        dr["acc_type"] = ((DataRow)dtm[i].Rows[m])["acc_type"].ToString().Trim();
                    }
                    else if (((DataRow)dtm[i + 1].Rows[n])["acc_type"].ToString().Trim() != "N")
                    {
                        dr["acc_type"] = ((DataRow)dtm[i + 1].Rows[n])["acc_type"].ToString().Trim();
                    }
                    dt.Rows.Add(dr);
                }
            }
            dtm[i + 1] = dt.Copy();
        }
        DataRow drCoa;
        foreach (DataRow drC in dtm[dgGlCoaGen.Rows.Count-1].Rows)
        {
            drCoa = dtCoa.NewRow();
            drCoa["gl_coa_code"] = drC["seg_coa_code"].ToString().Trim();
            drCoa["coa_desc"] = drC["seg_coa_desc"].ToString().Trim();
            drCoa["acc_type"] = drC["acc_type"].ToString().Trim();
            drCoa["status"] = "U";
            dtCoa.Rows.Add(drCoa);
        }
        for (int i = 0; i < dtAlready.Rows.Count; i++)
        {
            for (int j = 0; j < dtCoa.Rows.Count; j++)
            {
                if (((DataRow)dtCoa.Rows[j])["gl_coa_code"].ToString().Trim() == ((DataRow)dtAlready.Rows[i])["gl_coa_code"].ToString().Trim())
                {
                    dtCoa.Rows.RemoveAt(j);
                }
            }
        }
        char sep = Convert.ToChar(Session["septyp"].ToString());
        foreach (DataRow drc in dtCoa.Rows)
        {
            string mainseg="";
            string[] segcode = drc["gl_coa_code"].ToString().Split(sep);
            for (int i = 0; i < segcode.Length; i++)
            {
                string a = SegCoaManager.getMainSeg(segcode[i].ToString());
                if (a == "N")
                {
                    mainseg = segcode[i].ToString();
                }
            }
            drc.BeginEdit();
            drc["coa_natural_code"] = mainseg;
            drc.AcceptChanges();
        }

        dgGlCoa.EditIndex = -1;
        if (dtCoa.Rows.Count > 0)
        {
            btnSaveCoa.Visible = true;
            //lblTransStatus.ForeColor = System.Drawing.Color.Orange;
            //lblTransStatus.Text = "***To Save Gl COA Codes in Database Click on SaveCoa Link.***";
            ScriptManager.RegisterClientScriptBlock( this, this.GetType(), "ale", "alert('To Save Gl COA Codes in Database Click on SaveCoa Link!!');", true);
            btnSaveCoa.Visible = true;
        }
        else
        {
            btnSaveCoa.Visible = false;
            //lblTransStatus.ForeColor = System.Drawing.Color.Orange;
            //lblTransStatus.Text = "***Possible Gl COA Codes Are Already in Database.***";
            ScriptManager.RegisterClientScriptBlock( this, this.GetType(), "ale", "alert('Possible Gl COA Codes Are Already in Database!!');", true);
            btnSaveCoa.Visible = false;
        }
        dgGlCoa.DataSource = dtCoa;
        Session["coa"] = dtCoa;
        dgGlCoa.DataBind();        
        foreach (GridViewRow gvr in dgGlCoa.Rows)
        {
            ((LinkButton)gvr.FindControl("lbEdit")).Visible = false;
            ((LinkButton)gvr.FindControl("lbDelete")).Visible = false;
            ((CheckBox)gvr.FindControl("chkInc")).Visible = true;
        }
    }
    
    protected void btnClearCoa_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(3000);
        //DataTable dtLvl = LvlManager.GetLevels();        
        //DataColumn dcol1 = new DataColumn("seg_code", typeof(string));
        //dtLvl.Columns.Add(dcol1);
        //DataColumn dcol2 = new DataColumn("seg_desc", typeof(string));
        //dtLvl.Columns.Add(dcol2);
        //dgGlCoaGen.DataSource = dtLvl;
        //dgGlCoaGen.DataBind();
        //dgGlCoa.DataBind();
        dgGlCoaGen.DataSource = LvlManager.GetLevelsGrid();
        dgGlCoaGen.DataBind();
        btnSaveCoa.Visible = false;
        lblTransStatus.Text = "";
    }
    protected void btnShowCoa_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(3000);
        string sepTyp = Session["septyp"].ToString();
        int[] lvlSize = new int[dgGlCoaGen.Rows.Count];
        string[] lvlcode = new string[dgGlCoaGen.Rows.Count];
        string connectionString = DataManager.OraConnString();
        OracleDataReader dReader;
        OracleConnection conn = new OracleConnection();
        conn.ConnectionString = connectionString;
        OracleCommand cmd = new OracleCommand();        
        string criteria = "";
        int x = 0;
        int y = 0;
        foreach (GridViewRow gvr in dgGlCoaGen.Rows)
        {
            cmd = new OracleCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select lvl_code,lvl_max_size from gl_level_type where lvl_desc='" + gvr.Cells[0].Text.ToString().Trim() + "'";
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }
            dReader = cmd.ExecuteReader();
            if (dReader.HasRows == true)
            {
                while (dReader.Read())
                {
                    lvlSize[x] = int.Parse(dReader["lvl_max_size"].ToString());
                    lvlcode[x] = dReader["lvl_code"].ToString();
                }
            }
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
            if (x == 0 & gvr.Cells[1].Text.ToString().Replace("&nbsp;", "").ToString().Trim() != String.Empty)
            {
                criteria += "substr(gl_coa_code,instr(gl_coa_code,'" + sepTyp + "',0)+1," + lvlSize[x].ToString() + ") in (SELECT SEG_COA_CODE  FROM GL_SEG_COA S where rootleaf='L' and lvl_code='" + lvlcode[x] + "' CONNECT BY PRIOR SEG_COA_CODE=PARENT_CODE START WITH seg_coa_code = " +
                               " '" + gvr.Cells[1].Text.ToString().Trim() + "')  ";
            }
            else if (x > 0 & gvr.Cells[1].Text.ToString().Replace("&nbsp;", "").ToString().Trim() != String.Empty)
            {
                criteria += "and substr(gl_coa_code,instr(gl_coa_code,'" + sepTyp + "'," + y + ")+1," + lvlSize[x].ToString() + ") in (SELECT SEG_COA_CODE  FROM GL_SEG_COA S where rootleaf='L' and lvl_code='" + lvlcode[x] + "' CONNECT BY PRIOR SEG_COA_CODE=PARENT_CODE START WITH seg_coa_code = " +
                               " '" + gvr.Cells[1].Text.ToString().Trim() + "' )  ";
            }
            y = y + lvlSize[x];
            x = x + 1;
        }
        DataTable dt = GlCoaManager.GetGlCoaCode(criteria);
        dgGlCoa.DataSource = dt;
        Session.Remove("coa");
        Session["coa"] = dt;
        foreach (GridViewRow gvr in dgGlCoa.Rows)
        {
            ((LinkButton)gvr.FindControl("lbEdit")).Visible = true;
            ((LinkButton)gvr.FindControl("lbDelete")).Visible = true;
            ((CheckBox)gvr.FindControl("chkInc")).Visible = false;
        }
        if (dt.Rows.Count > 0)
        {
            btnSaveCoa.Visible = false;
            dgGlCoa.DataBind();
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock( this, this.GetType(), "ale", "alert('No Chart-of-Account Code was previously created!!');", true);
        }
        lblTransStatus.Text = "";
    }

    protected void dgGlCoa_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        if (Session["coa"] != null)
        {
            DataTable dt = (DataTable)Session["coa"];
            dgGlCoa.DataSource = dt;
            dgGlCoa.PageIndex = e.NewPageIndex;
            dgGlCoa.DataBind();
            if (btnSaveCoa.Visible == false)
            {               
                foreach (GridViewRow gvr in dgGlCoa.Rows)
                {
                    ((LinkButton)gvr.FindControl("lbEdit")).Visible = true;
                    ((LinkButton)gvr.FindControl("lbDelete")).Visible = true;
                    ((CheckBox)gvr.FindControl("chkInc")).Visible = false;
                }
            }
            else
            {
                foreach (GridViewRow gvr in dgGlCoa.Rows)
                {
                    ((LinkButton)gvr.FindControl("lbEdit")).Visible = false;
                    ((LinkButton)gvr.FindControl("lbDelete")).Visible = false;
                    ((CheckBox)gvr.FindControl("chkInc")).Visible = true;
                    if (((DataRow)dt.Rows[gvr.DataItemIndex])["inc"].ToString() == "N")
                    {
                        ((CheckBox)gvr.FindControl("chkInc")).Checked = false;
                    }
                }                
            }  
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock( this, this.GetType(), "ale", "alert('Your session is over. Please try these again!!');", true);
        }
    }
    protected void chkIncCheck_Changed(object sender, EventArgs e)
    {
        if (Session["coa"] != null)
        {
            DataTable dt = (DataTable)Session["coa"];
            CheckBox chk = (CheckBox)sender;
            GridViewRow gvr = (GridViewRow)chk.NamingContainer;
            DataRow dr = dt.Rows[gvr.DataItemIndex];
            if (chk.Checked == true)
            {
                dr["inc"] = "Y";
            }
            else
            {
                dr["inc"] = "N";
            }
        }
    }
    
    protected void btnSaveCoa_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(3000);
        if (Session["coa"] != null)
        {
            DataTable dt = (DataTable)Session["coa"];
            GlCoa gcoa;
            foreach (DataRow dr in dt.Rows)
            {
                if (dr["inc"].ToString() != "N")
                {
                    gcoa = new GlCoa();
                    gcoa.GlCoaCode = dr["gl_coa_code"].ToString();
                    gcoa.CoaDesc = dr["coa_desc"].ToString();
                    gcoa.CoaEnabled = "Y";
                    gcoa.CoaNaturalCode = dr["coa_natural_code"].ToString();
                    gcoa.PostAllowed = "Y";
                    gcoa.BudAllowed = "N";
                    gcoa.CoaCurrBal = "0";
                    gcoa.EffectiveFrom = null;
                    gcoa.EffectiveTo = null;
                    gcoa.Status = "A";
                    gcoa.Taxable = "N";
                    gcoa.AccType = dr["acc_type"].ToString();
                    gcoa.BookName = Session["book"].ToString();
                    GlCoaManager.CreateGlCoa(gcoa);
                }
            }
            btnSaveCoa.Visible = false;
            for (int i = 0; i < dt.Rows.Count; i++ )
            {
                if (((DataRow)dt.Rows[i])["inc"].ToString() == "N")
                {
                    dt.Rows.RemoveAt(i);
                }
            }
            dgGlCoa.DataSource = dt;
            dgGlCoa.DataBind();
            foreach (GridViewRow gvr in dgGlCoa.Rows)
            {
                ((LinkButton)gvr.FindControl("lbEdit")).Visible = true;
                ((LinkButton)gvr.FindControl("lbDelete")).Visible = true;
                ((CheckBox)gvr.FindControl("chkInc")).Visible = false;
            }
            ScriptManager.RegisterClientScriptBlock( this, this.GetType(), "ale", "alert('Gl COA codes are saved successfully!!');", true);            
        }
    }
    protected void dgGlCoa_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        if (Session["coa"] != null)
        {
            DataTable dt = (DataTable)Session["coa"];
            dgGlCoa.DataSource = dt;
            btnSaveCoa.Visible = false;
            dgGlCoa.EditIndex = -1;
            dgGlCoa.DataBind();
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock( this, this.GetType(), "ale", "alert('Your session is over. Please try these again!!');", true);
        }
    }
    protected void dgGlCoa_RowEditing(object sender, GridViewEditEventArgs e)
    {
        if (Session["coa"] != null)
        {
            DataTable dt = (DataTable)Session["coa"];
            string accType = ((Label)dgGlCoa.Rows[e.NewEditIndex].FindControl("lblGlAccType")).Text.ToString();
            string status = ((Label)dgGlCoa.Rows[e.NewEditIndex].FindControl("lblGlStatus")).Text.ToString();

            dgGlCoa.DataSource = dt;
            btnSaveCoa.Visible = false;
            dgGlCoa.EditIndex = e.NewEditIndex;
            dgGlCoa.DataBind();
            ((DropDownList)dgGlCoa.Rows[e.NewEditIndex].FindControl("ddlGlAccType")).SelectedIndex = ((DropDownList)dgGlCoa.Rows[e.NewEditIndex].FindControl("ddlGlAccType")).Items.IndexOf(((DropDownList)dgGlCoa.Rows[e.NewEditIndex].FindControl("ddlGlAccType")).Items.FindByValue(accType));
            ((DropDownList)dgGlCoa.Rows[e.NewEditIndex].FindControl("ddlGlStatus")).SelectedIndex = ((DropDownList)dgGlCoa.Rows[e.NewEditIndex].FindControl("ddlGlStatus")).Items.IndexOf(((DropDownList)dgGlCoa.Rows[e.NewEditIndex].FindControl("ddlGlStatus")).Items.FindByValue(status));
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock( this, this.GetType(), "ale", "alert('Your session is over. Please try these again!!');", true);
        }

    }
    protected void dgGlCoa_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        if (Session["coa"] != null)
        {
            DataTable dt = (DataTable)Session["coa"];
            DataRow dr = dt.Rows[dgGlCoa.Rows[e.RowIndex].DataItemIndex];
            dt.Rows.Remove(dr);
            dgGlCoa.DataSource = dt;
            btnSaveCoa.Visible = false;
            dgGlCoa.EditIndex = -1;
            dgGlCoa.DataBind();
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock( this, this.GetType(), "ale", "alert('Your session is over. Please try these again!!');", true);
        }
    }
    protected void dgGlCoa_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {  
        if (Session["coa"] != null)
        {
            DataTable dt = (DataTable)Session["coa"];
            DataRow dr = dt.Rows[dgGlCoa.Rows[e.RowIndex].DataItemIndex];
            dr["coa_desc"] = ((TextBox)dgGlCoa.Rows[e.RowIndex].FindControl("txtGlCoaDesc")).Text;
            dr["acc_type"] = ((DropDownList)dgGlCoa.Rows[e.RowIndex].FindControl("ddlGlAccType")).SelectedValue;
            dr["status"] = ((DropDownList)dgGlCoa.Rows[e.RowIndex].FindControl("ddlGlStatus")).SelectedValue;
            dgGlCoa.DataSource = dt;
            btnSaveCoa.Visible = false;
            dgGlCoa.EditIndex = -1;
            dgGlCoa.DataBind();
        }
        else
        {
            
            ScriptManager.RegisterClientScriptBlock( this, this.GetType(), "ale", "alert('Your session is over. Please try these again!!');", true);
        }
    }
    protected void dgLevel_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "AddNew")
        {
            dgLevel.FooterRow.Visible = true;
        }
        else if (e.CommandName == "Insert")
        {
            GlLevel glevel = new GlLevel();
            glevel.LvlCode=((TextBox)dgLevel.FooterRow.FindControl("txtLevelCode")).Text;
            glevel.LvlDesc = ((TextBox)dgLevel.FooterRow.FindControl("txtLevelDesc")).Text;
            glevel.LvlMaxSize = ((TextBox)dgLevel.FooterRow.FindControl("txtLevelMaxSize")).Text;
            glevel.LvlEnabled = ((DropDownList)dgLevel.FooterRow.FindControl("ddlLevelEnabled")).SelectedValue;
            glevel.LvlSegType = ((DropDownList)dgLevel.FooterRow.FindControl("ddlLevelSegType")).SelectedValue;
            glevel.LvlOrder = ((TextBox)dgLevel.FooterRow.FindControl("txtLevelOrder")).Text;
            LvlManager.InsertLevel(glevel);
            dgLevel.FooterRow.Visible = false;
            dgLevel.DataSource = LvlManager.GetLevels();
            dgLevel.DataBind();
            dgGlCoaGen.DataSource = LvlManager.GetLevelsGrid();
            dgGlCoaGen.DataBind();
            //ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script language=JavaScript>alert('Created Successfully! ');</script>");
        }        
        else
        {
            dgLevel.FooterRow.Visible = false;
        }
    }
    protected void dgLevel_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        LvlManager.DeleteLevel(((Label)dgLevel.Rows[e.RowIndex].FindControl("lblLevelCode")).Text);
        dgLevel.DataSource = LvlManager.GetLevels();
        dgLevel.DataBind();
        dgGlCoaGen.DataSource = LvlManager.GetLevelsGrid();
        dgGlCoaGen.DataBind();
    }


    protected void dgGlCoa_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[5].Attributes.Add("style", "display:none");
        }
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[5].Attributes.Add("style", "display:none");
        }
    }
    protected void dgLevel_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[1].Attributes.Add("style", "display:none");
        }
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[1].Attributes.Add("style", "display:none");
        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[1].Attributes.Add("style", "display:none");
        }
    }
}
