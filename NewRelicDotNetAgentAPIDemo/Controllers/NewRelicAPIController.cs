using System;
using System.Collections.Generic;
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
        public string RecordMetric() {
            DateTime start = DateTime.Now; 
            this.DelayTransaction(5000);
            TimeSpan ts = DateTime.Now.Subtract(start);

            NewRelic.Api.Agent.NewRelic.RecordMetric("Custom/DEMO_Record_Metric", ts.Milliseconds);

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
            DateTime start = DateTime.Now;
            this.DelayTransaction(5000);
            TimeSpan ts = DateTime.Now.Subtract(start);

            NewRelic.Api.Agent.NewRelic.RecordResponseTimeMetric("Custom/DEMO_Record_Response_Time_Metric", ts.Milliseconds);

            return "RecordResponseTimeMetric";
        }

        ///IncrementCounter(System.String) Method
        ///Increment the metric counter for the given name.

        ///Parameters
        ///name : The name of the metric to increment. Only the first 1000 characters are retained.
        /// http://localhost/NewRelicDotNetAgentAPIDemo/Api/NewRelicAPI/RecordResponseTimeMetric
        /// Can be found in New Relic via: https://rpm.newrelic.com/accounts/[accountid]/custom_dashboards
        [HttpGetAttribute]
        public string IncrementCounter()
        {
            this.RecordResponseTimeMetric();

            for (int i = 0; i < 10; i++)
            {
                NewRelic.Api.Agent.NewRelic.IncrementCounter("Custom/DEMO_Record_Response_Time_Metric");
            }

            return "IncrementCounter";
        }

        //// GET api/newrelicapi
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        //// GET api/newrelicapi/5
        //public string Get(int id)
        //{
        //    return "value";
        //}

        //// POST api/newrelicapi
        //public void Post([FromBody]string value)
        //{
        //}

        //// PUT api/newrelicapi/5
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        //// DELETE api/newrelicapi/5
        //public void Delete(int id)
        //{
        //}

        private void DelayTransaction(Int16 mills) {
            Thread.Sleep(mills);
        }
    }
}
