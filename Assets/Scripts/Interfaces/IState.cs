namespace BlackFox
{
    public interface IState
    {
        /// <summary>
        /// Metodo d'ingresso della state machine
        /// </summary>
        void OnStart();

        /// <summary>
        /// Metodo di uscita della state machine
        /// </summary>
        void OnEnd();
    }
}
