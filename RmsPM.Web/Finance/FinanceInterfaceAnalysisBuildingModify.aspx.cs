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
using Rms.ORMap;
using Rms.Web;
using RmsPM.DAL.EntityDAO;
using RmsPM.BLL;

namespace RmsPM.Web.Finance
{
	/// <summary>
	/// FinanceInterfaceAnalysisBuildingModify ��ժҪ˵����
	/// </summary>
	public partial class FinanceInterfaceAnalysisBuildingModify : PageBase
	{

		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (!IsPostBack)
			{
				IniPage();
				LoadData();
			}
		}

		private void IniPage() 
		{
			try 
			{
				this.txtBuildingCode.Value = Request.QueryString["BuildingCode"];
				this.txtSubjectSetCode.Value = Request["SubjectSetCode"];
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "��ʼ��ҳ�������" + ex.Message));
			}
		}

		private void LoadData()
		{
			try
			{
				if ((this.txtBuildingCode.Value == "") || (this.txtSubjectSetCode.Value == ""))
				{
					Response.Write(Rms.Web.JavaScript.Alert(true, "δ����¥����Ż����ױ��"));
					Response.Write(Rms.Web.JavaScript.WinClose(true));
					return;
				}

				this.lblBuildingName.Text = BLL.ProductRule.GetBuildingName(this.txtBuildingCode.Value);
				this.lblSubjectSetName.Text = BLL.SubjectRule.GetSubjectSetName(this.txtSubjectSetCode.Value);

				EntityData entity = DAL.EntityDAO.ProductDAO.GetBuildingSubjectSetByBuilding(this.txtBuildingCode.Value, this.txtSubjectSetCode.Value);
				if (entity.HasRecord()) 
				{
					this.txtU8Code.Value = entity.GetString("U8Code");
				}
				entity.Dispose();
			}
			catch(Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "��ʼ��ҳ�������" + ex.Message));
			}
		}

		#region Web ������������ɵĴ���
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: �õ����� ASP.NET Web ���������������ġ�
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		/// �����֧������ķ��� - ��Ҫʹ�ô���༭���޸�
		/// �˷��������ݡ�
		/// </summary>
		private void InitializeComponent()
		{    

		}
		#endregion

		/// <summary>
		/// ����
		/// </summary>
		private void SavaData()
		{
			try
			{
				EntityData entity = DAL.EntityDAO.ProductDAO.GetBuildingSubjectSetByBuilding(this.txtBuildingCode.Value, this.txtSubjectSetCode.Value);
				DataRow dr = null;
				if (!entity.HasRecord())
				{
					dr = entity.CurrentTable.NewRow();
					dr["BuildingSubjectSetCode"] = DAL.EntityDAO.SystemManageDAO.GetNewSysCode("BuildingSubjectSetCode");
					dr["BuildingCode"] = this.txtBuildingCode.Value;
					dr["SubjectSetCode"] = this.txtSubjectSetCode.Value;
					entity.CurrentTable.Rows.Add(dr);
				}
				else
				{
					dr = entity.CurrentRow;
				}

				dr["U8Code"] = this.txtU8Code.Value;

				DAL.EntityDAO.ProductDAO.SubmitAllBuildingSubjectSet(entity);

				entity.Dispose();

			}
			catch(Exception ex)
			{
				throw ex;
			}
		}

		/// <summary>
		/// ��Ч�Լ��
		/// </summary>
		/// <param name="Hint"></param>
		/// <returns></returns>
		private bool CheckValid(ref string Hint) 
		{
			Hint = "";

			return true;
		}

		/// <summary>
		/// ����
		/// </summary>
		private void GoBack() 
		{
			Response.Write(Rms.Web.JavaScript.ScriptStart);
			Response.Write("window.opener.location = window.opener.location;");
			Response.Write(Rms.Web.JavaScript.WinClose(false));
			Response.Write(Rms.Web.JavaScript.ScriptEnd);
		}

		/// <summary>
		/// ����
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void btnSave_ServerClick(object sender, System.EventArgs e)
		{
			try
			{
				string Hint = "";
				if (!CheckValid(ref Hint)) 
				{
					Response.Write(Rms.Web.JavaScript.Alert(true, Hint));
					return;
				}

				SavaData();

				GoBack();
			}
			catch(Exception ex) 
			{
				Response.Write(JavaScript.Alert(true, "����ʧ�ܣ�" + ex.Message));
				ApplicationLog.WriteLog(this.ToString(),ex,"");
			}
		}

	}
}