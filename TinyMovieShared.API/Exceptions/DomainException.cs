namespace TinyMovieShared.API.Exceptions
{
    public class DomainException : Exception
    {

        private List<string> _errors;
        public List<string> Errors => _errors;

        public DomainException()
        {
        }

        public DomainException(string message, List<string> errors) : base(message)
        {
            _errors = errors;
        }

        public DomainException(string message) : base(message)
        {
        }

        public DomainException(string messagem, Exception exception) : base(messagem, exception)
        {
        }
    }
}
