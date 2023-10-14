using HoaM.Domain.Common;
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
        
        private Article() { }

        public static Article CreateBulletin(ArticleTitle title, Text body)
        {
            return new() { Title = title, Body = body, Type = ArticleType.Bulletin };
        }

        public static Article CreateAnnouncement(ArticleTitle title, Text body)
        {
            return new() { Title = title, Body = body, Type = ArticleType.Announcement };
        }

        public void EditTitle(ArticleTitle title)
        {
            if (title == Title) return;

            Title = title;
        }

        public void EditBody(Text body)
        {
            if (body == Body) return;

            Body = body;
        }

        public IResult Publish(IArticleManager articleManager)
        {
            if (PublishedDate != null) throw new InvalidOperationException("Article has already been published!");

            var publishResult = articleManager.PublishArticle(this);

            if (publishResult.IsSuccess) PublishedDate = articleManager.SystemClock.UtcNow;

            return publishResult;
        }
    }

    public enum ArticleType
    {
        Bulletin, //detailed
        Announcement //brief
    }
}
