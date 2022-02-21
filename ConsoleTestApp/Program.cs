using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer;
using BusinessLayer;
using System.Net;

namespace ConsoleTestApp
{
    class Program
    {
        static Business Business = new Business();

        static void Main(string[] args)
        {
            Console.WriteLine("Started");
            IPAddress ipAddress = ipHostInfo.AddressList[0];
            Console.WriteLine(ipAddress.ToString());
            execute();
            Console.ReadKey();
        }

        static async void execute()
        {

            List<ProjectDetail> projects = await Business.GetAllProjectsIdName();

            Console.WriteLine(projects.Count);

            projects.ForEach(p =>
            {
                Console.WriteLine($"\nProject ID: {p.ProjectId}");
                Console.WriteLine($"Project Name: {p.ProjectName}");
            });


        }
    }
}
