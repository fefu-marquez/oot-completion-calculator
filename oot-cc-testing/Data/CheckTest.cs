using oot_completion_calculator.Data;
using System.Collections.Generic;
namespace oot_cc_testing.Data
{
    public class CheckTest
    {
        [Fact]
        public void CanComplete_OneRequirement_ReturnTrue()
        {
            Check requirement = new() { Completed = true };
            Check check = new() { Requires = new() { new() { requirement } } };

            Assert.True(check.CanComplete());
        }

        [Fact]
        public void CanComplete_OneRequirement_ReturnFalse()
        {
            Check requirement = new() { Completed = false };
            Check check = new() { Requires = new() { new() { requirement } } };

            Assert.False(check.CanComplete());
        }

        [Fact]
        public void CanComplete_ComplexRequirement_ReturnTrue()
        {
            // Requirement is r1 AND (r2 OR r3)
            Check r1 = new() { Completed = true };
            Check r2 = new() { Completed = false };
            Check r3 = new() { Completed = true };
            Check check = new() { Requires = new() { new() { r1 }, new() { r2, r3 } } };

            Assert.True(check.CanComplete());
        }

        [Fact]
        public void CanComplete_ComplexRequirement_ReturnFalse()
        {
            // Requirement is r1 AND (r2 OR r3)
            Check r1 = new() { Completed = false };
            Check r2 = new() { Completed = false };
            Check r3 = new() { Completed = false };
            Check check = new() { Requires = new() { new() { r1 }, new() { r2, r3 } } };

            Assert.False(check.CanComplete());
        }

        [Fact]
        public void CanComplete_ComplexRequirementPartiallyMet_ReturnFalse()
        {
            // Requirement is r1 AND (r2 OR r3)
            Check r1 = new() { Completed = false };
            Check r2 = new() { Completed = true };
            Check r3 = new() { Completed = true };
            Check check = new() { Requires = new() { new() { r1 }, new() { r2, r3 } } };

            Assert.False(check.CanComplete());
        }

        [Fact]
        public void Complete_IgnoreRequirements_SetCompletedTrue()
        {
            // Requirement is r1 AND (r2 OR r3)
            Check r1 = new() { Completed = false };
            Check r2 = new() { Completed = false };
            Check r3 = new() { Completed = false };
            Check check = new() { Completed = false, Requires = new() { new() { r1 }, new() { r2, r3 } }, Completes = new() { } };

            check.Complete(false);

            Assert.True(check.Completed);
        }

        [Fact]
        public void Complete_MeetsRequirements_SetCompletedTrue()
        {
            // Requirement is r1 AND (r2 OR r3)
            Check r1 = new() { Completed = true };
            Check r2 = new() { Completed = false };
            Check r3 = new() { Completed = true };
            Check check = new() { Completed = false, Requires = new() { new() { r1 }, new() { r2, r3 } }, Completes = new() { } };

            check.Complete();

            Assert.True(check.Completed);
        }

        [Fact]
        public void Complete_DoesNotMeetsRequirements_NotSetCompleted()
        {
            // Requirement is r1 AND (r2 OR r3)
            Check r1 = new() { Completed = false };
            Check r2 = new() { Completed = false };
            Check r3 = new() { Completed = true };
            Check check = new() { Completed = false, Requires = new() { new() { r1 }, new() { r2, r3 } }, Completes = new() { } };

            check.Complete();

            Assert.False(check.Completed);
        }


        [Fact]
        public void Complete_ShouldCompleteChildrenIfRequirementsMet_SetAllToCompleted()
        {
            // Requirement is r1 AND (r2 OR r3)
            Check r1 = new() { Completed = true };
            Check r2 = new() { Completed = false };
            Check r3 = new() { Completed = true };
            Check child1 = new() { Completed = false, Requires = new() { }, Completes = new() { } };
            Check child2 = new() { Completed = false, Requires = new() { }, Completes = new() { } };
            Check check = new() { Completed = false, Requires = new() { new() { r1 }, new() { r2, r3 } }, Completes = new() { child1, child2 } };

            check.Complete();

            Assert.True(check.Completed);
            Assert.True(child1.Completed);
            Assert.True(child2.Completed);
        }

        [Fact]
        public void Complete_ShouldNotCompleteChildrenIfRequirementsNotMet_SetNothing()
        {
            // Requirement is r1 AND (r2 OR r3)
            Check r1 = new() { Completed = true };
            Check r2 = new() { Completed = false };
            Check r3 = new() { Completed = false};
            Check child1 = new() { Completed = false, Requires = new() { }, Completes = new() { } };
            Check child2 = new() { Completed = false, Requires = new() { }, Completes = new() { } };
            Check check = new() { Completed = false, Requires = new() { new() { r1 }, new() { r2, r3 } }, Completes = new() { child1, child2 } };

            check.Complete();

            Assert.False(check.Completed);
            Assert.False(child1.Completed);
            Assert.False(child2.Completed);
        }

        [Fact]
        public void Complete_ShouldCompleteChildrenIfRequirementsIgnored_SetAllToCompleted()
        {
            // Requirement is r1 AND (r2 OR r3)
            Check r1 = new() { Completed = true };
            Check r2 = new() { Completed = false };
            Check r3 = new() { Completed = false };
            Check child1 = new() { Completed = false, Requires = new() {  }, Completes = new() { } };
            Check child2 = new() { Completed = false, Requires = new() {  }, Completes = new() { } };
            Check check = new() { Completed = false, Requires = new() { new() { r1 }, new() { r2, r3 } }, Completes = new() { child1, child2 } };

            check.Complete(false);

            Assert.True(check.Completed);
            Assert.True(child1.Completed);
            Assert.True(child2.Completed);
        }
    }
}
