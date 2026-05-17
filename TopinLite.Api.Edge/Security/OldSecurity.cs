using BPJ.Common.Security;

using System;
using System.Security.Cryptography;
using System.Text;

namespace TopinLite.Api.Edge.Security
{
    public class OldSecurity
    {
        /// <summary>
        /// Minutes
        /// </summary>
        public static double ValidatePeriod = 30; // 30 minutes default

        private const decimal activeUserStatus = 1;

        private int maxmin = 5;
        private int TokenRateInMinutes = 10;

        public OldSecurity()
        {
        }

        private bool CheckSQLSyntax(string str)
        {
            string[] sqlInjectionProtection = { "-", "/", "'", ";", " " };

            foreach (string sqlInjectionProtectionitem in sqlInjectionProtection)
                if (str.Contains(sqlInjectionProtectionitem))
                    return false;

            return true;
        }

        //public bool CheckSecurity(string HashStr, string userId, int usage, string ipAddress, string dbuserid)
        //{
        //    string UserIDDefult = userId;
        //    userId = userId.ToUpper();

        //    TopupCredential.VocherBroker vocher = new TopupCredential.VocherBroker();
        //    BPJ.WebApi.TopupCredential _vocher = new TopupCredential();

        //    vocher = _vocher.LoadByPrimaryID(userId, usage, dbuserid);
        //    if (vocher.STATUS == null)

        //        return false;
        //    if ((vocher.STATUS.Equals(0)) || !vocher.IP_LIST.Contains(ipAddress))

        //        // if (_vocher.Any(user => user.STATUS.Equals(0)) || !_vocher.Any(user => user.IP_LIST.Contains(ipAddress)))
        //        return false;
        //    string DPwd = vocher.PASSWORD;  // BPJ.Common.Security.Rijndael.Decrypt(Pwd);

        //    DateTime dt = DateTime.Now;

        //    if (TestHash(HashStr, userId, dt, string.Empty, DPwd))
        //    {
        //        return true;
        //    }
        //    if (TestHash(HashStr, userId, dt.AddMinutes(-1 * maxmin), string.Empty, DPwd))
        //    {
        //        return true;
        //    }
        //    if (TestHash(HashStr, userId, dt.AddMinutes(maxmin), string.Empty, DPwd))
        //    {
        //        return true;
        //    }

        //    return false;

        //}

        //public bool CheckSecurityNew(string HashStr, string userId, int usage, string ipAddress, string MethodName, string dbuserid)
        //{
        //    string UserIDDefult = userId;
        //    userId = userId.ToUpper();

        //    TopupCredential.VocherBroker vocher = new TopupCredential.VocherBroker();
        //    BPJ.WebApi.TopupCredential _vocher = new TopupCredential();

        //    vocher = _vocher.LoadByPrimaryIDNew(userId, usage, MethodName, dbuserid);
        //    if (vocher.STATUS == null)

        //        return false;
        //    if ((vocher.STATUS.Equals(0)) || !vocher.IP_LIST.Contains(ipAddress))

        //        // if (_vocher.Any(user => user.STATUS.Equals(0)) || !_vocher.Any(user => user.IP_LIST.Contains(ipAddress)))
        //        return false;
        //    string DPwd = vocher.PASSWORD;  // BPJ.Common.Security.Rijndael.Decrypt(Pwd);

        //    DateTime dt = DateTime.Now;

        //    if (TestHash(HashStr, userId, dt, string.Empty, DPwd))
        //    {
        //        return true;
        //    }
        //    if (TestHash(HashStr, userId, dt.AddMinutes(-1 * maxmin), string.Empty, DPwd))
        //    {
        //        return true;
        //    }
        //    if (TestHash(HashStr, userId, dt.AddMinutes(maxmin), string.Empty, DPwd))
        //    {
        //        return true;
        //    }

        //    return false;

        //}

        //public bool CheckSecurity(string HashStr, string userId, int usage, string ipAddress)
        //{
        //    string UserIDDefult = userId;
        //    userId = userId.ToUpper();

        //    TopupCredential.VocherBroker vocher = new TopupCredential.VocherBroker();
        //    BPJ.WebApi.TopupCredential _vocher = new TopupCredential();

