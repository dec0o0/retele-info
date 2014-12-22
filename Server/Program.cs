﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;


namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var host = new ServiceHost(typeof(Service)))
            {
                host.Open();
                while (Console.ReadLine() != "exit") { }
            }
        }
    }
}
