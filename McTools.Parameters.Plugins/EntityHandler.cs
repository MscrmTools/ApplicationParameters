using McTools.Parameters.Plugins.AppCode;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Metadata;
using Microsoft.Xrm.Sdk.Query;
using System;

namespace McTools.Parameters.Plugins
{
    /// <summary>
    /// Cette classe de plugin permet de gérer les événéments sur l'entité "Nom de l'entité"
    /// </summary>
    public class EntityHandler : IPlugin
    {
        public void Execute(IServiceProvider serviceProvider)
        {
            // Récupération des services liés aux plugins
            var ps = new PluginServices(serviceProvider);

            try
            {
                // Si ce plugin ne s'exécute pas dans le cadre de l'entité
                // attendue, on annule le traitement
                if (ps.Context.PrimaryEntityName != "mctools_parameter")
                    return;

                switch (ps.Context.Stage)
                {
                    case PluginStage.PreOperation:
                        {
                            switch (ps.Context.MessageName)
                            {
                                case PluginMessage.Create:
                                    {
                                        var inputData =
                                            (Entity)ps.Context.InputParameters[PluginInputParameters.Target];

                                        if (ParameterWithSameLogicalNameExists(
                                            inputData["mctools_logicalname"].ToString(),
                                            ps.GetIOrganizationService(true)))
                                            throw new InvalidPluginExecutionException(
                                                "Un paramètre avec ce nom existe déjà!");

                                        CopyLocalToGlobal(inputData, null, ps.GetIOrganizationService(true), ps);
                                        CleanOtherAttributesValue(inputData);
                                    }
                                    break;

                                case PluginMessage.Update:
                                    {
                                        var inputData = (Entity)ps.Context.InputParameters[PluginInputParameters.Target];
                                        var preImage = ps.Context.PreEntityImages["Image"];

                                        CopyLocalToGlobal(inputData, preImage, ps.GetIOrganizationService(true), ps);

                                        if (inputData.Contains("mctools_valuetype"))
                                        {
                                            CleanOtherAttributesValue(inputData, preImage);
                                        }
                                    }
                                    break;
                            }
                        }
                        break;
                }
            }
            catch (Exception error)
            {
                ps.Trace(error.ToString());
                throw new InvalidPluginExecutionException(error.Message);
            }
        }

        private static void CleanOtherAttributesValue(Entity inputData, Entity preImage = null)
        {
            var type = inputData.Contains("mctools_valuetype")
                ? inputData.GetAttributeValue<OptionSetValue>("mctools_valuetype").Value
                : preImage?.GetAttributeValue<OptionSetValue>("mctools_valuetype")?.Value ?? -1;

            switch (type)
            {
                case 1:
                    inputData["mctools_boolvalue"] = null;
                    inputData["mctools_datevalue"] = null;
                    inputData["mctools_decimalvalue"] = null;
                    inputData["mctools_floatvalue"] = null;
                    inputData["mctools_integervalue"] = null;
                    inputData["mctools_memovalue"] = null;
                    break;

                case 2:
                    inputData["mctools_boolvalue"] = null;
                    inputData["mctools_datevalue"] = null;
                    inputData["mctools_decimalvalue"] = null;
                    inputData["mctools_floatvalue"] = null;
                    inputData["mctools_integervalue"] = null;
                    inputData["mctools_textvalue"] = null;
                    break;

                case 3:
                    inputData["mctools_datevalue"] = null;
                    inputData["mctools_decimalvalue"] = null;
                    inputData["mctools_floatvalue"] = null;
                    inputData["mctools_integervalue"] = null;
                    inputData["mctools_memovalue"] = null;
                    inputData["mctools_textvalue"] = null;
                    break;

                case 4:
                    inputData["mctools_boolvalue"] = null;
                    inputData["mctools_datevalue"] = null;
                    inputData["mctools_decimalvalue"] = null;
                    inputData["mctools_floatvalue"] = null;
                    inputData["mctools_memovalue"] = null;
                    inputData["mctools_textvalue"] = null;
                    break;

                case 5:
                    inputData["mctools_boolvalue"] = null;
                    inputData["mctools_datevalue"] = null;
                    inputData["mctools_floatvalue"] = null;
                    inputData["mctools_integervalue"] = null;
                    inputData["mctools_memovalue"] = null;
                    inputData["mctools_textvalue"] = null;
                    break;

                case 6:
                    inputData["mctools_boolvalue"] = null;
                    inputData["mctools_datevalue"] = null;
                    inputData["mctools_decimalvalue"] = null;
                    inputData["mctools_integervalue"] = null;
                    inputData["mctools_memovalue"] = null;
                    inputData["mctools_textvalue"] = null;
                    break;

                case 7:
                    inputData["mctools_boolvalue"] = null;
                    inputData["mctools_decimalvalue"] = null;
                    inputData["mctools_floatvalue"] = null;
                    inputData["mctools_integervalue"] = null;
                    inputData["mctools_memovalue"] = null;
                    inputData["mctools_textvalue"] = null;
                    break;

                case 8:
                    inputData["mctools_boolvalue"] = null;
                    inputData["mctools_datevalue"] = null;
                    inputData["mctools_decimalvalue"] = null;
                    inputData["mctools_floatvalue"] = null;
                    inputData["mctools_integervalue"] = null;
                    inputData["mctools_memovalue"] = null;
                    inputData["mctools_textvalue"] = null;
                    break;
            }
        }

