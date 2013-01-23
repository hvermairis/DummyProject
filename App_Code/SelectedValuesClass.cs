using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace SonyIris
{
    /// <summary>
    /// Summary description for SelectedValuesClass
    /// </summary>
    public class SelectedValuesClass
    {
        public ReportTypeLevel ReportType=ReportTypeLevel.RegionLevel;

      
     
        public string Region = "";
        public string RegionName = "";
        public string State = "";
        public string StateName = "";
        public string City = "";
        public string RecceId = "";
        public string OutletTypeId = "0";
        public string OutletType = "";
        public string OutletId = "";
      

        public string CityName = "";
      
     
        public string _Year = "0";

        public string Year
        {
            get
            {
               
                return _Year;
            }
            set
            {
                _Year = value;
            }

        }
       
        

        public string FromDate = "";
        public string ToDate = "";

        public string FromWeek = "0";
        public string ToWeek = "0";

        public string SelectDateFormat = "1";

        public string FromMonth = "0";
        public string ToMonth= "0";

        public string RegionMngApproved = "0";
        public string StateMngApproved = "0";

        public string SelectRegionIndex = "";

     
        public ReportTypeLevel PageType = ReportTypeLevel.IndiaLevel;


        #region " QueryString Encryption "
        /// <summary>
        /// To Encrypt Query String
        /// </summary>
        /// <param name="querystring"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static string EncryptQueryString(string querystring)
        {
            return "Data=" + clsCryptography.Encrypt(querystring);
        }
        /// <summary>
        /// To Decrypt Query String
        /// </summary>
        /// <param name="querystring"></param>
        /// <returns></returns>
        public static QueryString DecryptQueryString(string querystring)
        {
            QueryString QString = new QueryString();
            string[] pair;
            string[] QStr = clsCryptography.Decrypt(querystring).Split('&');
            foreach (string qs in QStr)
            {
                pair = qs.Split('=');
                if (pair.Length == 2)
                {
                    QString.addQS(pair[0].ToUpper(), pair[1]);
                }
            }
            return QString;
        }
        /// <summary>
        /// To get value of query string
        /// </summary>
        /// <param name="Key"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static string QS(string Key)
        {
            string st = String.Empty;
            try
            {
                if (HttpContext.Current.Request.QueryString["Data"] != null)
                {
                    st = HttpContext.Current.Request.QueryString["Data"].Replace(" ", "+");
                    return DecryptQueryString(st).getQS(Key);

                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        #endregion


        public  void setValues()
        {
          

            if (QS("R") != null && QS("R") != "")
            {
                Region = Convert.ToString(QS("R"));
            }

            if (QS("S") != null && QS("S") != "")
            {
                State = Convert.ToString(QS("S"));
            }

            if (QS("C") != null && QS("C") != "")
            {
                City = Convert.ToString(QS("C"));
            }



            if (QS("RN") != null && QS("RN") != "")
            {
                RegionName = Convert.ToString(QS("RN"));
            }

            if (QS("SN") != null && QS("SN") != "")
            {
                StateName = Convert.ToString(QS("SN"));
            }

            if (QS("CN") != null && QS("CN") != "")
            {
                CityName = Convert.ToString(QS("CN"));
            }


            if (QS("RecceId") != null && QS("RecceId") != "")
            {
                RecceId = Convert.ToString(QS("RecceId"));
            }

            if (QS("OutletTypeId") != null && QS("OutletTypeId") != "")
            {
                OutletTypeId = Convert.ToString(QS("OutletTypeId"));
            }

            if (QS("OutletType") != null && QS("OutletType") != "")
            {
                OutletType = Convert.ToString(QS("OutletType"));
            }

                    

            if (QS("Year") != null && QS("Year") != "")
            {
                Year = Convert.ToString(QS("Year"));
            }


        
            if (QS("FrD") != null && QS("FrD") != "")
            {
                FromDate = Convert.ToString(QS("FrD"));
            }

            if (QS("ToD") != null && QS("ToD") != "")
            {
                ToDate = Convert.ToString(QS("ToD"));
            }

            if (QS("FrW") != null && QS("FrW") != "")
            {
                FromWeek = Convert.ToString(QS("FrW"));
            }

            if (QS("ToW") != null && QS("ToW") != "")
            {
                ToWeek = Convert.ToString(QS("ToW"));
            }

            if (QS("RMA") != null && QS("RMA") != "")
            {
                RegionMngApproved = Convert.ToString(QS("RMA"));
            }

            if (QS("SMA") != null && QS("SMA") != "")
            {
                StateMngApproved = Convert.ToString(QS("SMA"));
            }

            //

            if (QS("SDFor") != null && QS("SDFor") != "")
            {
                SelectDateFormat = Convert.ToString(QS("SDFor"));
            }



            if (QS("FrMon") != null && QS("FrMon") != "")
            {
                FromMonth = Convert.ToString(QS("FrMon"));
            }
            if (QS("ToMon") != null && QS("ToMon") != "")
            {
                ToMonth = Convert.ToString(QS("ToMon"));
            }



            //if (HttpContext.Current.Request.QueryString["SRIndex"] != null)
            //{
            //    SelectRegionIndex = general.getDecryptString(HttpContext.Current.Request.QueryString["SRIndex"].ToString());
            //}

          

          

            
            if (QS("PageType") != null && QS("PageType") != "")
            {
                if (QS("PageType") == ReportTypeLevel.IndiaLevel.ToString())
                {
                    PageType = ReportTypeLevel.IndiaLevel;
                }
                else if (QS("PageType") == ReportTypeLevel.RegionLevel.ToString())
                {
                    PageType = ReportTypeLevel.RegionLevel;
                }
                else if (QS("PageType") == ReportTypeLevel.StateLevel.ToString())
                {
                    PageType = ReportTypeLevel.StateLevel;
                }
                else if (QS("PageType") == ReportTypeLevel.CityLevel.ToString())
                {
                    PageType = ReportTypeLevel.CityLevel;
                }
                else
                {
                    PageType = ReportTypeLevel.OutletListLevel;
                }


            }

   

        }


        public  string getQueryString()
        {
            string strGetQueryStr = "";

         

           

            if (Region != "")
            {
                if (strGetQueryStr == "")
                {
                    strGetQueryStr = "?R=" + Region;
                }
                else
                {
                    strGetQueryStr += "&R=" + Region;

                }
            }
            
            if (State != "")
            {
                if (strGetQueryStr == "")
                {
                    strGetQueryStr = "?S=" + State;
                }
                else
                {
                    strGetQueryStr += "&S=" + State;

                }
            }

            if (StateName != "")
            {
                if (strGetQueryStr == "")
                {
                    strGetQueryStr = "?SN=" + StateName;
                }
                else
                {
                    strGetQueryStr += "&SN=" + StateName;

                }
            }


            
           
            if (City != "")
            {
                if (strGetQueryStr == "")
                {
                    strGetQueryStr = "?C=" + City;
                }
                else
                {
                    strGetQueryStr += "&C=" + City;

                }
            }


       
            if (CityName != "")
            {
                if (strGetQueryStr == "")
                {
                    strGetQueryStr = "?CN=" + CityName;
                }
                else
                {
                    strGetQueryStr += "&CN=" + CityName;

                }
            }
            if (RecceId != "")
            {
                if (strGetQueryStr == "")
                {
                    strGetQueryStr = "?RecceId=" + RecceId;
                }
                else
                {
                    strGetQueryStr += "&RecceId=" + RecceId;

                }
            }

            if (OutletTypeId != "")
            {
                if (strGetQueryStr == "")
                {
                    strGetQueryStr = "?OutletTypeId=" + OutletTypeId;
                }
                else
                {
                    strGetQueryStr += "&OutletTypeId=" + OutletTypeId;

                }
            }

        

            

            if (OutletType != "")
            {
                if (strGetQueryStr == "")
                {
                    strGetQueryStr = "?OutletType=" + OutletType;
                }
                else
                {
                    strGetQueryStr += "&OutletType=" + OutletType;

                }
            }


            
           

            //
            if (Year != "")
            {
                if (strGetQueryStr == "")
                {
                    strGetQueryStr = "?Year=" + Year;
                }
                else
                {
                    strGetQueryStr += "&Year=" + Year;

                }
            }

            if (FromDate != "")
            {
                if (strGetQueryStr == "")
                {
                    strGetQueryStr = "?FrD=" + FromDate;
                }
                else
                {
                    strGetQueryStr += "&FrD=" + FromDate;

                }
                
            }
            if (ToDate != "")
            {
                if (strGetQueryStr == "")
                {
                    strGetQueryStr = "?ToD=" + ToDate;
                }
                else
                {
                    strGetQueryStr += "&ToD=" + ToDate;

                }
                
            }
            if (FromWeek != "")
            {
                if (strGetQueryStr == "")
                {
                    strGetQueryStr = "?FrW=" + FromWeek;
                }
                else
                {
                    strGetQueryStr += "&FrW=" + FromWeek;

                }
                
            }
            if (ToWeek != "")
            {
                if (strGetQueryStr == "")
                {
                    strGetQueryStr = "?ToW=" + ToWeek;
                }
                else
                {
                    strGetQueryStr += "&ToW=" + ToWeek;

                }
                
            }

            if (RegionMngApproved != "")
            {
                if (strGetQueryStr == "")
                {
                    strGetQueryStr = "?RMA=" + RegionMngApproved;
                }
                else
                {
                    strGetQueryStr += "&RMA=" + RegionMngApproved;

                }

            }

            if (StateMngApproved != "")
            {
                if (strGetQueryStr == "")
                {
                    strGetQueryStr = "?SMA=" + StateMngApproved;
                }
                else
                {
                    strGetQueryStr += "&SMA=" + StateMngApproved;

                }

            }
         

            if (SelectDateFormat != null)
            {
                if (strGetQueryStr == "")
                {
                    strGetQueryStr = "?SDFor=" + SelectDateFormat;
                }
                else
                {
                    strGetQueryStr += "&SDFor=" + SelectDateFormat;

                }
                
            }
            if (FromMonth!="")
            {
                if (strGetQueryStr == "")
                {
                    strGetQueryStr = "?FrMon=" + FromMonth;
                }
                else
                {
                    strGetQueryStr += "&FrMon=" + FromMonth;

                }
            }
            if (ToMonth!="")
            {
                if (strGetQueryStr == "")
                {
                    strGetQueryStr = "?ToMon=" + ToMonth;
                }
                else
                {
                    strGetQueryStr += "&ToMon=" + ToMonth;
                }
            }
            if (SelectRegionIndex != "")
            {
                if (strGetQueryStr == "")
                {
                    strGetQueryStr = "?SRIndex=" + SelectRegionIndex;
                }
                else
                {
                    strGetQueryStr += "&SRIndex=" + SelectRegionIndex;
                }

            }


            if (RegionName != "")
            {
                if (strGetQueryStr == "")
                {
                    strGetQueryStr = "?RN=" + RegionName;
                }
                else
                {
                    strGetQueryStr += "&RN=" + RegionName;
                }
            }

            
            

                if (strGetQueryStr == "")
                {
                    strGetQueryStr = "?PageType=" + PageType.ToString();
                }
                else
                {
                    strGetQueryStr += "&PageType=" + PageType.ToString();
                }
               
                if (strGetQueryStr !="" && strGetQueryStr.Substring(0, 1) == "?")
                {
                    strGetQueryStr = strGetQueryStr.Substring(1, strGetQueryStr.Length - 1);
                }




              strGetQueryStr=  "?" + EncryptQueryString(strGetQueryStr);



            return strGetQueryStr;
        }

        public string getPlainQueryString()
        {
            string strGetQueryStr = "";


          

            if (Region != "")
            {
                if (strGetQueryStr == "")
                {
                    strGetQueryStr = "?R=" + Region;
                }
                else
                {
                    strGetQueryStr += "&R=" + Region;

                }
            }

            if (State != "")
            {
                if (strGetQueryStr == "")
                {
                    strGetQueryStr = "?S=" + State;
                }
                else
                {
                    strGetQueryStr += "&S=" + State;

                }
            }

            if (StateName != "")
            {
                if (strGetQueryStr == "")
                {
                    strGetQueryStr = "?SN=" + StateName;
                }
                else
                {
                    strGetQueryStr += "&SN=" + StateName;

                }
            }

           



            if (City != "")
            {
                if (strGetQueryStr == "")
                {
                    strGetQueryStr = "?C=" + City;
                }
                else
                {
                    strGetQueryStr += "&C=" + City;

                }
            }


         
            if (CityName != "")
            {
                if (strGetQueryStr == "")
                {
                    strGetQueryStr = "?CN=" + CityName;
                }
                else
                {
                    strGetQueryStr += "&CN=" + CityName;

                }
            }
            if (RecceId != "")
            {
                if (strGetQueryStr == "")
                {
                    strGetQueryStr = "?RecceId=" + RecceId;
                }
                else
                {
                    strGetQueryStr += "&RecceId=" + RecceId;

                }
            }

            if (OutletTypeId != "")
            {
                if (strGetQueryStr == "")
                {
                    strGetQueryStr = "?OutletTypeId=" + OutletTypeId;
                }
                else
                {
                    strGetQueryStr += "&OutletTypeId=" + OutletTypeId;

                }
            }

            if (OutletType != "")
            {
                if (strGetQueryStr == "")
                {
                    strGetQueryStr = "?OutletType=" + OutletType;
                }
                else
                {
                    strGetQueryStr += "&OutletType=" + OutletType;

                }
            }

           


           
            if (Year != "")
            {
                if (strGetQueryStr == "")
                {
                    strGetQueryStr = "?Year=" + Year;
                }
                else
                {
                    strGetQueryStr += "&Year=" + Year;

                }
            }

           
            if (FromDate != "")
            {
                if (strGetQueryStr == "")
                {
                    strGetQueryStr = "?FrD=" + FromDate;
                }
                else
                {
                    strGetQueryStr += "&FrD=" + FromDate;

                }

            }
            if (ToDate != "")
            {
                if (strGetQueryStr == "")
                {
                    strGetQueryStr = "?ToD=" + ToDate;
                }
                else
                {
                    strGetQueryStr += "&ToD=" + ToDate;

                }

            }
            if (FromWeek != "")
            {
                if (strGetQueryStr == "")
                {
                    strGetQueryStr = "?FrW=" + FromWeek;
                }
                else
                {
                    strGetQueryStr += "&FrW=" + FromWeek;

                }

            }
            if (ToWeek != "")
            {
                if (strGetQueryStr == "")
                {
                    strGetQueryStr = "?ToW=" + ToWeek;
                }
                else
                {
                    strGetQueryStr += "&ToW=" + ToWeek;

                }

            }


            if (RegionMngApproved != "")
            {
                if (strGetQueryStr == "")
                {
                    strGetQueryStr = "?RMA=" + RegionMngApproved;
                }
                else
                {
                    strGetQueryStr += "&RMA=" + RegionMngApproved;

                }

            }

            if (StateMngApproved != "")
            {
                if (strGetQueryStr == "")
                {
                    strGetQueryStr = "?SMA=" + StateMngApproved;
                }
                else
                {
                    strGetQueryStr += "&SMA=" + StateMngApproved;

                }

            }
            if (SelectDateFormat != null)
            {
                if (strGetQueryStr == "")
                {
                    strGetQueryStr = "?SDFor=" + SelectDateFormat;
                }
                else
                {
                    strGetQueryStr += "&SDFor=" + SelectDateFormat;

                }

            }
            if (FromMonth != "")
            {
                if (strGetQueryStr == "")
                {
                    strGetQueryStr = "?FrMon=" + FromMonth;
                }
                else
                {
                    strGetQueryStr += "&FrMon=" + FromMonth;

                }
            }
            if (ToMonth != "")
            {
                if (strGetQueryStr == "")
                {
                    strGetQueryStr = "?ToMon=" + ToMonth;
                }
                else
                {
                    strGetQueryStr += "&ToMon=" + ToMonth;
                }
            }
            if (SelectRegionIndex != "")
            {
                if (strGetQueryStr == "")
                {
                    strGetQueryStr = "?SRIndex=" + SelectRegionIndex;
                }
                else
                {
                    strGetQueryStr += "&SRIndex=" + SelectRegionIndex;
                }

            }


          

            if (RegionName != "")
            {
                if (strGetQueryStr == "")
                {
                    strGetQueryStr = "?RN=" + RegionName;
                }
                else
                {
                    strGetQueryStr += "&RN=" + RegionName;
                }
            }



          



            if (strGetQueryStr == "")
            {
                strGetQueryStr = "?PageType=" + PageType.ToString();
            }
            else
            {
                strGetQueryStr += "&PageType=" + PageType.ToString();
            }
          

            if (strGetQueryStr != "" && strGetQueryStr.Substring(0, 1) == "?")
            {
                strGetQueryStr = strGetQueryStr.Substring(1, strGetQueryStr.Length - 1);
            }





            return strGetQueryStr;
        }


        public SelectedValuesClass()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public void setValuesInObj(SelectedValuesClass DeSelectObj)
        {
           
            Region = DeSelectObj.Region;
            State = DeSelectObj.State;
            City = DeSelectObj.City;
            CityName = DeSelectObj.CityName;
            RecceId = DeSelectObj.RecceId;
            OutletTypeId = DeSelectObj.OutletTypeId;
            OutletType = DeSelectObj.OutletType;
            StateName = DeSelectObj.StateName;

            Year = DeSelectObj.Year;
            FromDate = DeSelectObj.FromDate;
            ToDate = DeSelectObj.ToDate;
            FromWeek = DeSelectObj.FromWeek;
            ToWeek = DeSelectObj.ToWeek;

            RegionMngApproved = DeSelectObj.RegionMngApproved;
            StateMngApproved = DeSelectObj.StateMngApproved;

            SelectDateFormat = DeSelectObj.SelectDateFormat;
            FromMonth = DeSelectObj.FromMonth;
            ToMonth = DeSelectObj.ToMonth;
            SelectRegionIndex = DeSelectObj.SelectRegionIndex;
            RegionName = DeSelectObj.RegionName;
           


        }
      
    
    }



    public enum ReportTypeLevel 
    {
         IndiaLevel,
         RegionLevel,
         StateLevel,
         CityLevel,
         OutletListLevel,
         Admit,
         RegionIrisLevel,
         StateIrisLevel
    };

  


  

}
