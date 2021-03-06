using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;

using RmsPM.BLL;
using RmsPM.DAL.EntityDAO;
using RmsPM.DAL.QueryStrategy;
using RmsPM.Web;
using Rms.ORMap;
using Rms.WorkFlow;

namespace RmsPM.Web.WorkFlowContral
{
	/// <summary>
	/// WorkFlowQuery 的摘要说明。
	/// </summary>
	public partial class WorkFlowQuery :PageBase
	{
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// 在此处放置用户代码以初始化页面
			if(!IsPostBack)
			{
				IniPage();

				BuildSqlString();
				LoadData();
			}
		}

		private void IniPage()
		{
			try
			{
				EntityData entity = DAL.EntityDAO.WorkFlowDAO.GetAllWorkFlowProcedure();
				int iCount = entity.CurrentTable.Rows.Count;
                string tempProcedureName = "";
                List<string> ProcedureNameList = new List<string>();
                for (int i = 0; i < iCount; i++)
                {
                    entity.SetCurrentRow(i);
                    string Activity = entity.GetInt("Activity").ToString();
                    if (Activity == "0")
                    {
                        continue;
                    }

                    string ProcedureName = entity.GetString("ProcedureName");
                    if (!ProcedureNameList.Contains(ProcedureName))
                    {
                        this.sltProcedure.Items.Add(new ListItem(entity.GetString("description"), ProcedureName));
                        ProcedureNameList.Add(ProcedureName);
                    }
                }
                this.DropDownProject.DataSource = new DataView(user.m_EntityDataAccessProject.CurrentTable, "", "ProjectName", DataViewRowState.CurrentRows);
                this.DropDownProject.DataTextField = "ProjectShortName";
                this.DropDownProject.DataValueField = "ProjectCode";
                this.DropDownProject.DataBind();
                ListItem li = new ListItem("--所有项目--", "");
                this.DropDownProject.Items.Add(li);
                this.DropDownProject.SelectedIndex = this.DropDownProject.Items.IndexOf(this.DropDownProject.Items.FindByValue(""));
				entity.Dispose();

                this.dgList.Columns[8].Visible = user.HasOperationRight("090102");
                this.dgList.Columns[9].Visible = this.dgList.Columns[8].Visible;

			}
			catch ( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
			}
		}

		private void LoadData()
		{
			try
			{
                //temp,天阳要求时间较紧 update by kenny 20070209
                string company = System.Configuration.ConfigurationManager.AppSettings["PMName"].ToLower();
                switch (company)
                {
                    case "tianyangoa":
                        this.dgList.Columns[7].Visible = true;
                        break;
                    default:
                        this.dgList.Columns[7].Visible = false;
                        break;
                }

				string sql = (string)this.ViewState["SqlString"];
				QueryAgent qa = new QueryAgent();
				DataSet ds = qa.ExecSqlForDataSet(sql);
				//EntityData entity = qa.FillEntityData( "V_WorkFlowHistory",sql );
				qa.Dispose();
				this.dgList.DataSource = ds;
				this.dgList.DataBind();
				this.GridPagination1.RowsCount = ds.Tables[0].Rows.Count.ToString();
			}
			catch( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
			}
		}


		private void BuildSqlString()
		{
			try
			{
				RmsPM.DAL.QueryStrategy.WorkFlowHistory sb = new RmsPM.DAL.QueryStrategy.WorkFlowHistory();

				sb.AddStrategy( new Strategy( RmsPM.DAL.QueryStrategy.WorkFlowHistoryStrategyName.Status,"End" ));
				
				if(!user.HasOperationRight("090102"))
				{
					sb.AddStrategy( new Strategy( WorkFlowHistoryStrategyName.ActUserCode,this.user.UserCode ));
				}


                if (this.sltProcedure.Value != "")
                    sb.AddStrategy(new Strategy(WorkFlowHistoryStrategyName.ProcedureCodeIn, BLL.WorkFlowRule.GetProcedureCodeListByName(this.sltProcedure.Value)));
                if (this.txtCaseCode.Value != "")
                    sb.AddStrategy(new Strategy(WorkFlowHistoryStrategyName.FlowNumber, this.txtCaseCode.Value));
                if (this.txtTitle.Value != "")
                    sb.AddStrategy(new Strategy(WorkFlowHistoryStrategyName.Title, this.txtTitle.Value));
                if (this.DropDownProject.SelectedValue != "")
                    sb.AddStrategy(new Strategy(WorkFlowHistoryStrategyName.ProjectCode, this.DropDownProject.SelectedValue));
                if (this.ucPerson.Value != "")
                    sb.AddStrategy(new Strategy(WorkFlowHistoryStrategyName.SourceUserCode, this.ucPerson.Value));
                if (this.DateStart.Value != "" || this.DateEnd.Value != "")
                {
                    string sDate = this.DateStart.Value;
                    string eDate = this.DateEnd.Value;
                    if (this.DateStart.Value == "")
                        sDate = DateTime.MinValue.ToShortDateString();
                    if (this.DateEnd.Value == "")
                        eDate = DateTime.MaxValue.ToShortDateString();
                    ArrayList arrlist = new ArrayList();
                    arrlist.Add(sDate);
                    arrlist.Add(eDate);
                    sb.AddStrategy(new Strategy(WorkFlowHistoryStrategyName.CreateDate, arrlist));
                }
                if (this.CalendarStart.Value != "" || this.CalendarEnd.Value != "")
                {
                    string sDate = this.CalendarStart.Value;
                    string eDate = this.CalendarEnd.Value;
                    if (sDate== "")
                        sDate = DateTime.MinValue.ToShortDateString();
                    if (eDate == "")
                        eDate = DateTime.MaxValue.ToShortDateString();
                    ArrayList arrlist = new ArrayList();
                    arrlist.Add(sDate);
                    arrlist.Add(eDate);
                    sb.AddStrategy(new Strategy(WorkFlowHistoryStrategyName.FinishDate, arrlist));
                }
				string sql = sb.BuildMainQueryString();
                //排序
                string sortsql = BLL.GridSort.GetSortSQL(ViewState, "CreateDate desc");
                if (sortsql != "")
                {
                    sql = sql + " order by " + sortsql;
                }
				this.ViewState.Add("SqlString",sql);
			}
			catch( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
			}
		}
		#region Web 窗体设计器生成的代码
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: 该调用是 ASP.NET Web 窗体设计器所必需的。
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		/// 设计器支持所需的方法 - 不要使用代码编辑器修改
		/// 此方法的内容。
		/// </summary>
		private void InitializeComponent()
		{    

		}
		#endregion

		protected void btnSearch_ServerClick(object sender, System.EventArgs e)
		{
			try
			{
				BuildSqlString();
                this.GridPagination1.CurrentPageIndex = 1 ;
				LoadData();
			}
			catch ( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
			}
		}

		protected void GridPagination1_PageIndexChange(object sender, System.EventArgs e)
		{
			//BuildSqlString();
			LoadData();
		}
        protected void dgList_SortCommand(object source, DataGridSortCommandEventArgs e)
        {
            this.GridPagination1.CurrentPageIndex = 1;
            BLL.GridSort.SortCommand((DataGrid)source, ViewState, source, e);
            BuildSqlString();
            LoadData();
        }
	}
}
