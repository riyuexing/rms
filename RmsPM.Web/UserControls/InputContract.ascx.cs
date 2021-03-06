namespace RmsPM.Web.UserControls
{
    using System;
    using System.Data;
    using System.Drawing;
    using System.Web;
    using System.Web.UI.WebControls;
    using System.Web.UI.HtmlControls;
    using System.Web.SessionState;

    /// <summary>
    /// InputContract 的摘要说明。
    /// </summary>
    public partial class InputContract : System.Web.UI.UserControl
    {

        protected void Page_Load(object sender, System.EventArgs e)
        {
            if (this.Visible)
            {
                this.txtCode.Attributes["ClientID"] = this.ClientID;
                    this.divHint.InnerText = this.txtName.Value;
                if (!Page.IsPostBack)
                {
                    IniPage();
                }
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
        ///		设计器支持所需的方法 - 不要使用代码编辑器
        ///		修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {

        }
        #endregion

        private void IniPage()
        {
            try
            {
                this.txtCode.Attributes.Add("ImagePath", this.ImagePath);
            }
            catch (Exception ex)
            {
                ApplicationLog.WriteLog(this.ToString(), ex, "初始化页面失败");
                Response.Write(Rms.Web.JavaScript.Alert(true, "初始化页面失败：" + ex.Message));
            }
        }

        /// <summary>
        /// 设置项目代码，初始化
        /// </summary>
        /// <param name="ProjectCode"></param>
        private void SetProject(string ProjectCode)
        {
            try
            {
                this.txtProjectCode.Value = ProjectCode;
            }
            catch (Exception ex)
            {
                ApplicationLog.WriteLog(this.ToString(), ex, "初始化页面失败");
                Response.Write(Rms.Web.JavaScript.Alert(true, "初始化页面失败：" + ex.Message));
            }
        }

        public string ProjectCode
        {
            get { return this.txtProjectCode.Value; }
            set { SetProject(value); }
        }

        /// <summary>
        /// 设置代码
        /// </summary>
        /// <param name="code"></param>
        private void SetCode(string code)
        {
            try
            {
                this.txtCode.Value = code;
                string name = BLL.ContractRule.GetContractName(code);
                this.txtName.Value = name;
                this.divHint.InnerHtml = name;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string Value
        {
            get { return this.txtCode.Value; }
            set { SetCode(value); }
        }

        public string Text
        {
            get { return this.txtName.Value; }
        }

        protected string imagePath = "../images/";

        public string ImagePath
        {
            get { return this.imagePath; }
            set { this.imagePath = value; }
        }
    }
}
