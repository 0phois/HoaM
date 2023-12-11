using HoaM.Domain.Common;
using HoaM.Domain.Exceptions;
using MassTransit;

namespace HoaM.Domain
{
    public class ActionItem : AuditableEntity<ActionItemId>
    {
        /// <summary>
        /// Unique ID of the <see cref="ActionItem"/>
        /// </summary>
        public override ActionItemId Id { get; protected set; } = ActionItemId.From(NewId.Next().ToGuid());

        /// <summary>
        /// Contents of the <see cref="ActionItem"/>
        /// </summary>
        public Text Content { get; private set; } = null!;

        /// <summary>
        /// <see cref="AssociationMember"/> the <see cref="ActionItem"/> is assigned to
        /// </summary>
        public AssociationMemberId AssignedTo { get; private set; }

        /// <summary>
        /// If the action item is completed or not
        /// </summary>
        public bool IsCompleted { get; private set; }

        private ActionItem() { }

        public static ActionItem Create(Text content)
        {
            if (content is null) throw new DomainException(DomainErrors.Note.ContentNullOrEmpty);

            return new() { Content = content };
        }

        public void EditContent(Text content)
        {
            if (content is null) throw new DomainException(DomainErrors.Note.ContentNullOrEmpty);

            if (content == Content) return;

            Content = content;
        }

        public void Assign(AssociationMemberId assignee)
        {
            if (assignee == default) throw new DomainException(DomainErrors.AssociationMember.NullOrEmpty);

            AssignedTo = assignee;
        }

        public void Complete() => IsCompleted = true;
    }
}
