using Microsoft.VisualBasic;
using System.ComponentModel.DataAnnotations;

namespace DataTransfer
{
    public class Game
    {
        public int GameID { get; set; }

        [Display(Name = "Date")]
        public DateTime GameDay { get; set; }

        [Display(Name = "Format")]
        public int GameFormat { get; set; }

        [Display(Name = "Theme")]
        public string? GameTheme { get; set; }

        [Display(Name = "Location")]
        public string? GameLocation { get; set; }

        [Display(Name = "Trivia Master First Name")]
        public string? MasterFirstName { get; set; }

        [Display(Name = "Trivia Master Last Name")]
        public string MasterLastName { get; set; } = string.Empty;

        [Display(Name = "Code")]
        public string? GameCode { get; set; }
    }
}
