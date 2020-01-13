using System;
using System.Collections.Generic;
using System.Text;
using superdigital.conta.model.MetaErrors;

namespace superdigital.conta.model.Results
{
    public class Result
    {
        /// <summary>
        /// Return true if everything went well, otherwise, false.
        /// </summary>
        public bool Success
        {
            get { return MetaError == null; }
        }

        /// <summary>
        /// Loaded only if an error has occurred.
        /// </summary>
        public MetaError MetaError { get; set; }
    }

    /// <summary>
    /// Pattern Result for business services with an object.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Result<T> : Result
    {
        private T _obj;

        /// <summary>
        /// Result data.
        /// </summary>
        public T Obj
        {
            get { return _obj; }
            set
            {
                if (value != null)
                {
                    MetaError = null;
                }

                _obj = value;
            }
        }
    }
}
