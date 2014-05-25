using System;
using System.Collections;
using System.Net;
using System.Net.Security;
using System.Net.Sockets;
using System.Security.Authentication;
using System.Text;
using System.Security.Cryptography.X509Certificates;
using System.IO;
using Newtonsoft.Json;
using System.Reflection;

namespace ClientTest
{
public abstract class JSONEvent
{
    public string EventType
	{
		get
        {
			return this.GetType().Name.Substring(0, this.GetType().Name.Length - "Event".Length);
        }
        set
	    {}
	}
}

public class RegisterEvent : JSONEvent 
{
	//public static readonly string EventType = "Register";
	public string Username;
	public string Password; 
	public string Email;
}

public class LoginEvent : JSONEvent
{
	public string Username;
	public string Password;
}
public class SslTcpClient
{
    private static Hashtable certificateErrors = new Hashtable();
    static readonly byte[] hardCodedServerCertificateHash = { 0xf1, 0x40, 0x8a, 0xd8, 0xb5, 0x1a, 0x42, 0xdb, 0x50, 0x44, 0x04, 0xa1, 0xa8, 0x92, 0xa8, 0xa8, 0x77, 0x41, 0x31, 0x2d };

    // The following method is invoked by the RemoteCertificateValidationDelegate. 
    public static bool ValidateServerCertificate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
    {

        if(sslPolicyErrors == SslPolicyErrors.None)
        {
            return true;
        }

        byte[] receivedCertificateHash = certificate.GetCertHash();
        //If length differs, obviously different hash.
        if(receivedCertificateHash.Length != hardCodedServerCertificateHash.Length)
        {
            return false;
        }

        //Check that each byte is the same
        for (int i = 0; i < hardCodedServerCertificateHash.Length; i++)
        {
            if(receivedCertificateHash[i] != hardCodedServerCertificateHash[i])
            {
                return false;
            }
        }

        //Equality of the certificates confirmed.
        return true;
    }
    public static void RunClient(string machineName, string serverName, int port)
    {
        TcpClient client = new TcpClient(machineName, port);
        Console.WriteLine("Client connected.");
        // Create an SSL stream that will close the client's stream.
        SslStream sslStream = new SslStream(
            client.GetStream(),
            false,
            new RemoteCertificateValidationCallback(ValidateServerCertificate),
            null
            );
        // The server name must match the name on the server certificate. 
        try
        {
            sslStream.AuthenticateAsClient(serverName);
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


        ////byte[] message = Encoding.UTF8.GetBytes("Hello from the client.<EOF>");
        //byte[] message = Encoding.UTF8.GetBytes("register tsurba test1");
		//byte[] message = Encoding.UTF8.GetBytes("login tsurba test1");
		LoginEvent levent = new LoginEvent();
		levent.Username = "tsurba";
		levent.Password = "test1";
		WriteJSONMessage(sslStream, levent);



        string serverMessage = ReadMessage(sslStream);
        Console.WriteLine("Server says: {0}", serverMessage);
        client.Close();
        Console.WriteLine("Client closed.");
    }
    static void WriteJSONMessage(SslStream sslStream, JSONEvent eventObj)
	{
		string message = JsonConvert.SerializeObject(eventObj);
		Console.WriteLine(message);
		byte[] msg = Encoding.UTF8.GetBytes(message);
        sslStream.Write(msg);
        sslStream.Flush();
	}
    static string ReadMessage(SslStream sslStream)
    {
        // Read the  message sent by the server. 
        // The end of the message is signaled using the 
        // "<EOF>" marker.
        byte[] buffer = new byte[2048];
        StringBuilder messageData = new StringBuilder();
        int bytes = -1;
        do
        {
            bytes = sslStream.Read(buffer, 0, buffer.Length);

            // Use Decoder class to convert from bytes to UTF8 
            // in case a character spans two buffers.
            Decoder decoder = Encoding.UTF8.GetDecoder();
            char[] chars = new char[decoder.GetCharCount(buffer, 0, bytes)];
            decoder.GetChars(buffer, 0, bytes, chars, 0);
            messageData.Append(chars);
            // Check for EOF. 
            if (messageData.ToString().IndexOf("<EOF>") != -1)
            {
                break;
            }
        } while (bytes != 0);

        return messageData.ToString();
    }

    public static void Main(string[] args)
    {
        string machineName = "datisbox.net";
        int port = 8666;
        string serverCertificateName = "tsarpf-cert.pem";

		//RegisterEvent revent = new RegisterEvent();
		//revent.Username = "tsurba";
		//revent.Password = "test1";
		//string reg = JsonConvert.SerializeObject(revent);
		//Console.WriteLine("derp:\n" + reg);

        SslTcpClient.RunClient(machineName, serverCertificateName, port);

        Console.ReadLine();
    }
}

}
