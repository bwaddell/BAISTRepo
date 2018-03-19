using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Web.Security;
using System.Security.Cryptography;

/// <summary>
/// Summary description for SecurityManager
/// </summary>
public class SecurityManager
{
    public string GetHashSHA245(string inputString)
    {
        StringBuilder sb = new StringBuilder();
        HashAlgorithm hashAlg = SHA256.Create();
        byte[] hashArr = hashAlg.ComputeHash(Encoding.UTF8.GetBytes(inputString));
        foreach (byte b in hashArr)
        {
            sb.Append(b.ToString("x2"));
        }

        return sb.ToString();
    }
    public string CreateSalt(int size)
    {
        byte[] SaltArr = new Byte[size];
        RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
        rng.GetBytes(SaltArr);
        return Convert.ToBase64String(SaltArr);
    }
    public string CreatePasswordHash(string pwd, string salt)
    {
        string pwdAndSalt;
        pwdAndSalt = pwd + salt;
        StringBuilder sb = new StringBuilder();
        HashAlgorithm hashAlg = SHA256.Create();
        byte[] hashArr = hashAlg.ComputeHash(Encoding.UTF8.GetBytes(pwdAndSalt));
        foreach (byte b in hashArr)
        {
            sb.Append(b.ToString("x2"));
        }

        return sb.ToString();
    }

    public bool IsAuthenticated(string email, string password)
    {
        FacilitatorDirector facilitatorDirector = new FacilitatorDirector();
        Facilitator pullFacilitator = facilitatorDirector.GetFacilitatorByEmail(email);
        string createNewHash = CreatePasswordHash(password, pullFacilitator.Salt);
        if ((createNewHash == pullFacilitator.Password))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}