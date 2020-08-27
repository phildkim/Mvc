namespace MS.Model.View
{
    using System;
    using System.ComponentModel.DataAnnotations;
    public class BaseVM
    {
        [Key]
        public int? Id { get; set; }
        public DateTime UpdatedOn { get; set; } = DateTime.Now;
        public Guid UpdatedBy { get; set; } = Guid.NewGuid();
        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }
            if (obj is BaseVM)
            {
                return ((BaseVM)obj).Id == this.Id;
            }
            return false;
        }
        public override int GetHashCode()
        {
            return this.Id.GetHashCode();
        }
    }
}