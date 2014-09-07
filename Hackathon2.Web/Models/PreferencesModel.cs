using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel;

namespace Hackathon2.Web.Models
{
    public class PreferencesModel
    {
        [DisplayName("Temperature (°F)")]
        public int? Temperature { get; set; }

        [DisplayName("Volume (db)")]
        public int? MusicVolume { get; set; }

        [DisplayName("Music Genre")]
        public string MusicGenre { get; set; }

        [DisplayName("Spotify Playlist")]
        public string FavouriteSpotifyList { get; set; }
    }
}