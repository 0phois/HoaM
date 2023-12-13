using HoaM.Domain.Common;
using HoaM.Domain.Exceptions;
using MassTransit;

namespace HoaM.Domain
{
    /// <summary>
    /// Represents an article entity with an identifier of type <see cref="ArticleId"/>.
    /// Inherits from <see cref="AuditableEntity{ArticleId}"/> and implements methods for creating, editing, and publishing articles.
    /// </summary>
    public class Article : AuditableEntity<ArticleId>
    {
        /// <summary>
        /// Gets or sets the unique identifier for the article.
        /// </summary>
        public override ArticleId Id { get; protected set; } = ArticleId.From(NewId.Next().ToGuid());

        /// <summary>
        /// Gets or sets a value indicating whether the article is pinned.
        /// </summary>
        public bool IsPinned { get; set; }

        /// <summary>
        /// Gets the type of the article.
        /// </summary>
        public ArticleType Type { get; init; }

        /// <summary>
        /// Gets or sets the title of the article.
        /// </summary>
        public ArticleTitle Title { get; private set; } = null!;

        /// <summary>
        /// Gets or sets the contents of the article.
        /// </summary>
        public Text Body { get; private set; } = null!;

        /// <summary>
        /// Gets or sets the date and time the article was published.
        /// </summary>
        public DateTimeOffset? PublishedDate { get; private set; }

        /// <summary>
        /// Gets a value indicating whether the article is published.
        /// </summary>
        public bool IsPublished => PublishedDate is not null;

        /// <summary>
        /// Private constructor to prevent direct instantiation of the article without using the creation methods.
        /// </summary>
        private Article() { }

        /// <summary>
        /// Creates a new bulletin article with the specified title and body.
        /// </summary>
        /// <param name="title">The title of the article.</param>
        /// <param name="body">The body of the article.</param>
        /// <returns>A new instance of the <see cref="Article"/> class with type <see cref="ArticleType.Bulletin"/>.</returns>
        public static Article CreateBulletin(ArticleTitle title, Text body)
        {
            if (title is null) throw new DomainException(DomainErrors.Article.TitleNullOrEmpty);
            if (body is null) throw new DomainException(DomainErrors.Article.BodyNullOrEmpty);

            return new Article { Title = title, Body = body, Type = ArticleType.Bulletin };
        }

        /// <summary>
        /// Creates a new announcement article with the specified title and body.
        /// </summary>
        /// <param name="title">The title of the article.</param>
        /// <param name="body">The body of the article.</param>
        /// <returns>A new instance of the <see cref="Article"/> class with type <see cref="ArticleType.Announcement"/>.</returns>
        public static Article CreateAnnouncement(ArticleTitle title, Text body)
        {
            if (title is null) throw new DomainException(DomainErrors.Article.TitleNullOrEmpty);
            if (body is null) throw new DomainException(DomainErrors.Article.BodyNullOrEmpty);

            return new Article { Title = title, Body = body, Type = ArticleType.Announcement };
        }

        /// <summary>
        /// Edits the title of the article with the specified title.
        /// </summary>
        /// <param name="title">The new title of the article.</param>
        public void EditTitle(ArticleTitle title)
        {
            if (title is null) throw new DomainException(DomainErrors.Article.TitleNullOrEmpty);

            if (title == Title) return;

            Title = title;
        }

        /// <summary>
        /// Edits the body of the article with the specified body.
        /// </summary>
        /// <param name="body">The new body of the article.</param>
        public void EditBody(Text body)
        {
            if (body is null) throw new DomainException(DomainErrors.Article.BodyNullOrEmpty);

            if (body == Body) return;

            Body = body;
        }

        /// <summary>
        /// Publishes the article with the specified publication date.
        /// </summary>
        /// <param name="datePublished">The date and time the article is published.</param>
        internal void Publish(DateTimeOffset datePublished)
        {
            if (PublishedDate != null) throw new DomainException(DomainErrors.Article.AlreadyPublished);

            if (datePublished == default) throw new DomainException(DomainErrors.Article.DateNullOrEmpty);

            PublishedDate = datePublished;
        }
    }

    /// <summary>
    /// Enum representing the type of an article.
    /// </summary>
    public enum ArticleType
    {
        Bulletin,       // Detailed
        Announcement    // Brief
    }

}
