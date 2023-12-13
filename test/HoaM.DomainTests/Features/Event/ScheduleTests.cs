namespace HoaM.Domain.UnitTests
{
    public class ScheduleTests
    {
        [Fact]
        public void CreateSchedule_WithValidInterval_ReturnsScheduleInstance()
        {
            // Arrange
            var interval = TimeSpan.FromDays(7);

            // Act
            var schedule = Schedule.Create(interval);

            // Assert
            Assert.NotNull(schedule);
            Assert.Equal(interval, schedule.Interval);
            Assert.Null(schedule.EndsAt);
        }

        [Fact]
        public void CreateWeeklySchedule_ReturnsScheduleWithWeeklyInterval()
        {
            // Act
            var schedule = Schedule.CreateWeekly();

            // Assert
            Assert.NotNull(schedule);
            Assert.Equal(TimeSpan.FromDays(7), schedule.Interval);
            Assert.Null(schedule.EndsAt);
        }

        [Fact]
        public void CreateScheduleWithIntervalLessThanOneDay_ThrowsDomainException()
        {
            // Arrange
            var invalidInterval = TimeSpan.FromHours(12);

            // Act and Assert
            Assert.Throws<DomainException>(() => Schedule.Create(invalidInterval));
        }

        [Fact]
        public void GetOccurrences_WithValidData_ReturnsExpectedOccurrences()
        {
            // Arrange
            var firstOccurrence = new Occurrence(DateTimeOffset.UtcNow, DateTimeOffset.UtcNow.AddHours(1));
            var schedule = Schedule.Create(TimeSpan.FromDays(1));

            // Act
            var occurrences = schedule.GetOccurrences(firstOccurrence, 3).ToList();

            // Assert
            Assert.Equal(3, occurrences.Count);

            for (int i = 0; i < occurrences.Count; i++)
            {
                Assert.Equal(firstOccurrence.Start + TimeSpan.FromDays(i), occurrences[i].Start);
                Assert.Equal(firstOccurrence.Stop + TimeSpan.FromDays(i), occurrences[i].Stop);
            }
        }

        [Fact]
        public void GetOccurrences_WithNullFirstOccurrence_ThrowsDomainException()
        {
            // Arrange
            var schedule = Schedule.CreateWeekly();

            // Act and Assert
            Assert.Throws<DomainException>(() => schedule.GetOccurrences(null, 3).ToList());
        }

        [Fact]
        public void GetOccurrences_WithLimitGreaterThanEndsAt_ReturnsOccurrencesUpToEndsAt()
        {
            // Arrange
            var firstOccurrence = new Occurrence(DateTimeOffset.UtcNow, DateTimeOffset.UtcNow.AddHours(1));
            var schedule = Schedule.Create(TimeSpan.FromDays(1), DateTimeOffset.UtcNow.AddHours(24));

            // Act
            var occurrences = schedule.GetOccurrences(firstOccurrence, 10).ToList();

            // Assert
            Assert.Equal(2, occurrences.Count); // Two occurrences within 24 hours
        }

        [Fact]
        public void NextOccurrence_WithValidData_ReturnsNextOccurrence()
        {
            // Arrange
            var start = DateTimeOffset.UtcNow;
            var interval = TimeSpan.FromDays(1);
            var endsAt = start.AddDays(7);
            var schedule = Schedule.Create(interval, endsAt);
            var currentOccurrence = new Occurrence(start, start.AddHours(1));

            // Act
            var nextOccurrence = schedule.NextOccurrence(currentOccurrence);

            // Assert
            Assert.NotNull(nextOccurrence);
            Assert.Equal(currentOccurrence.Start + interval, nextOccurrence.Start);
            Assert.Equal(currentOccurrence.Stop + interval, nextOccurrence.Stop);
        }

        [Fact]
        public void NextOccurrence_WithEndsAt_ReturnsNullAfterEndsAt()
        {
            // Arrange
            var start = DateTimeOffset.UtcNow;
            var interval = TimeSpan.FromDays(1);
            var endsAt = start.AddHours(2);
            var schedule = Schedule.Create(interval, endsAt);
            var currentOccurrence = new Occurrence(start, start.AddHours(1));

            // Act
            var nextOccurrence = schedule.NextOccurrence(currentOccurrence);

            // Assert
            Assert.Null(nextOccurrence);
        }

        [Fact]
        public void NextOccurrence_WithoutEndsAt_ReturnsNextOccurrence()
        {
            // Arrange
            var start = DateTimeOffset.UtcNow;
            var interval = TimeSpan.FromDays(1);
            var schedule = Schedule.Create(interval);
            var currentOccurrence = new Occurrence(start, start.AddHours(1));

            // Act
            var nextOccurrence = schedule.NextOccurrence(currentOccurrence);

            // Assert
            Assert.NotNull(nextOccurrence);
            Assert.Equal(currentOccurrence.Start + interval, nextOccurrence.Start);
            Assert.Equal(currentOccurrence.Stop + interval, nextOccurrence.Stop);
        }
    }
}
