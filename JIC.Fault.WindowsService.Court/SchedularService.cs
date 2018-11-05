using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace JIC.Fault.WindowsService.Court
{
    partial class SchedularService : ServiceBase
    {
        private readonly JobScheduler jobScheduler;
        public SchedularService(JobScheduler jobScheduler)
        {
            InitializeComponent();
            this.jobScheduler = jobScheduler;
        }

        protected override void OnStart(string[] args)
        {
            jobScheduler.StartAsync().Wait();
        }

        protected override void OnStop()
        {
            if (jobScheduler != null)
                jobScheduler.StopAsync().Wait();
        }
    }
}
