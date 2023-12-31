﻿using HoaM.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Text.Json;

namespace HoaM.Infrastructure.Data
{
    internal sealed class EventConfiguration : IEntityTypeConfiguration<Event>
    {
        public void Configure(EntityTypeBuilder<Event> builder)
        {
            builder.ToTable("Events");

            builder.HasKey(@event => @event.Id);

            builder.Property(@event => @event.Title).IsRequired();

            builder.OwnsOne(@event => @event.Occurrence, occurrence =>
            {
                occurrence.Property(o => o.Start).IsRequired();
                occurrence.Property(o => o.Stop).IsRequired();
            });

            builder.OwnsOne(@event => @event.Schedule, schedule =>
            {
                schedule.Property(s => s.Interval);
                schedule.Property(s => s.EndsAt);
            });

            builder.HasQueryFilter(@event => @event.DeletionDate == null);
        }
    }

    internal sealed class EventConfiguration<T> : IEntityTypeConfiguration<Event<T>>
    {
        public void Configure(EntityTypeBuilder<Event<T>> builder)
        {
            builder.ToTable("Events");

            builder.Property(@event => @event.Title).IsRequired();

            builder.Property(@event => @event.Data).IsRequired()
                   .HasConversion(TValue => JsonSerializer.Serialize(TValue, (JsonSerializerOptions?)null),
                                  jsonString => JsonSerializer.Deserialize<T>(jsonString, (JsonSerializerOptions?)null)!);

            builder.OwnsOne(@event => @event.Occurrence, occurrence =>
            {
                occurrence.Property(o => o.Start).IsRequired();
                occurrence.Property(o => o.Stop).IsRequired();
            });

            builder.OwnsOne(@event => @event.Schedule, schedule =>
            {
                schedule.Property(s => s.Interval);
                schedule.Property(s => s.EndsAt);
            });

            builder.HasQueryFilter(@event => @event.DeletionDate == null);
        }
    }

    internal sealed class PeriodicMeetingConfiguration : IEntityTypeConfiguration<PeriodicMeeting>
    {
        public void Configure(EntityTypeBuilder<PeriodicMeeting> builder)
        {
            var jsonOptions = new JsonSerializerOptions();

            builder.ToTable("Events");

            builder.Property(@event => @event.Title).IsRequired();

            builder.Property(@event => @event.Data).IsRequired()
                   .HasConversion(TValue => JsonSerializer.Serialize(TValue, jsonOptions),
                                  jsonString => JsonSerializer.Deserialize<Meeting>(jsonString, jsonOptions)!);

            builder.OwnsOne(@event => @event.Occurrence, occurrence =>
            {
                occurrence.Property(o => o.Start).IsRequired();
                occurrence.Property(o => o.Stop).IsRequired();
            });

            builder.OwnsOne(@event => @event.Schedule, schedule =>
            {
                schedule.Property(s => s.Interval);
                schedule.Property(s => s.EndsAt);
            });
        }
    }
}
