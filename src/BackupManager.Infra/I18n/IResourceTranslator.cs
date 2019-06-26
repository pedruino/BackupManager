namespace BackupManager.Infra.I18n
{
    public interface IResourceTranslator
    {
        /// <summary>
        /// Gets the resourced value associated with the specified key.
        /// </summary>
        /// <param name="resourceKey">The key whose resource to get.</param>
        /// <param name="locale">The locale for resource (e.g. pt-BR).</param>
        /// <returns>The string resourced value.</returns>
        string Translate(string resourceKey, string locale);

        /// <summary>
        /// Gets the resourced value associated with the specified key based on default locale defined on derived class
        /// </summary>
        /// <param name="resourceKey">The key whose resource to get.</param>
        /// <returns>The string resourced value.</returns>
        string Translate(string resourceKey);
    }
}