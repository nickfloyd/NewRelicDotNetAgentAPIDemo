using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Web.Http;

namespace NewRelicDotNetAgentAPIDemo.Controllers
{
    public class NewRelicAPIController : ApiController
    {

        /// <summary>
        /// RecordMetric(System.String,System.Single) Method
        ///Record a metric value for the given name.

        ///Parameters
        ///name : The name of the metric to record. Only the first 1000 characters are retained.
        ///value : The value to record. Only the first 1000 characters are retained.
        /// </summary>
        /// <returns></returns>
        /// http://localhost/NewRelicDotNetAgentAPIDemo/Api/NewRelicAPI/RecordMetric
        /// Can be found in New Relic via: https://rpm.newrelic.com/accounts/[accountid]/custom_dashboards
        [HttpGetAttribute]
        public string RecordMetric()
        {
            // Converting DateTime.Now calls to Stopwatch which is more appropriate for measuring performance times
            var timer = Stopwatch.StartNew();         
            this.DelayTransaction(5000);
            timer.Stop();
            NewRelic.Api.Agent.NewRelic.RecordMetric("Custom/DEMO_Record_Metric", timer.Elapsed.Milliseconds);

            return "RecordMetric";
        }

        ///RecordResponseTimeMetric(System.String,System.Int64) Method
        ///Record a response time in milliseconds for the given metric name.

        ///Parameters
        ///name : The name of the response time metric to record. Only the first 1000 characters are retained.
        ///millis : The response time to record in milliseconds.
        /// http://localhost/NewRelicDotNetAgentAPIDemo/Api/NewRelicAPI/RecordResponseTimeMetric
        /// Can be found in New Relic via: https://rpm.newrelic.com/accounts/[accountid]/custom_dashboards
        [HttpGetAttribute]
        public string RecordResponseTimeMetric()
        {
            // Converting DateTime.Now calls to Stopwatch which is more appropriate for measuring performance times
            var timer = Stopwatch.StartNew();
            this.DelayTransaction(5000);
            timer.Stop();

            NewRelic.Api.Agent.NewRelic.RecordResponseTimeMetric("Custom/DEMO_Record_Response_Time_Metric", timer.Elapsed.Milliseconds);

            return "RecordResponseTimeMetric";
        }

        ///IncrementCounter(System.String) Method
        ///Increment the metric counter for the given name.

        ///Parameters
        ///name : The name of the metric to increment. Only the first 1000 characters are retained.
        /// http://localhost/NewRelicDotNetAgentAPIDemo/Api/NewRelicAPI/IncrementCounter
        /// Can be found in New Relic via: https://rpm.newrelic.com/accounts/[accountid]/Aplications/[Appid]/transactions
        [HttpGetAttribute]
        public string IncrementCounter()
        {
            NewRelic.Api.Agent.NewRelic.SetTransactionName("Custom", "IncrementCounter");
            this.DelayTransaction(5000);
            
            for (int i = 0; i < 10; i++)
            {
                NewRelic.Api.Agent.NewRelic.IncrementCounter("IncrementCounter");
            }

            return "IncrementCounter";
        }

        ///NoticeError(System.Exception) Method
        ///Notice an error identified by an exception and report it to the New Relic service. 
        ///If this method is called within a transaction, the exception will be reported with the transaction when it finishes. 
        ///If it is invoked outside of a transaction, a traced error will be created and reported to the New Relic service. 
        ///Only the exception/parameter pair for the first call to NoticeError during the course of a transaction is retained.

        ///Parameters
        ///exception : The exception to be reported. Only part of the exception's information may be retained to prevent the report from being too large.
        /// http://localhost/NewRelicDotNetAgentAPIDemo/Api/NewRelicAPI/NoticeError
        /// Can be found in New Relic via: https://rpm.newrelic.com/accounts/[accountid]/Aplications/[Appid]/traced_errors
        [HttpGetAttribute]
        public string NoticeError()
        {
            try
            {
                var ImNotABool = "43";
                bool.Parse(ImNotABool);
            }
            catch (Exception ex)
            {
                NewRelic.Api.Agent.NewRelic.NoticeError(ex);
            }

            return "NoticeError";
        }

        ///NoticeError(System.Exception, Dictionary) Method
        ///Notice an error identified by an exception and report it to the New Relic service. 
        ///If this method is called within a transaction, the exception will be reported with the transaction when it finishes. 
        ///If it is invoked outside of a transaction, a traced error will be created and reported to the New Relic service. 
        ///Only the exception/parameter pair for the first call to NoticeError during the course of a transaction is retained.

        ///Parameters
        ///exception : The exception to be reported. Only part of the exception's information may be retained to prevent the report from being too large.
        ///Dictionary : Will add data to each error that can be found on the page displaying the error under the custom parameters
        /// http://localhost/NewRelicDotNetAgentAPIDemo/Api/NewRelicAPI/NoticeErrorWithDictionary
        /// Can be found in New Relic via: https://rpm.newrelic.com/accounts/[accountid]/Aplications/[Appid]/traced_errors
        [HttpGetAttribute]
        public string NoticeErrorWithDictionary()
        {
            try
            {
                var ImNotABool = "43";
                bool.Parse(ImNotABool);
            }
            catch (Exception ex)
            {
                var quotes = new Dictionary<string,string>();
                quotes.Add("1", "They had a large chunk of the garbage file? How much do they know?");
                quotes.Add("2", "I'll hack the Gibson.");
                quotes.Add("3", "Zero Cool? Crashed fifteen hundred and seven systems in one day?");
                quotes.Add("4", "Turn on your laptop. Set it to receive a file.");
                quotes.Add("5", "Listen you guys, help yourself to anything in the fridge. Cereal has.");

                NewRelic.Api.Agent.NewRelic.NoticeError(ex, quotes);
            }

            return "NoticeError";
        }

