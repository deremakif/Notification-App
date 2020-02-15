using MassTransit;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Test.Messages;

namespace MassTransitConsumerApi.Consumers
{
    public class TestConsumer : IConsumer<TestMessage>
    {
        public Task Consume(ConsumeContext<TestMessage> context)
        {
            try
            {
                //int x = 5/ Convert.ToInt32(context.Message.Text); 
                Console.WriteLine($"Received {context.Message.Body}");

                WebRequest tRequest = WebRequest.Create("https://fcm.googleapis.com/fcm/send");
                tRequest.Method = "post";
                //serverKey - Key from Firebase cloud messaging server  
                tRequest.Headers.Add(string.Format("Authorization: key={0}", "AAAAz3YMCW8:APA91bEv6COJzrr3Q9Akn0aOzR6SFLS2cj0iuTbfjip-lGVH2MZRR0bkjtBBphPZwbpbktoYHvCeodxKNGVqKqkOXxboOgpxb2KEJpU3B-XymSSi7-ftxeDMfqVaq8e2y7xwwF8qeWKS"));
                //Sender Id - From firebase project setting  
                tRequest.Headers.Add(string.Format("Sender: id={0}", "891038730607"));
                tRequest.ContentType = "application/json";
                var payload = new
                {
                    to = context.Message.To,
                    priority = "high",
                    content_available = true,
                    notification = new
                    {
                        body = context.Message.Body,
                        title = context.Message.Title
                    },
                    data = new
                    {
                        key1 = "value1",
                        key2 = "value2"
                    },
                    android = new
                    {
                        ttl = "36500s"
                    }
                };

                string postbody = JsonConvert.SerializeObject(payload).ToString();
                Byte[] byteArray = Encoding.UTF8.GetBytes(postbody);
                tRequest.ContentLength = byteArray.Length;
                using (Stream dataStream = tRequest.GetRequestStream())
                {
                    dataStream.Write(byteArray, 0, byteArray.Length);
                    using (WebResponse tResponse = tRequest.GetResponse())
                    {
                        using (Stream dataStreamResponse = tResponse.GetResponseStream())
                        {
                            if (dataStreamResponse != null)
                                using (StreamReader tReader = new StreamReader(dataStreamResponse))
                                {
                                    String sResponseFromServer = tReader.ReadToEnd();
                                    string resultResponse = sResponseFromServer;
                                    Console.WriteLine(sResponseFromServer);
                                }
                        }
                    }
                }


                return Task.FromResult(0);
            }
            catch (Exception ex)
            {
                // Exception döndüğünde tekrar edilmesi gerektiği sayı sıfırlanmamışsa yeniden kuyruğa alınır.
                return Task.FromException(ex);
            }


        }
    }
}
