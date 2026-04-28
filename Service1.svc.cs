using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WebApplication1_Assignment5
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service1 : IService1
    {
        public bool isSafeUrl(string url)

        {
            WebClient channel = new WebClient(); // create a channel
            string content = channel.DownloadString(url); // store content of web url
            // list of keywords that may contain unsafe or dangerous content
            string[] dangerousKeywords =
            {
                "malware",
                "phishing",
                "exploit",
                "hack",
            };
            content = content.ToLower(); // convert content to lowercase so keyword matching is case-insensitive
            foreach (string keyword in dangerousKeywords) // loop through each dangerous keyword
            {
                if(content.Contains(keyword)) // check if keyword exists in the webpage content
                {
                    return false; // if a match is found mark url as unsafe
                }
            }
            return true; // if no dangerous keywords are found mark url as safe 
            
        }
    }
}