        //    vocher = _vocher.LoadByPrimaryID(userId, usage);
        //    if (vocher.STATUS == null)

        //        return false;
        //    if ((vocher.STATUS.Equals(0)) || !vocher.IP_LIST.Contains(ipAddress))

        //        // if (_vocher.Any(user => user.STATUS.Equals(0)) || !_vocher.Any(user => user.IP_LIST.Contains(ipAddress)))
        //        return false;
        //    string DPwd = vocher.PASSWORD;  // BPJ.Common.Security.Rijndael.Decrypt(Pwd);

        //    DateTime dt = DateTime.Now;

        //    if (TestHash(HashStr, userId, dt, string.Empty, DPwd))
        //    {
        //        return true;
        //    }
        //    if (TestHash(HashStr, userId, dt.AddMinutes(-1 * maxmin), string.Empty, DPwd))
        //    {
        //        return true;
        //    }
        //    if (TestHash(HashStr, userId, dt.AddMinutes(maxmin), string.Empty, DPwd))
        //    {
        //        return true;
        //    }

        //    return false;

        //}

        //public bool CheckSecurity(string HashStr, string userId, int usage, string ipAddress)
        //{
        //    string UserIDDefult = userId;
        //    userId = userId.ToUpper();

        //    TopupCredential.VocherBroker vocher = new TopupCredential.VocherBroker();
        //    BPJ.WebApi.TopupCredential _vocher = new TopupCredential();

        //    vocher = _vocher.LoadByPrimaryID(userId, usage);
        //    if (vocher.STATUS==null)

        //        return false;
        //    if ((vocher.STATUS.Equals(0)) || !vocher.IP_LIST.Contains(ipAddress))

        //   // if (_vocher.Any(user => user.STATUS.Equals(0)) || !_vocher.Any(user => user.IP_LIST.Contains(ipAddress)))
        //        return false;
        //    string DPwd = vocher.PASSWORD;  // BPJ.Common.Security.Rijndael.Decrypt(Pwd);

        //    DateTime dt = DateTime.Now;

        //    if (TestHash(HashStr, userId, dt, string.Empty, DPwd))
        //    {
        //        return true;
        //    }
        //    if (TestHash(HashStr, userId, dt.AddMinutes(-1 * maxmin), string.Empty, DPwd))
        //    {
        //        return true;
        //    }
        //    if (TestHash(HashStr, userId, dt.AddMinutes(maxmin), string.Empty, DPwd))
        //    {
        //        return true;
        //    }

        //    return false;

        //}

        #region Rijndael

        public static string Decrypt(string str)
        {
            return BPJ.Common.Security.Rijndael.Decrypt(str);
        }

        public static string Decrypt(string str, RijndaelKeyIndex Index)
        {
            return BPJ.Common.Security.Rijndael.Decrypt(str, Index);
        }

        public static string Encrypt(string str)
        {
            return BPJ.Common.Security.Rijndael.Encrypt(str);
        }

        public static string Encrypt(string str, RijndaelKeyIndex Index)
        {
            return BPJ.Common.Security.Rijndael.Encrypt(str, Index);
        }

        #endregion Rijndael

        #region Tokenized login

        //public bool CheckSecurity(string HashStr, string userId, string ip)
        //{
        //    userId = userId.ToUpper();

        //    WS_USER_DataAccess pslda = new WS_USER_DataAccess();
        //    if (!pslda.LoadByPrimaryID(userId))
        //        return false;
        //    if ((pslda.WS_USER.STATUS ?? 0).Equals(0) || !(pslda.WS_USER.IP_LIST ?? string.Empty).Contains(ip))
        //        return false;

        //    string Pwd = BPJ.Common.Security.Rijndael.Decrypt(pslda.WS_USER.PASSWORD, BPJ.Common.Security.RijndaelKeyIndex.WebServices);

        //    DateTime dt = DateTime.Now;

