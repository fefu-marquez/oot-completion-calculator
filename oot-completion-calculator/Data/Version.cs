namespace oot_completion_calculator.Data
{
    /// <summary>
    /// A series of flags to determine the version of the game
    /// one particular Goal is for.
    /// </summary>
    public enum Version: byte
    {
        NormalMode = 0b1,
        MasterQuest = 0b10,
        NormalMode3D = 0b100,
        MasterQuest3D = 0b1000
    }
}