        ///AddCustomParameter(System.String,System.String) Method
        ///Add a key/value pair to the current transaction. These are reported in errors and transaction traces.

        ///Parameters
        ///key : The key name to add to the transaction parameters. Only the first 1000 characters are retained.
        ///value : The value to add to the current transaction.
        /// http://localhost/NewRelicDotNetAgentAPIDemo/Api/NewRelicAPI/AddCustomParameter
        /// Can be found in New Relic via: https://rpm.newrelic.com/accounts/[accountid]/transactions/[transactionid] >> in the custom parameters section
        [HttpGetAttribute]
        public string AddCustomParameter()
        {
            this.DelayTransaction(5000);
            
            NewRelic.Api.Agent.NewRelic.AddCustomParameter("Custom/DEMO_PARAMETER_ORDER_NUMBER","123456");

            return "AddCustomParameter";
        }

        ///SetTransactionName(System.String,System.String) Method
        ///Set the name of the current transaction.

        ///Parameters
        ///category : The category of this transaction, used to distinguish different types of transactions. Defaults to Custom. Only the first 1000 characters are retained.
        ///name : The name of the transaction starting with a forward slash. example: /store/order Only the first 1000 characters are retained.
        /// http://localhost/NewRelicDotNetAgentAPIDemo/Api/NewRelicAPI/SetTransactionName
        /// Can be found in New Relic via: https://rpm.newrelic.com/accounts/[accountid]/transactions/[transactionid]
        [HttpGetAttribute]
        public string SetTransactionName()
        {
            this.DelayTransaction(5000);

            NewRelic.Api.Agent.NewRelic.SetTransactionName("Custom", "DEMO_TRANSACTION_NAME");

            return "SetTransactionName";
        }

        ///IgnoreTransaction Method
        ///Ignore the transaction that is currently in process
        /// http://localhost/NewRelicDotNetAgentAPIDemo/Api/NewRelicAPI/IgnoreTransaction
        /// Can be found in New Relic via: You will not see this trnasaction - hense the name...
        [HttpGetAttribute]
        public string IgnoreTransaction()
        {
            this.DelayTransaction(5000);

            NewRelic.Api.Agent.NewRelic.IgnoreTransaction();

            return "IgnoreTransaction";
        }

        //IgnoreApdex Method
        //Ignore the current transaction in the apdex computation.
        /// http://localhost/NewRelicDotNetAgentAPIDemo/Api/NewRelicAPI/IgnoreApdex
        /// Normally apdx would drop if a method took 20 seconds to execute - this one will be ignored
        [HttpGetAttribute]
        public string IgnoreApdex()
        {
            this.DelayTransaction(20000);

            NewRelic.Api.Agent.NewRelic.IgnoreApdex();

            return "IgnoreApdex";
        }

        //GetBrowserTimingHeader - see views/shared/_Layout.cshtml


        //GetBrowserTimingFooter - see views/shared/_Layout.cshtml 

        ///SetUserParameters(System.String,System.String,System.String) Method
        ///Sets the User Name, Account Name and Product Name to associate with the RUM JavaScript footer for the current web transaction.
        /// http://localhost/NewRelicDotNetAgentAPIDemo/Api/NewRelicAPI/SetUserParameters
        /// Can be found in New Relic via: https://rpm.newrelic.com/accounts/[accountid]/Aplications/[Appid]/traced_errors
        /// 
        ///NOTE: THIS METHOD WILL NOT WORK HERE AS IMPLEMENTED - this is a part of Real User Monitoring (which requires a view to render javascript - see the home controller for an example)
        [HttpGetAttribute]
        public string SetUserParameters()
        {
            this.DelayTransaction(5000);

            NewRelic.Api.Agent.NewRelic.SetUserParameters("Nick","SomeUserHandle","NewRelicEcomSite");

            return "SetUserParameters";
        }

        ///GetBrowserTimingHeader() Method
        ///Gets the javascript to be used in the view for measuring real user performace
        ///NOTE: THIS METHOD WILL NOT WORK HERE AS IMPLEMENTED - this is a part of Real User Monitoring (which requires a view to render javascript - see the shared/_Layout.cshtml  for an example)
        [HttpGetAttribute]
        public string GetBrowserTimingHeader()
        {
            var header = NewRelic.Api.Agent.NewRelic.GetBrowserTimingHeader();

            return header;
        }

        ///GetBrowserTimingFooter() Method
        ///Gets the javascript to be used in the view for measuring real user performace
        ///NOTE: THIS METHOD WILL NOT WORK HERE AS IMPLEMENTED - this is a part of Real User Monitoring (which requires a view to render javascript - see the shared/_Layout.cshtml  for an example)
        [HttpGetAttribute]
        [Obsolete]
        public string GetBrowserTimingFooter()
        {
            var footer = NewRelic.Api.Agent.NewRelic.GetBrowserTimingFooter();

            return footer;
        }

        ///DisableBrowserMonitoring() Method
        ///Gets the javascript to be used in the view for measuring real user performace
        ///NOTE: THIS METHOD WILL NOT WORK HERE AS IMPLEMENTED - this is a part of Real User Monitoring (which requires a view to render javascript - see the Views/Home/Index.cshtml  for an example)
        ///NOTE: Works only with agent versions >= 2.5.x
        [HttpGetAttribute]
        public string DisableBrowserMonitoring()
        {
            NewRelic.Api.Agent.NewRelic.DisableBrowserMonitoring();

            return "DisableBrowserMonitoring";
        }

        private void DelayTransaction(Int16 mills) {
            Thread.Sleep(mills);
        }

    }
}
