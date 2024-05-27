namespace oot_completion_calculator.Data
{
    public class Check
    {
        public Goal Goal { get; set; }
        public bool Completed { get; set; }
        /// <summary>
        /// A list of requirements that need to be met in order to be able
        /// to complete this check. It replicates the structure from the Goal.
        /// </summary>
        public List<List<Check>> Requires { get; set; }
        /// <summary>
        /// A list of checks that complete when this check is completed.
        /// </summary>
        public List<Check> Completes { get; set; }

        /// <summary>
        /// Verifies if the Check can be completed.
        /// </summary>
        /// <returns>True if the check can be completed, false otherwise.</returns>
        public bool CanComplete()
        {
            foreach (List<Check> requirement in Requires)
            {
                bool requirementIsMet = requirement.Any(check => check.Completed);
                if (requirementIsMet)
                    continue;
                else
                    return false;
            }

            return true;
        }

        /// <summary>
        /// Sets this check as completed as well as its children.
        /// </summary>
        /// <param name="checkRequirements">If this is set to false, 
        /// requirements for completion are ignored.</param>
        public void Complete(bool checkRequirements = true)
        {
            if (!checkRequirements || CanComplete())
            {
                Completed = true;
                Completes.ForEach(check => check.Complete(false));
            }
        }
    }
}
