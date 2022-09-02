namespace NUIXRooms
{
    /// <summary>
    /// Provides a solver that constrains the target to a region safe for hand constrained interactive content.
    /// </summary>
    public class HandConstraint : Solver
    {
        public override void SolverUpdate()
        {
            SolverHandler.RefreshTrackedObject();
        }


    }

}