        private static void CopyLocalToGlobal(Entity inputData, Entity preImage, IOrganizationService service, PluginServices ps)
        {
            var amd = ((RetrieveAttributeResponse)service.Execute(new RetrieveAttributeRequest
            {
                EntityLogicalName = "mctools_parameter",
                LogicalName = "mctools_globalvalue"
            })).AttributeMetadata as StringAttributeMetadata;

            var type = inputData.Contains("mctools_valuetype")
                ? ((OptionSetValue)inputData["mctools_valuetype"]).Value
                : ((OptionSetValue)preImage["mctools_valuetype"]).Value;

            ps.Trace($"Type option set value is {type}");

            switch (type)
            {
                case 1:
                    if (!inputData.Contains("mctools_textvalue")) return;
                    string sValue = inputData.GetAttributeValue<string>("mctools_textvalue");
                    if (sValue != null && sValue.Length > amd.MaxLength.Value)
                    {
                        sValue = sValue.Substring(0, amd.MaxLength.Value - 3);
                        sValue += "...";
                    }
                    inputData["mctools_globalvalue"] = sValue;
                    break;

                case 2:
                    {
                        if (!inputData.Contains("mctools_memovalue")) return;
                        string mValue = inputData.GetAttributeValue<string>("mctools_memovalue");
                        if (mValue.Length > amd.MaxLength.Value)
                        {
                            mValue = mValue.Substring(0, amd.MaxLength.Value - 3);
                            mValue += "...";
                        }
                        inputData["mctools_globalvalue"] = mValue;
                    }
                    break;

                case 3:
                    if (!inputData.Contains("mctools_boolvalue")) return;
                    inputData["mctools_globalvalue"] = inputData.GetAttributeValue<bool>("mctools_boolvalue") ? "Oui" : "Non";
                    break;

                case 4:
                    if (!inputData.Contains("mctools_integervalue")) return;
                    inputData["mctools_globalvalue"] =
                        inputData.GetAttributeValue<int>("mctools_integervalue").ToString();
                    break;

                case 5:
                    if (!inputData.Contains("mctools_decimalvalue")) return;
                    inputData["mctools_globalvalue"] =
                        inputData.GetAttributeValue<decimal>("mctools_decimalvalue").ToString();
                    break;

                case 6:
                    if (!inputData.Contains("mctools_floatvalue")) return;
                    inputData["mctools_globalvalue"] =
                   inputData.GetAttributeValue<double>("mctools_floatvalue").ToString();
                    break;

                case 7:
                    if (!inputData.Contains("mctools_datevalue")) return;
                    inputData["mctools_globalvalue"] = $"{inputData.GetAttributeValue<DateTime>("mctools_datevalue").ToString("G")} (UTC)";
                    break;

                case 8:
                    inputData["mctools_globalvalue"] = null;
                    break;
            }
        }

        private static bool ParameterWithSameLogicalNameExists(string logicalName, IOrganizationService service)
        {
            var qba = new QueryByAttribute("mctools_parameter");
            qba.Attributes.Add("mctools_logicalname");
            qba.Values.Add(logicalName);

            return service.RetrieveMultiple(qba).Entities.Count > 0;
        }
    }
}