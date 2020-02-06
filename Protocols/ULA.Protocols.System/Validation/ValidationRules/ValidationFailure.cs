namespace YP.Toolkit.System.Validation.ValidationRules
{
    /// <summary>
    /// Represents a result of the validation.
    /// If the value is null it means that validation was succeded
    /// </summary>
    public class ValidationFailure
    {
        #region [Properties]
        /// <summary>
        /// Gets or sets the name of the property.
        /// </summary>
        public string PropertyName { get; private set; }
        /// <summary>
        /// Gets or sets the error message
        /// </summary>
        public string ErrorMessage { get; private set; } 
        #endregion [Properties]


        #region [Ctor's]
        /// <summary>
        /// Creaets an instance of <see cref="ValidationFailure"/>
        /// </summary>
        /// <param name="propertyName">The name of property that was failed while validation</param>
        /// <param name="errorMessage">The error message that is associated with current validation issue</param>
        internal ValidationFailure(string propertyName, string errorMessage)
        {
            this.PropertyName = propertyName;
            this.ErrorMessage = errorMessage;
        } 
        #endregion [Ctor's]


        #region [Override members]
        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>
        /// A string that represents the current object.
        /// </returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            return this.ErrorMessage;
        } 
        #endregion [Override members]
    }
}