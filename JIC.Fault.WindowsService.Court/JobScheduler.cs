using JIC.Base;
using JIC.Fault.WindowsService.Court.Jobs;
using JIC.Repositories;
using Quartz;
using Quartz.Impl;
using Quartz.Unity;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;

namespace JIC.Fault.WindowsService.Court
{
    public class JobScheduler
    {
        private ISchedulerFactory factory;
        public JobScheduler(ISchedulerFactory factory)
        {
            this.factory = factory;
        }
        public async Task StartAsync()
        {
            // get a scheduler
            IScheduler scheduler = await factory.GetScheduler();
            await scheduler.Start();

            IJobDetail job = new JobDetailImpl("SendDecisionJob", typeof(SendDecisionJob));
            //IJobDetail job2 = new JobDetailImpl("SendDecisionJob", typeof(SendDecisionJob));

            //this is a trigger that Runs the Service Every 15 min
            ITrigger trigger = TriggerBuilder.Create()
                   .WithSimpleSchedule(a => a.WithIntervalInMinutes(SystemConfigurations.ScheudlerInterval).RepeatForever())
                   .Build();

            await scheduler.ScheduleJob(job, trigger);
            // await scheduler.ScheduleJob(job2, trigger);

            //this is to run multiple Jobs
            //await scheduler.ScheduleJobs(
            //        new Dictionary<IJobDetail, IReadOnlyCollection<ITrigger>>
            //        {
            //            [job] = new List<ITrigger> { trigger },
            //            [job2] = new List<ITrigger> { trigger }
            //        }, true);
        }

        public async Task StopAsync()
        {
            IScheduler scheduler = await factory.GetScheduler();
            await scheduler.Shutdown();
        }
    }
}
