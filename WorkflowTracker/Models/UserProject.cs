//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WorkflowTracker.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class UserProject
    {
        public int UPID { get; set; }
        public string UserID { get; set; }
        public Nullable<int> ProjectID { get; set; }
    
        public virtual AspNetUser AspNetUser { get; set; }
        public virtual ProjectDetail ProjectDetail { get; set; }
    }
}
