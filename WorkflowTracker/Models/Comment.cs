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
    
    public partial class Comment
    {
        public int CommentID { get; set; }
        public string UserID { get; set; }
        public Nullable<int> StepID { get; set; }
        public System.DateTime CommentDate { get; set; }
        public string text { get; set; }
        public string attachment { get; set; }
    
        public virtual AspNetUser AspNetUser { get; set; }
        public virtual StepDetail StepDetail { get; set; }
    }
}
