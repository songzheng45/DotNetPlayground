using Microsoft.AspNet.Identity.EntityFramework;

namespace Users.Models
{
    public enum Cities
    {
        Beijing,
        Tokoy,
        Manila
    }

    public enum Countries
    {
        None,
        China,
        Japan,
        Philippines
    }

    public class AppUser : IdentityUser
    {
        public Cities City { get; set; }
        public Countries Country { get; set; }
        public string Nickname { get; set; }

        public Countries SetCountryFromCity(Cities city)
        {
            Countries c = Countries.None;
            switch (city)
            {
                case Cities.Beijing:
                    c = Countries.China;
                    break;
                case Cities.Tokoy:
                    c = Countries.Japan;
                    break;
                case Cities.Manila:
                    c = Countries.Philippines;
                    break;
                default:
                    c = Countries.None;
                    break;
            }
            return c;
        }
    }
}