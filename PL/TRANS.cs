//INSTANT C# NOTE: Formerly VB project-level imports:
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;

namespace POS
{
	public class Rootobject
	{
		public bool status {get; set;}
		public string message {get; set;}
		public string id {get; set;}
		public string src {get; set;}
	}



	public class devicestatus
	{
		public bool status {get; set;}
		public Data data {get; set;}
	}

	public class Data
	{
		public string name {get; set;}
		public string serial {get; set;}
		public string token {get; set;}
		public int account {get; set;}
		public string sender {get; set;}
		public string webhook_url {get; set;}
		public string quota {get; set;}
		public string expired_date {get; set;}
		public bool active {get; set;}
		public string status {get; set;}
	}


    public class messageResult
    {
        public bool status { get; set; }
        public string message { get; set; }
        public string _to { get; set; }
        public string from { get; set; }
        public string messageId { get; set; }
        public string response { get; set; }
        public string type { get; set; }
    }


}