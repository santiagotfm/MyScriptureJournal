using System;
using System.ComponentModel.DataAnnotations;
namespace MyScriptureJournal.Models

{
    public class Scripture
    {
        public int ID { get; set; }

        [Display(Name = "Entry Date"), DataType(DataType.Date)]
        public DateTime EntryDate { get; set; }

        [StringLength(20, MinimumLength = 3)]
        public string Book { get; set; }

        [Range(1, 200)]
        public int Chapter { get; set; }

        [Range(1, 2200)]
        public int Verse { get; set; }

        [StringLength(100, MinimumLength = 3)]
        public string Note { get; set; }
    }
}
