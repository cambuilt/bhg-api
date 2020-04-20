using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace bhg.Models
{
    public class Form : Collection<FormField>
    {
        [Key]
        public Guid Id { get; set; }
        public const string Relation = "form";
        public const string EditRelation = "edit-form";
        public const string CreateRelation = "create-form";
        public const string QueryRelation = "query-form";
    }
}
