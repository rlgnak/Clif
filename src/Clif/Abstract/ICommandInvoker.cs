namespace Clif.Abstract
{
    /// <summary>
    /// A basic class used for invoking commands
    /// </summary>
    public interface ICommandInvoker
    {
        /// <summary>
        /// Invokes a <see cref="Command"/> given a <see cref="CommandResult"/>
        /// </summary>
        /// <param name="commandResult"></param>
        /// <param name="command"></param>
        void Invoke(CommandResult commandResult, Command command);
    }
}