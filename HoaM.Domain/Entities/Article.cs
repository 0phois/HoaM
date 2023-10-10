using HoaM.Domain.Common;
using MassTransit;

namespace HoaM.Domain.Entities
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
        public ArticleType Type { get; set; }

        /// <summary>
        /// Title of the <see cref="Article"/>
        /// </summary>
        public required ArticleTitle Title { get; set; }

        /// <summary>
        /// Contents of the <see cref="Article"/>
        /// </summary>
        public required Text Body { get; set; }

        /// <summary>
        /// Date and time the <see cref="Article"/> was published
        /// </summary>
        public DateTimeOffset? PublishedDate { get; private set; }

        /// <summary>
        /// <see cref="AssociationMember"/> that authored the <seealso cref="Article"/>
        /// </summary>
        public required AssociationMember Author { get; set; }
    }

    public enum ArticleType
    {
        Bulletin, //detailed
        Announcement //brief
    }
}
