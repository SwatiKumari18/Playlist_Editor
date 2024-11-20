using System;
using System.ComponentModel.DataAnnotations;

namespace SK2247A3.ViewModels
{
    public class TrackBaseViewModel
    {
        [Key]
        public int TrackId { get; set; }

        [Required]
        [StringLength(200)]
        public string Name { get; set; }

        [StringLength(220)]
        public string Composer { get; set; }

        [Required]
        public int Milliseconds { get; set; }

        [Required]
        public decimal UnitPrice { get; set; }

        // Composed read-only property to display the full name with details
        public string NameFull
        {
            get
            {
                // Calculate track time in minutes
                var ms = Math.Round((((double)Milliseconds / 1000) / 60), 1);

                // Format composer if present
                var composer = string.IsNullOrEmpty(Composer) ? "" : $", composer {Composer}";

                // Format track length if greater than 0
                var trackLength = (ms > 0) ? $", {ms} minutes" : "";

                // Format unit price if greater than 0
                var unitPrice = (UnitPrice > 0) ? $", $ {UnitPrice}" : "";

                // Combine all parts
                return $"{Name}{composer}{trackLength}{unitPrice}";
            }
        }

        // Composed read-only property to display a shorter name with essential details
        public string NameShort
        {
            get
            {
                // Calculate track time in minutes
                var ms = Math.Round((((double)Milliseconds / 1000) / 60), 1);

                // Format track length if greater than 0
                var trackLength = (ms > 0) ? $"{ms} minutes" : "";

                // Format unit price if greater than 0
                var unitPrice = (UnitPrice > 0) ? $" $ {UnitPrice}" : "";

                // Combine essential parts for a short display
                //Console.WriteLine($"{Name} - {trackLength} - {unitPrice}");
                return $"{Name} - {trackLength} - {unitPrice}";
            }
        }
    }
}
