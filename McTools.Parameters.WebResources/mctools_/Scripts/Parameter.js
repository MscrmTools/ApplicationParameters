//=============================================================================
//
//                 SCRIPTS DE GESTION DU FORMULAIRE PARAMETRE
//							dern. modif. 30/03/2011
//
//=============================================================================

// Liste des champs
var attributeList = ["mctools_textvalue", "mctools_memovalue", "mctools_boolvalue", "mctools_integervalue", 
					"mctools_decimalvalue", "mctools_floatvalue", "mctools_datevalue"];

// FONCTION:    showControl
// ARGUMENT1:   Nom logique de l'attribut pour lequel on afficher le contrôle
// DESCRIPTION: Affiche le contrôle de l'attribut spécifié
function showControl(attributeName)
{
	for (var i=0; i<attributeList.length; i++)
	{
		if(attributeList[i] != attributeName)
		{
			Xrm.Page.getControl(attributeList[i]).getParent().setVisible(false);
		}
		else
		{
			Xrm.Page.getControl(attributeName).getParent().setVisible(true);
		}
	}
}

// FONCTION:    manageControlDisplay
// DESCRIPTION: Gère l'affichage des contrôles en fonction de la valeur
//              sélectionnée dans la liste déroulante
function manageControlDisplay()
{
	var selectedItem = Xrm.Page.getAttribute("mctools_valuetype").getValue();
		
	// Attention! les indexs des éléments correspondent aux indexs de picklist Crm - 1
	showControl(attributeList[selectedItem - 1]);
}

// FONCTION:    loadForm
// DESCRIPTION: Gère les traitements au chargement du formulaire
function loadForm()
{
	if (Xrm.Page.ui.getFormType() > 1) {
		Xrm.Page.getControl("mctools_logicalname").setDisabled(true);
	}

	Xrm.Page.getAttribute("mctools_valuetype").fireOnChange();
	Xrm.Page.getControl("mctools_globalvalue").setVisible(false);
}

// FONCTION:    copyDisplayNameToLogicalName
// DESCRIPTION: Copie le contenu du nom d'affichage dans le nom logique
function copyDisplayNameToLogicalName()
{
	var displayName = Xrm.Page.getAttribute("mctools_name");
	var logicalName = Xrm.Page.getAttribute("mctools_logicalname");
	
	if (displayName != null && displayName.getValue() != null && displayName.getValue().length > 0) {
		if (logicalName.getValue() == null || logicalName.getValue().length == 0) {
			logicalName.setValue(displayName.getValue());
		}
	}
}