using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quartz.Impl;

namespace Quartz
{
    class Program
    {
        static void Main(string[] args)
        {
            IScheduler sched;
            ISchedulerFactory sf = new StdSchedulerFactory();
            sched = sf.GetScheduler();
            IJobDetail job = JobBuilder.Create<KwywordsStatisticsJob>()
                .WithIdentity("myJob", "group1").Build();;//IndexJob为实现了IJob接口的类
            ITrigger trigger = TriggerBuilder.Create()
              .WithIdentity("myTrigger", "group1")
               //.StartAt(new DateTimeOffset(2016,5,26,16,15,20,0,new TimeSpan(0,0,0,0)))
               .StartAt(DateTimeOffset.Now.AddSeconds(30))
              //.StartNow()
              .WithSimpleSchedule(x => x
                  .WithIntervalInSeconds(40)
                  .RepeatForever())
              .Build();

            sched.ScheduleJob(job, trigger);
            sched.Start();
            Console.ReadKey();
        }
    }
}
