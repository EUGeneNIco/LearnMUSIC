namespace LearnMUSIC.Entities.Base
{
    public abstract class EntityBase : IEntity, IAuditEntity
    {
        public long Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
    }
}
