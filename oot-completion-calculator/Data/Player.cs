using Microsoft.AspNetCore.Identity;

namespace oot_completion_calculator.Data
{
    public class Player : IdentityUser
    {
        public string Name { get; set; }
        public List<Save> Saves { get; set; }
    }

}
