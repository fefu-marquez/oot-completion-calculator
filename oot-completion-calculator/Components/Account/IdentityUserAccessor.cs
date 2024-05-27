using Microsoft.AspNetCore.Identity;
using oot_completion_calculator.Data;

namespace oot_completion_calculator.Components.Account
{
    internal sealed class IdentityUserAccessor(UserManager<Player> userManager, IdentityRedirectManager redirectManager)
    {
        public async Task<Player> GetRequiredUserAsync(HttpContext context)
        {
            var user = await userManager.GetUserAsync(context.User);

            if (user is null)
            {
                redirectManager.RedirectToWithStatus("Account/InvalidUser", $"Error: Unable to load user with ID '{userManager.GetUserId(context.User)}'.", context);
            }

            return user;
        }
    }
}