        //    if (TestHash(HashStr, userId, dt, string.Empty, Pwd))
        //        return true;
        //    if (TestHash(HashStr, userId, dt.AddMinutes(-1 * maxmin), string.Empty, Pwd))
        //        return true;
        //    if (TestHash(HashStr, userId, dt.AddMinutes(maxmin), string.Empty, Pwd))
        //        return true;
        //    return false;

        //}

        public string GetToken()
        {
            return GetToken(DateTime.Now);
        }

        public string GetToken(DateTime dt)
        {
            string ToHash, sResult;
            int dif = dt.Minute % TokenRateInMinutes;
            dt = dt - new TimeSpan(0, dif, 0);
            ToHash = dt.ToString("yyyyMMdd") + "|" + dt.ToString("HHmm");
            sResult = Hash(ToHash);
            return sResult;
        }

        private bool TestHash(string HashStr, string UserName, DateTime dt, string ServiceName, string Pwd)
        {
            string ToHash;
            string sResultT, sResultToken;
            try
            {
                //DateTime dt = DateTime.Now;
                //System.TimeSpan minute = new System.TimeSpan(0, 0, minutes, 0, 0);
                //dt = dt - minute;
                //////TokenWeGotBefore
                ////ToHash = dt.ToString("yyyyMMdd") + "|" + dt.ToString("HHmm");
                ////sResultToken = Hash(ToHash);
                sResultToken = GetToken(dt);
                ////USERNAME|PassWord|TokenWeGotBefore
                ToHash = UserName.ToUpper() + "|" + Pwd + "|" + sResultToken;
                sResultT = Hash(ToHash);

                //string auth = UserName + ":" + sResultT;
                //byte[] encodeAuth = System.Text.Encoding.UTF8.GetBytes(auth);
                //string authHeader = "Basic " + Convert.ToBase64String(encodeAuth);

                if ((sResultT.ToUpper() == HashStr.ToUpper()))
                    return true;
                //else
                //    if (minutes < maxmin) // allowed maxmin minutes minus 1 second to call web service
                //        return TestHash(HashStr, UserName, minutes + maxmin, ServiceName , Pwd);
                //    else
                return false;
            }
            catch
            {
                return false;
            }
        }

        private bool TestHash(string HashStr, string UserName, DateTime dt, string ServiceName, string Pwd, ref string res)
        {
            string ToHash;
            string sResultT, sResultToken;
            try
            {
                //DateTime dt = DateTime.Now;
                //System.TimeSpan minute = new System.TimeSpan(0, 0, minutes, 0, 0);
                //dt = dt - minute;
                //////TokenWeGotBefore
                ////ToHash = dt.ToString("yyyyMMdd") + "|" + dt.ToString("HHmm");
                ////sResultToken = Hash(ToHash);

                sResultToken = GetToken(dt);
                res = "sResultToken : " + sResultToken;
                ////USERNAME|PassWord|TokenWeGotBefore
                ToHash = UserName.ToUpper() + "|" + Pwd + "|" + sResultToken;
                sResultT = Hash(ToHash);

                res = " ; ToHash : " + ToHash;
                //string auth = UserName + ":" + sResultT;
                //byte[] encodeAuth = System.Text.Encoding.UTF8.GetBytes(auth);
                //string authHeader = "Basic " + Convert.ToBase64String(encodeAuth);

                if ((sResultT.ToUpper() == HashStr.ToUpper()))
                    return true;
                //else
                //    if (minutes < maxmin) // allowed maxmin minutes minus 1 second to call web service
                //        return TestHash(HashStr, UserName, minutes + maxmin, ServiceName , Pwd);
                //    else
                return false;
            }
            catch
            {
                return false;
            }
        }

        private string Hash(string ToHash)
        {
            // First we need to convert the string into bytes, which means using a text encoder.
            Encoder enc = System.Text.Encoding.ASCII.GetEncoder();

            // Create a buffer large enough to hold the string
            byte[] data = new byte[ToHash.Length];
            enc.GetBytes(ToHash.ToCharArray(), 0, ToHash.Length, data, 0, true);

            // This is one implementation of the abstract class MD5.
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] result = md5.ComputeHash(data);

            return BitConverter.ToString(result).Replace("-", "").ToLower();
        }

        #endregion Tokenized login
    }
}