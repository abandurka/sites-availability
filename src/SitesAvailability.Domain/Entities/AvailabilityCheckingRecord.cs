using System;
using System.Text.RegularExpressions;
using SitesAvailability.Domain.Entities;

namespace SitesAvailability.Domain
{
    public sealed class AvailabilityCheckingRecord: IEntity
    {
        private const string UrlRegexpPattern = @"^(http:\/\/www\.|https:\/\/www\.|http:\/\/|https:\/\/)[a-z0-9]+([\-\.]{1}[a-z0-9]+)*\.[a-z]{2,5}(:[0-9]{1,5})?(\/.*)?$";

        public static readonly TimeSpan MinOccurencePeriod = TimeSpan.FromMinutes(1);
        public static readonly TimeSpan MaxOccurencePeriod = TimeSpan.FromDays(2);


        public Guid Id { get; set; }
        public string Url { get; set; }
        public TimeSpan Occurrence { get; set; }
        public bool IsAvailable { get; set; }
        public DateTime LastCheckedDate { get; set; }


        public bool IsValid() =>
            IsValidUrl() && IsValidTimeOccurence();

        public bool IsValidUrl() =>
            Regex.IsMatch(Url, UrlRegexpPattern, RegexOptions.Compiled | RegexOptions.IgnoreCase);

        public bool IsValidTimeOccurence() =>
            Occurrence > MinOccurencePeriod && Occurrence < MaxOccurencePeriod;
    }
}
