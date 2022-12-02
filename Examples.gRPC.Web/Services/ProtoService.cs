using System;
using System.Text;
using System.Threading.Tasks;
using Examples.gRPC.ProtoContracts;
using Examples.gRPC.Web.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Examples.gRPC.Web.Services
{
    public class ProtoService : IAmAProtoServiceContract
    {
        #region Fields

        private const string METHOD_CALLED_LOG_FORMAT = "Called endpoint '{OperationName}'.";
        private const string METHOD_THREW_EX_LOG_FORMAT = "Call to endpoint '{OperationName}' threw an exception.";
        private const string METHOD_EXITED_LOG_FORMAT = "Exited endpoint '{OperationName}'.";

        private readonly MyApplicationConfiguration _config;
        private readonly ILogger _logger;

        #endregion Fields

        #region Constructors

        public ProtoService(IOptions<MyApplicationConfiguration> options, ILogger<ProtoService> logger)
        {
            _config = options.Value;
            _logger = logger;
        }

        #endregion Constructors

        #region Public Methods

        public async ValueTask<GetRandomIntegerInRangeResponse> GetRandomIntegerInRange(GetRandomIntegerInRangeRequest request)
        {
            var response = new GetRandomIntegerInRangeResponse();

            using (var scope = _logger.BeginScope(METHOD_CALLED_LOG_FORMAT, nameof(GetRandomIntegerInRange)))
            {
                try
                {
                    response.RandomValue = Random.Shared.Next(request.InclusiveMinimum, request.InclusiveMaximum + 1);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, METHOD_THREW_EX_LOG_FORMAT, nameof(GetRandomIntegerInRange));
                }
                finally
                {
                    _logger.LogTrace(METHOD_EXITED_LOG_FORMAT, nameof(GetRandomIntegerInRange));
                }
            }

            return await ValueTask.FromResult(response);
        }

        public async ValueTask<MyObjectProto> GetAnObject()
        {
            MyObjectProto response;

            using (var scope = _logger.BeginScope(METHOD_CALLED_LOG_FORMAT, nameof(GetAnObject)))
            {
                try
                {
                    response = new MyObjectProto
                    {
                        SomeValue = Random.Shared.Next(_config.MinimumRandomRange, _config.MaximumRandomRange),
                        SomeString = DateTime.UtcNow.ToString(),
                        SomeSerializedObject = Encoding.UTF8.GetBytes(_config.MyJsonObject ?? "null")
                    };
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, METHOD_THREW_EX_LOG_FORMAT, nameof(GetAnObject));

                    response = new MyObjectProto();
                }
                finally
                {
                    _logger.LogTrace(METHOD_EXITED_LOG_FORMAT, nameof(GetAnObject));
                }
            }

            return await ValueTask.FromResult(response);
        }

        #endregion Public Methods
    }
}
