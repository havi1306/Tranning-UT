using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using Notification.Core.Constants;
using Notification.Core.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PushSharp.Apple;
using Notification.Api.Services.Interfaces;

namespace Notification.Api.Services
{
    public class ApplePushService : IApplePushService
    {
        ILogger<ApplePushService> _logger;
        private readonly IHostingEnvironment _environment;
        private readonly ApnsConfiguration _pushConfig;
        private readonly ApnsConfiguration _voipConfig;

        /// <summary>
        /// AppleServcies constructor
        /// </summary>
        public ApplePushService(
            ILogger<ApplePushService> logger,
            IHostingEnvironment environment,
            AppSettings appSettings)
        {
            this._logger = logger;
            this._environment = environment;

            // Configuration (NOTE: .pfx can also be used here)
            this._pushConfig = new ApnsConfiguration(
                appSettings.PushNotification.AppleCertificateEnv == "Production" ? ApnsConfiguration.ApnsServerEnvironment.Production : ApnsConfiguration.ApnsServerEnvironment.Sandbox,
                File.ReadAllBytes(Path.Combine(this._environment.ContentRootPath, appSettings.PushNotification.AppleCertificateFile)),
                appSettings.PushNotification.AppleCertificatePwd);

            this._voipConfig = new ApnsConfiguration(
                appSettings.VOIPNotification.AppleCertificateEnv == "Production" ? ApnsConfiguration.ApnsServerEnvironment.Production : ApnsConfiguration.ApnsServerEnvironment.Sandbox,
                Path.Combine(this._environment.ContentRootPath, appSettings.VOIPNotification.AppleCertificateFile),
                appSettings.PushNotification.AppleCertificatePwd,
                false);
        }

        /// <summary>
        /// Sends the push.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns> Result message </returns>
        public string SendPushNotification(PushNotificationModel model)
        {
            string resultmessage = string.Empty;

            // Create a new broker
            ApnsServiceBroker apnsBroker = new ApnsServiceBroker(this._pushConfig);

            // Wire up events
            apnsBroker.OnNotificationFailed += (notification, aggregateEx) =>
            {
                aggregateEx.Handle(ex =>
                {
                    // See what kind of exception it was to further diagnose
                    if (ex is ApnsNotificationException)
                    {
                        var notificationException = (ApnsNotificationException)ex;

                        //// Deal with the failed notification
                        var apnsNotification = notificationException.Notification;
                        var statusCode = notificationException.ErrorStatusCode;

                        ////Delete invalid token
                        if (statusCode == ApnsNotificationErrorStatusCode.InvalidToken)
                        {
                            this._logger.LogInformation($"Token deleted : {apnsNotification.DeviceToken}");
                        }

                        ////Console.WriteLine(
                        resultmessage = resultmessage +
                                        $"\r\nApple Notification Failed: ID={apnsNotification.DeviceToken}, Code={statusCode}";
                    }
                    else
                    {
                        // Inner exception might hold more useful information like an ApnsConnectionException
                        ////Console.WriteLine(
                        resultmessage = resultmessage + $"\r\nApple Notification Failed for some unknown reason : {ex.InnerException}";
                    }

                    // Mark it as handled
                    return true;
                });
            };

            apnsBroker.OnNotificationSucceeded += (notification) =>
            {
                ////Console.WriteLine(
                resultmessage = resultmessage + "\r\nApple Notification Sent!";
            };

            // Start the broker
            apnsBroker.Start();

            foreach(var deviceToken in  model.DeviceTokens)
            {
                    var jsonData = JsonConvert.SerializeObject(new
                    {
                        aps = new
                        {
                            alert = model.Message,
                            badge = 1,
                            sound = "default"
                        },
                        CustomParams = model.Parameter.Value
                    });

                    jsonData = jsonData.Replace("CustomParams", model.Parameter.Key);

                    // Queue a notification to send
                    apnsBroker.QueueNotification(
                        new ApnsNotification()
                        {
                            DeviceToken = deviceToken,
                            Payload = JObject.Parse(jsonData)
                        });
            };

            // Stop the broker, wait for it to finish
            // This isn't done after every message, but after you're
            // done with the broker
            apnsBroker.Stop();

            return resultmessage;
        }

