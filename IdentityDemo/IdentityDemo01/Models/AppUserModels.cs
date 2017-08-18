using Microsoft.AspNet.Identity.EntityFramework;

namespace Users.Models
{
    public enum Cities
    {
        北京,
        东京,
        马尼拉
    }

    public enum Countries
    {
        无,
        中国,
        日本,
        菲律宾
    }

    public class AppUser : IdentityUser
    {
        public Cities City { get; set; }
        public Countries Country { get; set; }

        public Countries GetCountryFromCity(Cities city)
        {
            Countries c = Countries.无;
            switch (city)
            {
                case Cities.北京:
                    c = Countries.中国;
                    break;
                case Cities.东京:
                    c = Countries.日本;
                    break;
                case Cities.马尼拉:
                    c = Countries.菲律宾;
                    break;
                default:
                    c = Countries.无;
                    break;
            }
            return c;
        }
    }
}