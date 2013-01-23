using System;
using System.Collections.Generic;
using System.Collections;
/// <summary>
/// Summary description for QueryString
/// </summary>
public class QueryString
{
    /// <summary>
    /// QueryString Container
    /// </summary>
    Hashtable param;
    public QueryString()
    {
        param = new Hashtable();
    }
    public void addQS(string Key, string Value)
    {
        param.Add(Key, Value);
    }
    public string getQS(string Key)
    {
        try
        {
            return param[Key.ToUpper()].ToString();

        }
        catch (Exception ex)
        {
            return null;
        }

    }
}
