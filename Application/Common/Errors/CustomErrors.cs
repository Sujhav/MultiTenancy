using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Errors
{
    public sealed record CustomErrors(string code, string message)
    {
        private static readonly string _recordNotFound = "RecordNotFound";
        private static readonly string _validationErrorCode = "ValdiationError";

        public static readonly CustomErrors none = new CustomErrors(string.Empty, string.Empty);


        public static CustomErrors RecordNotFound(string message) => new CustomErrors(_recordNotFound, message);
        public static CustomErrors ValidationError(string message) => new CustomErrors(_validationErrorCode, message);
    }
}
