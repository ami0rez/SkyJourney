namespace SkyJourney.Infrastructure.Data.Models
{
    /// <summary>
    /// Containes shared properties between all models
    /// </summary>
    public class BaseEntity
    {
        /// <summary>
        /// Model Key
        /// </summary>
        public Guid Id { get; set; }
    }
}
