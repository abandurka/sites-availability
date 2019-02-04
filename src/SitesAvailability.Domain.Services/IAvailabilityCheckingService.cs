using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using SitesAvailability.Domain.Interfaces.Repositories;

namespace SitesAvailability.Domain.Services
{
    interface IAvailabilityCheckingService
    {
        void ScheduleJob(AvailabilityCheckingRecord record);
    }

    public class AvailabilityCheckingService : IAvailabilityCheckingService
    {
        private readonly HashSet<ScheduledJob> _jobs;
        private readonly IRepository<AvailabilityCheckingRecord> _repository;

        public AvailabilityCheckingService(IRepository<AvailabilityCheckingRecord> repository)
        {
            _repository = repository;
            _jobs = new HashSet<ScheduledJob>();
        }

        public void ScheduleJob(AvailabilityCheckingRecord record)
        {
            if (!record.IsValid())
            {
                throw new ArgumentException("The record is invalid");
            }

            var job = new ScheduledJob(record, UpdateRecord);

            _jobs.Add(job);
        }

        private async Task UpdateRecord(AvailabilityCheckingRecord record)
        {
            var isOnline = await IsUrlAvailable(record.Url);

            record.IsAvailable = isOnline;

            await _repository.SaveAsync(record).ConfigureAwait(false);
        }

        private async Task<bool> IsUrlAvailable(string url)
        {
            using (var client = new HttpClient())
            {
                var request = new HttpRequestMessage(HttpMethod.Head, url);
                try
                {
                    var response = await client.SendAsync(request).ConfigureAwait(false);

                    return response.IsSuccessStatusCode;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }
    }

    internal class ScheduledJob: IDisposable
    {
        private AvailabilityCheckingRecord record;
        private readonly Func<AvailabilityCheckingRecord, Task> _action;
        private Timer _timer;

        public ScheduledJob(AvailabilityCheckingRecord record, Func<AvailabilityCheckingRecord,Task> action,bool autoStart = true)
        {
            this.record = record;
            _action = action;
            var callBack = new TimerCallback(ProcessTick);
            _timer = new Timer(callBack, null, TimeSpan.Zero, record.Occurrence);
        }

        public void Stop()
        {
            Dispose();
        }

        private async void ProcessTick(object state)
        {
            await _action(record).ConfigureAwait(false);
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}
