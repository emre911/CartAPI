namespace CartAPI.Models
{
    /// <summary>
    /// Base response model
    /// </summary>
    public abstract class BaseResponse
    {
        /// <summary>
        /// Is the process successfull?
        /// </summary>
        public bool IsSucceeded { get; set; } = false;
        /// <summary>
        /// Business error messages
        /// </summary>
        public List<string>? Messages { get; set; }

        /// <summary>
        /// Base Response constructor
        /// </summary>
        public BaseResponse()
        {

        }

        /// <summary>
        /// Base Response constructor
        /// </summary>
        /// <param name="messages"></param>
        public BaseResponse(List<string> messages)
        {
            Messages = messages;
        }
    }
}
