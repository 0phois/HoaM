using HoaM.Domain.Common;
using HoaM.Domain.Exceptions;
using MassTransit;

namespace HoaM.Domain.Features
{
    public class Article : AuditableEntity<ArticleId>
    {
        /// <summary>
        /// Unique ID of the <see cref="Article"/>
        /// </summary>
        public override ArticleId Id => ArticleId.From(NewId.Next().ToGuid());

        /// <summary>
        /// Indicates whether the <see cref="Article"/> is pinned
        /// </summary>
        public bool IsPinned { get; set; }

        /// <summary>
        /// Indicates the type of the <see cref="Article"/>
        /// </summary>
        public required ArticleType Type { get; init; }

        /// <summary>
        /// Title of the <see cref="Article"/>
        /// </summary>
        public ArticleTitle Title { get; private set; } = null!;

        /// <summary>
        /// Contents of the <see cref="Article"/>
        /// </summary>
        public Text Body { get; private set; } = null!;

        /// <summary>
        /// Date and time the <see cref="Article"/> was published
        /// </summary>
        public DateTimeOffset? PublishedDate { get; private set; }

        public bool IsPublished => PublishedDate is not null;

        private Article() { }

        public static Article CreateBulletin(ArticleTitle title, Text body)
        {
            if (title is null) throw new DomainException(DomainErrors.Article.TitleNullOrEmpty);
            
            if (body is null) throw new DomainException(DomainErrors.Article.BodyNullOrEmpty);

            return new() { Title = title, Body = body, Type = ArticleType.Bulletin };
        }

        public static Article CreateAnnouncement(ArticleTitle title, Text body)
        {
            if (title is null) throw new DomainException(DomainErrors.Article.TitleNullOrEmpty);

            if (body is null) throw new DomainException(DomainErrors.Article.BodyNullOrEmpty);

            return new() { Title = title, Body = body, Type = ArticleType.Announcement };
        }

        public void EditTitle(ArticleTitle title)
        {
            if (title is null) throw new DomainException(DomainErrors.Article.TitleNullOrEmpty);
            
            if (title == Title) return;

            Title = title;
        }

        public void EditBody(Text body)
        {
            if (body is null) throw new DomainException(DomainErrors.Article.BodyNullOrEmpty);
           
            if (body == Body) return;

            Body = body;
        }

        internal void Publish(DateTimeOffset datePublished) //TODO - publish with past date should fail (business logic)
        {
            if (PublishedDate != null) throw new DomainException(DomainErrors.Article.AlreadyPublished);

            if (datePublished == default) throw new DomainException(DomainErrors.Article.DateNullOrEmpty);

            PublishedDate = datePublished;
        }
    }

    public enum ArticleType
    {
        Bulletin, //detailed
        Announcement //brief
    }
}
