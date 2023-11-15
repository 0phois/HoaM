using HoaM.Domain.Exceptions;

namespace HoaM.Domain.Features
{
    /// <summary>
    /// Details of a recreational space within the community/neighborhood
    /// </summary>
    public sealed class GreenSpace : Parcel
    {
        private const string Reserved = "Reserved Green Space";

        /// <summary>
        /// Title of the <see cref="GreenSpace"/>
        /// </summary>
        public ParcelTitle Title { get; private set; } = ParcelTitle.From(Reserved);
        
        /// <summary>
        /// Description of the <see cref="GreenSpace"/>
        /// </summary>
        public Note? Description { get; private set; }

        /// <summary>
        /// Opening and closing times for the <see cref="GreenSpace"/>
        /// </summary>
        public OpeningHours OpeningHours { get; private set; } = OpeningHours.TwentyFourHours;
        
        /// <summary>
        /// Amenities available in the <see cref="GreenSpace"/>
        /// </summary>
        public IReadOnlyCollection<Text> Amenities => _amenities.AsReadOnly();
        private readonly List<Text> _amenities = new();

        /// <summary>
        /// Rules associated with this <see cref="Residence"/>
        /// </summary>
        public IReadOnlyCollection<Text> Rules => _rules.AsReadOnly();
        private readonly List<Text> _rules = new();

        public static GreenSpace Create(DevelopmentStatus status, params Lot[] lots)
        {
            if (lots is null || lots.Length == 0) throw new DomainException(DomainErrors.Lot.NullOrEmpty);

            if (!Enum.IsDefined(typeof(DevelopmentStatus), status)) throw new DomainException(DomainErrors.Parcel.StatusNotDefined);

            return new GreenSpace() { Status = status }.WithLots<GreenSpace>(lots);
        }

        public GreenSpace WithTitle(ParcelTitle title)
        {
            if (title is null) throw new DomainException(DomainErrors.GreenSpace.TitleNullOrEmpty);

            Title = title;

            return this;
        }

        public GreenSpace WithDescription(Note description)
        {
            if (description is null) throw new DomainException(DomainErrors.GreenSpace.DescriptionNullOrEmpty);

            Description = description;
            
            return this;
        }

        public GreenSpace WithOpeningHours(OpeningHours openingHours)
        {
            if (OpeningHours is null) throw new DomainException(DomainErrors.GreenSpace.HoursNullOrEmpty);

            OpeningHours = openingHours;

            return this;
        }

        public GreenSpace AddRule(Text rule)
        {
            if (rule is null) throw new DomainException(DomainErrors.GreenSpace.RuleNullOrEmpty);

            _rules.Add(rule);

            return this;
        }

        public GreenSpace WithRules(params Text[] rules)
        {
            if (rules is null || rules.Length == 0) throw new DomainException(DomainErrors.GreenSpace.RuleNullOrEmpty);

            _rules.Clear();
            _rules.AddRange(rules);

            return this;
        }

        public GreenSpace AddAmenity(Text amenity)
        {
            if (amenity is null) throw new DomainException(DomainErrors.GreenSpace.AmenityNullOrEmpty);

            _amenities.Add(amenity);

            return this;
        }

        public GreenSpace WithAmenities(params Text[] amenities)
        {
            if (amenities is null || amenities.Length == 0) throw new DomainException(DomainErrors.GreenSpace.AmenityNullOrEmpty);

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
