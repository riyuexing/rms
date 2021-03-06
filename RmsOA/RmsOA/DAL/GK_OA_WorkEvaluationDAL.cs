﻿namespace RmsOA.DAL
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;
    using RmsOA.MODEL;

    public class GK_OA_WorkEvaluationDAL
    {
        private SqlDataProcess _DataProcess;

        public GK_OA_WorkEvaluationDAL(SqlConnection Connection)
        {
            this._DataProcess = new SqlDataProcess(Connection);
        }

        public GK_OA_WorkEvaluationDAL(SqlTransaction Transaction)
        {
            this._DataProcess = new SqlDataProcess(Transaction);
        }

        private GK_OA_WorkEvaluationModel _DataBind(int Code)
        {
            GK_OA_WorkEvaluationModel model = new GK_OA_WorkEvaluationModel();
            StringBuilder builder = new StringBuilder();
            builder.Append("select * from GK_OA_WorkEvaluation ");
            builder.Append(" where Code=@Code");
            this._DataProcess.CommandText = builder.ToString();
            this._DataProcess.ProcessParametersAdd("@Code", SqlDbType.Int, 4, Code);
            SqlDataReader sqlDataReader = null;
            try
            {
                try
                {
                    sqlDataReader = this._DataProcess.GetSqlDataReader();
                    while (sqlDataReader.Read())
                    {
                        this.Initialize(sqlDataReader, model);
                    }
                }
                catch (SqlException exception)
                {
                    throw exception;
                }
            }
            finally
            {
                if (sqlDataReader != null)
                {
                    sqlDataReader.Close();
                }
            }
            return model;
        }

        private int _Delete(int Code)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from GK_OA_WorkEvaluation ");
            builder.Append(" where Code=@Code");
            this._DataProcess.CommandText = builder.ToString();
            this._DataProcess.ProcessParametersAdd("@Code", SqlDbType.Int, 4, Code);
            return this._DataProcess.RunSql();
        }

        private int _Insert(GK_OA_WorkEvaluationModel mObj)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into GK_OA_WorkEvaluation(");
            builder.Append("SystemCode,FileCode,PersonName,InComDate,UnitCode,StationCode,WorkPperation,MeritsAndDemerits,Decide,Status,Field1)");
            builder.Append(" values (");
            builder.Append("@SystemCode,@FileCode,@PersonName,@InComDate,@UnitCode,@StationCode,@WorkPperation,@MeritsAndDemerits,@Decide,@Status,@Field1) ");
            builder.Append("SELECT @Code = SCOPE_IDENTITY()");
            this._DataProcess.CommandText = builder.ToString();
            this._DataProcess.ProcessParametersAdd("@SystemCode", SqlDbType.VarChar, 50, mObj.SystemCode);
            this._DataProcess.ProcessParametersAdd("@FileCode", SqlDbType.VarChar, 50, mObj.FileCode);
            this._DataProcess.ProcessParametersAdd("@PersonName", SqlDbType.VarChar, 50, mObj.PersonName);
            this._DataProcess.ProcessParametersAdd("@InComDate", SqlDbType.DateTime, 8, mObj.InComDate);
            this._DataProcess.ProcessParametersAdd("@UnitCode", SqlDbType.VarChar, 50, mObj.UnitCode);
            this._DataProcess.ProcessParametersAdd("@StationCode", SqlDbType.VarChar, 50, mObj.StationCode);
            this._DataProcess.ProcessParametersAdd("@WorkPperation", SqlDbType.VarChar, 500, mObj.WorkPperation);
            this._DataProcess.ProcessParametersAdd("@MeritsAndDemerits", SqlDbType.VarChar, 500, mObj.MeritsAndDemerits);
            this._DataProcess.ProcessParametersAdd("@Decide", SqlDbType.VarChar, 50, mObj.Decide);
            this._DataProcess.ProcessParametersAdd("@Status", SqlDbType.VarChar, 50, mObj.Status);
            this._DataProcess.ProcessParametersAdd("@Field1", SqlDbType.VarChar, 50, mObj.Field1);
            SqlParameter parameter = this._DataProcess.ProcessParametersAdd("@Code", SqlDbType.Int, 4, null);
            parameter.Direction = ParameterDirection.Output;
            this._DataProcess.RunSql();
            mObj.Code = (int) parameter.Value;
            return mObj.Code;
        }

        private List<GK_OA_WorkEvaluationModel> _Select(GK_OA_WorkEvaluationQueryModel qmObj)
        {
            List<GK_OA_WorkEvaluationModel> list = new List<GK_OA_WorkEvaluationModel>();
            StringBuilder builder = new StringBuilder();
            builder.Append("select * from GK_OA_WorkEvaluation ");
            builder.Append(qmObj.QueryConditionStr);
            if (qmObj.SortColumns.Length == 0)
            {
                builder.Append(" ORDER BY Code desc");
            }
            else
            {
                builder.Append(" ORDER BY " + qmObj.SortColumns);
            }
            this._DataProcess.CommandText = builder.ToString();
            this._DataProcess.SqlParameters = qmObj.Parameters;
            SqlDataReader sqlDataReader = null;
            int num = 0;
            try
            {
                try
                {
                    sqlDataReader = this._DataProcess.GetSqlDataReader();
                    while (sqlDataReader.Read())
                    {
                        if ((num >= qmObj.StartRecord) && ((list.Count < qmObj.MaxRecords) || (qmObj.MaxRecords == -1)))
                        {
                            GK_OA_WorkEvaluationModel model = new GK_OA_WorkEvaluationModel();
                            this.Initialize(sqlDataReader, model);
                            list.Add(model);
                        }
                        num++;
                    }
                }
                catch (SqlException exception)
                {
                    throw exception;
                }
            }
            finally
            {
                if (sqlDataReader != null)
                {
                    sqlDataReader.Close();
                }
            }
            return list;
        }

        private int _Update(GK_OA_WorkEvaluationModel mObj)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update GK_OA_WorkEvaluation set ");
            if (mObj.SystemCode != null)
            {
                builder.Append("SystemCode=@SystemCode,");
                this._DataProcess.ProcessParametersAdd("@SystemCode", SqlDbType.VarChar, 50, mObj.SystemCode);
            }
            if (mObj.FileCode != null)
            {
                builder.Append("FileCode=@FileCode,");
                this._DataProcess.ProcessParametersAdd("@FileCode", SqlDbType.VarChar, 50, mObj.FileCode);
            }
            if (mObj.PersonName != null)
            {
                builder.Append("PersonName=@PersonName,");
                this._DataProcess.ProcessParametersAdd("@PersonName", SqlDbType.VarChar, 50, mObj.PersonName);
            }
            if (mObj.InComDate != DateTime.MinValue)
            {
                builder.Append("InComDate=@InComDate,");
                this._DataProcess.ProcessParametersAdd("@InComDate", SqlDbType.DateTime, 8, mObj.InComDate);
            }
            if (mObj.UnitCode != null)
            {
                builder.Append("UnitCode=@UnitCode,");
                this._DataProcess.ProcessParametersAdd("@UnitCode", SqlDbType.VarChar, 50, mObj.UnitCode);
            }
            if (mObj.StationCode != null)
            {
                builder.Append("StationCode=@StationCode,");
                this._DataProcess.ProcessParametersAdd("@StationCode", SqlDbType.VarChar, 50, mObj.StationCode);
            }
            if (mObj.WorkPperation != null)
            {
                builder.Append("WorkPperation=@WorkPperation,");
                this._DataProcess.ProcessParametersAdd("@WorkPperation", SqlDbType.VarChar, 500, mObj.WorkPperation);
            }
            if (mObj.MeritsAndDemerits != null)
            {
                builder.Append("MeritsAndDemerits=@MeritsAndDemerits,");
                this._DataProcess.ProcessParametersAdd("@MeritsAndDemerits", SqlDbType.VarChar, 500, mObj.MeritsAndDemerits);
            }
            if (mObj.Decide != null)
            {
                builder.Append("Decide=@Decide,");
                this._DataProcess.ProcessParametersAdd("@Decide", SqlDbType.VarChar, 50, mObj.Decide);
            }
            if (mObj.Status != null)
            {
                builder.Append("Status=@Status,");
                this._DataProcess.ProcessParametersAdd("@Status", SqlDbType.VarChar, 50, mObj.Status);
            }
            if (mObj.Field1 != null)
            {
                builder.Append("Field1=@Field1,");
                this._DataProcess.ProcessParametersAdd("@Field1", SqlDbType.VarChar, 50, mObj.Field1);
            }
            builder.Remove(builder.Length - 1, 1);
            builder.Append(" where Code=@Code");
            this._DataProcess.ProcessParametersAdd("@Code", SqlDbType.Int, 4, mObj.Code);
            this._DataProcess.CommandText = builder.ToString();
            return this._DataProcess.RunSql();
        }

        public int Delete(int Code)
        {
            return this._Delete(Code);
        }

        public int Delete(GK_OA_WorkEvaluationModel mObj)
        {
            return this._Delete(mObj.Code);
        }

        public GK_OA_WorkEvaluationModel GetModel(int Code)
        {
            return this._DataBind(Code);
        }

        private void Initialize(SqlDataReader reader, GK_OA_WorkEvaluationModel obj)
        {
            if (reader.GetValue(0) != DBNull.Value)
            {
                obj.Code = reader.GetInt32(0);
            }
            if (reader.GetValue(1) != DBNull.Value)
            {
                obj.SystemCode = reader.GetString(1);
            }
            if (reader.GetValue(2) != DBNull.Value)
            {
                obj.FileCode = reader.GetString(2);
            }
            if (reader.GetValue(3) != DBNull.Value)
            {
                obj.PersonName = reader.GetString(3);
            }
            if (reader.GetValue(4) != DBNull.Value)
            {
                obj.InComDate = reader.GetDateTime(4);
            }
            if (reader.GetValue(5) != DBNull.Value)
            {
                obj.UnitCode = reader.GetString(5);
            }
            if (reader.GetValue(6) != DBNull.Value)
            {
                obj.StationCode = reader.GetString(6);
            }
            if (reader.GetValue(7) != DBNull.Value)
            {
                obj.WorkPperation = reader.GetString(7);
            }
            if (reader.GetValue(8) != DBNull.Value)
            {
                obj.MeritsAndDemerits = reader.GetString(8);
            }
            if (reader.GetValue(9) != DBNull.Value)
            {
                obj.Decide = reader.GetString(9);
            }
            if (reader.GetValue(10) != DBNull.Value)
            {
                obj.Status = reader.GetString(10);
            }
            if (reader.GetValue(11) != DBNull.Value)
            {
                obj.Field1 = reader.GetString(11);
            }
        }

        public int Insert(GK_OA_WorkEvaluationModel mObj)
        {
            return this._Insert(mObj);
        }

        public List<GK_OA_WorkEvaluationModel> Select()
        {
            GK_OA_WorkEvaluationQueryModel qmObj = new GK_OA_WorkEvaluationQueryModel();
            return this._Select(qmObj);
        }

        public List<GK_OA_WorkEvaluationModel> Select(GK_OA_WorkEvaluationQueryModel qmObj)
        {
            return this._Select(qmObj);
        }

        public int Update(GK_OA_WorkEvaluationModel mObj)
        {
            return this._Update(mObj);
        }
    }
}

