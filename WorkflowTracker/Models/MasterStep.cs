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
    
    public partial class MasterStep
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public MasterStep()
        {
            this.StepDetails = new HashSet<StepDetail>();
        }
    
        public int MasterID { get; set; }
        public string MasterStepName { get; set; }
        public Nullable<bool> StepStatus { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StepDetail> StepDetails { get; set; }
    }
}