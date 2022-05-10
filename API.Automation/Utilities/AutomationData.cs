using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace API.Automation.Utilities
{
    public class AutomationData
    {
        DataTable _queryResults;
        string _connectionValue = "";

        public AutomationData()
        {
            _queryResults = new DataTable();

        }

        public DataTable selectDataFromTable(string tableName)
        {

            SqlConnection con = new SqlConnection(_connectionValue);

            SqlCommand sqlComm = new SqlCommand();
            sqlComm.Connection = con;


            sqlComm.CommandTimeout = 100;
            sqlComm.CommandText = "select top 1 * from" + tableName + "order by 1 desc";
            SqlDataAdapter da = new SqlDataAdapter(sqlComm);
            DataTable dt = new DataTable();
            try
            {
                da.Fill(dt);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
            }
            return dt;

        }

        public bool UpdateDataTable(string tableName, string updatedValue, string condition)
        {

            SqlConnection con = new SqlConnection(_connectionValue);

            SqlCommand sqlComm = new SqlCommand();
            sqlComm.Connection = con;
            int iResult = 0;

            sqlComm.CommandTimeout = 100;
            sqlComm.CommandText = "update " + tableName + " set " + updatedValue + " where " + condition;
            SqlDataAdapter da = new SqlDataAdapter(sqlComm);

            try
            {
                con.Open();
                iResult = sqlComm.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                con.Close();
            }

            if (iResult != 0)
            {
                return true;
            }
            else return false;

        }

        
        public bool InsertDataTable(string tableName, string insertedValue)
        {
            SqlConnection con = new SqlConnection(_connectionValue);

            SqlCommand sqlComm = new SqlCommand();
            sqlComm.Connection = con;
            int iResult = 0;

            sqlComm.CommandTimeout = 100;
            sqlComm.CommandText = "insert into " + tableName + " values " + insertedValue;
            SqlDataAdapter da = new SqlDataAdapter(sqlComm);

            try
            {
                con.Open();
                iResult = sqlComm.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                con.Close();
            }

            if (iResult != 0)
            {
                return true;
            }
            else return false;

        }

        public bool DeleteDataTable(string tableName, string conditionValue)
        {
            SqlConnection con = new SqlConnection(_connectionValue);

            SqlCommand sqlComm = new SqlCommand();
            sqlComm.Connection = con;
            int iResult = 0;

            sqlComm.CommandTimeout = 100;
            if (conditionValue.Length > 0)
            {
                sqlComm.CommandText = "delete from " + tableName + " where " + conditionValue;
                SqlDataAdapter da = new SqlDataAdapter(sqlComm);
            }
            else
            {
                sqlComm.CommandText = "delete from " + tableName;
                SqlDataAdapter da = new SqlDataAdapter(sqlComm);
            }

            try
            {
                con.Open();
                iResult = sqlComm.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                con.Close();
            }

            if (iResult != 0)
            {
                return true;
            }
            else return false;

        }
    }
}
