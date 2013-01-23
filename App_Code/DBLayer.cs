using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

/// <summary>
/// Summary description for DBLayer
/// </summary>
public class DBLayer
{
	public DBLayer()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public static DataSet getDataSetFromFinalDB(string sql, SqlParameter[] arrayparam)
    {

        DataSet DataSetObject = new DataSet();
        SqlDataAdapter da = new SqlDataAdapter(sql, dbConnection.strNonQCServerConString);

        foreach (SqlParameter param in arrayparam)
        {
            da.SelectCommand.Parameters.Add(param);
        }

        da.Fill(DataSetObject);

        return DataSetObject;
    }

    public static DataSet getDataSetFromFinalDB(string query)
    {
        DataSet dataSet = new DataSet();
        SqlConnection conn = new SqlConnection(dbConnection.strNonQCServerConString);

        try
        {
            SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
            adapter.Fill(dataSet);

        }
        catch (Exception ex)
        {

        }
        finally
        {
            conn.Close();
        }

        return dataSet;
    }

    public static int ExcuteNonQueryInFinalDB(string sql, SqlParameter[] arrayparam)
    {
        int result = 0;
        SqlConnection con = new SqlConnection(dbConnection.strNonQCServerConString);
        try
        {

            con.Open();
            SqlCommand cmd = new SqlCommand(sql, con);
           
            foreach (SqlParameter param in arrayparam)
            {
                cmd.Parameters.Add(param);

            }

            result = cmd.ExecuteNonQuery();
        }
        catch (Exception ex)
        {

        }

        con.Close();
        con.Dispose();

        return result;



    }

    public static int ExcuteNonQueryInFinalDB(string sql)
    {
        int result = 0;
        SqlConnection con = new SqlConnection(dbConnection.strNonQCServerConString);
        try
        {

            con.Open();
            SqlCommand cmd = new SqlCommand(sql, con);
           
            result = cmd.ExecuteNonQuery();
        }
        catch (Exception ex)
        {

        }

        con.Close();
        con.Dispose();

        return result;



    }



}