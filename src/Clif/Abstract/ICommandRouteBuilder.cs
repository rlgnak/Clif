namespace Clif.Abstract
{
    /// <summary>
    /// A basic class for parsing commands
    /// </summary>
    public interface ICommandRouteBuilder
    {
        /// <summary>
        /// Parses a <see cref="CommandRoute"/> 
        /// </summary>
        /// <param name="commandRoute"></param>
        void ParseRoute(CommandRoute commandRoute);
    }
}