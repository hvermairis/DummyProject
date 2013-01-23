using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Cryptography;
using System.IO;
using System.Text;

/// <summary>
/// Summary description for clsCryptography
/// </summary>
public class clsCryptography
{

    private static Byte[] key = { };
    private static Byte[] IV = { 18, 52, 86, 120, 144, 171, 205, 239 };

	public clsCryptography()
	{
		//
		// TODO: Add constructor logic here
		//
	}

   
    public static string SEncryptionKey = "!@#$%&)(";

    /// <summary>
    /// To Encrypt String
    /// </summary>
    /// <param name="stringToEncrypt"></param>
    /// <param name="SEncryptionKey"></param>
    /// <returns>string</returns>
    /// <remarks></remarks>
    public static string Encrypt(string stringToEncrypt)
    {
        try
        {
            key = System.Text.Encoding.UTF8.GetBytes(SEncryptionKey.Substring(0, 8));
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            Byte[] inputByteArray = Encoding.UTF8.GetBytes(stringToEncrypt);

            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(key, IV), CryptoStreamMode.Write);

            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            return Convert.ToBase64String(ms.ToArray());
        }
        catch (Exception e)
        {
            return e.Message;
        }
    }

    /// <summary>
    /// To Decript string
    /// </summary>
    /// <param name="stringToDecrypt"></param>
    /// <param name="sEncryptionKey"></param>
    /// <returns>string</returns>
    /// <remarks></remarks>
    public static string Decrypt(string stringToDecrypt)
    {
        stringToDecrypt = stringToDecrypt.Replace(" ","+");
        byte[] inputByteArray = new byte[stringToDecrypt.Length];
        try
        {
            key = System.Text.Encoding.UTF8.GetBytes((SEncryptionKey.Substring(0, 8)));
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            inputByteArray = Convert.FromBase64String(stringToDecrypt);
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(key, IV), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            System.Text.Encoding encoding = System.Text.Encoding.UTF8;
            return encoding.GetString(ms.ToArray());
        }
        catch (Exception e)
        {
            return e.Message;
        }

    }


}