using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using UnityEngine;

public class DownloadLevel 
{
    // Start is called before the first frame update
    public DownloadLevel(int letters, int index)
    {
        DisplayFileFromServer();
        //var username = "gamer";
        //var password = "gamerwantstodownload";
        //string encoded = System.Convert.ToBase64String(Encoding.GetEncoding("ISO-8859-1")
        //                               .GetBytes(username + ":" + password));
        //HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://leksomania.000webhostapp.com/l4s100.txt");
        //request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
        //request.Headers.Add("Authorization", "Basic " + encoded);

        //using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
        //using (Stream stream = response.GetResponseStream())
        //using (StreamReader reader = new StreamReader(stream))
        //{
        //    var x = reader.ReadToEnd();
        //}
    }
    public static void DisplayFileFromServer()
    {
        // The serverUri parameter should start with the ftp:// scheme.
        // Get the object used to communicate with the server.
        FtpWebRequest request = (FtpWebRequest)WebRequest.Create("ftp://files.000webhost.com/l4s100.txt");
        request.Method = WebRequestMethods.Ftp.DownloadFile;

        // This example assumes the FTP site uses anonymous logon.
        request.Credentials = new NetworkCredential("leksomania", "any");

        FtpWebResponse response = (FtpWebResponse)request.GetResponse();

        Stream responseStream = response.GetResponseStream();
        StreamReader reader = new StreamReader(responseStream);
        var x = reader.ReadToEnd();

        reader.Close();
        response.Close();
    }
    
}
