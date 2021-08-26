using Api.Domain.Entities;
using Api.Domain.Interfaces.Services.Log;
using Microsoft.Extensions.Logging;
using Serilog.Context;
using System;
using System.Threading.Tasks;

namespace Api.Service.Services
{
    public class LoggerService : ILoggerService
    {
        private const string applicationName = "ApplicationName";
        private const string logMessagem = "LogMessagem";
        private const string innerMessagem = "InnerMessagem";
        private const string stacktrace = "Stacktrace";
        private const string date = "Date";

        private readonly ILogger<LoggerService> _logger;

        public LoggerService(ILogger<LoggerService> logger) {
            _logger = logger;
        }

        public async Task LogError(LoggerRequestEntity request)
        {
            var dateExec = DateTime.UtcNow;
            using (LogContext.PushProperty(applicationName, request.ApplicationName))
            using (LogContext.PushProperty(logMessagem, request.Message))
            using (LogContext.PushProperty(innerMessagem, request.InnerMessage))
            using (LogContext.PushProperty(stacktrace, request.Stacktrace))
            using (LogContext.PushProperty(date, dateExec)) 
            { 
                await Task.Run(() => _logger.LogError($"Log Level: Error " +
                    $"ApplicationName: {request.ApplicationName} " +
                    $"LogMessage: {request.Message} " +
                    $"Date: {dateExec} " + 
                    $"InnerMessagem: {request.Message} " +
                    $"Stacktrace: {request.Stacktrace}"));
            }
        }

        public async Task LogInformation(LoggerRequestEntity request)
        {
            var dateExec = DateTime.UtcNow;
            using (LogContext.PushProperty(applicationName, request.ApplicationName))
            using (LogContext.PushProperty(logMessagem, request.Message))
            using (LogContext.PushProperty(date, dateExec))
            {
                await Task.Run(() => _logger.LogInformation($"Log Level: Information " +
                    $"ApplicationName: {request.ApplicationName} " +
                    $"LogMessage: {request.Message} " +
                    $"Date: {dateExec} "));
            }
        }

        public async Task LogWarning(LoggerRequestEntity request)
        {
            var dateExec = DateTime.UtcNow;
            using (LogContext.PushProperty(applicationName, request.ApplicationName))
            using (LogContext.PushProperty(logMessagem, request.Message))
            using (LogContext.PushProperty(date, dateExec))
            {
                await Task.Run(() => _logger.LogWarning($"Log Level: Warning " +
                    $"ApplicationName: {request.ApplicationName} " +
                    $"LogMessage: {request.Message} " +
                    $"Date: {dateExec} "));
            }
        }
    }
}
