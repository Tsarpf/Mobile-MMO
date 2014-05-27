using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

public static class ReadQueue
{
    private static Queue<string> queue = new Queue<string>();

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
    private static Queue<JSONEvent> queue = new Queue<JSONEvent>();

    public static void Write(JSONEvent json)
    {
        lock (queue)
        {
            queue.Enqueue(json);
        }
    }
    public static JSONEvent Read()
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
