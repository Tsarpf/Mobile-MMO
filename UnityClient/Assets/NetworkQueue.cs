using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

public static class ReadQueue
{
   public static Queue<string> queue = new Queue<string>(); 
   
   public static void Write(string json)
   {
       lock (queue)
       {
           queue.Enqueue(json);
       }
   }
   public static string Read()
   {
       lock (queue)
       {
           if (queue.Count > 0)
           {
               return queue.Dequeue();
           }

           return null;
       }
   }
}

public static class WriteQueue 
{
   public static Queue<string> queue = new Queue<string>(); 
   
   public static void Write(string json)
   {
       lock (queue)
       {
           queue.Enqueue(json);
       }
   }
   public static string Read()
   {
       lock (queue)
       {
           if (queue.Count > 0)
           {
               return queue.Dequeue();
           }

           return null;
       }
   }
}
