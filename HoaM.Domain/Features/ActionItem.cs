using HoaM.Domain.Common;
using HoaM.Domain.Exceptions;
using MassTransit;

namespace HoaM.Domain
{
    /// <summary>
    /// Represents an action item entity with an identifier of type <see cref="ActionItemId"/>.
    /// Inherits from <see cref="AuditableEntity{ActionItemId}"/> and implements methods for creating, editing, assigning, and completing action items.
    /// </summary>
    public class ActionItem : AuditableEntity<ActionItemId>
    {
        /// <summary>
        /// Gets or sets the unique identifier for the action item.
        /// </summary>
        public override ActionItemId Id { get; protected set; } = ActionItemId.From(NewId.Next().ToGuid());

        /// <summary>
        /// Gets or sets the contents of the action item.
        /// </summary>
        public Text Content { get; private set; } = null!;

        /// <summary>
        /// Gets or sets the identifier of the association member to whom the action item is assigned.
        /// </summary>
        public AssociationMemberId AssignedTo { get; private set; }

        /// <summary>
        /// Gets or sets a value indicating whether the action item is completed.
        /// </summary>
        public bool IsCompleted { get; private set; }

        /// <summary>
        /// Private constructor to prevent direct instantiation of the action item without using the creation method.
        /// </summary>
        private ActionItem() { }

        /// <summary>
        /// Creates a new action item with the specified content.
        /// </summary>
        /// <param name="content">The content of the action item.</param>
        /// <returns>A new instance of the <see cref="ActionItem"/> class.</returns>
        public static ActionItem Create(Text content)
        {
            if (content is null) throw new DomainException(DomainErrors.Note.ContentNullOrEmpty);

            return new ActionItem { Content = content };
        }

        /// <summary>
        /// Edits the content of the action item with the specified content.
        /// </summary>
        /// <param name="content">The new content of the action item.</param>
        public void EditContent(Text content)
        {
            if (content is null) throw new DomainException(DomainErrors.Note.ContentNullOrEmpty);

            if (content == Content) return;

            Content = content;
        }

        /// <summary>
        /// Assigns the action item to the specified association member.
        /// </summary>
        /// <param name="assignee">The identifier of the association member to whom the action item is assigned.</param>
        public void Assign(AssociationMemberId assignee)
        {
            if (assignee == default) throw new DomainException(DomainErrors.AssociationMember.NullOrEmpty);

            AssignedTo = assignee;
        }

        /// <summary>
        /// Marks the action item as completed.
        /// </summary>
        public void Complete() => IsCompleted = true;
    }

}
