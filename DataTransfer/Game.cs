using Microsoft.VisualBasic;
using System.ComponentModel.DataAnnotations;

namespace DataTransfer
{
    public class Game
    {
        public int GameID { get; set; }
        public DateTime GameDay { get; set; }
        public int GameFormat { get; set; }
        public string? GameTheme { get; set; }
        public string? GameLocation { get; set; }
        public string? MasterFirstName { get; set; }
        public string MasterLastName { get; set; } = string.Empty;
        public string? GameCode { get; set; }
    }
}
