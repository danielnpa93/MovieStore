namespace TinyMovieShared.API.Models.Entities
{
    public abstract class BaseEntity
    {
        public Guid Id { get; private protected set; }

        private protected List<string> _errors;
        
        public IReadOnlyList<string> Errors => _errors;
        protected abstract void Validate();

    }
}
