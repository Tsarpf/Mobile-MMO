﻿using System.Collections;
using System.Net.Sockets;
using System;
using System.Net.Security;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using Newtonsoft.Json;
using System.IO;
using UnityEngine;

public class SynchronousNetworkLoop : MonoBehaviour {
    static readonly byte[] hardCodedServerCertificateHash = { 0xf1, 0x40, 0x8a, 0xd8, 0xb5, 0x1a, 0x42, 0xdb, 0x50, 0x44, 0x04, 0xa1, 0xa8, 0x92, 0xa8, 0xa8, 0x77, 0x41, 0x31, 0x2d };
    SslStream stream;
	NetworkStream client;
	// Use this for initialization
	void Start()
	{
		string machineName = "datisbox.net";
		int port = 8666;
		string serverCertificateName = "tsarpf-cert.pem";


		client = new TcpClient(machineName, port).GetStream();
		Console.WriteLine("Client connected.");
		// Create an SSL stream that will close the client's stream.
		stream = new SslStream(
		   client,
		   false,
		   new RemoteCertificateValidationCallback(ValidateServerCertificate),
		   null
		   );
		// The server name must match the name on the server certificate. 
		try
		{
			stream.AuthenticateAsClient(serverCertificateName);
		}
		catch (AuthenticationException e)
		{
			Console.WriteLine("Exception: {0}", e.Message);
			if (e.InnerException != null)
			{
				Console.WriteLine("Inner exception: {0}", e.InnerException.Message);
			}
			Console.WriteLine("Authentication failed - closing the connection.");
			client.Close();
			return;

		}

		//TODO: authentication handlinga ödlfjglkj
  

	}
	static string ReadMessage(SslStream sslStream, NetworkStream client)
	{

		byte[] buffer = new byte[2048];
		StringBuilder messageData = new StringBuilder();

		//Debug.Log(client.DataAvailable);
		if (!client.DataAvailable)
			return null;

		//if (!client.CanRead)
		//	return null;
		int bytes = sslStream.Read(buffer, 0, buffer.Length);

		//Decoder d  = Encoding.UTF8.GetDecoder();
		//char[] cs = new char[d.GetCharCount(buffer, 0, bytes)];
		//d.GetChars(buffer, 0, bytes, cs, 0);
        
		//string test = "";
		//for (int i = 0; i < cs.Length; i++)
		//{
		//	test += cs[i];
		//}
		string data = "";
        Decoder decoder = Encoding.UTF8.GetDecoder();
        char[] chars = new char[decoder.GetCharCount(buffer, 0, bytes)];
        decoder.GetChars(buffer, 0, bytes, chars, 0);
        for (int i = 0; i < chars.Length; i++)
        {
            data += chars[i];
        }
        //if (messageData.ToString().IndexOf("<EOF>") != -1)
        //{
        //    break;
        //}

		if (data.Trim() == "")
			return null;

		return data.Trim();
	}

	void Update ()
	{
		//Read from server (using ssl stream) -- if stuff found, write it to the network read queue?
		string incomingData = ReadMessage(stream, client);
		if (incomingData != null)
		{
		    //JSONEvent readEvent = JsonConvert.DeserializeObject<JSONEvent>(incomingData);
			ReadQueue.Write(incomingData);

		}
		//stream.BeginRead(buffer, 0, buffer.Length, new AsyncCallback(ReadCallback), stream);

		JSONEvent newEvent = WriteQueue.Read();     //read from write queue, send it to socket using write*stuff
		if (newEvent != null)
		{
			WriteJSONMessage(stream, newEvent);
		}
	}
    public static bool ValidateServerCertificate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
    {

        if (sslPolicyErrors == SslPolicyErrors.None)
        {
            return true;
        }

        byte[] receivedCertificateHash = certificate.GetCertHash();
        //If length differs, obviously different hash.
        if (receivedCertificateHash.Length != hardCodedServerCertificateHash.Length)
        {
            return false;
        }

        //Check that each byte is the same
        for (int i = 0; i < hardCodedServerCertificateHash.Length; i++)
        {
            if (receivedCertificateHash[i] != hardCodedServerCertificateHash[i])
            {
                return false;
            }
        }

        //Equality of the certificates confirmed.
        return true;
    }

    static void WriteJSONMessage(SslStream sslStream, JSONEvent eventObj)
    {
        string message = JsonConvert.SerializeObject(eventObj);
        //Console.WriteLine(message);
        byte[] msg = Encoding.UTF8.GetBytes(message);
        sslStream.Write(msg);
        sslStream.Flush();
    }
}