        /// <summary>
        /// Sends the push.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns> Result message </returns>
        public string SendVOIPNotification(PushNotificationModel model)
        {
            string resultmessage = string.Empty;

            // Create a new broker
            ApnsServiceBroker apnsBroker = new ApnsServiceBroker(this._voipConfig);

            // Wire up events
            apnsBroker.OnNotificationFailed += (notification, aggregateEx) =>
            {
                aggregateEx.Handle(ex =>
                {
                    // See what kind of exception it was to further diagnose
                    if (ex is ApnsNotificationException)
                    {
                        var notificationException = (ApnsNotificationException)ex;

                        //// Deal with the failed notification
                        var apnsNotification = notificationException.Notification;
                        var statusCode = notificationException.ErrorStatusCode;

                        ////Delete invalid token
                        if (statusCode == ApnsNotificationErrorStatusCode.InvalidToken)
                        {
                            this._logger.LogInformation($"Token deleted : {apnsNotification.DeviceToken}");
                        }

                        ////Console.WriteLine(
                        resultmessage = resultmessage +
                                        $"\r\nApple Notification Failed: ID={apnsNotification.DeviceToken}, Code={statusCode}";
                    }
                    else
                    {
                        // Inner exception might hold more useful information like an ApnsConnectionException
                        ////Console.WriteLine(
                        resultmessage = resultmessage + $"\r\nApple Notification Failed for some unknown reason : {ex.InnerException}";
                    }

                    // Mark it as handled
                    return true;
                });
            };

            apnsBroker.OnNotificationSucceeded += (notification) =>
            {
                ////Console.WriteLine(
                resultmessage = resultmessage + "\r\nApple Notification Sent!";
            };

            // Start the broker
            apnsBroker.Start();

            foreach (var deviceToken in model.DeviceTokens)
            {
                var jdata = new JObject
                {
                    { "aps", new JObject
                        {
                            { "alert", model.Message },
                            { "content-available", 1 }
                        }
                    }
                };

                // Queue a notification to send
                apnsBroker.QueueNotification(
                    new ApnsNotification()
                    {
                        DeviceToken = deviceToken,
                        Payload = jdata
                    });
            };

            // Stop the broker, wait for it to finish
            // This isn't done after every message, but after you're
            // done with the broker
            apnsBroker.Stop();

            return resultmessage;
        }

        /// <summary>
        /// Checks the device token.
        /// </summary>
        public void CheckDeviceToken()
        {
            var fbs = new FeedbackService(this._pushConfig);
            fbs.FeedbackReceived += (string deviceToken, DateTime timestamp) =>
            {
                this._logger.LogInformation($"Token deleted : {deviceToken}");
            };

            fbs.Check();
        }

        /// <summary>
        /// Finds the apns cert.
        /// </summary>
        /// <param name="thumbprint">The thumbprint.</param>
        /// <returns> X509 Certificate </returns>
        /// <exception cref="System.Exception">No certificate with thumprint: " + thumbprint</exception>
        private X509Certificate2 FindApnsCert(string thumbprint)
        {
            var store = new X509Store(StoreName.My, StoreLocation.LocalMachine);
            store.Open(OpenFlags.OpenExistingOnly | OpenFlags.ReadOnly);

            var cert = store.Certificates
                .Cast<X509Certificate2>()
                .SingleOrDefault(c => string.Equals(c.Thumbprint, thumbprint, StringComparison.OrdinalIgnoreCase));

            if (cert == null)
            {
                throw new Exception("No certificate with thumprint: " + thumbprint);
            }

            return cert;
        }
    }
}
