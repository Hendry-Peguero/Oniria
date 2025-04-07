namespace Oniria.Infrastructure.Shared.Entities
{
    public class MailTemplate
    {
        public string Subject { get; set; }
        public string Body { get; set; }

        /// <summary>
        /// Inserts a URL into the email body using string formatting.
        /// </summary>
        /// <param name="url">The URL to be inserted into the email body.</param>
        /// <returns>The formatted email body with the provided URL.</returns>
        /// <remarks>
        /// This method uses <c>String.Format</c>, so the body must contain a valid placeholder (e.g., <c>{0}</c>).
        /// </remarks>
        public string InsertUrl(string url)
        {
            return String.Format(Body, url);
        }
    }
}
