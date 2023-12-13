using HoaM.Domain.Exceptions;

namespace HoaM.Domain
{
    /// <summary>
    /// Represents a green space area within the <see cref="Community"/> class.
    /// </summary>
    public sealed class GreenSpace : Parcel
    {
        private const string Reserved = "Reserved Green Space";

        /// <summary>
        /// Gets or sets the title of the green space.
        /// </summary>
        public ParcelTitle Title { get; private set; } = ParcelTitle.From(Reserved);

        /// <summary>
        /// Gets or sets the description of the green space.
        /// </summary>
        public Note? Description { get; private set; }

        /// <summary>
        /// Gets or sets the opening hours of the green space.
        /// </summary>
        public OpeningHours OpeningHours { get; private set; } = OpeningHours.TwentyFourHours;

        /// <summary>
        /// Gets the collection of amenities associated with the green space.
        /// </summary>
        public IReadOnlyCollection<Text> Amenities => _amenities.AsReadOnly();
        private readonly List<Text> _amenities = [];

        /// <summary>
        /// Gets the collection of rules associated with the green space.
        /// </summary>
        public IReadOnlyCollection<Text> Rules => _rules.AsReadOnly();
        private readonly List<Text> _rules = [];

        /// <summary>
        /// Private parameterless constructor for entity creation.
        /// </summary>
        private GreenSpace() { }

        /// <summary>
        /// Creates a new <see cref="GreenSpace"/> with the specified development status and lots.
        /// </summary>
        /// <param name="status">The development status of the green space.</param>
        /// <param name="lots">The lots associated with the green space.</param>
        /// <returns>The created <see cref="GreenSpace"/> instance.</returns>
        public static GreenSpace Create(DevelopmentStatus status, params Lot[] lots)
        {
            if (lots is null || lots.Length == 0) throw new DomainException(DomainErrors.Lot.NullOrEmpty);

            if (!Enum.IsDefined(typeof(DevelopmentStatus), status)) throw new DomainException(DomainErrors.Parcel.StatusNotDefined);

            return new GreenSpace() { Status = status }.WithLots<GreenSpace>(lots);
        }

        /// <summary>
        /// Sets the title of the green space.
        /// </summary>
        /// <param name="title">The title to set.</param>
        /// <returns>The updated <see cref="GreenSpace"/> instance.</returns>
        public GreenSpace WithTitle(ParcelTitle title)
        {
            if (title is null) throw new DomainException(DomainErrors.GreenSpace.TitleNullOrEmpty);

            Title = title;

            return this;
        }

        /// <summary>
        /// Sets the description of the green space.
        /// </summary>
        /// <param name="description">The description to set.</param>
        /// <returns>The updated <see cref="GreenSpace"/> instance.</returns>
        public GreenSpace WithDescription(Note description)
        {
            if (description is null) throw new DomainException(DomainErrors.GreenSpace.DescriptionNullOrEmpty);

            Description = description;

            return this;
        }

        /// <summary>
        /// Sets the opening hours of the green space.
        /// </summary>
        /// <param name="openingHours">The opening hours to set.</param>
        /// <returns>The updated <see cref="GreenSpace"/> instance.</returns>
        public GreenSpace WithOpeningHours(OpeningHours openingHours)
        {
            if (openingHours is null) throw new DomainException(DomainErrors.GreenSpace.HoursNullOrEmpty);

            OpeningHours = openingHours;

            return this;
        }

        /// <summary>
        /// Adds a rule to the green space.
        /// </summary>
        /// <param name="rule">The rule to add.</param>
        /// <returns>The updated <see cref="GreenSpace"/> instance.</returns>
        public GreenSpace AddRule(Text rule)
        {
            if (rule is null) throw new DomainException(DomainErrors.GreenSpace.RuleNullOrEmpty);

            _rules.Add(rule);

            return this;
        }

        /// <summary>
        /// Sets the collection of rules for the green space.
        /// </summary>
        /// <param name="rules">The rules to set.</param>
        /// <returns>The updated <see cref="GreenSpace"/> instance.</returns>
        public GreenSpace WithRules(params Text[] rules)
        {
            if (rules is null || rules.Length == 0) throw new DomainException(DomainErrors.GreenSpace.RuleNullOrEmpty);

            _rules.Clear();
            _rules.AddRange(rules);

            return this;
        }

        /// <summary>
        /// Adds an amenity to the green space.
        /// </summary>
        /// <param name="amenity">The amenity to add.</param>
        /// <returns>The updated <see cref="GreenSpace"/> instance.</returns>
        public GreenSpace AddAmenity(Text amenity)
        {
            if (amenity is null) throw new DomainException(DomainErrors.GreenSpace.AmenityNullOrEmpty);

            _amenities.Add(amenity);

            return this;
        }

        /// <summary>
        /// Sets the collection of amenities for the green space.
        /// </summary>
        /// <param name="amenities">The amenities to set.</param>
        /// <returns>The updated <see cref="GreenSpace"/> instance.</returns>
        public GreenSpace WithAmenities(params Text[] amenities)
        {
            if (amenities is null || amenities.Length == 0) throw new DomainException(DomainErrors.GreenSpace.AmenityNullOrEmpty);

            _amenities.Clear();
            _amenities.AddRange(amenities);

            return this;
        }

        /// <summary>
        /// Removes all rules associated with the green space.
        /// </summary>
        public void RemoveRules() => _rules.Clear();

        /// <summary>
        /// Removes all amenities associated with the green space.
        /// </summary>
        public void RemoveAmenities() => _amenities.Clear();
    }

    /// <summary>
    /// Represents the opening hours of a green space.
    /// </summary>
    public record OpeningHours(TimeOnly OpeningTime, TimeOnly ClosingTime)
    {
        /// <summary>
        /// Gets the predefined instance representing twenty-four hours availability.
        /// </summary>
        public static OpeningHours TwentyFourHours => new(TimeOnly.MinValue, TimeOnly.MaxValue);
    };

}
