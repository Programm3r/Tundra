using System;
using GalaSoft.MvvmLight;

namespace Tundra.Implementation.Model
{
    public class PersonModel : ObservableObject
    {
        private int _age;
        private string _fullName;

        /// <summary>
        /// Initializes a new instance of the <see cref="PersonModel"/> class.
        /// </summary>
        public PersonModel()
        {
            Id = Guid.NewGuid();
        }

        /// <summary>
        ///     Gets or sets the age.
        /// </summary>
        /// <value>
        ///     The age.
        /// </value>
        public int Age
        {
            get
            {
                return _age;
            }
            set
            {
                base.Set(ref this._age, value);
            }
        }

        /// <summary>
        ///     Gets or sets the full name.
        /// </summary>
        /// <value>
        ///     The full name.
        /// </value>
        public string FullName
        {
            get
            {
                return _fullName;
            }
            set
            {
                base.Set(ref this._fullName, value);
            }
        }

        /// <summary>
        ///     Gets the identifier.
        /// </summary>
        /// <value>
        ///     The identifier.
        /// </value>
        public Guid Id { get; set; }
    }
}