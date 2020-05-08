using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using asp_tender_be.Models;

namespace asp_tender_be.Data
{
    public class DbInitializer
    {
        public static void Initialize(TenderContext context)
        {
            context.Database.EnsureCreated();

            // Pokud už je v databázi nějaká pracovní pozice, databáze je už naplněná
            if (context.Positions.Any())
            {
                return;
            }

            var rand = new Random();

            var jobPart1 = new string[]
            {
                "Lead", "Senior", "Direct", "Corporate", "Dynamic", "Future", "Product", "National", "Regional", "District", "Central", "Global",
                "Relational", "Customer", "Investor", "Dynamic", "International", "Legacy", "Forward", "Interactive", "Internal", "Human", "Chief", "Principal",
            };
            var jobPart2 = new string[]
            {
                "Solutions", "Program", "Brand", "Security", "Research", "Marketing", "Directives", "Implementation", "Integration", "Functionality",
                "Response", "Paradigm", "Tactics", "Identity", "Markets", "Group", "Resonance", "Applications", "Optimization", "Operations",
                "Infrastructure", "Intranet", "Communications", "Web", "Branding", "Quality", "Assurance", "Impact", "Mobility", "Ideation", "Data",
                "Creative", "Configuration", "Accountability", "Interactions", "Factors", "Usability", "Metrics", "Team",
            };
            var jobPart3 = new string[]
            {
                "Supervisor", "Associate", "Executive", "Liason", "Officer", "Manager", "Engineer", "Specialist", "Director", "Coordinator",
                "Administrator","Architect", "Analyst", "Designer", "Planner", "Synergist", "Orchestrator", "Technician", "Developer",
                "Producer", "Consultant", "Assistant", "Facilitator", "Agent", "Representative", "Strategist",
            };

            // oddělení
            var workplaces = new Workplace[]
            {
                new Workplace { Name = "Mainframe Intranet Administration Department" },
                new Workplace { Name = "Source Code Connectivity and Control Department" },
                new Workplace { Name = "Wireless Maintenance Department" },
                new Workplace { Name = "Mainframe Database Backup Department" },
                new Workplace { Name = "Code Installation and Networking Department" },
                new Workplace { Name = "Database Installation and Development Department" },
                new Workplace { Name = "Analytical Extranet Security Department" },
                new Workplace { Name = "Application Technology Department" },
                new Workplace { Name = "Web-based Computer Acquisition Department" },
                new Workplace { Name = "Hardware Installation and Destruction Department" },
                new Workplace { Name = "Multimedia Security Department" },
                new Workplace { Name = "Object-Oriented Source Code Troubleshooting Department" },
                new Workplace { Name = "Extranet Maintenance Department" },
                new Workplace { Name = "Internet Backup Department" },
                new Workplace { Name = "Open-Source Control Department" },
            };
            foreach (Workplace workplace in workplaces)
            {
                context.Workplaces.Add(workplace);
            }
            context.SaveChanges();

            // 5 až 15 náhodných pozic pro každé oddělení
            foreach (Workplace workplace in workplaces)
            {
                var numberOfPositions = rand.Next(5, 16);
                for (var i = 0; i < numberOfPositions; i++)
                {
                    context.Positions.Add(new Position { WorkplaceID = workplace.ID, Name = $"{jobPart1[rand.Next(jobPart1.Length)]} {jobPart2[rand.Next(jobPart2.Length)]} {jobPart3[rand.Next(jobPart3.Length)]}" });
                }
            }
            context.SaveChanges();
        }
    }
}
