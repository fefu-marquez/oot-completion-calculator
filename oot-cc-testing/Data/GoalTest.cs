using oot_completion_calculator.Data;
using Xunit;

namespace oot_cc_testing.Data
{
    internal class FakeGoal : Goal
    {
    }

    public class GoalTest
    {
        [Fact]
        public void AddVersion_NoVersionAddNormalMode_VersionShouldBeSetTo1()
        {
            // Fake goal with no version
            FakeGoal goal = new() { Versions = 0 };

            // Add version
            goal.AddVersion(Version.NormalMode);

            // Check version
            Assert.Equal((byte)Version.NormalMode, goal.Versions);
        }

        [Fact]
        public void AddVersion_NormalModeAddNormalMode3D_VersionShouldBeSetTo5()
        {
            // Fake goal with normal mode
            FakeGoal goal = new() { Versions = (byte)Version.NormalMode };

            // Add version
            goal.AddVersion(Version.NormalMode3D);

            // Check version
            // 0b1 + 0b100 = 0b101 = 5
            Assert.Equal(5, goal.Versions);
            Assert.Equal((byte)Version.NormalMode + (byte)Version.NormalMode3D, goal.Versions);
        }

        [Fact]
        public void AddVersion_NormalModeAndNormalMode3DRemoveNormalMode_VersionShouldBeSetTo4()
        {
            // Goal with NormalMode and NormalMode3D
            FakeGoal goal = new() { Versions = 5 };

            // Remove version
            goal.RemoveVersion(Version.NormalMode);

            // Check version
            Assert.Equal((byte)Version.NormalMode3D, goal.Versions);
        }

        [Fact]
        public void IsVersion_NormalModeAndNormalMode3DIsNormalMode_ReturnTrue()
        {
            // Goal with NormalMode and NormalMode3D
            FakeGoal goal = new() { Versions = 5 };
            bool isNormalMode = goal.IsVersion(Version.NormalMode);
            Assert.True(isNormalMode);
        }

        [Fact]
        public void IsVersion_NormalModeAndNormalMode3DIsMasterQuest_ReturnFalse()
        {
            // Goal with NormalMode and NormalMode3D
            FakeGoal goal = new() { Versions = 5 };
            bool isMasterQuest = goal.IsVersion(Version.MasterQuest);
            Assert.False(isMasterQuest);
        }

        [Fact]
        public void IsVersions_NormalModeAndNormalMode3DIsBoth_ReturnTrue()
        {
            // Goal with NormalMode and NormalMode3D
            FakeGoal goal = new() { Versions = 5 };
            bool isBoth = goal.IsVersions((byte)Version.NormalMode + (byte)Version.NormalMode3D);
            Assert.True(isBoth);
        }

        [Fact]
        public void IsVersions_NormalModeAndNormalMode3DIsTwoButOnlyOneIsCorrect_ReturnFalse()
        {
            // Goal with NormalMode and NormalMode3D
            FakeGoal goal = new() { Versions = 5 };
            bool isOnlyOne = goal.IsVersions((byte)Version.NormalMode + (byte)Version.MasterQuest);
            Assert.False(isOnlyOne);
        }
    }
}
