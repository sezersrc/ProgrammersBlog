using System;


namespace ProgrammersBlog.Shared.Entities.Abstract
{
    public abstract class EntityBase
    {
        // base değerlerin diğer sınıflarda değiştirebilmek için Abstract  ekliyoruz . Override etmeye yarıyor . 

        public virtual int Id { get; set; }
        public virtual DateTime CreatedDate { get; set; } = DateTime.Now; // override CreatedDate {get;set;} = new Datetime(2020/01/01); Örnek
        public virtual DateTime ModifiedDate { get; set; } = DateTime.Now;
        public virtual bool IsDeleted { get; set; } = false;
        public virtual bool IsActive { get; set; } = true;
        public virtual string CreatedByName { get; set; } = "Admin"; // Default Değer Atama
        public virtual string ModifiedByName { get; set; } = "Admin";
        public virtual string Note { get; set; }
    }
}
