using System.ComponentModel.DataAnnotations.Schema;

namespace PostScheduler.Infrastructure.Repository.Model
{
    [Table("scheduled_messages")]
    sealed class ScheduledMessage
    {
        [Column("id")]
        public Guid Id { get; set; }

        [Column("user_id")]
        public Guid UserId { get; set; }

        [Column("message")]
        public string? Message { get; set; }

        [Column("is_embedding_enabled")]
        public bool IsEmbeddingEnabled { get; set; }

        [Column("scheduled_posting_time")]
        public DateTime ScheduledPostingTime { get; set; }

        #region Posted message abstraction
        [Column("posted_at")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime? PostedAt { get; set; }

        [Column("posted_message_id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid? PostedMessageId { get; set; }
        #endregion

        [Column("created_at")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime CreatedAt { get; set; }

        [Column("updated_at")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime UpdatedAt { get; set; }

        internal Domain.Model.ScheduledMessage ToDomainModel()
        {
            return new Domain.Model.ScheduledMessage(
               Id,
               UserId,
               Message ?? string.Empty,
               IsEmbeddingEnabled,
               ScheduledPostingTime,
               PostedMessageId.HasValue
                   ? new Domain.Model.PostedMessageAbstraction { MessageId = PostedMessageId.Value, PostedAt = PostedAt!.Value }
                   : null,
               CreatedAt,
               UpdatedAt
               );
        }
    }
}
