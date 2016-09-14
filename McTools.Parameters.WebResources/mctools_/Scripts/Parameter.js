//=============================================================================
//
//                 SCRIPTS DE GESTION DU FORMULAIRE PARAMETRE
//							dern. modif. 07/09/2016
//
//=============================================================================

// FONCTION:    loadForm
// DESCRIPTION: Gère les traitements au chargement du formulaire
function loadForm() {
    if (Xrm.Page.ui.getFormType() > 1) {
        Xrm.Page.getControl("mctools_logicalname").setDisabled(true);
    }

    Xrm.Page.getAttribute("mctools_valuetype").fireOnChange();
    Xrm.Page.getControl("mctools_globalvalue").setVisible(false);
}

// FONCTION:    copyDisplayNameToLogicalName
// DESCRIPTION: Copie le contenu du nom d'affichage dans le nom logique
function copyDisplayNameToLogicalName() {
    var displayName = Xrm.Page.getAttribute("mctools_name");
    var logicalName = Xrm.Page.getAttribute("mctools_logicalname");

    if (displayName != null && displayName.getValue() != null && displayName.getValue().length > 0) {
        if (logicalName.getValue() == null || logicalName.getValue().length == 0) {
            logicalName.setValue(displayName.getValue());
        }
    }
}