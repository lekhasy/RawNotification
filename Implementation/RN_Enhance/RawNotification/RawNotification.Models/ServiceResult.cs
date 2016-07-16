using System;
using System.Runtime.Serialization;

namespace RawNotification.Models
{
    // *** This Class only provides to you maximum 3 additional return values.
    // If you want more additional return value, let's create a new class.
    // Otherwise, this code may be became a "smell" one. So, don't do that. =)

    [DataContract]
    public class BaseServiceResult<T, T1, T2, T3> : BaseServiceResult<T, T1, T2>
    {
        [DataMember]
        public T3 DataT3 { get; set; }

        public BaseServiceResult( ResultStatusCodes statusCode , T data, T1 dataT1, T2 dataT2, T3 dataT3, string message = null) : base(statusCode, data, dataT1, dataT2, message)
        {
            DataT3 = dataT3;
        }

        [IgnoreDataMember]
        public new static readonly BaseServiceResult<T, T1, T2> InternalErrorResult = new BaseServiceResult<T, T1, T2>( ResultStatusCodes.UnknownError, default(T), default(T1), default(T2));
    }

    [DataContract]
    public class BaseServiceResult<T, T1, T2> : BaseServiceResult<T, T1>
    {
        [DataMember]
        public T2 DataT2 { get; set; }
        public BaseServiceResult(ResultStatusCodes statusCode, T data, T1 dataT1, T2 dataT2, string message = null) : base(statusCode , data, dataT1, message)
        {
            DataT2 = dataT2;
        }

        [IgnoreDataMember]
        public new static readonly BaseServiceResult<T, T1, T2> InternalErrorResult = new BaseServiceResult<T, T1, T2>( ResultStatusCodes.UnknownError, default(T), default(T1), default(T2));
    }

    [DataContract]
    public class BaseServiceResult<T, T1> : BaseServiceResult<T>
    {
        [DataMember]
        public T1 DataT1 { get; set; }


        public BaseServiceResult(ResultStatusCodes statusCode, T data, T1 dataT1, string message = null) : base(statusCode, data, message)
        {
            DataT1 = dataT1;
        }

        [IgnoreDataMember]
        public new static readonly BaseServiceResult<T, T1> InternalErrorResult = new BaseServiceResult<T, T1>( ResultStatusCodes.UnknownError, default(T), default(T1));
    }

    [DataContract]
    public class BaseServiceResult<T> : BaseServiceResult
    {
        [DataMember]
        public T Data { get; set; }

        public BaseServiceResult(ResultStatusCodes statusCode, T data, string message = null) : base(statusCode, message)
        {
            Data = data;
        }

        [IgnoreDataMember]
        public new static readonly BaseServiceResult<T> InternalErrorResult = new BaseServiceResult<T>(ResultStatusCodes.UnknownError, default(T));
    }

    [DataContract(Namespace = "RawNotification.Models", Name = "BaseServiceResult")]
    public class BaseServiceResult
    {
        [DataMember]
        public string Message { get; set; }
        [DataMember]
        public ResultStatusCodes StatusCode { get; set; }

        public BaseServiceResult(ResultStatusCodes statusCode, string message = null)
        {
            Message = message;
            StatusCode = statusCode;
        }

        [IgnoreDataMember]
        public static BaseServiceResult InternalErrorResult = new BaseServiceResult(ResultStatusCodes.InternalServerError);
    }

    public enum ResultStatusCodes
    {
        /// <summary>
        /// Request have been procesed successfully
        /// </summary>
        OK = 200,

        /// <summary>
        /// Request can't be process because some internal conflict
        /// </summary>

        /// <summary>
        /// User have not authenticated
        /// </summary>
        UnAuthorised = 401,

        /// <summary>
        /// Requested resourse not found
        /// </summary>
        NotFound = 404,

        /// <summary>
        /// The token that provided is wrong or expired
        /// </summary>
        InvalidToken = 498,

        /// <summary>
        /// An errror occurred in server while processing request
        /// </summary>
        InternalServerError = 500,

        /// <summary>
        /// The server is currently unavailable
        /// </summary>
        ServiceUnavailable = 503,

        /// <summary>
        /// An Undetermined error occurred while processing request
        /// </summary>
        UnknownError = 520,
    }
}
