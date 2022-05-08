using System;

namespace Library.Database
{
    public abstract class BasePersistenceModel
    {
        public DateTime CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public bool IsDeleted { get; set; }

        public void UpdateNow() => ModifiedOn = DateTime.UtcNow;

        public void Delete() => IsDeleted = true;
    }
}