using System;
using McTools.Parameters.Plugins.AppCode;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Metadata;
using Microsoft.Xrm.Sdk.Query;

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
                                            (Entity) ps.Context.InputParameters[PluginInputParameters.Target];

                                        if (ParameterWithSameLogicalNameExists(
                                            inputData["mctools_logicalname"].ToString(),
                                            ps.GetIOrganizationService(true)))
                                            throw new InvalidPluginExecutionException(
                                                "Un paramètre avec ce nom existe déjà!");

                                        CopyLocalToGlobal(inputData, null, ps.GetIOrganizationService(true));
                                    }
                                    break;
                                case PluginMessage.Update:
                                    {
                                        var inputData =  (Entity) ps.Context.InputParameters[PluginInputParameters.Target];
                                        var preImage = ps.Context.PreEntityImages["Image"];

                                        CopyLocalToGlobal(inputData, preImage, ps.GetIOrganizationService(true));

                                        if(inputData.Contains("mctools_valuetype"))
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

        private static bool ParameterWithSameLogicalNameExists(string logicalName, IOrganizationService service)
        {
            var qba = new QueryByAttribute("mctools_parameter");
            qba.Attributes.Add("mctools_logicalname");
            qba.Values.Add(logicalName);

            return service.RetrieveMultiple(qba).Entities.Count > 0;
        }

        private static void CopyLocalToGlobal(Entity inputData, Entity preImage, IOrganizationService service)
        {
            var amd = ((RetrieveAttributeResponse) service.Execute(new RetrieveAttributeRequest
            {
                EntityLogicalName = "mctools_parameter",
                LogicalName = "mctools_globalvalue"
            })).AttributeMetadata as StringAttributeMetadata;


            var type = inputData.Contains("mctools_valuetype")
                ? ((OptionSetValue) inputData["mctools_valuetype"]).Value
                : ((OptionSetValue)preImage["mctools_valuetype"]).Value;

            switch (type)
            {
                case 1:
                    inputData["mctools_globalvalue"] = inputData["mctools_textvalue"];
                    break;
                case 2:
                {
                    string value = inputData.GetAttributeValue<string>("mctools_memovalue");
                    if (value.Length > amd.MaxLength.Value)
                    {
                        value = value.Substring(0, amd.MaxLength.Value - 3);
                        value += "...";
                    }
                    inputData["mctools_globalvalue"] = value;
                }
                    break;
                case 3:
                    inputData["mctools_globalvalue"] = (bool)inputData["mctools_boolvalue"] ? "Oui" : "Non";
                    break;
                case 4:
                    inputData["mctools_globalvalue"] =
                    ((int)inputData["mctools_integervalue"]).ToString();
                    break;
                case 5:
                    inputData["mctools_globalvalue"] =
                   ((decimal)inputData["mctools_decimalvalue"]).ToString();
                    break;
                case 6:
                    inputData["mctools_globalvalue"] =
                   ((float)inputData["mctools_floatvalue"]).ToString();
                    break;
                case 7:
                    inputData["mctools_globalvalue"] =
                    ((DateTime)inputData["mctools_datevalue"]).ToString("dd/MM/yyyy");
                    break;
            }
        }

        private static void CleanOtherAttributesValue(Entity inputData, Entity preImage = null)
        {
            var type = inputData.Contains("mctools_valuetype")
               ? ((OptionSetValue)inputData["mctools_valuetype"]).Value
               : ((OptionSetValue)preImage["mctools_valuetype"]).Value;

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
            }
        }
    }
}
