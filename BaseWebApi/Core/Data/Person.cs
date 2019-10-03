namespace prioritizemeServices.Core.Data
{
    /// <summary>
    /// Represents a single person
    /// </summary>
    public class Person
    {
        /// <summary>
        /// The ID of this person
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The name of this person
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The age of this person
        /// </summary>
        public int Age { get; set; }
    }
}
