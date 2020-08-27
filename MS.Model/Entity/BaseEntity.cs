using System;
using System.ComponentModel.DataAnnotations;
namespace MS.Model.Entity
{
    public class BaseEntity
    {
        [Key]
        public int Id { get; set; }
        public DateTime UpdatedOn { get; protected set; } = DateTime.Now;
        public Guid UpdatedBy { get; protected set; } = Guid.NewGuid();
        public virtual bool IsTransient()
        {
            return this.Id == 0;
        }
        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }
            if (obj is BaseEntity)
            {
                return ((BaseEntity)obj).Id == this.Id;
            }
            return false;
        }
        public override int GetHashCode()
        {
            return this.Id.GetHashCode();
        }
    }
}