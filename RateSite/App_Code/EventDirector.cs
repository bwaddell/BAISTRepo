using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Cryptography;
/// <summary>
/// Summary description for EventDirector
/// </summary>
public class EventDirector
{
	public EventDirector()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public string CreateEventKey(int size)
    {
        byte[] KeyArr = new Byte[size];
        RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
        string key;
        do
        {
            rng.GetBytes(KeyArr);
            key = Convert.ToBase64String(KeyArr).ToUpper();
            key = key.Replace("=", "");
        } while (key.Contains('/') || key.Contains('+'));

        return key;
    }
}