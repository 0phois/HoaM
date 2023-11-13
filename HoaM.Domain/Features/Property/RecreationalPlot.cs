using HoaM.Domain.Exceptions;

namespace HoaM.Domain.Features
{
    public sealed class RecreationalPlot : CommunityPlot
    {
        private const string Reserved = "Reserved Green Space";

        /// <summary>
        /// Title of the <see cref="RecreationalPlot"/>
        /// </summary>
        public PlotTitle Title { get; private set; } = PlotTitle.From(Reserved);
        
        /// <summary>
        /// Description of the <see cref="RecreationalPlot"/>
        /// </summary>
        public Note? Description { get; private set; }

        /// <summary>
        /// Opening and closing times for the <see cref="RecreationalPlot"/>
        /// </summary>
        public OpeningHours OpeningHours { get; private set; } = OpeningHours.TwentyFourHours;
        
        /// <summary>
        /// Amenities available in the <see cref="RecreationalPlot"/>
        /// </summary>
        public IReadOnlyCollection<Text> Amenities => _amenities.AsReadOnly();
        private readonly List<Text> _amenities = new();

        /// <summary>
        /// Rules associated with this <see cref="ResidentialPlot"/>
        /// </summary>
        public IReadOnlyCollection<Text> Rules => _rules.AsReadOnly();
        private readonly List<Text> _rules = new();

        public static RecreationalPlot Create(Lot lot, DevelopmentStatus status)
        {
            if (lot is null) throw new DomainException(DomainErrors.Lot.NullOrEmpty);

            if (!Enum.IsDefined(typeof(DevelopmentStatus), status)) throw new DomainException(DomainErrors.CommunityPlot.StatusNotDefined);

            return new() { Lot = lot, Status = status };
        }

        public RecreationalPlot WithTitle(PlotTitle title)
        {
            if (title is null) throw new DomainException(DomainErrors.RecreationalPlot.TitleNullOrEmpty);

            Title = title;

            return this;
        }

        public RecreationalPlot WithDescription(Note description)
        {
            if (description is null) throw new DomainException(DomainErrors.RecreationalPlot.DescriptionNullOrEmpty);

            Description = description;
            
            return this;
        }

        public RecreationalPlot WithOpeningHours(OpeningHours openingHours)
        {
            if (OpeningHours is null) throw new DomainException(DomainErrors.RecreationalPlot.HoursNullOrEmpty);

            OpeningHours = openingHours;

            return this;
        }

        public RecreationalPlot AddRule(Text rule)
        {
            if (rule is null) throw new DomainException(DomainErrors.RecreationalPlot.RuleNullOrEmpty);

            _rules.Add(rule);

            return this;
        }

        public RecreationalPlot WithRules(params Text[] rules)
        {
            if (rules is null || rules.Length == 0) throw new DomainException(DomainErrors.RecreationalPlot.RuleNullOrEmpty);

            _rules.Clear();
            _rules.AddRange(rules);

            return this;
        }

        public RecreationalPlot AddAmenity(Text amenity)
        {
            if (amenity is null) throw new DomainException(DomainErrors.RecreationalPlot.AmenityNullOrEmpty);

            _amenities.Add(amenity);

            return this;
        }

        public RecreationalPlot WithAmenities(params Text[] amenities)
        {
            if (amenities is null || amenities.Length == 0) throw new DomainException(DomainErrors.RecreationalPlot.AmenityNullOrEmpty);

            _amenities.Clear();
            _amenities.AddRange(amenities);

            return this;
        }

        public void RemoveRules() => _rules.Clear();

        public void RemoveAmenities() => _amenities.Clear();
    }

    public record OpeningHours(TimeOnly OpeningTime, TimeOnly ClosingTime)
    {
        public static OpeningHours TwentyFourHours => new(TimeOnly.MinValue, TimeOnly.MaxValue);
    };
}
