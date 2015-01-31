﻿using System;
using System.Linq;
using System.Activities;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using Microsoft.Xrm.Sdk.Workflow;

namespace McTools.Parameters.Plugins
{
    public sealed class GetParameter : CodeActivity
    {
        // Logical name of the parameter
        [Input("Logical name of the parameter")]
        public InArgument<string> LogicalName { get; set; }

        [Output("Text value")]
        public OutArgument<string> TextValue { get; set; }
        [Output("Boolean value")]
        public OutArgument<bool> BooleanValue { get; set; }
        [Output("Date value")]
        public OutArgument<DateTime> DateValue { get; set; }
        [Output("Integer value")]
        public OutArgument<int> IntegerValue { get; set; }
        [Output("Decimal value")]
        public OutArgument<decimal> DecimalValue { get; set; }
        [Output("Float value")]
        public OutArgument<double> FloatValue { get; set; }
       
        protected override void Execute(CodeActivityContext executionContext)
        {
            var context = executionContext.GetExtension<IWorkflowContext>();
            var serviceFactory = executionContext.GetExtension<IOrganizationServiceFactory>();
            var service = serviceFactory.CreateOrganizationService(null);

            var qe = new QueryExpression("mctools_parameter")
            {
                Criteria = new FilterExpression
                {
                    Conditions =
                    {
                        new ConditionExpression("mctools_logicalname", ConditionOperator.Equal, executionContext.GetValue(LogicalName))
                    }
                },
                ColumnSet = new ColumnSet(true)
            };


            var results = service.RetrieveMultiple(qe);

            if (results.Entities.Count == 0)
            {
                throw new Exception(string.Format("Unable to retrieve a parameter with logical name '{0}'",
                    executionContext.GetValue(LogicalName)));
            }

            // Assign parameter values to output arguments
            var parameter = results.Entities.First();
            executionContext.SetValue(TextValue, parameter.GetAttributeValue<string>("mctools_textvalue"));
            executionContext.SetValue(BooleanValue, parameter.GetAttributeValue<bool>("mctools_boolvalue"));
            executionContext.SetValue(DateValue, parameter.GetAttributeValue<DateTime>("mctools_datevalue"));
            executionContext.SetValue(DecimalValue, parameter.GetAttributeValue<decimal>("mctools_decimalvalue"));
            executionContext.SetValue(FloatValue, parameter.GetAttributeValue<double>("mctools_floatvalue"));
            executionContext.SetValue(IntegerValue, parameter.GetAttributeValue<int>("mctools_integervalue"));

            if (TextValue == null)
            {
                executionContext.SetValue(TextValue, parameter.GetAttributeValue<string>("mctools_memovalue"));
            }
        }
    }
}
