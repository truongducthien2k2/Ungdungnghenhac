//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace music.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class CATEGORY
    {
        public CATEGORY()
        {
            this.SONG = new HashSet<SONG>();
            this.VIDEO = new HashSet<VIDEO>();
        }
    
        public int id { get; set; }
        public string categoryName { get; set; }
        public string categoryImage { get; set; }
        public Nullable<int> topicId { get; set; }
    
        public virtual TOPIC TOPIC { get; set; }
        public virtual ICollection<SONG> SONG { get; set; }
        public virtual ICollection<VIDEO> VIDEO { get; set; }
    }
}
