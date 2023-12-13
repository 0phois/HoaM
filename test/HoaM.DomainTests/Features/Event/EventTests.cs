namespace HoaM.Domain.UnitTests
{
    public class EventTests
    {
        [Fact]
        public void CreateEvent_WithValidData_ReturnsEventInstance()
        {
            // Arrange
            var activity = "Some Activity";
            var title = new EventTitle("Event Title");
            var start = DateTimeOffset.UtcNow;
            var stop = start.AddHours(2);

            // Act
            var eventInstance = Event<string>.Create(activity, title, start, stop);

            // Assert
            Assert.NotNull(eventInstance);
            Assert.Equal(activity, eventInstance.Data);
            Assert.Equal(title, eventInstance.Title);
            Assert.Equal(start, eventInstance.Occurrence.Start);
            Assert.Equal(stop, eventInstance.Occurrence.Stop);
            Assert.Null(eventInstance.Schedule);
        }

        [Fact]
        public void CreateEvent_WithNullActivity_ThrowsDomainException()
        {
            // Arrange
            var title = new EventTitle("Event Title");
            var start = DateTimeOffset.UtcNow;
            var stop = start.AddHours(2);

            // Act and Assert
            Assert.Throws<DomainException>(() => Event<string>.Create(null, title, start, stop));
        }

        [Fact]
        public void EditTitle_WithValidTitle_UpdatesTitle()
        {
            // Arrange
            var eventInstance = Event<string>.Create("Some Activity", new EventTitle("Original Title"), DateTimeOffset.UtcNow, DateTimeOffset.UtcNow.AddHours(2));
            var newTitle = new EventTitle("New Title");

            // Act
            eventInstance.EditTitle(newTitle);

            // Assert
            Assert.Equal(newTitle, eventInstance.Title);
        }

        [Fact]
        public void EditTitle_WithNullTitle_ThrowsDomainException()
        {
            // Arrange
            var eventInstance = Event<string>.Create("Some Activity", new EventTitle("Original Title"), DateTimeOffset.UtcNow, DateTimeOffset.UtcNow.AddHours(2));

            // Act and Assert
            Assert.Throws<DomainException>(() => eventInstance.EditTitle(null));
        }

        [Fact]
        public void GetInstances_WithSchedule_ReturnsExpectedInstances()
        {
            // Arrange
            var activity = "Some Activity";
            var title = new EventTitle("Event Title");
            var start = DateTimeOffset.UtcNow;
            var stop = start.AddHours(2);
            var schedule = Schedule.Create(TimeSpan.FromDays(1));

            var eventInstance = Event<string>.Create(activity, title, start, stop, schedule);

            // Act
            var instances = eventInstance.GetInstances(3).ToList();

            // Assert
            Assert.Equal(3, instances.Count);
            Assert.Equal(eventInstance, instances[0]);

            for (int i = 1; i < instances.Count; i++)
            {
                Assert.Equal(activity, instances[i].Data);
                Assert.Equal(title, instances[i].Title);
                Assert.Equal(start.AddDays(i), instances[i].Occurrence.Start);
                Assert.Equal(stop.AddDays(i), instances[i].Occurrence.Stop);
            }
        }

        [Fact]
        public void GetInstances_WithoutSchedule_ReturnsSingleInstance()
        {
            // Arrange
            var activity = "Some Activity";
            var title = new EventTitle("Event Title");
            var start = DateTimeOffset.UtcNow;
            var stop = start.AddHours(2);

            var eventInstance = Event<string>.Create(activity, title, start, stop);

            // Act
            var instances = eventInstance.GetInstances(3).ToList();

            // Assert
            Assert.Single(instances);
            Assert.Equal(eventInstance, instances[0]);
        }

        [Fact]
        public void GetInstances_WithNegativeLimit_ThrowsDomainException()
        {
            // Arrange
            var eventInstance = Event<string>.Create("Some Activity", new EventTitle("Event Title"), DateTimeOffset.UtcNow, DateTimeOffset.UtcNow.AddHours(2), Schedule.Create(TimeSpan.FromDays(1)));

            // Act and Assert
            Assert.Throws<DomainException>(() => eventInstance.GetInstances(-1).ToList());
        }

        [Fact]
        public void GetInstances_WithScheduleAndLimitGreaterThanOccurrences_ReturnsAllOccurrences()
        {
            // Arrange
            var activity = "Some Activity";
            var title = new EventTitle("Event Title");
            var start = DateTimeOffset.UtcNow;
            var stop = start.AddHours(2);
            var schedule = Schedule.Create(TimeSpan.FromDays(1));

            var eventInstance = Event<string>.Create(activity, title, start, stop, schedule);

            // Act
            var instances = eventInstance.GetInstances(5).ToList();

            // Assert
            Assert.Equal(5, instances.Count);
        }

        [Fact]
        public void GetInstances_WithoutScheduleAndLimitGreaterThanOne_ReturnsSingleInstance()
        {
            // Arrange
            var activity = "Some Activity";
            var title = new EventTitle("Event Title");
            var start = DateTimeOffset.UtcNow;
            var stop = start.AddHours(2);

            var eventInstance = Event<string>.Create(activity, title, start, stop);

            // Act
            var instances = eventInstance.GetInstances(3).ToList();

            // Assert
            Assert.Single(instances);
        }

        [Fact]
        public void GetInstances_WithoutScheduleAndLimitNegative_ThrowsDomainException()
        {
            // Arrange
            var activity = "Some Activity";
            var title = new EventTitle("Event Title");
            var start = DateTimeOffset.UtcNow;
            var stop = start.AddHours(2);

            var eventInstance = Event<string>.Create(activity, title, start, stop);

            // Act and Assert
            Assert.Throws<DomainException>(() => eventInstance.GetInstances(-1).ToList());
        }
    }
}
