using System;
using Xunit;

namespace SitesAvailability.Domain.UnitTests
{
    public class AvailabilityCheckingRecordTests
    {
        [Fact]
        public void ValidHttpUrl_shouldBe_SuccesfullCreated()
        {
            var record = new AvailabilityCheckingRecord();

            record.Url = "http://www.yandex.com/";
            
            Assert.True(record.IsValidUrl());
        }

        [Fact]
        public void ValidHttpUrl_withParameters_SuccesfullCreated()
        {
            var record = new AvailabilityCheckingRecord();

            record.Url = "http://www.yandex.com/?asd=20";
            Assert.True(record.IsValidUrl());
        }

        [Fact]
        public void ValidHttpsUrl_shouldBe_SuccesfullCreated()
        {
            var record = new AvailabilityCheckingRecord();

            record.Url = "https://www.yandex.com";
            Assert.True(record.IsValidUrl());
        }

        [Fact]
        public void NotValidUrl_shouldBe_SuccesfullCreated()
        {
            var record = new AvailabilityCheckingRecord();

            record.Url = "dex.com/?asd=20";
            Assert.False(record.IsValidUrl());
        }

        [Fact]
        public void ValidOccurence_shouldBe_SuccesfullCreated()
        {
            var record = new AvailabilityCheckingRecord();

            record.Occurrence = TimeSpan.FromHours(1);

            Assert.True(record.IsValidTimeOccurence());
        }

        [Fact]
        public void NotValidOccurence_shouldBe_SuccesfullCreated()
        {
            var record = new AvailabilityCheckingRecord();

            record.Occurrence = TimeSpan.FromMilliseconds(1);

            Assert.False(record.IsValidTimeOccurence());
        }

        [Fact]
        public void ValidUrlAndOccurence_shouldBe_ValidAsRecord()
        {
            var record = new AvailabilityCheckingRecord();

            record.Url = "https://www.yandex.com";
            record.Occurrence = TimeSpan.FromHours(1);

            Assert.True(record.IsValid());
        }


        //I am not implemented other test to safe my time. The real production code should has more tests.
        //It's just a demo of unit tests
    }
}
