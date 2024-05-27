using System;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace oot_completion_calculator.Data
{
    public abstract class Goal
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageSrc { get; set; }
        // Use the Version enum to set this number
        public byte Versions { get; set; }
        /// <summary>
        /// A list of requirements that need to be met in order to complete this goal.
        /// </summary>
        /// <remarks>
        /// <para>
        /// A requirement is a list of goals.
        /// If any goals from the list are met, the requirement is met.
        /// All requirements need to be met in order to be able to complete this goal.
        /// </para>
        /// 
        /// <example>
        /// <para>
        /// Basically, this structure allows to use both AND and OR operations, in the following fashion:
        /// </para>
        /// 
        /// <code>
        /// (Goal1 OR Goal2 OR Goal3 OR ...) AND (GoalN OR GoalN+1 OR ...) AND ...
        /// </code>
        /// 
        /// <para>
        /// For example, to get inside the Deku Tree, we need to have the Deku Shield AND the Kokiri Sword.
        /// </para>
        /// <para>
        /// Requirements will look like this:
        /// </para>
        /// <code>
        /// EnterDekuTree = { { DekuShield }, { KokiriSword } }
        /// </code>
        /// 
        /// <para>
        /// A different example would be something like the Goron Tunic. To get it we can 
        /// either buy it (we need to have Adult's Wallet) OR we can get it from Darunia's
        /// son if we have the Bomb Bag.
        /// </para>
        /// <para>
        /// Requirements will look like this:
        /// </para>
        /// <code>
        /// GoronTunic = { { BombBag, AdultsWallet } }
        /// </code>
        /// </example>
        /// </remarks>
        public List<List<Goal>> Requires { get; set; }
        /// <summary>
        /// A list of goals that are completed when this goal is completed.
        /// </summary>
        public List<Goal> Completes { get; set; }

        public void AddVersion(Version version)
        {
            Versions += (byte)version;
        }

        public void RemoveVersion(Version version)
        {
            Versions -= (byte)version;
        }

        public bool IsVersion(Version version)
        {
            return IsVersions((byte)version);
        }

        public bool IsVersions(byte versions)
        {
            return (Versions & versions) == versions;
        }
    }
}
