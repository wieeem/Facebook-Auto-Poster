using Facebook;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Security.Policy;
using System.Web.Script.Serialization;

namespace FacebookPoster
{
    class Program
    {

        //Facebook user token
        public const string USER_ACCESS_TOKEN = "your facebook token";

        static void Main(string[] args)
        {
            //Create a facebook client instance with the associated token,
            //the id and the secret code of the application created on the facebook developer ,
        
            
            var fbClient = new FacebookClient(USER_ACCESS_TOKEN);
            fbClient.AppId = "your app ID";
            fbClient.AppSecret = "your app secret key";
            string pageID = "your page ID ";

            //Prepare the date for sharing the post on facebook
            //it must be greater than the current date and time

            //Year 
            var year = 2020;
            //Month
            var month = 08;
            //Day
            var day = 31;
            //HOUR
            var hour = 02;
            //Minute
            var minute = 40;
            var date = new DateTime(year, month, day, hour, minute, 0);


            //Convert date to unix Timestamp
            long epochTicks = new DateTime(1970, 1, 1).Ticks;
            long unixTime = ((date.Ticks - epochTicks) / TimeSpan.TicksPerSecond);
            Console.WriteLine(unixTime);

         
            //Prepare the url of the photo to share
            var url = @"pathtopicture\logo.png";
            string extension = Path.GetExtension(url);
            string FileName = Path.GetFileName(url);


            if (!string.IsNullOrEmpty(extension))
            {
                extension = extension.Replace(".", "");
            }

       
            //Prepare the parameters of the publication, (message, time when to be published ..)
            //You must set parameter published to false (to be able to configure the date of sharing of the post)
               dynamic parameters = new ExpandoObject();
                    parameters.message = "my first publication";
                    parameters.published = "false";
                    parameters.scheduled_publish_time = unixTime;
                    parameters.source = new FacebookMediaObject
                       {
                          ContentType = "image/"+ extension,
                          FileName =FileName
                       }.SetValue(File.ReadAllBytes(url));



               dynamic hallo = fbClient.Post($"/{pageID}/photos", parameters);


               //this will tell you if the publication will be published or not 
               Console.WriteLine(hallo);
            
        }

      

    }
}
